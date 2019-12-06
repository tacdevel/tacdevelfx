/***************************************************************************************************
 * FileName:             NativeAssemblyBase.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using TCDFx.Native;
using TCDFx.Resources;
using TCDFx.SafeHandles;

namespace TCDFx.Runtime.InteropServices
{
    public sealed class NativeAssemblyLoadedEventArgs : EventArgs
    {
        public NativeAssemblyLoadedEventArgs(string assemblyName) => AssemblyName = assemblyName;

        public string AssemblyName { get; }
    }

    /// <summary>
    /// Represents a native (shared) assembly.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public class NativeAssembly : SafeNativeComponent<SafeAssemblyHandle>, INativeComponent<SafeAssemblyHandle>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NativeAssembly"/> class.
        /// </summary>
        /// <param name="names">An ordered list of assembly names to attempt to load.</param>
        public NativeAssembly(params string[] names)
        {
            IntPtr asmHnd = LoadAssembly(names);
            Handle = new SafeAssemblyHandle(asmHnd);
        }

        /// <summary>
        /// Occurs when an assembly is loaded.
        /// </summary>
        public event EventHandler<NativeAssembly, NativeAssemblyLoadedEventArgs> Loaded;

        /// <summary>
        /// Loads a function whose signature and name match the given delegate type's signature and name.
        /// </summary>
        /// <typeparam name="T">The type of delegate to return.</typeparam>
        /// <returns>A delegate wrapping the native function.</returns>
        public T LoadFunction<T>() where T : Delegate => LoadFunction<T>(typeof(T).Name);

        /// <summary>
        /// Loads a function whose signature matches the given delegate type's signature.
        /// </summary>
        /// <typeparam name="T">The type of delegate to return.</typeparam>
        /// <param name="name">The name of the native function.</param>
        /// <returns>A delegate wrapping the native function.</returns>
        public T LoadFunction<T>(string name)
        {
            IntPtr functionPtr = LoadFunctionPointer(name);
            if (functionPtr == IntPtr.Zero)
                throw new EntryPointNotFoundException(string.Format(CultureInfo.InvariantCulture, Strings.NativeFunctionNotFound, nameof(name)));
            return Marshal.GetDelegateForFunctionPointer<T>(functionPtr);
        }

        /// <summary>
        /// Loads a function pointer with the given name.
        /// </summary>
        /// <param name="name">The name of the native function.</param>
        /// <returns>A function pointer for the given name, or <see cref="IntPtr.Zero"/> if no function with the specified name was found.</returns>
        public IntPtr LoadFunctionPointer(string name)
        {
            IntPtr ret;
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNullEmptyOrWhitespace, nameof(name)));
            ret = LoadFunctionPointer(Handle, name);

            if (ret == IntPtr.Zero)
                throw new EntryPointNotFoundException(string.Format(CultureInfo.InvariantCulture, Strings.NativeFunctionNotFound, nameof(name)));
            return ret;
        }

        private IEnumerable<string> EnumerateLoadTargets(string name)
        {
            yield return name;
            yield return Path.Combine(AppContext.BaseDirectory, name);
            if (TryLocateNativeAssetFromDeps(name, out string appLocalNativePath, out string depsResolvedPath))
            {
                yield return appLocalNativePath;
                yield return depsResolvedPath;
            }
        }

        protected virtual void OnLoaded(string assemblyName) => Loaded?.Invoke(this, new NativeAssemblyLoadedEventArgs(assemblyName));

        private IntPtr LoadAssembly(params string[] names)
        {
            if (names == null || names.Length == 0)
                throw new ArgumentNullException(nameof(names), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNullOrEmpty, nameof(names)));

            IntPtr ret = IntPtr.Zero;
            foreach (string name in names)
            {
                if (Path.IsPathRooted(name))
                    ret = LoadAssembly(name);
                else
                {
                    foreach (string loadTarget in EnumerateLoadTargets(name))
                    {
                        if (!Path.IsPathRooted(loadTarget) || File.Exists(loadTarget))
                        {
                            IntPtr ret2 = LoadAssembly(loadTarget);
                            if (ret2 != IntPtr.Zero)
                                ret = ret2;
                        }
                    }
                }
                if (ret != IntPtr.Zero)
                    break;
            }
            if (ret == IntPtr.Zero)
                throw new FileNotFoundException(string.Format(CultureInfo.InvariantCulture, Strings.NativeAssemblyNotFound, $"'{string.Join("', '", names)}'"));
            return ret;
        }

        private static IntPtr LoadAssembly(string name)
        {
            switch (Platform.PlatformType)
            {
                case PlatformType.Windows:
                    return Kernel32.LoadLibrary(name);
                case PlatformType.MacOS:
                case PlatformType.Linux:
                case PlatformType.FreeBSD:
                    return Libdl.dlopen(name, Libdl.RTLD_NOW);
                case PlatformType.Unknown:
                default:
                    return IntPtr.Zero;
            }
        }

        private static IntPtr LoadFunctionPointer(IntPtr handle, string name)
        {
            switch (Platform.PlatformType)
            {
                case PlatformType.Windows:
                    return Kernel32.GetProcAddress(handle, name);
                case PlatformType.MacOS:
                case PlatformType.Linux:
                case PlatformType.FreeBSD:
                    return Libdl.dlsym(handle, name);
                case PlatformType.Unknown:
                default:
                    return IntPtr.Zero;
            }
        }

        private bool TryLocateNativeAssetFromDeps(string name, out string appLocalNativePath, out string depsResolvedPath)
        {
            DependencyContext defaultContext = DependencyContext.Default;
            if (defaultContext == null)
            {
                appLocalNativePath = null;
                depsResolvedPath = null;
                return false;
            }

            string currentRID = Platform.RuntimeID;
            List<string> allRIDs = new List<string> { currentRID };
            if (!AddFallbacks(allRIDs, currentRID, defaultContext.RuntimeGraph))
            {
                string guessedFallbackRID = GuessFallbackRID(currentRID);
                if (guessedFallbackRID != null)
                {
                    allRIDs.Add(guessedFallbackRID);
                    AddFallbacks(allRIDs, guessedFallbackRID, defaultContext.RuntimeGraph);
                }
            }

            foreach (string rid in allRIDs)
            {
                foreach (RuntimeLibrary runtimeLib in defaultContext.RuntimeLibraries)
                {
                    foreach (string nativeAsset in runtimeLib.GetRuntimeNativeAssets(defaultContext, rid))
                    {
                        if (Path.GetFileName(nativeAsset) == name || Path.GetFileNameWithoutExtension(nativeAsset) == name)
                        {
                            appLocalNativePath = Path.Combine(
                                AppContext.BaseDirectory,
                                nativeAsset);
                            appLocalNativePath = Path.GetFullPath(appLocalNativePath);

                            depsResolvedPath = Path.Combine(
                                GetNugetPackagesRootDirectory(),
                                runtimeLib.Name.ToLowerInvariant(),
                                runtimeLib.Version,
                                nativeAsset);
                            depsResolvedPath = Path.GetFullPath(depsResolvedPath);

                            return true;
                        }
                    }
                }
            }

            appLocalNativePath = null;
            depsResolvedPath = null;
            return false;
        }

        private static string GuessFallbackRID(string actualRuntimeIdentifier)
        {
            if (actualRuntimeIdentifier == "osx.10.13-x64")
                return "osx.10.12-x64";
            else if (actualRuntimeIdentifier.StartsWith("osx", StringComparison.Ordinal))
                return "osx-x64";

            return null;
        }

        private static bool AddFallbacks(List<string> fallbacks, string rid, IReadOnlyList<RuntimeFallbacks> allFallbacks)
        {
            foreach (RuntimeFallbacks fb in allFallbacks)
            {
                if (fb.Runtime == rid)
                {
                    fallbacks.AddRange(fb.Fallbacks);
                    return true;
                }
            }

            return false;
        }

        private string GetNugetPackagesRootDirectory() => Path.Combine(GetUserDirectory(), ".nuget", "packages");

        private static string GetUserDirectory() => Platform.PlatformType == PlatformType.Windows ? Environment.GetEnvironmentVariable("USERPROFILE") : Environment.GetEnvironmentVariable("HOME");
    }
}