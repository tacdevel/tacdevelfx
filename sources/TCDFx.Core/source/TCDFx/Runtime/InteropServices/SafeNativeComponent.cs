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

        /// <inheritdoc />
        public override bool IsInvalid => Handle.IsClosed || Handle.IsInvalid;

        /// <inheritdoc />
        public bool Equals(SafeNativeComponent<T> component) => Handle == component.Handle;

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is SafeNativeComponent<T>) ? false : Equals((SafeNativeComponent<T>)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(HashCode.Combine(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.DangerousGetHandle().ToInt64().ToString(CultureInfo.InvariantCulture);

        /// <inheritdoc />
        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid)
                Handle.Dispose();
            base.ReleaseManagedResources();
        }
    }
}