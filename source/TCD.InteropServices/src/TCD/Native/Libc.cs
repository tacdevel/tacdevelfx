/***************************************************************************************************
 * FileName:             Libc.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCD.Native
{
    internal static class Libc
    {
        private const string AssemblyRef = "libc";

        [DllImport(AssemblyRef)]
        internal static extern unsafe int sysctl(int* name, uint namelen, byte* oldp, uint* oldlenp, IntPtr newp, uint newlen);
    }
}