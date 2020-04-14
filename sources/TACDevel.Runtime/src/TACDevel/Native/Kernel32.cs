/***********************************************************************************************************************
 * FileName:            Kernel32.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TACDevel.Native
{
#pragma warning disable CA1060 // Move pinvokes to native methods class
#pragma warning disable CA2101 // Specify marshaling for P/Invoke string arguments
    [SuppressUnmanagedCodeSecurity]
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
#pragma warning restore CA2101 // Specify marshaling for P/Invoke string arguments
#pragma warning restore CA1060 // Move pinvokes to native methods class
}