/***************************************************************************************************
 * FileName:             Platform.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using TCDFx.Native;

namespace TCDFx.Runtime
{
    // Based on: https://github.com/dotnet/core-setup/blob/master/src/managed/Microsoft.DotNet.PlatformAbstractions/RuntimeEnvironment.cs
    /// <summary>
    /// Contains information about the current running platform.
    /// </summary>
    public static class Platform
    {
        private static readonly string pRIDEnv = Environment.GetEnvironmentVariable("DOTNET_RUNTIME_ID");

        static Platform()
        {
            if (pRIDEnv != null)
            {
                string[] ridParts = pRIDEnv.Split('-');

                string osStr = Regex.Replace(ridParts[0], "[^0-9.]", "");
                if (osStr == "win")
                    OperatingSystem = PlatformOS.Windows;
#pragma warning disable IDE0045 // 'if' statement can be simplified
                else if (osStr == "osx")
#pragma warning restore IDE0045 // 'if' statement can be simplified
                    OperatingSystem = PlatformOS.MacOS;
                else
                    OperatingSystem = Enum.TryParse(osStr, true, out PlatformOS os) ? os : PlatformOS.Unknown;

                switch (OperatingSystem)
                {
                    case PlatformOS.Windows:
                        PlatformType = PlatformType.Windows;
                        break;
                    case PlatformOS.MacOS:
                        PlatformType = PlatformType.MacOS;
                        break;
                    case PlatformOS.FreeBSD:
                        PlatformType = PlatformType.FreeBSD;
                        break;
                    case PlatformOS.Linux:
                    case PlatformOS.Debian:
                    case PlatformOS.Ubuntu:
                    case PlatformOS.LinuxMint:
                    case PlatformOS.Fedora:
                    case PlatformOS.RHEL:
                    case PlatformOS.OL:
                    case PlatformOS.OpenSUSE:
                    case PlatformOS.SLES:
                    case PlatformOS.CentOS:
                    case PlatformOS.Alpine:
                        PlatformType = PlatformType.Linux;
                        break;
                    case PlatformOS.Unknown:
                    default:
                        PlatformType = PlatformType.Unknown;
                        break;
                }

                string versionStr = Regex.Replace(ridParts[0], "[^a-zA-Z]", "");
                if (!versionStr.Contains("."))
                    Version = new Version(int.Parse(versionStr), 0);
                else
                {
                    string[] splitVersion = versionStr.Split('.');
                    Version = new Version(int.Parse(splitVersion[0]), int.Parse(splitVersion[1]));
                }

#pragma warning disable IDE0045 // 'if' statement can be simplified
                if (ridParts[1] == "arm")
#pragma warning restore IDE0045 // 'if' statement can be simplified
                    Architecture = PlatformArch.ARM32;
                else
                    Architecture = Enum.TryParse(ridParts[1], true, out PlatformArch arch) ? arch : PlatformArch.Unknown;

                RuntimeID = pRIDEnv;
            }
            else
            {
                Architecture = (PlatformArch)RuntimeInformation.ProcessArchitecture;

                PlatformType = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? PlatformType.Windows
                : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? PlatformType.Linux
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? PlatformType.MacOS
                : RuntimeInformation.IsOSPlatform(OSPlatform.Create("FREEBSD")) ? PlatformType.FreeBSD
                : PlatformType.Unknown;

                (PlatformOS OS, Version Version) osInfo;
                switch (PlatformType)
                {
                    case PlatformType.Windows:
                        osInfo = GetWindowsInfo();
                        break;
                    case PlatformType.Linux:
                        osInfo = GetLinuxInfo();
                        break;
                    case PlatformType.MacOS:
                        osInfo = GetMacOSInfo();
                        break;
                    case PlatformType.FreeBSD:
                        osInfo = GetFreeBSDInfo();
                        break;
                    default:
                        osInfo = (PlatformOS.Unknown, new Version(0, 0));
                        break;
                }

                OperatingSystem = osInfo.OS;
                Version = osInfo.Version;
                RuntimeID = $"{GetRIDOS()}{GetRIDVersion()}{GetRIDArch()}";
                GenericRuntimeID = $"{GetRIDOS()}{GetRIDArch()}";
            }
        }

        /// <summary>
        /// The processor architecture.
        /// </summary>
        public static PlatformArch Architecture { get; }

        /// <summary>
        /// The operating system platform.
        /// </summary>
        public static PlatformType PlatformType { get; }

        /// <summary>
        /// The operating system type.
        /// </summary>
        public static PlatformOS OperatingSystem { get; }

        /// <summary>
        /// The operating system version.
        /// </summary>
        public static Version Version { get; }

        /// <summary>
        /// The .NET Runtime Identifier (RID) for the platform.
        /// </summary>
        public static string RuntimeID { get; }

        /// <summary>
        /// The generic .NET Runtime Identifier (RID) for the platform.
        /// </summary>
        public static string GenericRuntimeID { get; }

        private static (PlatformOS OS, Version Version) GetWindowsInfo()
        {
            Ntdll.RTL_OSVERSIONINFOEX osvi = new Native.Ntdll.RTL_OSVERSIONINFOEX();
            osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);

            return Ntdll.RtlGetVersion(out osvi) == 0
                ? (PlatformOS.Windows, new Version((int)osvi.dwMajorVersion, (int)osvi.dwMinorVersion))
                : (PlatformOS.Windows, new Version(0, 0));
        }

        private static unsafe (PlatformOS OS, Version Version) GetMacOSInfo()
        {
            int CTL_KERN = 1;
            int KERN_OSRELEASE = 2;

            const uint BUFFER_LENGTH = 32;

            int* name = stackalloc int[2];
            name[0] = CTL_KERN;
            name[1] = KERN_OSRELEASE;

            byte* buf = stackalloc byte[(int)BUFFER_LENGTH];
            uint* len = stackalloc uint[1];
            *len = BUFFER_LENGTH;

            try
            {
                if (Libc.sysctl(name, 2, buf, len, IntPtr.Zero, 0) == 0 && *len < BUFFER_LENGTH)
                    if (Version.TryParse(Marshal.PtrToStringAnsi((IntPtr)buf, (int)*len), out Version version))
                        return (PlatformOS.MacOS, new Version(10, version.Major));
            }
            catch
            {
            }
            return (PlatformOS.MacOS, new Version(0, 0));
        }

        private static (PlatformOS OS, Version Version) GetLinuxInfo()
        {
            string osID = "linux";
            string osVersion = "0.0";

            // For sample os-release files: https://gitlab.com/zygoon/os-release-zoo
            if (File.Exists("/etc/os-release"))
            {
                string[] lines = File.ReadAllLines("/etc/os-release");
                foreach (string line in lines)
                {
                    if (line.StartsWith("ID=", StringComparison.Ordinal))
                        osID = line.Substring(3).Trim('"', '\'');
                    else if (line.StartsWith("VERSION_ID=", StringComparison.Ordinal))
                        osVersion = line.Substring(11).Trim('"', '\'');
                }
            }
            else if (File.Exists("/etc/redhat-release"))
            {
                string[] lines = File.ReadAllLines("/etc/redhat-release");

                if (lines.Length >= 1)
                {
                    string line = lines[0];
                    if (line.StartsWith("Red Hat Enterprise Linux Server release") || line.StartsWith("CentOS release"))
                    {
                        osID = "rhel";
                        osVersion = "6";
                    }
                }
            }

            if (osVersion != "0.0")
            {
                // Handle if VersionId is null by just setting the index to -1.
                int lastVersionNumberSeparatorIndex = osVersion?.IndexOf('.') ?? -1;

                // For Alpine, the version reported has three components, so we need to find the second version separator
                if (lastVersionNumberSeparatorIndex != -1 && osID == "alpine")
                    lastVersionNumberSeparatorIndex = osVersion.IndexOf('.', lastVersionNumberSeparatorIndex + 1);

                if (lastVersionNumberSeparatorIndex != -1 && (osID == "alpine"))
                    osVersion = osVersion.Substring(0, lastVersionNumberSeparatorIndex);
            }
            return (Enum.TryParse(osID, true, out PlatformOS os) ? os : PlatformOS.Linux, Version.TryParse(osVersion, out Version version) ? version : new Version(0, 0));
        }

        private static (PlatformOS OS, Version Version) GetFreeBSDInfo()
        {
            // This is same as sysctl kern.version.
            // FreeBSD 11.0-RELEASE-p1 FreeBSD 11.0-RELEASE-p1 #0 r306420: Thu Sep 29 01:43:23 UTC 2016
            try
            {
                return (PlatformOS.FreeBSD, new Version(RuntimeInformation.OSDescription.Split()[1].Split('-')[0]));
            }
            catch { }
            return (PlatformOS.FreeBSD, new Version(0, 0));
        }

        private static string GetRIDArch() => Architecture == PlatformArch.ARM32 ? "-arm" : $"-{Architecture}";

        private static string GetRIDVersion()
        {
            if (Version.Major == 0 && Version.Minor == 0)
                return string.Empty;

            switch (PlatformType)
            {
                // Windows RIDs do not separate OS name and version by "."
                case PlatformType.Windows:
                    if (Version.Major == 6)
                    {
                        if (Version.Minor == 1)
                            return "7";
                        else if (Version.Minor == 2)
                            return "8";
                        else if (Version.Minor == 3)
                            return "81";
                    }
                    else if (Version.Major >= 10)
                        return Version.Major.ToString();
                    return string.Empty;
                case PlatformType.MacOS:
                case PlatformType.Linux:
                case PlatformType.FreeBSD:
                    if (Version.Minor > 0)
                        return $".{Version.Major}";
                    return $".{Version.Major}.{Version.Minor}";
                case PlatformType.Unknown:
                default:
                    return string.Empty;
            }
        }

        private static string GetRIDOS()
        {
            switch (OperatingSystem)
            {
                case PlatformOS.Windows:
                    return "win";
                case PlatformOS.MacOS:
                    return "osx";
                case PlatformOS.Linux:
                case PlatformOS.FreeBSD:
                case PlatformOS.Debian:
                case PlatformOS.Ubuntu:
                case PlatformOS.LinuxMint:
                case PlatformOS.Fedora:
                case PlatformOS.RHEL:
                case PlatformOS.OL:
                case PlatformOS.OpenSUSE:
                case PlatformOS.SLES:
                case PlatformOS.CentOS:
                case PlatformOS.Alpine:
                    return OperatingSystem.ToString().ToLowerInvariant();
                case PlatformOS.Unknown:
                default:
                    return "unknown";
            }
        }
    }
}