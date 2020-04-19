/***********************************************************************************************************************
 * FileName:            Platform.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using TACDevel.Native;

namespace TACDevel.Runtime
{
    /// <summary>
    /// Contains information about the current platform.
    /// </summary>
    public static class Platform
    {
        private static readonly string pRidEnv = Environment.GetEnvironmentVariable("DOTNET_RUNTIME_ID");

        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "<Pending>")]
        static Platform()
        {
            if (!string.IsNullOrWhiteSpace(pRidEnv))
            {
                PlatformOS os = GetOSFromRid(pRidEnv);
                OS = os;
                Type = GetTypeFromOS(os);
                Version = GetVersionFromRid(pRidEnv);
                Arch = GetArchFromRid(pRidEnv);
                RuntimeID = pRidEnv.ToLowerInvariant();
                GenericRuntimeID = $"{GetRIDOS(OS)}{GetRIDArch(Arch)}";
            }
            else
            {
                Arch = (PlatformArch)RuntimeInformation.ProcessArchitecture;
                Type = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? PlatformType.Windows
                    : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? PlatformType.Linux
                    : RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? PlatformType.MacOS
                    : RuntimeInformation.IsOSPlatform(OSPlatform.Create("FREEBSD")) ? PlatformType.FreeBSD
                    : PlatformType.Unknown;
                (PlatformOS, Version) osInfo = Type switch
                {
                    PlatformType.Windows => GetWindowsInfo(),
                    PlatformType.Linux => GetLinuxInfo(),
                    PlatformType.MacOS => GetMacOSInfo(),
                    PlatformType.FreeBSD => GetFreeBSDInfo(),
                    _ => (PlatformOS.Unknown, new Version(0, 0)),
                };
                OS = osInfo.Item1;
                Version = osInfo.Item2;
                RuntimeID = $"{GetRIDOS(OS)}{GetRIDVersion(Type, Version)}{GetRIDArch(Arch)}";
                GenericRuntimeID = $"{GetRIDOS(OS)}{GetRIDArch(Arch)}";
            }
        }

        /// <summary>
        /// The processor architecture.
        /// </summary>
        public static PlatformArch Arch { get; }

        /// <summary>
        /// The operating system platform.
        /// </summary>
        public static PlatformType Type { get; }

        /// <summary>
        /// The operating system type.
        /// </summary>
        public static PlatformOS OS { get; }

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

        /// <summary>
        /// Determines whether the current operating system is a Windows-based operating system.
        /// </summary>
        public static bool IsWindows => Type == PlatformType.Windows;

        /// <summary>
        /// Determines whether the current operating system is a macOS-based operating system.
        /// </summary>
        public static bool IsMacOS => Type == PlatformType.MacOS;

        /// <summary>
        /// Determines whether the current operating system is a Linux-based operating system.
        /// </summary>
        public static bool IsLinux => Type == PlatformType.Linux;

        /// <summary>
        /// Determines whether the current operating system is a FreeBSD-based operating system.
        /// </summary>
        public static bool IsFreeBSD => Type == PlatformType.FreeBSD;

        /// <summary>
        /// Determines whether the current operating system is a 32-bit operating system.
        /// </summary>
        public static bool Is32Bit => Arch == PlatformArch.X86;

        /// <summary>
        /// Determines whether the current operating system is a 64-bit operating system.
        /// </summary>
        public static bool Is64Bit => Arch == PlatformArch.X64;

        /// <summary>
        /// Determines whether the current operating system is a 32-bit ARM operating system.
        /// </summary>
        public static bool IsARM => Arch == PlatformArch.ARM;

        /// <summary>
        /// Determines whether the current operating system is a 64-bit ARM operating system.
        /// </summary>
        public static bool IsARM64 => Arch == PlatformArch.ARM64;

        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase")]
        private static string GetRIDArch(PlatformArch arch) => $"-{arch.ToString().ToLowerInvariant()}";

        private static string GetRIDVersion(PlatformType type, Version version)
        {
            if (version.Major == 0 && version.Minor == 0)
                return string.Empty;

            switch (type)
            {
                // Windows RIDs do not separate OS name and version by "."
                case PlatformType.Windows:
                    if (version.Major == 6)
                    {
                        if (version.Minor == 1)
                            return "7";
                        else if (version.Minor == 2)
                            return "8";
                        else if (version.Minor == 3)
                            return "81";
                    }
                    else if (version.Major >= 10)
                        return version.Major.ToString(CultureInfo.InvariantCulture);
                    return string.Empty;
                case PlatformType.MacOS:
                case PlatformType.Linux:
                case PlatformType.FreeBSD:
                    if (version.Minor == 0)
                        return $".{version.Major}";
                    return $".{version.Major}.{version.Minor}";
                case PlatformType.Unknown:
                default:
                    return string.Empty;
            }
        }

        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "<Pending>")]
        private static string GetRIDOS(PlatformOS os)
        {
            switch (os)
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
                    return os.ToString().ToLowerInvariant();
                case PlatformOS.Unknown:
                default:
                    return "unknown";
            }
        }

        private static PlatformOS GetOSFromRid(string rid)
        {
            string[] ridParts = rid.Split('-');
            string ridOS = Regex.Replace(ridParts[0], "[^0-9.]", "");
            return ridOS == "win"
                ? PlatformOS.Windows : ridOS == "osx"
                ? PlatformOS.MacOS : Enum.TryParse(ridOS, true, out PlatformOS os)
                ? os : PlatformOS.Unknown;
        }

        private static PlatformType GetTypeFromOS(PlatformOS os)
        {
            switch (os)
            {
                case PlatformOS.Windows:
                    return PlatformType.Windows;
                case PlatformOS.MacOS:
                    return PlatformType.MacOS;
                case PlatformOS.FreeBSD:
                    return PlatformType.FreeBSD;
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
                    return PlatformType.Linux;
                case PlatformOS.Unknown:
                default:
                    return PlatformType.Unknown;
            }
        }

        private static Version GetVersionFromRid(string rid)
        {
            string versionStr = Regex.Replace(rid.Split('-')[0], "[^a-zA-Z]", "");
            if (!versionStr.Contains(".", StringComparison.OrdinalIgnoreCase))
                return new Version(int.Parse(versionStr, CultureInfo.InvariantCulture), 0);
            else
            {
                string[] splitVersion = versionStr.Split('.');
                return new Version(int.Parse(splitVersion[0], CultureInfo.InvariantCulture), int.Parse(splitVersion[1], CultureInfo.InvariantCulture));
            }
        }

        private static PlatformArch GetArchFromRid(string rid) => Enum.TryParse(rid.Split('-')[1], true, out PlatformArch arch) ? arch : PlatformArch.Unknown;

        private static (PlatformOS, Version) GetWindowsInfo()
        {
            Ntdll.RTL_OSVERSIONINFOEX osvi = new Ntdll.RTL_OSVERSIONINFOEX();
            osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);

            return Ntdll.RtlGetVersion(out osvi) == 0
                ? (PlatformOS.Windows, new Version((int)osvi.dwMajorVersion, (int)osvi.dwMinorVersion))
                : (PlatformOS.Windows, new Version(0, 0));
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        private static unsafe (PlatformOS, Version) GetMacOSInfo()
        {
            int* name = stackalloc int[2];
            name[0] = 1;
            name[1] = 2;

            byte* buf = stackalloc byte[(int)32U];
            uint* len = stackalloc uint[1];
            *len = 32U;

            try
            {
                if (Libc.sysctl(name, 2, buf, len, IntPtr.Zero, 0) == 0 && *len < 32U)
                    if (Version.TryParse(Marshal.PtrToStringAnsi((IntPtr)buf, (int)*len), out Version version))
                        return (PlatformOS.MacOS, new Version(10, version.Major));
            }
            catch { }
            return (PlatformOS.MacOS, new Version(0, 0));
        }

        private static (PlatformOS, Version) GetLinuxInfo()
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
                    if (line.StartsWith("Red Hat Enterprise Linux Server release", StringComparison.Ordinal) || line.StartsWith("CentOS release", StringComparison.Ordinal))
                    {
                        osID = "rhel";
                        osVersion = "6";
                    }
                }
            }

            if (osVersion != "0.0")
            {
                // Handle if VersionId is null by just setting the index to -1.
                int lastVersionNumberSeparatorIndex = osVersion?.IndexOf('.', StringComparison.Ordinal) ?? -1;

                // For Alpine, the version reported has three components, so we need to find the second version separator
                if (lastVersionNumberSeparatorIndex != -1 && osID == "alpine")
                    lastVersionNumberSeparatorIndex = osVersion.IndexOf('.', lastVersionNumberSeparatorIndex + 1);

                if (lastVersionNumberSeparatorIndex != -1 && (osID == "alpine"))
                    osVersion = osVersion.Substring(0, lastVersionNumberSeparatorIndex);
            }
            return (Enum.TryParse(osID, true, out PlatformOS os) ? os : PlatformOS.Linux, Version.TryParse(osVersion, out Version version) ? version : new Version(0, 0));
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types")]
        private static (PlatformOS, Version) GetFreeBSDInfo()
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
    }
}