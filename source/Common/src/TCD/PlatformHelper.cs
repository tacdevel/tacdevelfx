/***************************************************************************************************
 * FileName:             PlatformHelper.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;
using TCD.Native;

namespace TCD
{
    // https://github.com/dotnet/core-setup/tree/master/src/Microsoft.DotNet.PlatformAbstractions
    // NOTE: This file is dependant on './Native/Libc.cs' and './Native/NtDll.cs'.
    internal static class PlatformHelper
    {
        internal static Platform CurrentPlatform
        {
            get
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    return Platform.Windows;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    return Platform.Linux;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    return Platform.MacOS;
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Create("FREEBSD")))
                    return Platform.FreeBSD;
                else return Platform.Unknown;
            }
        }

        internal static string OSVersion
        {
            get
            {
                switch (CurrentPlatform)
                {
                    case Platform.Windows:
                        return GetWindowsVersion() ?? string.Empty;
                    case Platform.Linux:
                        return GetLinuxInfo().Version ?? string.Empty;
                    case Platform.MacOS:
                        return GetDarwinVersion() ?? string.Empty;
                    case Platform.FreeBSD:
                        return GetFreeBSDVersion() ?? string.Empty;
                    default:
                        return string.Empty;
                }
            }
        }

        internal static string OSName
        {
            get
            {
                switch (CurrentPlatform)
                {
                    case Platform.Windows:
                        return "Windows";
                    case Platform.Linux:
                        return GetLinuxInfo().ID ?? "Linux";
                    case Platform.MacOS:
                        return "macOS";
                    case Platform.FreeBSD:
                        return "FreeBSD";
                    default:
                        return "Unknown";
                }
            }
        }

        internal static Architecture OSArchitecture => RuntimeInformation.ProcessArchitecture;

        internal static string PlatformRID => Environment.GetEnvironmentVariable("DOTNET_RUNTIME_ID") ?? $"{GetRIDPlatform()}{GetRIDOSVersion()}-{GetRIDArchitecture()}";

        private static string GetRIDArchitecture() => OSArchitecture.ToString().ToLowerInvariant();

        private static string GetRIDPlatform()
        {
            switch (CurrentPlatform)
            {
                case Platform.Windows:
                    return "win";
                case Platform.Linux:
                    return OSName.ToLowerInvariant();
                case Platform.MacOS:
                    return "osx";
                case Platform.FreeBSD:
                    return "freebsd";
                default:
                    return "unknown";
            }
        }

        private static string GetRIDOSVersion()
        {
            // Windows RIDs do not separate OS name and version by "." due to legacy
            // Others do, that's why we have the "." prefix on them below
            switch (CurrentPlatform)
            {
                case Platform.Windows:
                    Version ver = Version.Parse(OSVersion);
                    if (ver.Major == 6)
                    {
                        if (ver.Minor == 1)
                            return "7";
                        else if (ver.Minor == 2)
                            return "8";
                        else if (ver.Minor == 3)
                            return "81";
                    }
                    // Return the major version for use in RID computation without applying any cap.
                    else if (ver.Major >= 10)
                        return ver.Major.ToString();
                    return string.Empty; // Unknown version
                case Platform.Linux:
                    if (string.IsNullOrEmpty(OSVersion))
                        return string.Empty;
                    return $".{OSVersion}";
                case Platform.MacOS:
                    return $".{OSVersion}";
                case Platform.FreeBSD:
                    return $".{OSVersion}";
                default:
                    return string.Empty; // Unknown Platform? Unknown Version!
            }
        }

        private static string GetWindowsVersion()
        {
            Ntdll.RTL_OSVERSIONINFOEX osvi = new Ntdll.RTL_OSVERSIONINFOEX();
            osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);
            if (Ntdll.RtlGetVersion(out osvi) == 0)
                return $"{osvi.dwMajorVersion}.{osvi.dwMinorVersion}.{osvi.dwBuildNumber}";
            else
                return string.Empty;
        }

        private static unsafe string GetDarwinVersion()
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

            string kernelRelease = null;
            try
            {
                if (Libc.sysctl(name, 2, buf, len, IntPtr.Zero, 0) == 0 && *len < BUFFER_LENGTH)
                    kernelRelease = Marshal.PtrToStringAnsi((IntPtr)buf, (int)*len);
            }
            catch (Exception ex)
            {
                throw new PlatformNotSupportedException("Unable to retrieve macOS version.", ex);
            }

            if (kernelRelease == null)
                throw new PlatformNotSupportedException("Unknown error trying to read macOS version.");

            return !Version.TryParse(kernelRelease, out Version version) || version.Major < 5 ? "10.0" : $"10.{version.Major - 4}";
        }

        private static string GetFreeBSDVersion()
        {
            // This is same as sysctl kern.version
            // FreeBSD 11.0-RELEASE-p1 FreeBSD 11.0-RELEASE-p1 #0 r306420: Thu Sep 29 01:43:23 UTC 2016     root@releng2.nyi.freebsd.org:/usr/obj/usr/src/sys/GENERIC
            // What we want is major release as minor releases should be compatible.
            try
            {
                return RuntimeInformation.OSDescription.Split()[1].Split('.')[0]; // second token up to first dot
            }
            catch { }
            return string.Empty;
        }

        private static LinuxInfo GetLinuxInfo()
        {
            LinuxInfo result = null;

            // Sample os-release file:
            //   NAME="Ubuntu"
            //   VERSION = "14.04.3 LTS, Trusty Tahr"
            //   ID = ubuntu
            //   ID_LIKE = debian
            //   PRETTY_NAME = "Ubuntu 14.04.3 LTS"
            //   VERSION_ID = "14.04"
            //   HOME_URL = "http://www.ubuntu.com/"
            //   SUPPORT_URL = "http://help.ubuntu.com/"
            //   BUG_REPORT_URL = "http://bugs.launchpad.net/ubuntu/"
            // We use ID and VERSION_ID

            if (File.Exists("/etc/os-release"))
            {
                string[] lines = File.ReadAllLines("/etc/os-release");
                result = new LinuxInfo();
                foreach (string line in lines)
                {
                    if (line.StartsWith("ID=", StringComparison.Ordinal))
                        result.ID = line.Substring(3).Trim('"', '\'');
                    else if (line.StartsWith("VERSION_ID=", StringComparison.Ordinal))
                        result.Version = line.Substring(11).Trim('"', '\'');
                }
            }
            else if (File.Exists("/etc/redhat-release"))
            {
                string[] lines = File.ReadAllLines("/etc/redhat-release");

                if (lines.Length >= 1)
                {
                    string line = lines[0];
                    if (line.StartsWith("Red Hat Enterprise Linux Server release 6.") || line.StartsWith("CentOS release 6."))
                        result = new LinuxInfo("rhel", "6");
                }
            }

            if (result != null)
            {
                // For some distros, we don't want to use the full version from VERSION_ID. One example is
                // Red Hat Enterprise Linux, which includes a minor version in their VERSION_ID but minor
                // versions are backwards compatable.
                //
                // In this case, we'll normalized RIDs like 'rhel.7.2' and 'rhel.7.3' to a generic
                // 'rhel.7'. This brings RHEL in line with other distros like CentOS or Debian which
                // don't put minor version numbers in their VERSION_ID fields because all minor versions
                // are backwards compatible.

                // Handle if VersionId is null by just setting the index to -1.
                int lastVersionNumberSeparatorIndex = result.Version?.IndexOf('.') ?? -1;

                // For Alpine, the version reported has three components, so we need to find the second version separator
                if (lastVersionNumberSeparatorIndex != -1 && result.ID == "alpine")
                    lastVersionNumberSeparatorIndex = result.Version.IndexOf('.', lastVersionNumberSeparatorIndex + 1);

                if (lastVersionNumberSeparatorIndex != -1 && (result.ID == "rhel" || result.ID == "alpine"))
                    result.Version = result.Version.Substring(0, lastVersionNumberSeparatorIndex);
            }

            return result;
        }

        internal enum Platform
        {
            Unknown = 0,
            Windows = 1,
            Linux = 2,
            MacOS = 3,
            FreeBSD = 4
        }

        private sealed class LinuxInfo
        {
            public LinuxInfo(string id = null, string version = null)
            {
                ID = id;
                Version = version;
            }

            public string ID { get; set; }
            public string Version { get; set; }
        }
    }
}