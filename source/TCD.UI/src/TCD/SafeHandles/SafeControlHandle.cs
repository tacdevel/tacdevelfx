/***************************************************************************************************
 * FileName:             SafeControlHandle.cs
 * Date:                 20180921
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;

namespace TCD.SafeHandles
{
    /// <summary>
    /// Represents a wrapper class for a UI control handle.
    /// </summary>
    public sealed class SafeControlHandle : SafeHandleZeroIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeControlHandle"/> class.
        /// </summary>
        /// <param name="existingHandle"> An <see cref="IntPtr"/> object that represents the preexisting handle to use.</param>
        /// <param name="ownsHandle"><see langword="true"/> to reliably release the handle during the finalization phase; <see langword="false"/> to prevent reliable release (not recommended).</param>
        public SafeControlHandle(IntPtr existingHandle, bool ownsHandle = true) : base(ownsHandle) => SetHandle(existingHandle);

        /// <summary>
        /// Executes the code required to free the handle.
        /// </summary>
        /// <returns><see langword="true"/> if the handle is released successfully; otherwise, in the event of a catastrophic failure, <see langword="false"/>.</returns>
        protected override bool ReleaseHandle()
        {
            bool released;
            try
            {
                Libui.ControlDestroy(this);
                handle = IntPtr.Zero;
                released = true;
            }
            catch
            {
                released = false;
            }
            return released;
        }
    }
}