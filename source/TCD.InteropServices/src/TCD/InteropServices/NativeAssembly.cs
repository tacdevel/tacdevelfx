/***************************************************************************************************
 * FileName:             NativeAssembly.cs
 * Date:                 20180919
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;
using TCD.Native;
using TCD.SafeHandles;
using static TCD.Platform;

namespace TCD
{
    namespace InteropServices
    {
        /// <summary>
        /// Represents a native shared assembly that function pointers may be loaded from.
        /// </summary>
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
            public NativeAssembly(NativeAssemblyResolver resolver, params string[] names) : base(new SafeAssemblyHandle(GetHandle(resolver, names))) { }

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
            public T LoadFunction<T>(string name) where T : Delegate
            {
                IntPtr funcPtr = LoadFunction(name);
                if (funcPtr == IntPtr.Zero)
                    throw new InvalidOperationException($"No function was found with the name {name}.");
                T t = ;
                return Marshal.GetDelegateForFunctionPointer<T>(funcPtr);
            }

            /// <summary>
            /// Loads a function pointer with the given name.
            /// </summary>
            /// <param name="name">The name of the native function.</param>
            /// <returns>A function pointer for the given name, or <see cref="IntPtr.Zero"/> if no function with the specified name was found.</returns>
            public IntPtr LoadFunction(string name)
            {
                if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

                switch (CurrentPlatform.Platform)
                {
                    case PlatformType.Windows:
                        return Kernel32.GetProcAddress(Handle, name);
                    case PlatformType.Linux:
                    case PlatformType.MacOS:
                    case PlatformType.FreeBSD:
                        return Libdl.dlsym(Handle, name);
                    case PlatformType.Unknown:
                    default:
                        return IntPtr.Zero;
                }
            }

            private static IntPtr GetHandle(NativeAssemblyResolver resolver, params string[] names)
            {
                if (names == null || names.Length == 0) throw new ArgumentNullException(nameof(names));

                IntPtr value = IntPtr.Zero;
                foreach (string name in names)
                {
                    if (Path.IsPathRooted(name))
                    {
                        switch (CurrentPlatform.Platform)
                        {
                            case PlatformType.Windows:
                                value = Kernel32.LoadLibrary(name);
                                break;
                            case PlatformType.Linux:
                            case PlatformType.MacOS:
                            case PlatformType.FreeBSD:
                                value = Libdl.dlopen(name, 0x002);
                                break;
                            case PlatformType.Unknown:
                            default:
                                break;
                        }
                    }
                    else
                    {
                        foreach (string loadTarget in resolver.EnumerateLoadTargets(name))
                        {
                            if (!Path.IsPathRooted(loadTarget) || File.Exists(loadTarget))
                            {
                                IntPtr v = IntPtr.Zero;
                                switch (CurrentPlatform.Platform)
                                {
                                    case PlatformType.Windows:
                                        v = Kernel32.LoadLibrary(loadTarget);
                                        break;
                                    case PlatformType.Linux:
                                    case PlatformType.MacOS:
                                    case PlatformType.FreeBSD:
                                        v = Libdl.dlopen(loadTarget, 0x002);
                                        break;
                                    case PlatformType.Unknown:
                                    default:
                                        break;
                                }
                                if (v != IntPtr.Zero)
                                    value = v;
                            }
                        }
                    }
                    if (value != IntPtr.Zero)
                        break;
                }

                if (value == IntPtr.Zero) throw new FileNotFoundException($"Could not load a library with the specified name(s): [ {string.Join(", ", names)} ]");
                return value;
            }
        }
    }

    namespace Native
    {
        internal static class Kernel32
        {
            private const string AssemblyRef = "kernel32";

            [DllImport(AssemblyRef)]
            internal static extern IntPtr LoadLibrary(string fileName);

            [DllImport(AssemblyRef)]
            internal static extern IntPtr GetProcAddress(IntPtr module, string procName);

            [DllImport(AssemblyRef)]
            internal static extern int FreeLibrary(IntPtr module);
        }

        internal static class Libdl
        {
            private const string AssemblyRef = "libdl";

            [DllImport(AssemblyRef)]
#pragma warning disable IDE1006 // Naming rule violation
            internal static extern IntPtr dlopen(string fileName, int flags);
#pragma warning enable IDE1006 // Naming rule violation

            [DllImport(AssemblyRef)]
#pragma warning disable IDE1006 // Naming rule violation
            internal static extern IntPtr dlsym(IntPtr handle, string name);
#pragma warning enable IDE1006 // Naming rule violation

            [DllImport(AssemblyRef)]
#pragma warning disable IDE1006 // Naming rule violation
            internal static extern int dlclose(IntPtr handle);
#pragma warning enable IDE1006 // Naming rule violation
        }
    }
}