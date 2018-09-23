/****************************************************************************
 * FileName:   SafeHandleZeroIsInvalid.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180918
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCD.InteropServices
{
    /// <summary>
    /// Provides a base class for safe handle implementations in which the value of 0 indicates an invalid handle.
    /// </summary>
    public abstract class SafeHandleZeroIsInvalid : SafeHandle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref"SafeHandleZeroIsInvalid"/> class, specifying whether the handle is to be reliably released.
        /// </summary>
        /// <param name="ownsHandle"><see langword="true"/> to reliably release the handle during the finalization phase; <see langword="false"/> to prevent reliable release (not recommended).</param>
        protected SafeHandleZeroIsInvalid(bool ownsHandle) : base(IntPtr.Zero, ownsHandle) { }

        /// <summary>
        /// Gets a value that indicates whether the handle is invalid.
        /// </summary>
        public override bool IsInvalid => handle == IntPtr.Zero;

        public static implicit operator IntPtr(SafeHandleZeroIsInvalid safeHandle) => safeHandle.handle;
    }
}