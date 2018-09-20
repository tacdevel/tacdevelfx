/****************************************************************************
 * FileName:   NativeAssembly.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180919
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

 using System;
using System.IO;
using System.Runtime.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.InteropServices
{
    public class NativeAssembly : NativeComponent<SafeLibraryHandle>
    {
        public NativeAssembly(params string[] names) : this(NativeAssemblyResolver.Default, names) { }

        public NativeAssembly(NativeAssemblyResolver resolver, params string[] names)
        {
            if (names == null || names.Length == 0) throw new ArgumentNullException(nameof(names));

            IntPtr value = IntPtr.Zero;
            foreach (string name in names)
            {
                if (Path.IsPathRooted(name))
                {
                    switch (PlatformHelper.CurrentPlatform)
                    {
                        case PlatformHelper.Platform.Windows:
                            value = Kernel32.LoadLibrary(name);
                            break;
                        case PlatformHelper.Platform.Linux:
                        case PlatformHelper.Platform.MacOS:
                        case PlatformHelper.Platform.FreeBSD:
                            value = Libdl.dlopen(name, 0x002);
                            break;
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
                            switch (PlatformHelper.CurrentPlatform)
                            {
                                case PlatformHelper.Platform.Windows:
                                    v = Kernel32.LoadLibrary(loadTarget);
                                    break;
                                case PlatformHelper.Platform.Linux:
                                case PlatformHelper.Platform.MacOS:
                                case PlatformHelper.Platform.FreeBSD:
                                    v = Libdl.dlopen(loadTarget, 0x002);
                                    break;
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
            Handle = new SafeLibraryHandle(value);
        }

        public T LoadFunction<T>(string name)
        {
            IntPtr funcPtr = LoadFunction(name);
            if (funcPtr == IntPtr.Zero)
                throw new InvalidOperationException($"No function was found with the name {name}.");

            return Marshal.GetDelegateForFunctionPointer<T>(funcPtr);
        }

        public IntPtr LoadFunction(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            switch (PlatformHelper.CurrentPlatform)
            {
                case PlatformHelper.Platform.Windows:
                    return Kernel32.GetProcAddress(Handle, name);
                case PlatformHelper.Platform.Linux:
                case PlatformHelper.Platform.MacOS:
                case PlatformHelper.Platform.FreeBSD:
                    return Libdl.dlsym(Handle, name);
                default:
                    return IntPtr.Zero;
            }
        }
    }
}