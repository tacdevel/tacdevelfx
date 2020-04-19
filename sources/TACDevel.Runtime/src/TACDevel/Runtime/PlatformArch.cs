/***********************************************************************************************************************
 * FileName:            PlatformArch.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

namespace TACDevel.Runtime
{
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
        X86 = 0,

        /// <summary>
        /// An Intel-based 64-bit processor architecture.
        /// </summary>
        X64 = 1,

        /// <summary>
        /// A 32-bit ARM processor architecture.
        /// </summary>
        ARM = 2,

        /// <summary>
        /// A 64-bit ARM processor architecture.
        /// </summary>
        ARM64 = 3
    }
}