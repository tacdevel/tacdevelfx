/***************************************************************************************************
 * FileName:             IntNativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

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

        /// <inheritdoc />
        public bool Equals(IntNativeComponent component) => Handle == component.Handle;

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is IntNativeComponent) ? false : Equals((IntNativeComponent)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(HashCode.Combine(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.ToInt64().ToString();
    }
}