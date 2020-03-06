/***************************************************************************************************
 * FileName:             MultiValueDictionary.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using TCDFx.Resources;

namespace TCDFx.Collections
{
    /// <summary>
    /// A wrapper class for a <see cref="Dictionary{TKey, TValue}"/>, representing a collection of keys with two values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.</typeparam>
    /// <typeparam name="TValue2">The type of the second values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.</typeparam>
    [Serializable]
    public class MultiValueDictionary<TKey, TValue1, TValue2> : IMultiValueDictionary, IMultiValueDictionary<TKey, TValue1, TValue2>, ISerializable, IDeserializationCallback
    {
        private KeyCollection keys;
        private ValueCollection values;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public MultiValueDictionary() : this(0, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>  and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2> dictionary) : this(dictionary, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> class that is empty, has the default initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IEqualityComparer<TKey> comparer) : this(0, comparer) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> can contain.</param>
        public MultiValueDictionary(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>  and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2> dictionary, IEqualityComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2> kmvs in dictionary)
                Add(kmvs.Key, kmvs.Value1, kmvs.Value2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> can contain.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(int capacity, IEqualityComparer<TKey> comparer) => Inner = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>(capacity, comparer);

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public KeyCollection Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        /// <summary>
        /// Gets a collection containing the values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public ValueCollection Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        /// <summary>
        /// Gets the <see cref="EqualityComparer{T}"/> that is used to determine equality of keys for the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public IEqualityComparer<TKey> Comparer => Inner.Comparer;

        /// <summary>
        /// Gets the number of key/multi-value sets contained in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public int Count => Inner.Count;

        internal Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>> Inner { get; }

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        public void Add(TKey key, TValue1 value1, TValue2 value2) => Inner.Add(key, new MultiObjectContainer<TValue1, TValue2>(value1, value2));

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <returns>1 if added successfully; else 0.</returns>
        public bool TryAdd(TKey key, TValue1 value1, TValue2 value2) => Inner.TryAdd(key, new MultiObjectContainer<TValue1, TValue2>(value1, value2));

        /// <summary>
        /// Removes all keys and values from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public void Clear() => Inner.Clear();

        /// <summary>
        /// Determines whether the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key) => Inner.ContainsKey(key);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and returns the data needed to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> instance.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> object that contains the information required to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> instance.</param>
        /// <param name="context">A <see cref="StreamingContext"/> structure that contains the source and destination of the serialized stream associated with the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> instance.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) => Inner.GetObjectData(info, context);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and raises the deserialization event when the deserialization is complete.
        /// </summary>
        /// <param name="sender">the source of the deserialization event.</param>
        public virtual void OnDeserialization(object sender) => Inner.OnDeserialization(sender);

        /// <summary>
        /// Removes the values with the specified key from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <param name="key">the key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>. This method returns <c>false</c> if <paramref name="key"/> is not found in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.</returns>
        public bool Remove(TKey key) => Inner.Remove(key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value1">When this method returns, contains the first value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, contains the second value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2)
        {
            if (Inner.TryGetValue(key, out MultiObjectContainer<TValue1, TValue2> moc))
            {
                value1 = moc.Value1;
                value2 = moc.Value2;
                return true;
            }
            value1 = default;
            value2 = default;
            return false;
        }

        private void CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2> kmvs in this)
                array[index++] = kmvs;
        }

        private bool VerifyKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return key is TKey;
        }

        private bool VerifyValues(object value1, object value2)
        {
            if (value1 == null && (value1 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value1));
            if (value2 == null && (value2 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value2));
            return value1 is TValue1 && value2 is TValue2;
        }

        /// <summary>
        /// Gets or sets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="KeyNotFoundException"/>, and a set operation creates a new element with the specified key.</returns>
        public MultiObjectContainer<TValue1, TValue2> this[TKey key]
        {
            get => Inner[key];
            set => Inner[key] = value;
        }

        #region IMultiValueDictionary Implementation
        ICollection IMultiValueDictionary.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection IMultiValueDictionary.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }
        bool IMultiValueDictionary.IsFixedSize => ((IMultiValueDictionary)Inner).IsFixedSize;

        bool IMultiValueDictionary.IsReadOnly => ((IMultiValueDictionary)Inner).IsReadOnly;

        bool IMultiValueDictionary.ContainsKey(object key) => VerifyKey(key) && ContainsKey((TKey)key);

        void IMultiValueDictionary.Remove(object key)
        {
            if (VerifyKey(key))
                Remove((TKey)key);
        }

        bool ICollection.IsSynchronized => ((ICollection)Inner).IsSynchronized;

        object ICollection.SyncRoot => ((ICollection)Inner).SyncRoot;

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if (array.Rank != 1 || array.GetLowerBound(0) != 0)
                throw new ArgumentException(Strings.MultiDimArrayNotSupported, nameof(array));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            if (array is KeyMultiValueSet<TKey, TValue1, TValue2>[] setArray)
                CopyTo(setArray, index);
            else if (array is DictionaryEntry[])
            {
                DictionaryEntry[] entryArray = array as DictionaryEntry[];
                foreach (TKey key in Keys)
                    entryArray[index++] = new DictionaryEntry(key, this[key]);
            }
            else
            {
                if (!(array is object[] objArray))
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                try
                {
                    foreach (TKey key in Keys)
                    {
                        MultiObjectContainer<TValue1, TValue2> container = this[key];
                        objArray[index++] = new KeyMultiValueSet<TKey, TValue1, TValue2>(key, container.Value1, container.Value2);
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => Inner.GetEnumerator();
        #endregion IMultiValueDictionary Implementation

        #region IMultiValueDictionary<TKey, TValue1, TValue2> Implementation
        ICollection<TKey> IMultiValueDictionary<TKey, TValue1, TValue2>.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection<MultiObjectContainer<TValue1, TValue2>> IMultiValueDictionary<TKey, TValue1, TValue2>.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        void IMultiValueDictionary<TKey, TValue1, TValue2>.Add(object key, object value1, object value2)
        {
            if (!VerifyKey(key))
                throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
            if (!VerifyValues(value1, value2))
                throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, and value2 must be of type {typeof(TValue2).Name}");
            Add((TKey)key, (TValue1)value1, (TValue2)value2);
        }

        MultiObjectContainer<object, object> IMultiValueDictionary<TKey, TValue1, TValue2>.this[object key]
        {
            get
            {
                if (VerifyKey(key))
                {
                    TKey goodKey = (TKey)key;
                    if (Inner.ContainsKey(goodKey))
                    {
                        MultiObjectContainer<TValue1, TValue2> container = Inner[goodKey];
                        return new MultiObjectContainer<object, object>(container.Value1, container.Value2);
                    }
                }
                return null;
            }
            set
            {
                if (!VerifyKey(key))
                    throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
                if (!VerifyValues(value.Value1, value.Value2))
                    throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, and value2 must be of type {typeof(TValue2).Name}");
                this[(TKey)key] = new MultiObjectContainer<TValue1, TValue2>((TValue1)value.Value1, (TValue2)value.Value2);
            }
        }

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>.IsReadOnly => ((ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>)Inner).IsReadOnly;

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>.Add(KeyMultiValueSet<TKey, TValue1, TValue2> kmvs) => Add(kmvs.Key, kmvs.Value1, kmvs.Value2);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>.Contains(KeyMultiValueSet<TKey, TValue1, TValue2> item)
        {
            if (ContainsKey(item.Key))
            {
                MultiObjectContainer<TValue1, TValue2> container = this[item.Key];
                return EqualityComparer<TValue1>.Default.Equals(container.Value1, item.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2, item.Value2);
            }
            return false;
        }

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>.CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2>[] array, int arrayIndex) => CopyTo(array, arrayIndex);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>.Remove(KeyMultiValueSet<TKey, TValue1, TValue2> KeyMultiValueSet)
        {
            if (ContainsKey(KeyMultiValueSet.Key))
            {
                MultiObjectContainer<TValue1, TValue2> container = this[KeyMultiValueSet.Key];
                if (EqualityComparer<TValue1>.Default.Equals(container.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2))
                {
                    Remove(KeyMultiValueSet.Key);
                    return true;
                }
            }
            return false;
        }

        IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2>> IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2>>.GetEnumerator() => ((IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2>>)Inner).GetEnumerator();
        #endregion IMultiValueDictionary<TKey, TValue1, TValue2> Implementation

        /// <summary>
        /// Enumerates the elements of a <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IDictionaryEnumerator, IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2> dictionary, int getEnumeratorRetType)
            {
                object[] args = { dictionary.Inner, getEnumeratorRetType };
                enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, args, null);
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            public KeyMultiValueSet<TKey, TValue1, TValue2> Current
            {
                get
                {
                    KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2>> kvp = enumerator.Current;
                    return new KeyMultiValueSet<TKey, TValue1, TValue2>(kvp);
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
            /// </summary>
            /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext() => enumerator.MoveNext();

            /// <summary>
            /// Releases all resources used by the <see cref="Enumerator"/>.
            /// </summary>
            public void Dispose() => enumerator.Dispose();

            #region IDictionaryEnumerator Implementation
            DictionaryEntry IDictionaryEnumerator.Entry => ((IDictionaryEnumerator)enumerator).Entry;
            object IDictionaryEnumerator.Key => ((IDictionaryEnumerator)enumerator).Key;
            object IDictionaryEnumerator.Value => ((IDictionaryEnumerator)enumerator).Value;
            #endregion IDictionaryEnumerator Implementation

            #region IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2>> Implementation
            object IEnumerator.Current => ((IEnumerator)enumerator).Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            #endregion IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2>> Implementation
        }

        /// <summary>
        /// Represents the collection of keys in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public sealed class KeyCollection : ICollection, ICollection<TKey>, IEnumerable<TKey>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.KeyCollection keyCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="KeyCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> whose keys are reflected in the new <see cref="KeyCollection"/>.</param>
            public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2> dictionary) =>
                keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.KeyCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="KeyCollection"/>.
            /// </summary>
            public int Count => keyCollection.Count;

            /// <summary>
            /// Copies the <see cref="KeyCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="KeyCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<TKey> Implementation
            bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
            void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
            void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
            bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
            bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);
            #endregion ICollection<TKey> Implementation

            #region IEnumerable<TKey> Implementation
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
            #endregion IEnumerable<TKey> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="KeyCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<TKey>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public TKey Current => enumerator.Current.Key;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="KeyCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<TKey> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<TKey> Implementation
            }
        }

        /// <summary>
        /// Represents the collection of values in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public sealed class ValueCollection : ICollection, ICollection<MultiObjectContainer<TValue1, TValue2>>, IEnumerable<MultiObjectContainer<TValue1, TValue2>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.ValueCollection valueCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="ValueCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2}"/> whose keys are reflected in the new <see cref="ValueCollection"/>.</param>
            public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2> dictionary) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.ValueCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="ValueCollection"/>.
            /// </summary>
            public int Count => valueCollection.Count;

            /// <summary>
            /// Copies the <see cref="ValueCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="ValueCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(MultiObjectContainer<TValue1, TValue2>[] array, int index) => valueCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<MultiObjectContainer<TValue1, TValue2>> Implementation
            bool ICollection<MultiObjectContainer<TValue1, TValue2>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).IsReadOnly;
            void ICollection<MultiObjectContainer<TValue1, TValue2>>.Add(MultiObjectContainer<TValue1, TValue2> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Add(item);
            void ICollection<MultiObjectContainer<TValue1, TValue2>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Clear();
            bool ICollection<MultiObjectContainer<TValue1, TValue2>>.Contains(MultiObjectContainer<TValue1, TValue2> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Contains(item);
            bool ICollection<MultiObjectContainer<TValue1, TValue2>>.Remove(MultiObjectContainer<TValue1, TValue2> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Remove(item);
            #endregion ICollection<MultiObjectContainer<TValue1, TValue2>> Implementation

            #region IEnumerable<MultiObjectContainer<TValue1, TValue2>> Implementation
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
            IEnumerator<MultiObjectContainer<TValue1, TValue2>> IEnumerable<MultiObjectContainer<TValue1, TValue2>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2>>)valueCollection).GetEnumerator();
            #endregion IEnumerable<MultiObjectContainer<TValue1, TValue2>> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="ValueCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2>>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public MultiObjectContainer<TValue1, TValue2> Current => enumerator.Current.Value;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="ValueCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<MultiObjectContainer<TValue1, TValue2>> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<MultiObjectContainer<TValue1, TValue2>> Implementation
            }
        }
    }

    /// <summary>
    /// A wrapper class for a <see cref="Dictionary{TKey, TValue}"/>, representing a collection of keys with three values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</typeparam>
    /// <typeparam name="TValue2">The type of the second values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</typeparam>
    /// <typeparam name="TValue3">The type of the third values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</typeparam>
    [Serializable]
    public class MultiValueDictionary<TKey, TValue1, TValue2, TValue3> :
        IMultiValueDictionary, IMultiValueDictionary<TKey, TValue1, TValue2, TValue3>, ISerializable, IDeserializationCallback
    {
        private KeyCollection keys;
        private ValueCollection values;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public MultiValueDictionary() : this(0, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>  and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2, TValue3> dictionary) : this(dictionary, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> class that is empty, has the default initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IEqualityComparer<TKey> comparer) : this(0, comparer) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> can contain.</param>
        public MultiValueDictionary(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>  and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2, TValue3> dictionary, IEqualityComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> kmvs in dictionary)
                Add(kmvs.Key, kmvs.Value1, kmvs.Value2, kmvs.Value3);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> can contain.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(int capacity, IEqualityComparer<TKey> comparer) => Inner = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>(capacity, comparer);

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        public KeyCollection Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        /// <summary>
        /// Gets a collection containing the values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        public ValueCollection Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        /// <summary>
        /// Gets the <see cref="EqualityComparer{T}"/> that is used to determine equality of keys for the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        public IEqualityComparer<TKey> Comparer => Inner.Comparer;

        /// <summary>
        /// Gets the number of key/multi-value sets contained in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        public int Count => Inner.Count;

        internal Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>> Inner { get; }

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value3">The third value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        public void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3) => Inner.Add(key, new MultiObjectContainer<TValue1, TValue2, TValue3>(value1, value2, value3));

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value3">The third value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <returns>1 if added successfully; else 0.</returns>
        public bool TryAdd(TKey key, TValue1 value1, TValue2 value2, TValue3 value3) => Inner.TryAdd(key, new MultiObjectContainer<TValue1, TValue2, TValue3>(value1, value2, value3));

        /// <summary>
        /// Removes all keys and values from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        public void Clear() => Inner.Clear();

        /// <summary>
        /// Determines whether the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key) => Inner.ContainsKey(key);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and returns the data needed to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> instance.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> object that contains the information required to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> instance.</param>
        /// <param name="context">A <see cref="StreamingContext"/> structure that contains the source and destination of the serialized stream associated with the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> instance.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) => Inner.GetObjectData(info, context);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and raises the deserialization event when the deserialization is complete.
        /// </summary>
        /// <param name="sender">the source of the deserialization event.</param>
        public virtual void OnDeserialization(object sender) => Inner.OnDeserialization(sender);

        /// <summary>
        /// Removes the values with the specified key from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        /// <param name="key">the key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>. This method returns <c>false</c> if <paramref name="key"/> is not found in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</returns>
        public bool Remove(TKey key) => Inner.Remove(key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value1">When this method returns, contains the first value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, contains the second value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value3">When this method returns, contains the third value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value3"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3)
        {
            if (Inner.TryGetValue(key, out MultiObjectContainer<TValue1, TValue2, TValue3> moc))
            {
                value1 = moc.Value1;
                value2 = moc.Value2;
                value3 = moc.Value3;
                return true;
            }
            value1 = default;
            value2 = default;
            value3 = default;
            return false;
        }

        private void CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> kmvs in this)
                array[index++] = kmvs;
        }

        private bool VerifyKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return key is TKey;
        }

        private bool VerifyValues(object value1, object value2, object value3)
        {
            if (value1 == null && (value1 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value1));
            if (value2 == null && (value2 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value2));
            if (value3 == null && (value3 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value3));
            return value1 is TValue1 && value2 is TValue2 && value3 is TValue3;
        }

        /// <summary>
        /// Gets or sets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="KeyNotFoundException"/>, and a set operation creates a new element with the specified key.</returns>
        public MultiObjectContainer<TValue1, TValue2, TValue3> this[TKey key]
        {
            get => Inner[key];
            set => Inner[key] = value;
        }

        #region IMultiValueDictionary Implementation
        ICollection IMultiValueDictionary.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection IMultiValueDictionary.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }
        bool IMultiValueDictionary.IsFixedSize => ((IMultiValueDictionary)Inner).IsFixedSize;

        bool IMultiValueDictionary.IsReadOnly => ((IMultiValueDictionary)Inner).IsReadOnly;

        bool IMultiValueDictionary.ContainsKey(object key) => VerifyKey(key) && ContainsKey((TKey)key);

        void IMultiValueDictionary.Remove(object key)
        {
            if (VerifyKey(key))
                Remove((TKey)key);
        }

        bool ICollection.IsSynchronized => ((ICollection)Inner).IsSynchronized;

        object ICollection.SyncRoot => ((ICollection)Inner).SyncRoot;

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if (array.Rank != 1 || array.GetLowerBound(0) != 0)
                throw new ArgumentException(Strings.MultiDimArrayNotSupported, nameof(array));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            if (array is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>[] setArray)
                CopyTo(setArray, index);
            else if (array is DictionaryEntry[])
            {
                DictionaryEntry[] entryArray = array as DictionaryEntry[];
                foreach (TKey key in Keys)
                    entryArray[index++] = new DictionaryEntry(key, this[key]);
            }
            else
            {
                if (!(array is object[] objArray))
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                try
                {
                    foreach (TKey key in Keys)
                    {
                        MultiObjectContainer<TValue1, TValue2, TValue3> container = this[key];
                        objArray[index++] = new KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>(key, container.Value1, container.Value2, container.Value3);
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => Inner.GetEnumerator();
        #endregion IMultiValueDictionary Implementation

        #region IMultiValueDictionary<TKey, TValue1, TValue2, TValue3> Implementation
        ICollection<TKey> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3>.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3>.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        void IMultiValueDictionary<TKey, TValue1, TValue2, TValue3>.Add(object key, object value1, object value2, object value3)
        {
            if (!VerifyKey(key))
                throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
            if (!VerifyValues(value1, value2, value3))
                throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, value2 must be of type {typeof(TValue2).Name}, and value3 must be of type {typeof(TValue2).Name}.");
            Add((TKey)key, (TValue1)value1, (TValue2)value2, (TValue3)value3);
        }

        MultiObjectContainer<object, object, object> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3>.this[object key]
        {
            get
            {
                if (VerifyKey(key))
                {
                    TKey goodKey = (TKey)key;
                    if (Inner.ContainsKey(goodKey))
                    {
                        MultiObjectContainer<TValue1, TValue2, TValue3> container = Inner[goodKey];
                        return new MultiObjectContainer<object, object, object>(container.Value1, container.Value2, container.Value3);
                    }
                }
                return null;
            }
            set
            {
                if (!VerifyKey(key))
                    throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
                if (!VerifyValues(value.Value1, value.Value2, value.Value3))
                    throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, value2 must be of type {typeof(TValue2).Name}, and value3 must be of type {typeof(TValue2).Name}.");
                this[(TKey)key] = new MultiObjectContainer<TValue1, TValue2, TValue3>((TValue1)value.Value1, (TValue2)value.Value2, (TValue3)value.Value3);
            }
        }

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>.IsReadOnly => ((ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>)Inner).IsReadOnly;

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>.Add(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> kmvs) => Add(kmvs.Key, kmvs.Value1, kmvs.Value2, kmvs.Value3);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>.Contains(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> item)
        {
            if (ContainsKey(item.Key))
            {
                MultiObjectContainer<TValue1, TValue2, TValue3> container = this[item.Key];
                return EqualityComparer<TValue1>.Default.Equals(container.Value1, item.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2, item.Value2) && EqualityComparer<TValue3>.Default.Equals(container.Value3, item.Value3);
            }
            return false;
        }

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>.CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>[] array, int arrayIndex) => CopyTo(array, arrayIndex);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>.Remove(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> KeyMultiValueSet)
        {
            if (ContainsKey(KeyMultiValueSet.Key))
            {
                MultiObjectContainer<TValue1, TValue2, TValue3> container = this[KeyMultiValueSet.Key];
                if (EqualityComparer<TValue1>.Default.Equals(container.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2) && EqualityComparer<TValue3>.Default.Equals(container.Value3))
                {
                    Remove(KeyMultiValueSet.Key);
                    return true;
                }
            }
            return false;
        }

        IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>> IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>.GetEnumerator() => ((IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>)Inner).GetEnumerator();
        #endregion IMultiValueDictionary<TKey, TValue1, TValue2, TValue3> Implementation

        /// <summary>
        /// Enumerates the elements of a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IDictionaryEnumerator, IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dictionary, int getEnumeratorRetType)
            {
                object[] args = { dictionary.Inner, getEnumeratorRetType };
                enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, args, null);
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            public KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> Current
            {
                get
                {
                    KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>> kvp = enumerator.Current;
                    return new KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>(kvp);
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
            /// </summary>
            /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext() => enumerator.MoveNext();

            /// <summary>
            /// Releases all resources used by the <see cref="Enumerator"/>.
            /// </summary>
            public void Dispose() => enumerator.Dispose();

            #region IDictionaryEnumerator Implementation
            DictionaryEntry IDictionaryEnumerator.Entry => ((IDictionaryEnumerator)enumerator).Entry;
            object IDictionaryEnumerator.Key => ((IDictionaryEnumerator)enumerator).Key;
            object IDictionaryEnumerator.Value => ((IDictionaryEnumerator)enumerator).Value;
            #endregion IDictionaryEnumerator Implementation

            #region IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>> Implementation
            object IEnumerator.Current => ((IEnumerator)enumerator).Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            #endregion IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>> Implementation
        }

        /// <summary>
        /// Represents the collection of keys in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        public sealed class KeyCollection : ICollection, ICollection<TKey>, IEnumerable<TKey>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.KeyCollection keyCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="KeyCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> whose keys are reflected in the new <see cref="KeyCollection"/>.</param>
            public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dictionary) =>
                keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.KeyCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="KeyCollection"/>.
            /// </summary>
            public int Count => keyCollection.Count;

            /// <summary>
            /// Copies the <see cref="KeyCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="KeyCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<TKey> Implementation
            bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
            void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
            void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
            bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
            bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);
            #endregion ICollection<TKey> Implementation

            #region IEnumerable<TKey> Implementation
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
            #endregion IEnumerable<TKey> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="KeyCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<TKey>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public TKey Current => enumerator.Current.Key;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="KeyCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<TKey> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<TKey> Implementation
            }
        }

        /// <summary>
        /// Represents the collection of values in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        public sealed class ValueCollection : ICollection, ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>, IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.ValueCollection valueCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="ValueCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> whose keys are reflected in the new <see cref="ValueCollection"/>.</param>
            public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dictionary) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.ValueCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="ValueCollection"/>.
            /// </summary>
            public int Count => valueCollection.Count;

            /// <summary>
            /// Copies the <see cref="ValueCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="ValueCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(MultiObjectContainer<TValue1, TValue2, TValue3>[] array, int index) => valueCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>> Implementation
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).IsReadOnly;
            void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Add(MultiObjectContainer<TValue1, TValue2, TValue3> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Add(item);
            void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Clear();
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Contains(MultiObjectContainer<TValue1, TValue2, TValue3> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Contains(item);
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Remove(MultiObjectContainer<TValue1, TValue2, TValue3> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Remove(item);
            #endregion ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>> Implementation

            #region IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3>> Implementation
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
            IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3>> IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).GetEnumerator();
            #endregion IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3>> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="ValueCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3>>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public MultiObjectContainer<TValue1, TValue2, TValue3> Current => enumerator.Current.Value;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="ValueCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3>> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3>> Implementation
            }
        }
    }

    /// <summary>
    /// A wrapper class for a <see cref="Dictionary{TKey, TValue}"/>, representing a collection of keys with four values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</typeparam>
    /// <typeparam name="TValue2">The type of the second values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</typeparam>
    /// <typeparam name="TValue3">The type of the third values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</typeparam>
    /// <typeparam name="TValue4">The type of the fourth values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</typeparam>
    [Serializable]
    public class MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> :
        IMultiValueDictionary, IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>, ISerializable, IDeserializationCallback
    {
        private KeyCollection keys;
        private ValueCollection values;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public MultiValueDictionary() : this(0, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>  and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dictionary) : this(dictionary, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> class that is empty, has the default initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IEqualityComparer<TKey> comparer) : this(0, comparer) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> can contain.</param>
        public MultiValueDictionary(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>  and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dictionary, IEqualityComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> kmvs in dictionary)
                Add(kmvs.Key, kmvs.Value1, kmvs.Value2, kmvs.Value3, kmvs.Value4);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> can contain.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(int capacity, IEqualityComparer<TKey> comparer) => Inner = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>(capacity, comparer);

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        public KeyCollection Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        /// <summary>
        /// Gets a collection containing the values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        public ValueCollection Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        /// <summary>
        /// Gets the <see cref="EqualityComparer{T}"/> that is used to determine equality of keys for the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        public IEqualityComparer<TKey> Comparer => Inner.Comparer;

        /// <summary>
        /// Gets the number of key/multi-value sets contained in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        public int Count => Inner.Count;

        internal Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Inner { get; }

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value3">The third value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value4">The fourth value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        public void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4) => Inner.Add(key, new MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>(value1, value2, value3, value4));

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value3">The third value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value4">The fourth value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <returns>1 if added successfully; else 0.</returns>
        public bool TryAdd(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4) => Inner.TryAdd(key, new MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>(value1, value2, value3, value4));

        /// <summary>
        /// Removes all keys and values from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        public void Clear() => Inner.Clear();

        /// <summary>
        /// Determines whether the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key) => Inner.ContainsKey(key);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and returns the data needed to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> instance.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> object that contains the information required to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> instance.</param>
        /// <param name="context">A <see cref="StreamingContext"/> structure that contains the source and destination of the serialized stream associated with the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> instance.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) => Inner.GetObjectData(info, context);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and raises the deserialization event when the deserialization is complete.
        /// </summary>
        /// <param name="sender">the source of the deserialization event.</param>
        public virtual void OnDeserialization(object sender) => Inner.OnDeserialization(sender);

        /// <summary>
        /// Removes the values with the specified key from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        /// <param name="key">the key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>. This method returns <c>false</c> if <paramref name="key"/> is not found in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</returns>
        public bool Remove(TKey key) => Inner.Remove(key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value1">When this method returns, contains the first value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, contains the second value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value3">When this method returns, contains the third value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value3"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value4">When this method returns, contains the fourth value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value4"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3, out TValue4 value4)
        {
            if (Inner.TryGetValue(key, out MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> moc))
            {
                value1 = moc.Value1;
                value2 = moc.Value2;
                value3 = moc.Value3;
                value4 = moc.Value4;
                return true;
            }
            value1 = default;
            value2 = default;
            value3 = default;
            value4 = default;
            return false;
        }

        private void CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> kmvs in this)
                array[index++] = kmvs;
        }

        private bool VerifyKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return key is TKey;
        }

        private bool VerifyValues(object value1, object value2, object value3, object value4)
        {
            if (value1 == null && (value1 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value1));
            if (value2 == null && (value2 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value2));
            if (value3 == null && (value3 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value3));
            if (value4 == null && (value4 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value4));
            return value1 is TValue1 && value2 is TValue2 && value3 is TValue3 && value4 is TValue4;
        }

        /// <summary>
        /// Gets or sets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="KeyNotFoundException"/>, and a set operation creates a new element with the specified key.</returns>
        public MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> this[TKey key]
        {
            get => Inner[key];
            set => Inner[key] = value;
        }

        #region IMultiValueDictionary Implementation
        ICollection IMultiValueDictionary.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection IMultiValueDictionary.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }
        bool IMultiValueDictionary.IsFixedSize => ((IMultiValueDictionary)Inner).IsFixedSize;

        bool IMultiValueDictionary.IsReadOnly => ((IMultiValueDictionary)Inner).IsReadOnly;

        bool IMultiValueDictionary.ContainsKey(object key) => VerifyKey(key) && ContainsKey((TKey)key);

        void IMultiValueDictionary.Remove(object key)
        {
            if (VerifyKey(key))
                Remove((TKey)key);
        }

        bool ICollection.IsSynchronized => ((ICollection)Inner).IsSynchronized;

        object ICollection.SyncRoot => ((ICollection)Inner).SyncRoot;

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if (array.Rank != 1)
                throw new ArgumentException(Strings.MultiDimArrayNotSupported, nameof(array));
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException(Strings.MultiDimArrayNotSupported, nameof(array));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            if (array is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>[] setArray)
                CopyTo(setArray, index);
            else if (array is DictionaryEntry[])
            {
                DictionaryEntry[] entryArray = array as DictionaryEntry[];
                foreach (TKey key in Keys)
                    entryArray[index++] = new DictionaryEntry(key, this[key]);
            }
            else
            {
                if (!(array is object[] objArray))
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                try
                {
                    foreach (TKey key in Keys)
                    {
                        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> container = this[key];
                        objArray[index++] = new KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>(key, container.Value1, container.Value2, container.Value3, container.Value4);
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => Inner.GetEnumerator();
        #endregion IMultiValueDictionary Implementation

        #region IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> Implementation
        ICollection<TKey> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        void IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>.Add(object key, object value1, object value2, object value3, object value4)
        {
            if (!VerifyKey(key))
                throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
            if (!VerifyValues(value1, value2, value3, value4))
                throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, value2 must be of type {typeof(TValue2).Name}, value3 must be of type {typeof(TValue3).Name}, and value4 must be of type {typeof(TValue4).Name}.");
            Add((TKey)key, (TValue1)value1, (TValue2)value2, (TValue3)value3, (TValue4)value4);
        }

        MultiObjectContainer<object, object, object, object> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>.this[object key]
        {
            get
            {
                if (VerifyKey(key))
                {
                    TKey goodKey = (TKey)key;
                    if (Inner.ContainsKey(goodKey))
                    {
                        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> container = Inner[goodKey];
                        return new MultiObjectContainer<object, object, object, object>(container.Value1, container.Value2, container.Value3, container.Value4);
                    }
                }
                return null;
            }
            set
            {
                if (!VerifyKey(key))
                    throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
                if (!VerifyValues(value.Value1, value.Value2, value.Value3, value.Value4))
                    throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, value2 must be of type {typeof(TValue2).Name}, value3 must be of type {typeof(TValue3).Name}, and value4 must be of type {typeof(TValue4).Name}.");
                this[(TKey)key] = new MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>((TValue1)value.Value1, (TValue2)value.Value2, (TValue3)value.Value3, (TValue4)value.Value4);
            }
        }

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>.IsReadOnly => ((ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>)Inner).IsReadOnly;

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>.Add(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> kmvs) => Add(kmvs.Key, kmvs.Value1, kmvs.Value2, kmvs.Value3, kmvs.Value4);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>.Contains(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> item)
        {
            if (ContainsKey(item.Key))
            {
                MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> container = this[item.Key];
                return EqualityComparer<TValue1>.Default.Equals(container.Value1, item.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2, item.Value2) && EqualityComparer<TValue3>.Default.Equals(container.Value3, item.Value3) && EqualityComparer<TValue4>.Default.Equals(container.Value4, item.Value4);
            }
            return false;
        }

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>.CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>[] array, int arrayIndex) => CopyTo(array, arrayIndex);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>.Remove(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> KeyMultiValueSet)
        {
            if (ContainsKey(KeyMultiValueSet.Key))
            {
                MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> container = this[KeyMultiValueSet.Key];
                if (EqualityComparer<TValue1>.Default.Equals(container.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2) && EqualityComparer<TValue3>.Default.Equals(container.Value3) && EqualityComparer<TValue4>.Default.Equals(container.Value4))
                {
                    Remove(KeyMultiValueSet.Key);
                    return true;
                }
            }
            return false;
        }

        IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>> IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>.GetEnumerator() => ((IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>)Inner).GetEnumerator();
        #endregion IMultiValueDictionary<TKey, TValue1, TValue2, TValue3> Implementation

        /// <summary>
        /// Enumerates the elements of a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IDictionaryEnumerator, IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dictionary, int getEnumeratorRetType)
            {
                object[] args = { dictionary.Inner, getEnumeratorRetType };
                enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, args, null);
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            public KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> Current
            {
                get
                {
                    KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> kvp = enumerator.Current;
                    return new KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>(kvp);
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
            /// </summary>
            /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext() => enumerator.MoveNext();

            /// <summary>
            /// Releases all resources used by the <see cref="Enumerator"/>.
            /// </summary>
            public void Dispose() => enumerator.Dispose();

            #region IDictionaryEnumerator Implementation
            DictionaryEntry IDictionaryEnumerator.Entry => ((IDictionaryEnumerator)enumerator).Entry;
            object IDictionaryEnumerator.Key => ((IDictionaryEnumerator)enumerator).Key;
            object IDictionaryEnumerator.Value => ((IDictionaryEnumerator)enumerator).Value;
            #endregion IDictionaryEnumerator Implementation

            #region IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>> Implementation
            object IEnumerator.Current => ((IEnumerator)enumerator).Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            #endregion IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>> Implementation
        }

        /// <summary>
        /// Represents the collection of keys in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        public sealed class KeyCollection : ICollection, ICollection<TKey>, IEnumerable<TKey>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.KeyCollection keyCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="KeyCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> whose keys are reflected in the new <see cref="KeyCollection"/>.</param>
            public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dictionary) =>
                keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.KeyCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="KeyCollection"/>.
            /// </summary>
            public int Count => keyCollection.Count;

            /// <summary>
            /// Copies the <see cref="KeyCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="KeyCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<TKey> Implementation
            bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
            void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
            void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
            bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
            bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);
            #endregion ICollection<TKey> Implementation

            #region IEnumerable<TKey> Implementation
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
            #endregion IEnumerable<TKey> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="KeyCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<TKey>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public TKey Current => enumerator.Current.Key;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="KeyCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<TKey> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<TKey> Implementation
            }
        }

        /// <summary>
        /// Represents the collection of values in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        public sealed class ValueCollection : ICollection, ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>, IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.ValueCollection valueCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="ValueCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> whose keys are reflected in the new <see cref="ValueCollection"/>.</param>
            public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dictionary) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.ValueCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="ValueCollection"/>.
            /// </summary>
            public int Count => valueCollection.Count;

            /// <summary>
            /// Copies the <see cref="ValueCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="ValueCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>[] array, int index) => valueCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Implementation
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).IsReadOnly;
            void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Add(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Add(item);
            void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Clear();
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Contains(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Contains(item);
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Remove(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Remove(item);
            #endregion ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Implementation

            #region IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Implementation
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
            IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).GetEnumerator();
            #endregion IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="ValueCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> Current => enumerator.Current.Value;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="ValueCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Implementation
            }
        }
    }

    /// <summary>
    /// A wrapper class for a <see cref="Dictionary{TKey, TValue}"/>, representing a collection of keys with four values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</typeparam>
    /// <typeparam name="TValue2">The type of the second values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</typeparam>
    /// <typeparam name="TValue3">The type of the third values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</typeparam>
    /// <typeparam name="TValue4">The type of the fourth values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</typeparam>
    /// <typeparam name="TValue5">The type of the fifth values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</typeparam>
    [Serializable]
    public class MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> :
        IMultiValueDictionary, IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>, ISerializable, IDeserializationCallback
    {
        private KeyCollection keys;
        private ValueCollection values;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        public MultiValueDictionary() : this(0, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>  and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dictionary) : this(dictionary, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> class that is empty, has the default initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IEqualityComparer<TKey> comparer) : this(0, comparer) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> can contain.</param>
        public MultiValueDictionary(int capacity) : this(capacity, null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> class that contains elements copied from the specified <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>  and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="dictionary">The <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> whose elements are copied to the new <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dictionary, IEqualityComparer<TKey> comparer) : this((dictionary != null) ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> kmvs in dictionary)
                Add(kmvs.Key, kmvs.Value1, kmvs.Value2, kmvs.Value3, kmvs.Value4, kmvs.Value5);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> class that is empty, has the specified initial capacity, and uses the specified <see cref="EqualityComparer{T}"/>.
        /// </summary>
        /// <param name="capacity">The initial number of elements that the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> can contain.</param>
        /// <param name="comparer">The <see cref="EqualityComparer{T}"/> implementation to use when comparing keys, or <c>null</c> to use the default <see cref="EqualityComparer{T}"/> for the type of the key.</param>
        public MultiValueDictionary(int capacity, IEqualityComparer<TKey> comparer) => Inner = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>(capacity, comparer);

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        public KeyCollection Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        /// <summary>
        /// Gets a collection containing the values in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        public ValueCollection Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        /// <summary>
        /// Gets the <see cref="EqualityComparer{T}"/> that is used to determine equality of keys for the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        public IEqualityComparer<TKey> Comparer => Inner.Comparer;

        /// <summary>
        /// Gets the number of key/multi-value sets contained in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        public int Count => Inner.Count;

        internal Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Inner { get; }

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value3">The third value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value4">The fourth value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value5">The fifth value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        public void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5) => Inner.Add(key, new MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>(value1, value2, value3, value4, value5));

        /// <summary>
        /// Adds the specified key and value to the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value2">The second value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value3">The third value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value4">The fourth value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <param name="value5">The fifth value of the element to add. The value can be <see langword="null"/> for reference types.</param>
        /// <returns>1 if added successfully; else 0.</returns>
        public bool TryAdd(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5) => Inner.TryAdd(key, new MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>(value1, value2, value3, value4, value5));

        /// <summary>
        /// Removes all keys and values from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        public void Clear() => Inner.Clear();

        /// <summary>
        /// Determines whether the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key) => Inner.ContainsKey(key);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and returns the data needed to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> instance.
        /// </summary>
        /// <param name="info">A <see cref="SerializationInfo"/> object that contains the information required to serialize the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> instance.</param>
        /// <param name="context">A <see cref="StreamingContext"/> structure that contains the source and destination of the serialized stream associated with the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> instance.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) => Inner.GetObjectData(info, context);

        /// <summary>
        /// Implements the <see cref="ISerializable"/> interface and raises the deserialization event when the deserialization is complete.
        /// </summary>
        /// <param name="sender">the source of the deserialization event.</param>
        public virtual void OnDeserialization(object sender) => Inner.OnDeserialization(sender);

        /// <summary>
        /// Removes the values with the specified key from the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        /// <param name="key">the key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>. This method returns <c>false</c> if <paramref name="key"/> is not found in the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</returns>
        public bool Remove(TKey key) => Inner.Remove(key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value1">When this method returns, contains the first value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, contains the second value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value3">When this method returns, contains the third value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value3"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value4">When this method returns, contains the fourth value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value4"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value5">When this method returns, contains the fifth value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value5"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3, out TValue4 value4, out TValue5 value5)
        {
            if (Inner.TryGetValue(key, out MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> moc))
            {
                value1 = moc.Value1;
                value2 = moc.Value2;
                value3 = moc.Value3;
                value4 = moc.Value4;
                value5 = moc.Value5;
                return true;
            }
            value1 = default;
            value2 = default;
            value3 = default;
            value4 = default;
            value5 = default;
            return false;
        }

        private void CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>[] array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> kmvs in this)
                array[index++] = kmvs;
        }

        private bool VerifyKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            return key is TKey;
        }

        private bool VerifyValues(object value1, object value2, object value3, object value4, object value5)
        {
            if (value1 == null && (value1 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value1));
            if (value2 == null && (value2 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value2));
            if (value3 == null && (value3 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value3));
            if (value4 == null && (value4 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value4));
            if (value5 == null && (value5 as Type).IsValueType)
                throw new ArgumentNullException(nameof(value5));
            return value1 is TValue1 && value2 is TValue2 && value3 is TValue3 && value4 is TValue4 && value5 is TValue5;
        }

        /// <summary>
        /// Gets or sets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="KeyNotFoundException"/>, and a set operation creates a new element with the specified key.</returns>
        public MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> this[TKey key]
        {
            get => Inner[key];
            set => Inner[key] = value;
        }

        #region IMultiValueDictionary Implementation
        ICollection IMultiValueDictionary.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection IMultiValueDictionary.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }
        bool IMultiValueDictionary.IsFixedSize => ((IMultiValueDictionary)Inner).IsFixedSize;

        bool IMultiValueDictionary.IsReadOnly => ((IMultiValueDictionary)Inner).IsReadOnly;

        bool IMultiValueDictionary.ContainsKey(object key) => VerifyKey(key) && ContainsKey((TKey)key);

        void IMultiValueDictionary.Remove(object key)
        {
            if (VerifyKey(key))
                Remove((TKey)key);
        }

        bool ICollection.IsSynchronized => ((ICollection)Inner).IsSynchronized;

        object ICollection.SyncRoot => ((ICollection)Inner).SyncRoot;

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array), string.Format(CultureInfo.InvariantCulture, Strings.ObjectMustNotBeNull, nameof(array)));
            if (array.Rank != 1)
                throw new ArgumentException(Strings.MultiDimArrayNotSupported, nameof(array));
            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException(Strings.MultiDimArrayNotSupported, nameof(array));
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException(nameof(index), string.Format(CultureInfo.InvariantCulture, Strings.IndexMustBeNonNegative, nameof(index)));
            if ((array.Length - index) < Count)
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.ArrayPlusOffsetTooSmall, nameof(index), nameof(array)));

            if (array is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>[] setArray)
                CopyTo(setArray, index);
            else if (array is DictionaryEntry[])
            {
                DictionaryEntry[] entryArray = array as DictionaryEntry[];
                foreach (TKey key in Keys)
                    entryArray[index++] = new DictionaryEntry(key, this[key]);
            }
            else
            {
                if (!(array is object[] objArray))
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                try
                {
                    foreach (TKey key in Keys)
                    {
                        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> container = this[key];
                        objArray[index++] = new KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>(key, container.Value1, container.Value2, container.Value3, container.Value4, container.Value5);
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Strings.InvalidArrayType, nameof(array)), nameof(array));
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => Inner.GetEnumerator();
        #endregion IMultiValueDictionary Implementation

        #region IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> Implementation
        ICollection<TKey> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection(this);
                return keys;
            }
        }

        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection(this);
                return values;
            }
        }

        void IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>.Add(object key, object value1, object value2, object value3, object value4, object value5)
        {
            if (!VerifyKey(key))
                throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
            if (!VerifyValues(value1, value2, value3, value4, value5))
                throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, value2 must be of type {typeof(TValue2).Name}, value3 must be of type {typeof(TValue3).Name}, value4 must be of type {typeof(TValue4).Name}, and value5 must be of type {typeof(TValue5).Name}.");
            Add((TKey)key, (TValue1)value1, (TValue2)value2, (TValue3)value3, (TValue4)value4, (TValue5)value5);
        }

        MultiObjectContainer<object, object, object, object, object> IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>.this[object key]
        {
            get
            {
                if (VerifyKey(key))
                {
                    TKey goodKey = (TKey)key;
                    if (Inner.ContainsKey(goodKey))
                    {
                        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> container = Inner[goodKey];
                        return new MultiObjectContainer<object, object, object, object, object>(container.Value1, container.Value2, container.Value3, container.Value4, container.Value5);
                    }
                }
                return null;
            }
            set
            {
                if (!VerifyKey(key))
                    throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
                if (!VerifyValues(value.Value1, value.Value2, value.Value3, value.Value4, value.Value5))
                    throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, value2 must be of type {typeof(TValue2).Name}, value3 must be of type {typeof(TValue3).Name}, value4 must be of type {typeof(TValue4).Name}, and value5 must be of type {typeof(TValue5).Name}.");
                this[(TKey)key] = new MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>((TValue1)value.Value1, (TValue2)value.Value2, (TValue3)value.Value3, (TValue4)value.Value4, (TValue5)value.Value5);
            }
        }

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>.IsReadOnly => ((ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>)Inner).IsReadOnly;

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>.Add(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> kmvs) => Add(kmvs.Key, kmvs.Value1, kmvs.Value2, kmvs.Value3, kmvs.Value4, kmvs.Value5);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>.Contains(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> item)
        {
            if (ContainsKey(item.Key))
            {
                MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> container = this[item.Key];
                return EqualityComparer<TValue1>.Default.Equals(container.Value1, item.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2, item.Value2) && EqualityComparer<TValue3>.Default.Equals(container.Value3, item.Value3) && EqualityComparer<TValue4>.Default.Equals(container.Value4, item.Value4) && EqualityComparer<TValue5>.Default.Equals(container.Value5, item.Value5);
            }
            return false;
        }

        void ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>.CopyTo(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>[] array, int arrayIndex) => CopyTo(array, arrayIndex);

        bool ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>.Remove(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> KeyMultiValueSet)
        {
            if (ContainsKey(KeyMultiValueSet.Key))
            {
                MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> container = this[KeyMultiValueSet.Key];
                if (EqualityComparer<TValue1>.Default.Equals(container.Value1) && EqualityComparer<TValue2>.Default.Equals(container.Value2) && EqualityComparer<TValue3>.Default.Equals(container.Value3) && EqualityComparer<TValue4>.Default.Equals(container.Value4) && EqualityComparer<TValue5>.Default.Equals(container.Value5))
                {
                    Remove(KeyMultiValueSet.Key);
                    return true;
                }
            }
            return false;
        }

        IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>> IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>.GetEnumerator() => ((IEnumerable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>)Inner).GetEnumerator();
        #endregion IMultiValueDictionary<TKey, TValue1, TValue2, TValue3> Implementation

        /// <summary>
        /// Enumerates the elements of a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IDictionaryEnumerator, IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dictionary, int getEnumeratorRetType)
            {
                object[] args = { dictionary.Inner, getEnumeratorRetType };
                enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, args, null);
            }

            /// <summary>
            /// Gets the element at the current position of the enumerator.
            /// </summary>
            public KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> Current
            {
                get
                {
                    KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> kvp = enumerator.Current;
                    return new KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>(kvp);
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
            /// </summary>
            /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext() => enumerator.MoveNext();

            /// <summary>
            /// Releases all resources used by the <see cref="Enumerator"/>.
            /// </summary>
            public void Dispose() => enumerator.Dispose();

            #region IDictionaryEnumerator Implementation
            DictionaryEntry IDictionaryEnumerator.Entry => ((IDictionaryEnumerator)enumerator).Entry;
            object IDictionaryEnumerator.Key => ((IDictionaryEnumerator)enumerator).Key;
            object IDictionaryEnumerator.Value => ((IDictionaryEnumerator)enumerator).Value;
            #endregion IDictionaryEnumerator Implementation

            #region IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation
            object IEnumerator.Current => ((IEnumerator)enumerator).Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            #endregion IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation
        }

        /// <summary>
        /// Represents the collection of keys in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        public sealed class KeyCollection : ICollection, ICollection<TKey>, IEnumerable<TKey>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.KeyCollection keyCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="KeyCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> whose keys are reflected in the new <see cref="KeyCollection"/>.</param>
            public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dictionary) =>
                keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.KeyCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="KeyCollection"/>.
            /// </summary>
            public int Count => keyCollection.Count;

            /// <summary>
            /// Copies the <see cref="KeyCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="KeyCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<TKey> Implementation
            bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
            void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
            void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
            bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
            bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);
            #endregion ICollection<TKey> Implementation

            #region IEnumerable<TKey> Implementation
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
            #endregion IEnumerable<TKey> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="KeyCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<TKey>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public TKey Current => enumerator.Current.Key;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="KeyCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<TKey> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<TKey> Implementation
            }
        }

        /// <summary>
        /// Represents the collection of values in a <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        public sealed class ValueCollection : ICollection, ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>, IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>
        {
            private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.ValueCollection valueCollection;

            /// <summary>
            /// Initializes a new instance of the <see cref="ValueCollection"/>class the reflects the keys in the specified <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
            /// </summary>
            /// <param name="dictionary">The <see cref="MultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> whose keys are reflected in the new <see cref="ValueCollection"/>.</param>
            public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dictionary) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.ValueCollection(dictionary.Inner);

            /// <summary>
            /// Gets the number of elements contained in the <see cref="ValueCollection"/>.
            /// </summary>
            public int Count => valueCollection.Count;

            /// <summary>
            /// Copies the <see cref="ValueCollection"/> elements to an existing one-dimensional <see cref="Array"/>, starting at the specified array index.
            /// </summary>
            /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from the <see cref="ValueCollection"/>. The <see cref="Array"/> must have zero-based indexing.</param>
            /// <param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param>
            public void CopyTo(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>[] array, int index) => valueCollection.CopyTo(array, index);

            #region ICollection Implementation
            bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
            object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
            void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
            #endregion ICollection Implementation

            #region ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).IsReadOnly;
            void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Add(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Add(item);
            void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Clear();
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Contains(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Contains(item);
            bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Remove(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Remove(item);
            #endregion ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation

            #region IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation
            IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
            IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).GetEnumerator();
            #endregion IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation

            /// <summary>
            /// Enumerates the elements of a <see cref="ValueCollection"/>.
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>
            {
                private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator enumerator;

                internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dictionary) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator)Activator.CreateInstance(
                    typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dictionary }, null);

                /// <summary>
                /// Gets the element at the current position of the enumerator.
                /// </summary>
                public MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> Current => enumerator.Current.Value;

                /// <summary>
                /// Advances the enumerator to the next element of the <see cref="ValueCollection"/>.
                /// </summary>
                /// <returns><c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c> if the enumerator has passed the end of the collection.</returns>
                public bool MoveNext() => enumerator.MoveNext();

                /// <summary>
                /// Releases all resources used by the <see cref="Enumerator"/>.
                /// </summary>
                public void Dispose() => enumerator.Dispose();

                #region IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation
                object IEnumerator.Current => enumerator.Current;
                void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
                #endregion IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Implementation
            }
        }
    }
}