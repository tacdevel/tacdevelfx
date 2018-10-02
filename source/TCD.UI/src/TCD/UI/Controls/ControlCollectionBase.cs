/****************************************************************************
 * FileName:   ControlCollectionBase.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using TCD.InteropServices;

namespace TCD.UI.Controls
{
    /// <summary>
    /// Provides the base implementation for collections of <see cref="Control"/> objects.
    /// </summary>
    /// <typeparam name="TControl">The type of <see cref="Control"/>.</typeparam>
    public abstract class ControlCollectionBase<TControl> : IList, IList<TControl>, ICollection, ICollection<TControl>, IEnumerable, IEnumerable<TControl>
        where TControl : Control
    {
        private readonly int defaultCapacity = 4;
        private readonly int growFactor = 2;
        private bool isReadOnly = false;
        private TControl[] innerArray;
        private int size;

        internal ControlCollectionBase(MultiChildContainer<TControl, ControlCollectionBase<TControl>> owner, int defaultCapacity = 4, int growFactor = 2)
        {
            Owner = owner;
            this.defaultCapacity = defaultCapacity;
            this.growFactor = growFactor;
        }

        internal MultiChildContainer<TControl, ControlCollectionBase<TControl>> Owner { get; }

        //TODO: Implement this.
        /// <summary>
        /// Gets the number of elements contained in the <see cref="ControlCollectionBase"/>.
        /// </summary>
        public int Count => size + 1;

        /// <summary>
        /// Gets a value indicating whether the <see cref="ControlCollectionBase"/> is read-only.
        /// </summary>
        public bool IsReadOnly => isReadOnly == false;

        /// <summary>
        /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollectionBase"/>.
        /// </summary>
        /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollectionBase"/>.</param>
        public virtual void Add(TControl child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (child.IsInvalid) throw new InvalidHandleException();
            if (child.TopLevel) throw new ArgumentException("Cannot add a top-level control to a ControlCollectionBase.");
            if (isReadOnly) throw new InvalidOperationException("Cannot remove items while the collection is read-only.");
            if (Contains(child)) throw new InvalidOperationException("Cannot add the same control more than once.");

            if (innerArray == null)
                innerArray = new TControl[defaultCapacity];
            else if (size >= innerArray.Length)
            {
                TControl[] array = new TControl[innerArray.Length * growFactor];
                Array.Copy(innerArray, array, innerArray.Length);
                innerArray = array;
            }

            child.Index = size;
            child.Parent = Owner;
            innerArray[size] = child;
            //TODO: Owner.UpdateLayout();
            size++;
        }

        /// <summary>
        /// Adds a control to the <see cref="ControlCollectionBase"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="child">The <see cref="Control"/> to insert into the <see cref="ControlCollectionBase"/>.</param>
        public virtual void Insert(int index, TControl child)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException(nameof(index));
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (child.IsInvalid) throw new InvalidHandleException();
            if (child.TopLevel) throw new ArgumentException("Cannot add a top-level control to a ControlCollectionBase.");
            if (isReadOnly) throw new InvalidOperationException("Cannot remove items while the collection is read-only.");
            if (Contains(child)) throw new InvalidOperationException("Cannot add the same control more than once.");

            if (innerArray == null)
                innerArray = new TControl[defaultCapacity];
            else if (size >= innerArray.Length)
            {
                TControl[] array = new TControl[innerArray.Length * growFactor];
                Array.Copy(innerArray, array, index);
                array[index] = child;
                Array.Copy(innerArray, index, array, index + 1, size - index);
                innerArray = array;
            }
            else if (index < size)
                Array.Copy(innerArray, index, innerArray, index + 1, size - index);

            child.Index = index;
            child.Parent = Owner;
            innerArray[size] = child;
            size++;
        }

        /// <summary>
        /// Removes the first occurrence of a specific <see cref="Control"/> from the <see cref="ControlCollectionBase"/>.
        /// </summary>
        /// <param name="child">The <see cref="Control"/> to remove from the <see cref="ControlCollectionBase"/>.</param>
        /// <returns>true if item is successfully removed; otherwise, false. This method also returns false if item was not found in the <see cref="ControlCollectionBase"/>.</returns>
        public virtual bool Remove(TControl child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (child.IsInvalid) throw new InvalidHandleException();
            if (isReadOnly) throw new InvalidOperationException("Cannot remove items while the collection is read-only.");
            if (!Contains(child)) return false;

            int index = IndexOf(child);
            if (index >= 0)
            {
                size--;
                child.Index = -1;
                child.Parent = null;
                if (index < size)
                    Array.Copy(innerArray, index + 1, innerArray, index, size - 1);
                innerArray[size] = default;
                child.Dispose();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="ControlCollectionBase"/>.
        /// </summary>
        public void Clear()
        {
            if (innerArray != null)
            {
                foreach (TControl child in innerArray)
                {
                    Remove(child);
                }
            }
            //TODO: Owner.UpdateLayout();
        }

        /// <summary>
        /// Determines whether a <see cref="Control"/> is in the <see cref="ControlCollectionBase"/>.
        /// </summary>
        /// <param name="child">The <see cref="Control"/> to locate in the <see cref="ControlCollectionBase"/>.</param>
        /// <returns>true if item is found in the <see cref="ControlCollectionBase"/>; otherwise, false.</returns>
        public bool Contains(TControl child)
        {
            if (innerArray == null || child == null || child.IsInvalid)
                return false;

            for (int i = 0; i < size; i++)
            {
                if (ReferenceEquals(child, innerArray[i]))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Copies the entire <see cref="ControlCollectionBase"/> to a compatible one-dimensional array, starting at the specified index of the target array.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="ControlCollectionBase"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            if (innerArray == null) return;
            if ((array != null) && (array.Rank != 1)) throw new ArgumentException("array must be 1-dimensional.", nameof(array));
            Array.Copy(innerArray, 0, array, index, size);
        }

        /// <summary>
        /// Determines the index of a specific value in the <see cref="ControlCollectionBase"/>.
        /// </summary>
        /// <param name="value">The control to locate in the <see cref="ControlCollectionBase"/>.</param>
        /// <returns>The index of item if found in the list; otherwise, -1.</returns>
        public int IndexOf(TControl value)
        {
            if (innerArray == null)
                return -1;
            return Array.IndexOf(innerArray, value, 0, size);
        }

        internal void SetReadOnly(bool readOnly)
        {
            if (isReadOnly != readOnly)
                isReadOnly = readOnly;
        }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        public virtual TControl this[int index]
        {
            get
            {
                if (index < 0 || index >= size) throw new ArgumentOutOfRangeException(nameof(index));
                return innerArray[index];
            }
        }

        #region IList<TControl> Implementation
        int IList.Add(object value)
        {
            try
            {
                Add((TControl)value);
            }
            catch
            {
                return -1;
            }
            return IndexOf((TControl)value);
        }
        void ICollection<TControl>.Add(TControl item) => Add(item);
        void IList.Clear() => Clear();
        void ICollection<TControl>.Clear() => Clear();
        bool IList.Contains(object value) => Contains((TControl)value);
        bool ICollection<TControl>.Contains(TControl item) => Contains(item);
        void ICollection.CopyTo(Array array, int index) => CopyTo(array, index);
        void ICollection<TControl>.CopyTo(TControl[] array, int arrayIndex) => CopyTo(array, arrayIndex);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TControl>)this).GetEnumerator();
        IEnumerator<TControl> IEnumerable<TControl>.GetEnumerator() => new ControlListEnumerator(this);
        int IList.IndexOf(object value) => IndexOf((TControl)value);
        int IList<TControl>.IndexOf(TControl item) => IndexOf(item);
        void IList.Insert(int index, object value) => Insert(index, (TControl)value);
        void IList<TControl>.Insert(int index, TControl item) => Insert(index, item);
        void IList.Remove(object value) => Remove((TControl)value);
        bool ICollection<TControl>.Remove(TControl item) => Remove(item);
        void IList.RemoveAt(int index) => throw new NotSupportedException();
        void IList<TControl>.RemoveAt(int index) => throw new NotSupportedException();

        object IList.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
        }
        TControl IList<TControl>.this[int index]
        {
            get => this[index];
            set => throw new NotSupportedException();
        }

        int ICollection.Count => Count;
        int ICollection<TControl>.Count => Count;
        bool IList.IsFixedSize => IsReadOnly;
        bool IList.IsReadOnly => IsReadOnly;
        bool ICollection<TControl>.IsReadOnly => IsReadOnly;
        bool ICollection.IsSynchronized => false;
        object ICollection.SyncRoot => this;
        #endregion

        private sealed class ControlListEnumerator : IEnumerator<TControl>, ICloneable
        {
            private ControlCollectionBase<TControl> list;
            private TControl current;
            private int index;
            private bool disposed = false;

            internal ControlListEnumerator(ControlCollectionBase<TControl> list)
            {
                this.list = list;
                index = -1;
            }

            public bool MoveNext()
            {
                if (index < (list.Count - 1))
                {
                    index++;
                    current = list[index];
                    return true;
                }

                index = list.Count;
                return false;
            }

            object IEnumerator.Current => Current;

            public TControl Current
            {
                get
                {
                    if (index == -1 || index >= list.Count)
                        throw new IndexOutOfRangeException();
                    return current;
                }
            }

            public void Reset()
            {
                current = default;
                index = -1;
            }

            public object Clone() => MemberwiseClone();

            public void Dispose() => Dispose(true);

            private void Dispose(bool disposing)
            {
                if (disposed) return;
                if (disposing)
                {
                    if (list != null)
                        list.Clear();
                }
                disposed = true;
            }
        }
    }
}