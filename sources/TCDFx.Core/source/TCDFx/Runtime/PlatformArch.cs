/***************************************************************************************************
 * FileName:             PlatformArch.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCDFx.Runtime
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
}