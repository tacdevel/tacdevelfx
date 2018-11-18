/***************************************************************************************************
 * FileName:             AssemblyRef.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

#if TCD_UI
using System;
using System.Runtime.InteropServices;
using TCD.InteropServices;
using static TCD.PlatformHelper;
#endif

namespace TCD.Native
{
    internal static class AssemblyRef
    {
#if TCD_UI
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