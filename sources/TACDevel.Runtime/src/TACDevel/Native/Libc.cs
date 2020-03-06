/***********************************************************************************************************************
 * FileName:             Libc.cs
 * Copyright:            Copyright © 2017-2020 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
 **********************************************************************************************************************/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace TACDevel.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Libc
    {
        private const string AssemblyRef = "libc";

        [DllImport(AssemblyRef)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMethodName")]
        internal static extern unsafe int sysctl(int* name, uint namelen, byte* oldp, uint* oldlenp, IntPtr newp, uint newlen);
    }
}