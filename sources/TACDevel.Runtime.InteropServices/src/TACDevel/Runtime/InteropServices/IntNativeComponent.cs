/***************************************************************************************************
 * FileName:             IntNativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Globalization;

namespace TCDFx.Runtime.InteropServices
{
    /// <summary>
    /// Provides the base implementation of a native component with an <see cref="IntPtr"/> as a handle.
    /// </summary>
    public abstract class IntNativeComponent : NativeComponent<IntPtr>, INativeComponent<IntPtr>, IEquatable<IntNativeComponent>
    {
        /// <summary>
        /// Initializes a new instance if the <see cref="IntNativeComponent"/> class with the specified handle.
        /// </summary>
        protected internal IntNativeComponent() : base() { }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public bool Equals(IntNativeComponent other) => Handle == other.Handle;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public override bool Equals(object obj) => !(obj is IntNativeComponent) ? false : Equals((IntNativeComponent)obj);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => unchecked(HashCode.Combine(Handle));

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => Handle.ToInt64().ToString(CultureInfo.InvariantCulture);
    }
}