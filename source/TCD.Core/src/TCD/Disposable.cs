/****************************************************************************
 * FileName:   Disposable.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180913
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;

namespace TCD
{
    public abstract class Disposable : IDisposable
    {
        public bool IsDisposed { get; private set; } = false;

        protected abstract void ReleaseUnmanagedResources();
        protected abstract void ReleaseManagedResources();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                    ReleaseManagedResources();
                ReleaseUnmanagedResources();
                IsDisposed = true;
            }
        }
    }
}