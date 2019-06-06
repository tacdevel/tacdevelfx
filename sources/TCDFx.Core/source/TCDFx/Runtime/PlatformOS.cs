/***************************************************************************************************
 * FileName:             PlatformHelper.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCDFx
{
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