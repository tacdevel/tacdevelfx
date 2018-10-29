/****************************************************************************
 * FileName:   Disposable.cs
 * Assembly:   TCD.Disposable.dll
 * Package:    TCD.Disposable
 * Date:       20180913
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 ***************************************************************************/

using System;

namespace TCD
{
    /// <summary>
    /// Provides a base implemetation for the <see cref="IDisposable"/> interface.
    /// </summary>
    public abstract class Disposable : IDisposable
    {
        private bool disposed = false;

        /// <summary>
        /// Occurs when the object is disposed.
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Disposable"/> has been disposed.
        /// </summary>
        public bool IsDisposed
        {
            get => disposed;
            private set
            {
                disposed = value;
                if (value == true)
                    OnDisposed();
            }
        }

        /// <summary>
        /// When overriden in a derived class, performs tasks associated with releasing unmanaged resources.
        /// </summary>
        protected abstract void ReleaseUnmanagedResources();

        /// <summary>
        /// When overriden in a derived class, performs tasks associated with releasing managed resources.
        /// </summary>
        protected abstract void ReleaseManagedResources();

        /// <summary>
        /// Raises the <see cref="Disposed"/> event.
        /// </summary>
        public virtual void OnDisposed() => Disposed?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool SafeDispose(Action<Exception> exceptionHandler = null)
        {
            if (this == null)
                return false;

            try
            {
                Dispose();
                return true;
            }
            catch (ObjectDisposedException)
            {
                return true; // Already disposed.
            }
            catch (Exception ex)
            {
                exceptionHandler?.Invoke(ex);
                return false;
            }
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