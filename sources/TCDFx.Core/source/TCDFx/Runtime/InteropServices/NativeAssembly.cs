/***************************************************************************************************
 * FileName:             NativeAssemblyBase.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using Microsoft.Extensions.DependencyModel;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using TCDFx.ComponentModel;
using TCDFx.Native;
using TCDFx.Runtime.InteropServices.SafeHandles;

namespace TCDFx.Runtime.InteropServices
{
    /// <summary>
    /// The type of a <see cref="NativeAssembly"/>.
    /// </summary>
    public enum NativeAssemblyType
    {
        /// <summary>
        /// An assembly that is located in a relative folder for is a system assembly.
        /// </summary>
        Default = 0,

        /// <summary>
        /// An assembly that is located in a dependency. (i.e. from a nupkg)
        /// </summary>
        Dependency = 1,

        /// <summary>
        /// an assembly that is embedded in an assembly as a resource.
        /// </summary>
        Embedded = 2
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
        /// <param name="type">The type of  assembly.</param>
        /// <param name="names">An ordered list of assembly names to attempt to load.</param>
        public NativeAssembly(NativeAssemblyType type, params string[] names) : base()
        {
            Type = type;
            IntPtr asmHnd = LoadAssembly(names);
            Handle = new SafeAssemblyHandle(asmHnd);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeAssembly"/> class.
        /// </summary>
        /// <param name="names">An ordered list of assembly names to attempt to load.</param>
        public NativeAssembly(params string[] names) : this(NativeAssemblyType.Default, names) { }

        public event EventHandler<Component, EventArgs> Initialized;

        public NativeAssemblyType Type { get; }

        /// <summary>
        /// Loads a function whose signature and name match the given delegate type's signature and name.
        /// </summary>
        /// <typeparam name="T">The type of delegate to return.</typeparam>
        /// <returns>A delegate wrapping the native function.</returns>
        public T LoadFunction<T>() => LoadFunction<T>(typeof(T).Name);

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
                throw new EntryPointNotFoundException($"No function was found with the name {name}.");
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
                throw new ArgumentNullException(nameof(name), "The function pointer's name nust not be null, empty, or whitespace.");
            ret = LoadFunctionPointer(Handle, name);

            if (ret == IntPtr.Zero)
                throw new EntryPointNotFoundException($"No function was found with the name {name}.");
            return ret;
        }

        private IEnumerable<string> EnumerateLoadTargets(string name)
        {
            switch (Type)
            {
                case NativeAssemblyType.Dependency:
                    if (TryLocateNativeAssetFromDeps(name, out string appLocalNativePath, out string depsResolvedPath))
                    {
                        yield return appLocalNativePath;
                        yield return depsResolvedPath;
                    }
                    break;
                case NativeAssemblyType.Embedded:
                    if (TryExtractEmbeddedAssembly(name, out string embeddedResolvedPath))
                    {
                        yield return embeddedResolvedPath;
                    }
                    break;
                case NativeAssemblyType.Default:
                default:
                    yield return Path.Combine(AppContext.BaseDirectory, name);
                    yield return name;
                    break;
            }
        }

        /// <inheritdoc />
        protected virtual void OnInitialized() => Initialized?.Invoke(this, EventArgs.Empty);

        private IntPtr LoadAssembly(params string[] names)
        {
            if (names == null || names.Length == 0)
                throw new ArgumentNullException(nameof(names), "Parameter must not be null or empty.");

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
                throw new FileNotFoundException($"Could not find or load the native library from any name: [ {string.Join(", ", names)} ]");
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
            else if (actualRuntimeIdentifier.StartsWith("osx"))
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

        private static bool TryExtractEmbeddedAssembly(string name, out string embeddedResolvedPath)
        {
            Assembly asm = Assembly.GetEntryAssembly();
            string[] resNames = asm.GetManifestResourceNames();
            string resAsmName = name.Replace(".dll", string.Empty).Replace("-", "_").Replace(" ", "_").Replace(".", "_");
            string tempDir = Path.Combine(Path.GetTempPath(), asm.GetName().Name, Platform.RuntimeID);
            string outputAsm = Path.Combine(tempDir, name);

            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);

            Stream asmStream;
            bool resAsmExists = false;
            foreach (string resName in resNames)
                if (resName == resAsmName)
                    resAsmExists = true;
            if (resAsmExists)
            {
                embeddedResolvedPath = outputAsm;
                return true;
            }

            asmStream = asm.GetManifestResourceStream(resAsmName);
            if (!File.Exists(outputAsm))
            {
                byte[] buffer = new byte[8 * 1024];
                int len;
                while ((len = asmStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    using Stream output = File.Create(outputAsm);
                    output.Write(buffer, 0, len);
                }
                embeddedResolvedPath = outputAsm;
                return true;
            }
            embeddedResolvedPath = string.Empty;
            return false;
        }
    }
}