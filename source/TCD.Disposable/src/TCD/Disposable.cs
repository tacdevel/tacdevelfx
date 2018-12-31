/***************************************************************************************************
 * FileName:             Disposable.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
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
        /// <summary>
        /// Occurs when this object is about to be disposed.
        /// </summary>
        public event EventHandler Disposing;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Disposable"/> has been disposed.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

       /// <summary>
       /// When overridden in a derived class, performs tasks associated with releasing unmanaged resources.
        /// </summary>
        protected abstract void ReleaseUnmanagedResources();

        /// <summary>
        /// When overridden in a derived class, performs tasks associated with releasing managed resources.
        /// </summary>
        protected abstract void ReleaseManagedResources();

        /// <summary>
        /// Raises the <see cref="Disposing"/> event.
        /// </summary>
        public virtual void OnDisposing() => Disposing?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
       public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Safely disposes of this <see cref="Disposable"/> instance, performing the specified action in the event of an exception.
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
                OnDisposing();
                if (disposing)
                    ReleaseManagedResources();
                ReleaseUnmanagedResources();
                IsDisposed = true;
            }
        }
   }
}