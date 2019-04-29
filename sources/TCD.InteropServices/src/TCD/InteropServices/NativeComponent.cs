/***************************************************************************************************
 * FileName:             NativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.ComponentModel;
using TCD.Numerics.Hashing;

namespace TCD.InteropServices
{
    /// <summary>
    /// Provides the base implementation of the <see cref="INativeComponent{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of handle.</typeparam>
    public abstract class NativeComponent<T> : Component, INativeComponent<T>, IEquatable<NativeComponent<T>>
    {
        private T handle = default;

        /// <summary>
        /// Initializes a new instance if the <see cref="NativeComponent{T}"/> class.
        /// </summary>
        protected internal NativeComponent() : base() { }

        /// <summary>
        /// Initializes a new instance if the <see cref="NativeComponent{T}"/> class with an immutable name.
        /// </summary>
        /// <param name="name">The name of the new <see cref="NativeComponent{T}"/>.</param>
        protected NativeComponent(string name) : base(name) { }

        /// <inheritdoc />
        public T Handle
        {
            get => handle;
            protected internal set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                if (handle.Equals(value)) return;
                handle = value;
                OnPropertyChanged("Handle");
            }
        }

        /// <inheritdoc />
        public abstract override bool IsInvalid { get; }

        /// <inheritdoc />
        public bool Equals(NativeComponent<T> component) => Handle.Equals(component.Handle);

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is NativeComponent<T>) ? false : Equals((NativeComponent<T>)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(this.GenerateHashCode(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.ToString();
    }
}