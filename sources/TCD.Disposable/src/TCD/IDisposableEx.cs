/***************************************************************************************************
 * FileName:             IDisposableEx.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD
{
    /// <summary>
    /// Provides functionality required by all disposable objects.
    /// </summary>
    public interface IDisposableEx : IDisposable
    {
        /// <summary>
        /// Occurs when this disposable object is about to be disposed.
        /// </summary>
        event EventHandler Disposing;

        /// <summary>
        /// Occurs when this disposable object is finished disposing.
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// Gets a value indicating whether this disposable object is preparing to dispose.
        /// </summary>
        /// <value><c>true</c> if the disposable object is preparing to dispose (or currently disposing); otherwise, <c>false</c>.</value>
        bool IsDisposing { get; }

        /// <summary>
        /// Gets a value indicating whether this disposable object is disposed.
        /// </summary>
        /// <value><c>true</c> if the disposable object is disposed; otherwise, <c>false</c>.</value>
        bool IsDisposed { get; }

        /// <summary>
        /// Safely performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources, invoking the specified action in the event of an exception.
        /// </summary>
        /// <param name="exceptionHandler">The action to be invoked in the event of an exception.</param>
        /// <returns><c>true</c> if properly disposed; otherwise, <c>false</c>.</returns>
        bool SafeDispose(Action<Exception> exceptionHandler = null);
    }
}