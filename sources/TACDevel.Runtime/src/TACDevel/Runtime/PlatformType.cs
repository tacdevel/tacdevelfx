/***********************************************************************************************************************
 * FileName:            PlatformType.cs
 * Copyright/License:   https://github.com/tacdevel/tacdevlibs/blob/master/LICENSE.md
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
        Windows,

        /// <summary>
        /// An Apple OSX/macOS platform.
        /// </summary>
        MacOS,

        /// <summary>
        /// A Linux platform.
        /// </summary>
        Linux, 

        /// <summary>
        /// A FreeBSD platform.
        /// </summary>
        FreeBSD,
    }
}