/***********************************************************************************************************************
 * FileName:            Ntdll.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System.Runtime.InteropServices;
using System.Security;

namespace TACDevel.Native
{
#pragma warning disable CA1060 // Move pinvokes to native methods class
#pragma warning disable IDE1006 // Naming Styles
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
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore CA1060 // Move pinvokes to native methods class
}