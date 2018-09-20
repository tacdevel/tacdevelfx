using System;
using TCD.InteropServices;
using TCD.Native;

namespace TCD.SafeHandles
{
    public sealed class SafeLibraryHandle : SafeHandleZeroIsInvalid
    {
        public SafeLibraryHandle(IntPtr existingHandle, bool ownsHandle = true) : base(ownsHandle) => SetHandle(existingHandle);

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