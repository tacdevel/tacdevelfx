/***************************************************************************************************
 * FileName:             SafeNativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace TCDFx.Runtime.InteropServices
{
    /// <summary>
    /// Provides the base implementation of a native component with a <see cref="SafeHandle"/> of the specified type as a handle.
    /// </summary>
    /// <typeparam name="T">They type of <see cref="SafeHandle"/>.</typeparam>
    public abstract class SafeNativeComponent<T> : NativeComponent<T>, INativeComponent<T>, IEquatable<SafeNativeComponent<T>>
        where T : SafeHandle
    {
        /// <summary>
        /// Initializes a new instance if the <see cref="SafeNativeComponent{T}"/> class with the specified handle.
        /// </summary>
        protected internal SafeNativeComponent() : base() { }

        /// <summary>
        /// Gets a value indicating whether this component is invalid.
        /// </summary>
        /// <value><c>true</c> if this component is invalid; otherwise, <c>false</c>.</value>
        public override bool IsInvalid => Handle.IsClosed || Handle.IsInvalid;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public bool Equals(SafeNativeComponent<T> other) => Handle == other.Handle;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public override bool Equals(object obj) => !(obj is SafeNativeComponent<T>) ? false : Equals((SafeNativeComponent<T>)obj);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => unchecked(HashCode.Combine(Handle));

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Handle.DangerousGetHandle().ToInt64().ToString(CultureInfo.InvariantCulture);

        /// <summary>
        /// Performs tasks associated with freeing, releasing, or resetting managed resources.
        /// </summary>
        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid)
                Handle.Dispose();
            base.ReleaseManagedResources();
        }
    }
}