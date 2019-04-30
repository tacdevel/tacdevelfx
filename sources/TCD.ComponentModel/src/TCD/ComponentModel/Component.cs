/***************************************************************************************************
 * FileName:             Component.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.Collections;

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides the base implementation for the <see cref="IComponent"/> interface.
    /// </summary>
    public abstract class Component : IComponent, IDisposable, INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string name;
        internal static readonly MultiValueDictionary<string, Type, Component> Cache = new MultiValueDictionary<string, Type, Component>();

        /// <summary>
        /// Initializes a new instance if the <see cref="Component"/> class with a mutable name.
        /// </summary>
        protected internal Component()
        {
            IsNameImmutable = false;
            InitializeComponent();
        }

        ~Component() => Dispose(false);

        /// <inheritdoc />
        public event DisposingEventHandler Disposing;

        /// <inheritdoc />
        public event DisposedEventHandler Disposed;

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Gets a value determining if <see cref="Name"/> is immutable.
        /// </summary>
        /// <value><c>true</c> if <see cref="Name"/> is immutable; otherwise, <c>false</c>.</value>
        public bool IsNameImmutable { get; private set; }

        /// <inheritdoc />
        public string Name
        {
            get => name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (IsNameImmutable) throw new ArgumentException("Name property has already been set.", nameof(value));
                if (Cache.ContainsKey(value)) throw new DuplicateComponentException($"The component '{value}' has already been created.");

                if (name != value)
                    name = value;
                IsNameImmutable = true;
                Cache.Add(name, GetType(), this);
            }
        }

        /// <inheritdoc />
        public abstract bool IsInvalid { get; }

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            OnDisposing();
            Dispose(true);
            GC.SuppressFinalize(this);
            OnDisposed();
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
        /// Initializes this <see cref="Component"/>.
        /// </summary>
        protected abstract void InitializeComponent();

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting managed resources.
        /// </summary>
        protected virtual void ReleaseManagedResources()
        {
            if (!IsInvalid)
                if (Cache.ContainsKey(Name))
                    Cache.Remove(Name);
        }

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        protected virtual void ReleaseUnmanagedResources() { }

        /// <summary>
        /// Raises the <see cref="Disposing"/> event.
        /// </summary>
        protected virtual void OnDisposing() => Disposing?.Invoke(this, new DisposingEventArgs(Name));

        /// <summary>
        /// Raises the <see cref="Disposed"/> event.
        /// </summary>
        protected virtual void OnDisposed() => Disposed?.Invoke(this, new DisposedEventArgs(Name));

        private void Dispose(bool disposing)
        {
            if (!IsInvalid)
            {
                OnDisposing();
                if (disposing)
                    ReleaseManagedResources();
                ReleaseUnmanagedResources();
                OnDisposed();
            }
        }
    }
}