/***************************************************************************************************
 * FileName:             NativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
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
        /// Initializes a new instance if the <see cref="IntNativeComponent"/> class with the specified handle and immutable name.
        /// </summary>
        /// <param name="name">The name of the new <see cref="IntNativeComponent"/>.</param>
        protected IntNativeComponent(string name) : base(name) { }

        /// <inheritdoc />
        public bool Equals(IntNativeComponent component) => Handle == component.Handle;

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is IntNativeComponent) ? false : Equals((IntNativeComponent)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(this.GenerateHashCode(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.ToInt64().ToString();
    }

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

        /// <summary>
        /// Initializes a new instance if the <see cref="UIntNativeComponent"/> class with the specified handle and immutable name.
        /// </summary>
        /// <param name="name">The name of the new <see cref="UIntNativeComponent"/>.</param>
        protected UIntNativeComponent(string name) : base(name) { }

        /// <inheritdoc />
        public bool Equals(UIntNativeComponent component) => Handle == component.Handle;

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is UIntNativeComponent) ? false : Equals((UIntNativeComponent)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(this.GenerateHashCode(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.ToUInt64().ToString();
    }

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
        /// Initializes a new instance if the <see cref="SafeNativeComponent{T}"/> class with the specified handle and immutable name.
        /// </summary>
        /// <param name="name">The name of the new <see cref="SafeNativeComponent{T}"/>.</param>
        protected SafeNativeComponent(string name) : base(name) { }

        /// <inheritdoc />
        public override bool IsInvalid => Handle.IsClosed || Handle.IsInvalid;

        /// <inheritdoc />
        public bool Equals(SafeNativeComponent<T> component) => Handle == component.Handle;

        /// <inheritdoc />
        public override bool Equals(object obj) => !(obj is SafeNativeComponent<T>) ? false : Equals((SafeNativeComponent<T>)obj);

        /// <inheritdoc />
        public override int GetHashCode() => unchecked(this.GenerateHashCode(Handle));

        /// <inheritdoc />
        public override string ToString() => Handle.DangerousGetHandle().ToInt64().ToString();

        /// <inheritdoc />
        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid)
                Handle.Dispose();
        }
    }
}