/***************************************************************************************************
 * FileName:             Kernel32.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;


namespace TCD.Native
{
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
}