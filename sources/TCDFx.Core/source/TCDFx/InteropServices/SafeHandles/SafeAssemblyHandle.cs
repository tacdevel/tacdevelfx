/***************************************************************************************************
 * FileName:             SafeAssemblyHandle.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Security;
using TCDFx.InteropServices;
using TCDFx.Native;

namespace TCDFx.SafeHandles
{
    /// <summary>
    /// Represents a wrapper class for a native assembly handle.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public sealed class SafeAssemblyHandle : SafeHandleZeroIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeAssemblyHandle"/> class.
        /// </summary>
        /// <param name="existingHandle"> An <see cref="IntPtr"/> object that represents the preexisting handle to use.</param>
        /// <param name="ownsHandle"><see langword="true"/> to reliably release the handle during the finalization phase; <see langword="false"/> to prevent reliable release (not recommended).</param>
        public SafeAssemblyHandle(IntPtr existingHandle, bool ownsHandle = true) : base(ownsHandle) => SetHandle(existingHandle);

        /// <summary>
        /// When overridden in a derived class, executes the code required to free the handle.
        /// </summary>
        /// <returns><see langword="true"/> if the handle is released successfully; otherwise, in the event of a catastrophic failure, <see langword="false"/>.</returns>
        protected override bool ReleaseHandle()
        {
            bool released;
            try
            {
                if (handle == IntPtr.Zero) throw new InvalidHandleException();

                switch (Platform.PlatformType)
                {
                    case PlatformType.Windows:
                        Kernel32.FreeLibrary(handle);
                        break;
                    case PlatformType.Linux:
                    case PlatformType.MacOS:
                    case PlatformType.FreeBSD:
                        Libdl.dlclose(handle);
                        break;
                    case PlatformType.Unknown:
                    default:
                        break;
                }
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