/***************************************************************************************************
 * FileName:             NativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCDFx.ComponentModel;
using TCDFx.Resources;

namespace TCDFx.Runtime.InteropServices
{
    /// <summary>
    /// Provides the base implementation of the <see cref="INativeComponent{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of handle.</typeparam>
    public abstract class NativeComponent<T> : Component, IEquatable<NativeComponent<T>>, INativeComponent<T>
    {
        private T handle = default;

        /// <summary>
        /// Initializes a new instance if the <see cref="NativeComponent{T}"/> class.
        /// </summary>
        protected internal NativeComponent() : base() { }

        /// <summary>
        /// Gets a value determining if <see cref="Handle"/> is immutable.
        /// </summary>
        /// <value><c>true</c> if <see cref="Handle"/> is immutable; otherwise, <c>false</c>.</value>
        public bool IsHandleImmutable { get; private set; }

        /// <inheritdoc />
        public T Handle
        {
            get => handle;
            protected internal set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), Strings.NativeComponentSetHandleNull);
                if (IsHandleImmutable)
                    throw new ArgumentException(Strings.NativeComponentSetHandleImmutable, nameof(value));

                OnPropertyChanging(nameof(Handle));
                if (handle == null || !handle.Equals(value))
                    handle = value;
                IsHandleImmutable = true;
                OnPropertyChanged(nameof(Handle));
            }
        }

        /// <inheritdoc />
        public abstract override bool IsInvalid { get; }

        /// <inheritdoc />
        public bool Equals(NativeComponent<T> component) => Handle.Equals(component.Handle);

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is NativeComponent<T>) ? false : Equals((NativeComponent<T>)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(HashCode.Combine(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.ToString();
    }
}