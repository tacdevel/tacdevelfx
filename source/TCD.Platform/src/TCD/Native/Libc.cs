/***************************************************************************************************
 * FileName:             PlatformHelper.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCD.Native
{
    internal static class Libc
    {
        private const string AssemblyRef = "libc";

        [DllImport(AssemblyRef)]
#pragma warning disable IDE1006
        internal static extern unsafe int sysctl(int* name, uint namelen, byte* oldp, uint* oldlenp, IntPtr newp, uint newlen);
#pragma warning restore IDE1006
    }
}