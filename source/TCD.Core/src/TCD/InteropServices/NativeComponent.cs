/****************************************************************************
 * FileName:   NativeComponent.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180918
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TCD.InteropServices
{
    public abstract class NativeComponent<T> : Disposable, IEquatable<NativeComponent<T>>
        where T : SafeHandle
    {
        private static Dictionary<NativeComponent<T>, T> handleCache = new Dictionary<NativeComponent<T>, T>();

        public T Handle
        {
            get => handleCache[this];
            protected internal set
            {
                if (handleCache.ContainsValue(value) || handleCache.ContainsKey(this))
                    throw new DuplicateHandleException();

                if (value.IsInvalid || value.IsClosed)
                    throw new InvalidHandleException();

                handleCache.Add(this, value);
            }
        }

        public bool IsInvalid => Handle.IsInvalid || Handle.IsClosed;

        public override bool Equals(object obj)
        {
            if (!(obj is NativeComponent<T>))
                return false;
            return Equals((NativeComponent<T>)obj);
        }

        public bool Equals(NativeComponent<T> component) => handleCache[this] == handleCache[component];

        public override int GetHashCode() => unchecked(this.GenerateHashCode());

        public override string ToString() => handleCache[this].DangerousGetHandle().ToString();

        protected virtual void InitializeComponent() { }

        protected virtual void InitializeEvents() { }

        protected override void ReleaseUnmanagedResources() { }

        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid)
            {
                handleCache[this].Dispose();
                handleCache.Remove(this);
            }
        }
    }
}