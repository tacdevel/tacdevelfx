/***************************************************************************************************
 * FileName:             UIntNativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCDFx.Numerics.Hashing;

namespace TCDFx.InteropServices
{
    /// <summary>
    /// Provides the base implementation of a native component with an <see cref="UIntPtr"/> as a handle.
    /// </summary>
    [CLSCompliant(false)]
    public abstract class UIntNativeComponent : NativeComponent<UIntPtr>, INativeComponent<UIntPtr>, IEquatable<UIntNativeComponent>
    {
        /// <summary>
        /// Initializes a new instance if the <see cref="UIntNativeComponent"/> class with the specified handle.
        /// </summary>
        protected internal UIntNativeComponent() : base() { }

        /// <inheritdoc />
        public bool Equals(UIntNativeComponent component) => Handle == component.Handle;

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is UIntNativeComponent) ? false : Equals((UIntNativeComponent)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(this.GenerateHashCode(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.ToUInt64().ToString();
    }
}