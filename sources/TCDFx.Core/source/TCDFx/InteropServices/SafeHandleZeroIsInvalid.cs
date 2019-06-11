/***************************************************************************************************
 * FileName:             SafeHandleZeroIsInvalid.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCDFx.InteropServices
{
    /// <summary>
    /// Provides a base class for safe handle implementations in which the value of <see cref="IntPtr.Zero"/> indicates an invalid handle.
    /// </summary>
    public abstract class SafeHandleZeroIsInvalid : SafeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeHandleZeroIsInvalid"/> class, specifying whether the handle is to be reliably released.
        /// </summary>
        /// <param name="ownsHandle"><see langword="true"/> to reliably release the handle during the finalization phase; <see langword="false"/> to prevent reliable release (not recommended).</param>
        protected SafeHandleZeroIsInvalid(bool ownsHandle) : base(IntPtr.Zero, ownsHandle) { }

        /// <summary>
        /// Gets a value that indicates whether the handle is invalid.
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;

        /// <summary>
        /// Converts the specified <see cref="SafeHandleZeroIsInvalid"/> structure to a <see cref="IntPtr"/> structure.
        /// </summary>
        /// <param name="safeHandle">The <see cref="SafeHandleZeroIsInvalid"/> to be converted.</param>
        /// <returns>The <see cref="IntPtr"/> that results from the conversion.</returns>
        public static implicit operator IntPtr(SafeHandleZeroIsInvalid safeHandle) => safeHandle.handle;
    }
}