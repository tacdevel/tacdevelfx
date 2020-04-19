/***********************************************************************************************************************
 * FileName:            PlatformOS.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System.Diagnostics.CodeAnalysis;

namespace TACDevel.Runtime
{
    /// <summary>
    /// Indicates the operating system type.
    /// </summary>
    [SuppressMessage("Naming", "CA1717:Only FlagsAttribute enums should have plural names")]
    public enum PlatformOS
    {
        /// <summary>
        /// An unknown operating system.
        /// </summary>
        Unknown = -1,

        /// <summary>
        /// A Microsoft Windows operating system.
        /// </summary>
        Windows,

        /// <summary>
        /// An Apple OSX/macOS operating system.
        /// </summary>
        MacOS,

        /// <summary>
        /// A Linux operating system.
        /// </summary>
        Linux,

        /// <summary>
        /// A FreeBSD operating system.
        /// </summary>
        FreeBSD,

        /// <summary>
        /// A Debian operating system.
        /// </summary>
        Debian,

        /// <summary>
        /// An Ubuntu operating system.
        /// </summary>
        Ubuntu,

        /// <summary>
        /// A Linux Mint operating system.
        /// </summary>
        LinuxMint,

        /// <summary>
        /// A Fedora operating system.
        /// </summary>
        Fedora,

        /// <summary>
        /// A Red Hat Enterprise Linux operating system.
        /// </summary>
        RHEL,

        /// <summary>
        /// An Oracle Linux operating system.
        /// </summary>
        OL,

        /// <summary>
        /// An openSUSE operating system.
        /// </summary>
        OpenSUSE,

        /// <summary>
        /// A SUSE Linux Entrprise Server operating system.
        /// </summary>
        SLES,

        /// <summary>
        /// A CentOS operating system.
        /// </summary>
        CentOS,

        /// <summary>
        /// An Alpine operating system.
        /// </summary>
        Alpine
    }
}