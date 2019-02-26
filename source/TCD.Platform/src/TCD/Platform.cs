/***************************************************************************************************
 * FileName:             PlatformHelper.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace TCD
{
    // Based on: https://github.com/dotnet/core-setup/blob/master/src/managed/Microsoft.DotNet.PlatformAbstractions/RuntimeEnvironment.cs
    public static class Platform
    {
        private static readonly PlatformArch pArch;
        private static readonly PlatformType pType;
        private static readonly PlatformOS pOS;
        private static readonly Version pVersion;
        private static readonly string pRID;
        private static readonly string pRIDEnv = Environment.GetEnvironmentVariable("DOTNET_RUNTIME_ID");

        static Platform()
        {
            if (pRIDEnv != null)
            {
                string[] ridParts = pRIDEnv.Split('-');

                string osStr = Regex.Replace(ridParts[0], "[^0-9.]", "");
                if (osStr == "win")
                    pOS = PlatformOS.Windows;
#pragma warning disable IDE0045 // 'if' statement can be simplified
                else if (osStr == "osx")
#pragma warning enable IDE0045 // 'if' statement can be simplified
                    pOS = PlatformOS.MacOS;
                else
                    pOS = Enum.TryParse(osStr, true, out PlatformOS os) ? os : PlatformOS.Unknown;

                switch (pOS)
                {
                    case PlatformOS.Windows:
                        pType = PlatformType.Windows;
                        break;
                    case PlatformOS.MacOS:
                        pType = PlatformType.MacOS;
                        break;
                    case PlatformOS.FreeBSD:
                        pType = PlatformType.FreeBSD;
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
                        pType = PlatformType.Linux;
                        break;
                    case PlatformOS.Unknown:
                    default:
                        pType = PlatformType.Unknown;
                        break;
                }

                string versionStr = Regex.Replace(ridParts[0], "[^a-zA-Z]", "");
                if (!versionStr.Contains("."))
                    pVersion = new Version(int.Parse(versionStr), 0);
                else
                {
                    string[] splitVersion = versionStr.Split('.');
                    pVersion = new Version(int.Parse(splitVersion[0]), int.Parse(splitVersion[1]));
                }

#pragma warning disable IDE0045 // 'if' statement can be simplified
                if (ridParts[1] == "arm")
#pragma warning enable IDE0045 // 'if' statement can be simplified
                    pArch = PlatformArch.ARM32;
                else
                    pArch = Enum.TryParse(ridParts[1], true, out PlatformArch arch) ? arch : PlatformArch.Unknown;

                pRID = null;
            }
            else
            {
                pArch = (PlatformArch)RuntimeInformation.ProcessArchitecture;

                pType = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? PlatformType.Windows
                : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? PlatformType.Linux
                : RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? PlatformType.MacOS
                : RuntimeInformation.IsOSPlatform(OSPlatform.Create("FREEBSD")) ? PlatformType.FreeBSD
                : PlatformType.Unknown;

                (PlatformOS OS, Version Version) osInfo;
                switch (pType)
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

                pOS = osInfo.OS;
                pVersion = osInfo.Version;
                pRID = $"{GetRIDOS()}{GetRIDVersion()}{GetRIDArch()}";
            }
            #if DEBUG
            Console.WriteLine($"[DEBUG] Platform detected: {pOS} {pVersion} {pArch}");
            #endif
        }

        public static PlatformInfo CurrentPlatform => new PlatformInfo(pArch, pType, pOS, pVersion, pRID ?? pRIDEnv);

        private static (PlatformOS OS, Version Version) GetWindowsInfo()
        {
            Native.Ntdll.RTL_OSVERSIONINFOEX osvi = new Native.Ntdll.RTL_OSVERSIONINFOEX();
            osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);

            return Native.Ntdll.RtlGetVersion(out osvi) == 0
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
                if (Native.Libc.sysctl(name, 2, buf, len, IntPtr.Zero, 0) == 0 && *len < BUFFER_LENGTH)
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

        private static string GetRIDArch() => pArch == PlatformArch.ARM32 ? "-arm" : $"-{pArch}";

        private static string GetRIDVersion()
        {
            if (pVersion.Major == 0 && pVersion.Minor == 0)
                return string.Empty;

            switch (pType)
            {
                // Windows RIDs do not separate OS name and version by "."
                case PlatformType.Windows:
                    if (pVersion.Major == 6)
                    {
                        if (pVersion.Minor == 1)
                            return "7";
                        else if (pVersion.Minor == 2)
                            return "8";
                        else if (pVersion.Minor == 3)
                            return "81";
                    }
                    else if (pVersion.Major >= 10)
                        return pVersion.Major.ToString();
                    return string.Empty;
                case PlatformType.MacOS:
                case PlatformType.Linux:
                case PlatformType.FreeBSD:
                    if (pVersion.Minor > 0)
                        return $".{pVersion.Major}";
                    return $".{pVersion.Major}.{pVersion.Minor}";
                case PlatformType.Unknown:
                default:
                    return string.Empty;
            }
        }

        private static string GetRIDOS()
        {
            switch (pOS)
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
                    return pOS.ToString().ToLowerInvariant();
                case PlatformOS.Unknown:
                default:
                    return "unknown";
            }
        }
    }

    /// <summary>
    /// Contains information about a platform.
    /// </summary>
    public sealed class PlatformInfo
    {
        private PlatformInfo() { }

        internal PlatformInfo(PlatformArch arch, PlatformType platform, PlatformOS os, Version version, string rid)
        {
            Architecture = arch;
            Platform = platform;
            OperatingSystem = os;
            Version = version;
            RuntimeID = rid;
        }

        /// <summary>
        /// The processor architecture.
        /// </summary>
        public PlatformArch Architecture { get; }

        /// <summary>
        /// The operating system platform.
        /// </summary>
        public PlatformType Platform { get; }

        /// <summary>
        /// The operating system type.
        /// </summary>
        public PlatformOS OperatingSystem { get; }

        /// <summary>
        /// The operating system version.
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// The .NET Runtime Identifier (RID) for the platform.
        /// </summary>
        public string RuntimeID { get; }
    }

    /// <summary>
    /// Indicates the architecture of the processor.
    /// </summary>
    public enum PlatformArch
    {
        /// <summary>
        /// An unknown processor architecture.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// An Intel-based 32-bit processor architecture.
        /// </summary>
        X86 = 0,   // x86

        /// <summary>
        /// An Intel-based 64-bit processor architecture.
        /// </summary>
        X64 = 1,   // x64

        /// <summary>
        /// A 32-bit ARM processor architecture.
        /// </summary>
        ARM32 = 2, // arm

        /// <summary>
        /// A 64-bit ARM processor architecture.
        /// </summary>
        ARM64 = 3  // arm64
    }

    /// <summary>
    /// Indicates the type of platform.
    /// </summary>
    public enum PlatformType
    {
        /// <summary>
        /// An unknown platform.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// A Microsoft Windows platform.
        /// </summary>
        Windows, // win

        /// <summary>
        /// An Apple OSX/macOS platform.
        /// </summary>
        MacOS,   // osx

        /// <summary>
        /// A Linux platform.
        /// </summary>
        Linux,   // linux

        /// <summary>
        /// A FreeBSD platform.
        /// </summary>
        FreeBSD, // freebsd
    }

    /// <summary>
    /// Indicates the operating system type.
    /// </summary>
    public enum PlatformOS
    {
        /// <summary>
        /// An unknown operating system.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// A Microsoft Windows operating system.
        /// </summary>
        Windows,     // win

        /// <summary>
        /// An Apple OSX/macOS operating system.
        /// </summary>
        MacOS,       // osx

        /// <summary>
        /// A Linux operating system.
        /// </summary>
        Linux,       // linux

        /// <summary>
        /// A FreeBSD operating system.
        /// </summary>
        FreeBSD,     // freebsd

        /// <summary>
        /// A Debian operating system.
        /// </summary>
        Debian,      // debian

        /// <summary>
        /// An Ubuntu operating system.
        /// </summary>
        Ubuntu,      // ubuntu

        /// <summary>
        /// A Linux Mint operating system.
        /// </summary>
        LinuxMint,   // linuxmint

        /// <summary>
        /// A Fedora operating system.
        /// </summary>
        Fedora,      // fedora

        /// <summary>
        /// A Red Hat Enterprise Linux operating system.
        /// </summary>
        RHEL,        // rhel

        /// <summary>
        /// An Oracle Linux operating system.
        /// </summary>
        OL,          // ol

        /// <summary>
        /// An openSUSE operating system.
        /// </summary>
        OpenSUSE,    // opensuse

        /// <summary>
        /// A SUSE Linux Entrprise Server operating system.
        /// </summary>
        SLES,        // sles

        /// <summary>
        /// A CentOS operating system.
        /// </summary>
        CentOS,      // centos

        /// <summary>
        /// An Alpine operating system.
        /// </summary>
        Alpine       // alpine
    }
}