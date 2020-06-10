/***********************************************************************************************************************
 * FileName:             NativeObject.cs
 * Copyright:            Copyright © 2017-2020 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tacdevlibs/blob/master/LICENSE.md
 **********************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using TACDevel.Runtime.Resources;

namespace TACDevel.Runtime.InteropServices
{
    /// <summary>
    /// Represents the base class for a native object.
    /// </summary>
    public abstract class NativeObject : Disposable, INativeObject
    {
        /// <summary>
        /// Initializes a new instance if the <see cref="NativeObject"/> class.
        /// </summary>
        protected internal NativeObject() : base() { }

        /// <summary>
        /// Gets a value indicating whether this native object is invalid.
        /// </summary>
        /// <value><c>true</c> if this object is invalid; otherwise, <c>false</c>.</value>
        public abstract bool IsInvalid { get; }
    }

    /// <summary>
    /// Provides the base implementation of the <see cref="INativeObject{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of handle.</typeparam>
    public abstract class NativeObject<T> : NativeObject, IEquatable<NativeObject<T>>, INativeObject<T>
        where T : unmanaged
    {
        private T handle = default;

        /// <summary>
        /// Initializes a new instance if the <see cref="NativeObject{T}"/> class.
        /// </summary>
        protected internal NativeObject() : base() { }

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
                if (value.Equals(IntPtr.Zero))
                    throw new ArgumentNullException(nameof(value), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(value)));
                if (IsHandleImmutable)
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ObjectIsImmutable, nameof(value)), nameof(value));

                if (handle.Equals(IntPtr.Zero) || !handle.Equals(value))
                    handle = value;
                IsHandleImmutable = true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this component is invalid.
        /// </summary>
        /// <value><c>true</c> if this component is invalid; otherwise, <c>false</c>.</value>
        public override bool IsInvalid => EqualityComparer<T>.Default.Equals(Handle, default);


        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.</returns>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public bool Equals(NativeObject<T> other) => EqualityComparer<T>.Default.Equals(Handle, other.Handle);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) => !(obj is NativeObject<T>) ? false : Equals((NativeObject<T>)obj);

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