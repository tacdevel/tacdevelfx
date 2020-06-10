/***********************************************************************************************************************
 * FileName:            Libc.cs
 * Copyright/License:   https://github.com/tacdevel/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TACDevel.Native
{
#pragma warning disable CA1060 // Move pinvokes to native methods class
#pragma warning disable IDE1006 // Naming Styles
    [SuppressUnmanagedCodeSecurity]
    internal static class Libc
    {
        private const string AssemblyRef = "libc";

        [DllImport(AssemblyRef)]
        internal static extern unsafe int sysctl(int* name, uint namelen, byte* oldp, uint* oldlenp, IntPtr newp, uint newlen);
    }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA1060 // Move pinvokes to native methods class
}