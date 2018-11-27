/***************************************************************************************************
 * FileName:             Disposable.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD
{
    /// <summary>
    /// Provides a base implementation for the <see cref="IDisposable"/> interface.
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
        /// When overridden in a derived class, performs tasks associated with releasing unmanaged resources.
        /// </summary>
        protected abstract void ReleaseUnmanagedResources();

        /// <summary>
        /// When overridden in a derived class, performs tasks associated with releasing managed resources.
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

        /// <summary>
        /// Safely disposes of this <see cref="Disposable"/>, performing the specified action in the event of an exception.
        /// </summary>
        /// <param name="exceptionHandler">The action to be performed on an exception.</param>
        /// <returns><c>true</c> if properly disposed; otherwise, <c>false</c>.</returns>
        public bool SafeDispose(Action<Exception> exceptionHandler = null)
        {
            if (this == null) return true; // Not initialized, so, already disposed.

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