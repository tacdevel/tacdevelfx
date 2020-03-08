/***********************************************************************************************************************
 * FileName:            PlatformInfo.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;

namespace TACDevel.Runtime
{
    /// <summary>
    /// Contains information about a platform.
    /// </summary>
    public readonly struct PlatformInfo
    {
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
}