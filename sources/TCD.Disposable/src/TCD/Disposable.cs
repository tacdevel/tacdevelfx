/***************************************************************************************************
 * FileName:             Disposable.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD
{
    /// <summary>
    /// Provides the base implementation for the <see cref="IDisposableEx"/> interface.
    /// </summary>
    public abstract class Disposable : IDisposableEx
    {
        ~Disposable() => Dispose(false);

        /// <inheritdoc />
        public event EventHandler Disposing;

        /// <inheritdoc />
        public event EventHandler Disposed;

        /// <inheritdoc />
        public bool IsDisposing { get; private set; } = false;

        /// <inheritdoc />
        public bool IsDisposed { get; private set; } = false;

        /// <summary>
        /// Performs  tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            IsDisposing = true;
            OnDisposing();
            Dispose(true);
            GC.SuppressFinalize(this);
            IsDisposing = false;
            IsDisposed = true;
            OnDisposed();
        }

        /// <inheritdoc />
        public bool SafeDispose(Action<Exception> exceptionHandler = null)
        {
            if (this == null) return true; // Not initialized, so, already disposed.

            IsDisposing = true;
            OnDisposing();
            try
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                IsDisposing = false;
                IsDisposed = true;
                OnDisposed();
                return true;
            }
            catch (ObjectDisposedException)
            {
                IsDisposing = false;
                return true; // Already disposed.
            }
            catch (Exception ex)
            {
                exceptionHandler?.Invoke(ex);
                IsDisposing = false;
                return false;
            }
        }

        /// <summary>
        /// Raises the <see cref="Disposing"/> event.
        /// </summary>
        protected virtual void OnDisposing() => Disposing?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Raises the <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed() => Disposed?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// When overridden in a derived class, performs tasks associated with freeing, releasing, or resetting managed resources.
        /// </summary>
        protected virtual void ReleaseManagedResources() { }

        /// <summary>
        /// When overridden in a derived class, performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        protected virtual void ReleaseUnmanagedResources() { }

        private void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                OnDisposing();
                IsDisposing = true;
                if (disposing)
                    ReleaseManagedResources();
                ReleaseUnmanagedResources();
                IsDisposing = false;
                IsDisposed = true;
                OnDisposed();
            }
        }
    }
}