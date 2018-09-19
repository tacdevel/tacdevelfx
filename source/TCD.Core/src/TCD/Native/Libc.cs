/****************************************************************************
 * FileName:   Libc.cs
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
    internal static class Libc
    {
        [DllImport(AssemblyRef_DllImport.Libc)]
        internal unsafe static extern int sysctl(int* name, uint namelen, byte* oldp, uint* oldlenp, IntPtr newp, uint newlen);
    }
}