/***************************************************************************************************
 * FileName:             Disposable.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD
{
    /// <summary>
    /// Provides a base implementation for the <see cref="IDisposableEx"/> interface.
    /// </summary>
    public abstract class Disposable : IDisposableEx
    {
        ~Disposable() => Dispose(false);

        /// <inheritdoc />
        public event DisposingEventHandler Disposing;

        /// <inheritdoc />
        public event DisposedEventHandler Disposed;

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
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

        /// <summary>
        /// Raises the <see cref="Disposing"/> event.
        /// </summary>
        protected virtual void OnDisposing(Disposable sender) => Disposing?.Invoke(sender ?? (this), EventArgs.Empty);

        /// <summary>
        /// Raises the <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed(Disposable sender) => Disposed?.Invoke(sender ?? (this), EventArgs.Empty);

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting managed resources.
        /// </summary>
        protected virtual void ReleaseManagedResources() { }

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        protected virtual void ReleaseUnmanagedResources() { }

        private void Dispose(bool disposing)
        {
            OnDisposing(null);
            if (disposing)
                ReleaseManagedResources();
            ReleaseUnmanagedResources();
            OnDisposed(null);
        }

    }
}