/***********************************************************************************************************************
 * FileName:             Ntdll.cs
 * Copyright:            Copyright © 2017-2020 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
 **********************************************************************************************************************/

using System.Runtime.InteropServices;
using System.Security;

namespace TACDevel.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Ntdll
    {
        private const string AssemblyRef = "ntdll";

        [StructLayout(LayoutKind.Sequential)]
        internal struct RTL_OSVERSIONINFOEX
        {
            internal uint dwOSVersionInfoSize;
            internal uint dwMajorVersion;
            internal uint dwMinorVersion;
            internal uint dwBuildNumber;
            internal uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal string szCSDVersion;
        }

        [DllImport(AssemblyRef)]
        internal static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);
    }
}