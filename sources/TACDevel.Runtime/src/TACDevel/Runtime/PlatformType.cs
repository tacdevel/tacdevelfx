/***********************************************************************************************************************
 * FileName:            PlatformType.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

namespace TACDevel.Runtime
{
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
}