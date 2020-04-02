/***********************************************************************************************************************
 * FileName:            Libdl.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TACDevel.Native
{
#pragma warning disable CA1060 // Move pinvokes to native methods class
#pragma warning disable IDE1006 // Naming Styles
    [SuppressUnmanagedCodeSecurity]
    internal static class Libdl
    {
        private const string AssemblyRef = "libdl";

        public const int RTLD_NOW = 0x002;

        [DllImport(AssemblyRef)]
        internal static extern IntPtr dlopen(string fileName, int flags);

        [DllImport(AssemblyRef)]
        internal static extern IntPtr dlsym(IntPtr handle, string name);

        [DllImport(AssemblyRef)]
        internal static extern int dlclose(IntPtr handle);
    }
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA1060 // Move pinvokes to native methods class
}