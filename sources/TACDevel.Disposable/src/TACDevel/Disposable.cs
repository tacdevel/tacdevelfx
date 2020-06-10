/***********************************************************************************************************************
 * FileName:            Disposable.cs
 * Copyright/License:   https://github.com/tacdevel/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Diagnostics.CodeAnalysis;

namespace TACDevel
{
    /// <summary>
    /// Provides a base implementation for the <see cref="IDisposableEx"/> interface.
    /// </summary>
    public abstract class Disposable : IDisposableEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Disposable"/> class.
        /// </summary>
        protected Disposable() => IsDisposed = false;

        /// <summary>
        /// Trys to free resources and perform other cleanup operations before being reclaimed by garbage collection.
        /// </summary>
        ~Disposable() => Dispose(false);

        /// <summary>
        /// Occurs when an object is disposing.
        /// </summary>
        public event GenericEventHandler<Disposable, EventArgs> Disposing;

        /// <summary>
        /// Occurs when an object is disposed.
        /// </summary>
        public event GenericEventHandler<Disposable, EventArgs> Disposed;

        /// <summary>
        /// Determines whether this object is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
        /// Performs tasks associated with freeing, releasing, or resetting managed resources.
        /// </summary>
        protected virtual void ReleaseManagedResources() { }

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        protected virtual void ReleaseUnmanagedResources() { }

        //TODO: Add justification to SuppressMessage:CA1063
        [SuppressMessage("Design", "CA1063:Implement IDisposable Correctly")]
        private void Dispose(bool disposing)
        {
            OnDisposing();
            if (disposing)
                ReleaseManagedResources();
            ReleaseUnmanagedResources();
            IsDisposed = true;
            OnDisposed();
        }
    }
}