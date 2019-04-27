/***************************************************************************************************
 * FileName:             Component.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.ComponentModel;

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides the base implementation for the <see cref="IComponent"/> interface.
    /// </summary>
    public abstract class Component : Disposable, IComponent, INotifyPropertyChanged
    {
        private string name;

        /// <summary>
        /// Initializes a new instance if the <see cref="Component"/> class with a mutable name.
        /// </summary>
        protected internal Component() => IsNameImmutable = false;

        /// <summary>
        /// Initializes a new instance if the <see cref="Component"/> class with a mutable name.
        /// </summary>
        /// <param name="name">The name of the new <see cref="Component"/>.</param>
        protected Component(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                this.name = name;
                IsNameImmutable = true;
            }
        }

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets a value determining if <see cref="Name"/> is immutable.
        /// </summary>
        /// <value><c>true</c> if <see cref="Name"/> is immutable; otherwise, <c>false</c>.</value>
        public bool IsNameImmutable { get; }

        /// <inheritdoc />
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (IsNameImmutable)
                    throw new ArgumentException("Name property is immutable for this instance", "value");
                if (name == value) return;
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <inheritdoc />
        public virtual bool IsInvalid => IsDisposing || IsDisposed;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Initializes this <see cref="Component"/>.
        /// </summary>
        protected virtual void InitializeComponent() { }

        /// <summary>
        /// Initializes this <see cref="Component"/> object's events.
        /// </summary>
        protected virtual void InitializeEvents() { }
    }
}