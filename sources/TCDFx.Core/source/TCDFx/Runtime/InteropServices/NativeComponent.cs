/***************************************************************************************************
 * FileName:             NativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Globalization;
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

        /// <summary>
        /// Gets the native handle representing this native component.
        /// </summary>
        public T Handle
        {
            get => handle;
            protected internal set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(value)));
                if (IsHandleImmutable)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ObjectIsImmutable, nameof(value)), nameof(value));

                OnPropertyChanging(nameof(Handle));
                if (handle == null || !handle.Equals(value))
                    handle = value;
                IsHandleImmutable = true;
                OnPropertyChanged(nameof(Handle));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this component is invalid.
        /// </summary>
        /// <value><c>true</c> if this component is invalid; otherwise, <c>false</c>.</value>
        public abstract override bool IsInvalid { get; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public bool Equals(NativeComponent<T> other) => Handle.Equals(other.Handle);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public override bool Equals(object obj) => !(obj is NativeComponent<T>) ? false : Equals((NativeComponent<T>)obj);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => unchecked(HashCode.Combine(Handle));

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Handle.ToString();
    }
}