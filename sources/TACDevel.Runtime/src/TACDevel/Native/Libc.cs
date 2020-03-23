/***********************************************************************************************************************
 * FileName:            Libc.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TACDevel.Native
{
    [SuppressUnmanagedCodeSecurity]
#pragma warning disable CA1060 // Move pinvokes to native methods class
    internal static class Libc
#pragma warning restore CA1060 // Move pinvokes to native methods class
    {
        private const string AssemblyRef = "libc";

        [DllImport(AssemblyRef)]
#pragma warning disable IDE1006 // Naming Styles
        internal static extern unsafe int sysctl(int* name, uint namelen, byte* oldp, uint* oldlenp, IntPtr newp, uint newlen);
#pragma warning restore IDE1006 // Naming Styles
    }
}