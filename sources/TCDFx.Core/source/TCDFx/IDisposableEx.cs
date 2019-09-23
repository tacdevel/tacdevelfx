/***************************************************************************************************
 * FileName:             IDisposableEx.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCDFx
{
    /// <summary>
    /// Provides added functionality on top of the <see cref="IDisposable"/> interface.
    /// </summary>
    public interface IDisposableEx : IDisposable
    {
        /// <summary>
        /// Occurs when an object is disposing.
        /// </summary>
        event EventHandler<Disposable, EventArgs> Disposing;

        /// <summary>
        /// Occurs when an object is disposed.
        /// </summary>
        event EventHandler<Disposable, EventArgs> Disposed;

        /// <summary>
        /// Determines whether this object is disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Safely performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources, invoking the specified action in the event of an exception.
        /// </summary>
        /// <param name="exceptionHandler">The action to be invoked in the event of an exception.</param>
        /// <returns><c>true</c> if properly disposed; otherwise, <c>false</c>.</returns>
        bool SafeDispose(Action<Exception> exceptionHandler = null)
        {
            if (this == null)
                return true; // Not initialized, so, already disposed.

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
        /// Performs tasks associated with freeing, releasing, or resetting managed resources.
        /// </summary>
        void ReleaseManagedResources() { }

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void ReleaseUnmanagedResources() { }

    }
}