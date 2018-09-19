/****************************************************************************
 * FileName:   Ntdll.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180913
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System.Runtime.InteropServices;

namespace TCD.Native
{
    internal static class Ntdll
    {
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

        [DllImport(AssemblyRef_DllImport.Ntdll)]
        internal static extern int RtlGetVersion(out RTL_OSVERSIONINFOEX lpVersionInformation);
    }
}