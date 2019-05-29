/***************************************************************************************************
 * FileName:             NativeAssemblyBase.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.InteropServices
{
    /// <summary>
    /// Provides the base implementation of a native (shared) assembly.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public abstract class NativeAssemblyBase : SafeNativeComponent<SafeAssemblyHandle>, INativeComponent<SafeAssemblyHandle>
    {
        internal static readonly Dictionary<string, NativeAssemblyBase> LoadedAssemblies = new Dictionary<string, NativeAssemblyBase>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NativeAssemblyBase"/> class.
        /// </summary>
        /// <param name="names">An ordered list of assembly names to attempt to load.</param>
        public NativeAssemblyBase(params string[] names) : base()
        {
            IntPtr asmHnd = LoadAssembly(names);
            Handle = new SafeAssemblyHandle(asmHnd);
        }

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
        /// <returns>A function pointer for the given name, or <see cref="IntPtr.Zero"/> if no function with the specified name was fou
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

        /// <summary>
        /// Returns an enumerator which yields possible library load targets, in priority order.
        /// </summary>
        /// <param name="name">The name of the library to load.</param>
        /// <returns>An enumerator yielding load targets.</returns>
        protected abstract IEnumerable<string> EnumerateLoadTargets(string name);

        /// <inheritdoc />
        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid)
                LoadedAssemblies.Remove(Name);
            base.ReleaseManagedResources();
        }

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
                            {
                                ret = ret2;
                                Name = Path.GetFileNameWithoutExtension(loadTarget);
                                LoadedAssemblies.TryAdd(Name, this);
                            }
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
    }
}