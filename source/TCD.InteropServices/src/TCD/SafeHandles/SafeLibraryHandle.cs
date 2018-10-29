/****************************************************************************
 * FileName:   SafeLibraryHandle.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180918
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;

namespace TCD.SafeHandles
{
    /// <summary>
    /// Represents a wrapper class for a native assembly handle.
    /// </summary>
    public sealed class SafeLibraryHandle : SafeHandleZeroIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeLibraryHandle"/> class.
        /// </summary>
        /// <param name="existingHandle"> An <see cref="IntPtr"/> object that represents the pre-existing handle to use.</param>
        /// <param name="ownsHandle"><see langword="true"/> to reliably release the handle during the finalization phase; <see langword="false"/> to prevent reliable release (not recommended).</param>
        public SafeLibraryHandle(IntPtr existingHandle, bool ownsHandle = true) : base(ownsHandle) => SetHandle(existingHandle);

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

                switch (PlatformHelper.CurrentPlatform)
                {
                    case PlatformHelper.Platform.Windows:
                        Kernel32.FreeLibrary(handle);
                        break;
                    case PlatformHelper.Platform.Linux:
                    case PlatformHelper.Platform.MacOS:
                    case PlatformHelper.Platform.FreeBSD:
                        Libdl.dlclose(handle);
                        break;
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