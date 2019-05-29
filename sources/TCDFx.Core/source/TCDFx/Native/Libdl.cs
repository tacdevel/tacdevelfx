/***************************************************************************************************
 * FileName:             Libdl.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TCD.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Libdl
    {
        private const string AssemblyRef = "libdl";

        public const int RTLD_NOW = 0x002;

        [DllImport(AssemblyRef)]
#pragma warning disable IDE1006 // Naming rule violation
        internal static extern IntPtr dlopen(string fileName, int flags);
#pragma warning restore IDE1006 // Naming rule violation

        [DllImport(AssemblyRef)]
#pragma warning disable IDE1006 // Naming rule violation
        internal static extern IntPtr dlsym(IntPtr handle, string name);
#pragma warning restore IDE1006 // Naming rule violation

        [DllImport(AssemblyRef)]
#pragma warning disable IDE1006 // Naming rule violation
        internal static extern int dlclose(IntPtr handle);
#pragma warning restore IDE1006 // Naming rule violation
    }
}