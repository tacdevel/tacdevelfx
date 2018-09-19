/****************************************************************************
 * FileName:   Kernel32.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180913
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCD.Native
{
    internal static class Kernel32
    {
        [DllImport(AssemblyRef_DllImport.Kernel32)]
        internal static extern IntPtr LoadLibrary(string fileName);

        [DllImport(AssemblyRef_DllImport.Kernel32)]
        internal static extern IntPtr GetProcAddress(IntPtr module, string procName);

        [DllImport(AssemblyRef_DllImport.Kernel32)]
        internal static extern int FreeLibrary(IntPtr module);
    }
}