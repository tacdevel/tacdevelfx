/***************************************************************************************************
 * FileName:             NativeAssembly.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD
{
    namespace InteropServices
    {
        /// <summary>
        /// Represents a shared, native library.
        /// </summary>
        [SuppressUnmanagedCodeSecurity]
        public class NativeAssembly : NativeComponent<SafeAssemblyHandle>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="NativeAssembly"/> class
            /// with the default <see cref="NativeAssemblyResolver"/>.
            /// </summary>
            /// <param name="names">An ordered list of assembly names to attempt to load.</param>
            public NativeAssembly(params string[] names) : this(NativeAssemblyResolver.Default, names) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="NativeAssembly"/> class
            /// with the specified <see cref="NativeAssemblyResolver"/>.
            /// </summary>
            /// <param name="names">An ordered list of assembly names to attempt to load.</param>
            /// <param name="resolver">The resolver used to identify possible load targets for the assembly.</param>
            public NativeAssembly(NativeAssemblyResolver resolver, params string[] names) : base(new SafeAssemblyHandle(LoadAssembly(resolver, names))) { }

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
                    throw new InvalidOperationException($"No function was found with the name {name}.");
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
                    throw new EntryPointNotFoundException($"Could not find or load the function pointer from name: [ {name} ]");
                return ret;
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

            private static IntPtr LoadAssembly(NativeAssemblyResolver resolver, params string[] names)
            {
                if (names == null || names.Length == 0)
                    throw new ArgumentException("Parameter must not be null or empty.", nameof(names));

                IntPtr ret = IntPtr.Zero;
                foreach (string name in names)
                {
                    if (Path.IsPathRooted(name))
                        ret = LoadAssembly(name);
                    else
                    {
                        foreach (string loadTarget in resolver.EnumerateLoadTargets(name))
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
        }
    }
}