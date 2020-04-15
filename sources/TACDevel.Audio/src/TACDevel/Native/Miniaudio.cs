/***********************************************************************************************************************
 * FileName:            Miniaudio.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security;
using TACDevel.Runtime;
using TACDevel.Runtime.InteropServices;

namespace TACDevel.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Miniaudio
    {
        private const CallingConvention Convention = CallingConvention.Cdecl;
        private const LayoutKind Layout = LayoutKind.Sequential;
        private static readonly NativeAssembly asm = new NativeAssembly(
            (Platform.Is64Bit && Platform.IsWindows) ? @"runtimes\win-x64\miniaudio.dll" :
            (Platform.Is64Bit && Platform.IsMacOS) ? @"runtimes\osx-x64\miniaudio.dylib" :
            (Platform.Is64Bit && Platform.IsLinux) ? @"runtimes\linux-x64\miniaudio.so" :
            throw new PlatformNotSupportedException());
        private static T Call<T>() where T : Delegate => asm.LoadFunction<T>();


    }
}