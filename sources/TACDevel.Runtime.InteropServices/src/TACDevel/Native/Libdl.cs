/***************************************************************************************************
 * FileName:             Libdl.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace TCDFx.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Libdl
    {
        private const string AssemblyRef = "libdl";

        public const int RTLD_NOW = 0x002;

        [DllImport(AssemblyRef)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMethodName")]
        internal static extern IntPtr dlopen(string fileName, int flags);

        [DllImport(AssemblyRef)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMethodName")]
        internal static extern IntPtr dlsym(IntPtr handle, string name);

        [DllImport(AssemblyRef)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMethodName")]
        internal static extern int dlclose(IntPtr handle);
    }
}