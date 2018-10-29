/****************************************************************************
 * FileName:   AssemblyRef.cs
 * Date:       20180913
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;
#if !TCD_CORE || !TCD_DRAWING_PRIMITIVES
using TCD.InteropServices;
#endif
using static TCD.PlatformHelper;

namespace TCD.Native
{
    internal static class AssemblyRef
    {
#if !TCD_CORE || !TCD_DRAWING_PRIMITIVES
        internal static NativeAssembly Libui
        {
            get
            {
                if (CurrentPlatform == Platform.Windows && OSArchitecture == Architecture.X64) return new NativeAssembly(@"lib\win-x64\libui.dll");
                else if (CurrentPlatform == Platform.MacOS && OSArchitecture == Architecture.X64) return new NativeAssembly(@"lib\osx-x64\libui.dylib", @"lib\osx-x64\libui.A.dylib");
                else if (CurrentPlatform == Platform.Linux && OSArchitecture == Architecture.X64) return new NativeAssembly(@"lib\linux-x64\libui.so", @"lib\linux-x64\libui.so.0");
                else if (CurrentPlatform == Platform.FreeBSD && OSArchitecture == Architecture.X64) return new NativeAssembly(@"lib\linux-x64\libui.so", @"lib\linux-x64\libui.so.0");
                else throw new PlatformNotSupportedException();
            }
        }
#endif

        // Windows
        internal const string Kernel32 = "kernel32";
        internal const string Ntdll = "ntdll";

        //Unix
        internal const string Libc = "libc";
        internal const string Libdl = "libdl";
    }
}