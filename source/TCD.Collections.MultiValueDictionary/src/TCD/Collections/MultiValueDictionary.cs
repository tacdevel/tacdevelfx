using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace TCD.Collections
{
    //TODO: MultiValueDictionary<TKey, TValue1, TValue2, TValue3>
    //TODO: MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>
    //TODO: MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>
    public class MultiValueDictionary<TKey, TValue1, TValue2> : IMultiValueDictionary, IMultiValueDictionary<TKey, TValue1, TValue2>, ISerializable, IDeserializationCallback
    {
        private KeyCollection<TKey, TValue1, TValue2> keys;
        private ValueCollection<TKey, TValue1, TValue2> values;

        public MultiValueDictionary() : this(0, null) { }
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2> dict) : this(dict, null) { }
        public MultiValueDictionary(IEqualityComparer<TKey> comparer) : this(0, comparer) { }
        public MultiValueDictionary(int capacity) : this(capacity, null) { }
        public MultiValueDictionary(IMultiValueDictionary<TKey, TValue1, TValue2> dict, IEqualityComparer<TKey> comparer) : this((dict != null) ? dict.Count : 0, comparer)
        {
            if (dict == null) throw new ArgumentNullException(nameof(dict));

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2> kmvs in dict)
                Add(kmvs.Key, kmvs.Value1, kmvs.Value2);
        }
        public MultiValueDictionary(int capacity, IEqualityComparer<TKey> comparer) => Inner = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>(capacity, comparer);

        public KeyCollection<TKey, TValue1, TValue2> Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection<TKey, TValue1, TValue2>(this);
                return keys;
            }
        }
        public ValueCollection<TKey, TValue1, TValue2> Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection<TKey, TValue1, TValue2>(this);
                return values;
            }
        }
        public IEqualityComparer<TKey> Comparer => Inner.Comparer;
        public int Count => Inner.Count;
        internal Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>> Inner { get; }

        public void Add(TKey key, TValue1 value1, TValue2 value2) => Inner.Add(key, new MultiObjectContainer<TValue1, TValue2>(value1, value2));
        public void Clear() => Inner.Clear();
        public bool ContainsKey(TKey key) => Inner.ContainsKey(key);
        public void Get(TKey key, out TValue1 value1, out TValue2 value2)
        {
            value1 = Inner[key].Value1;
            value2 = Inner[key].Value2;
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) => Inner.GetObjectData(info, context);
        public virtual void OnDeserialization(object sender) => Inner.OnDeserialization(sender);
        public bool Remove(TKey key) => Inner.Remove(key);
        public bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2)
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
                throw new ArgumentNullException("array");
            if ((index < 0) || (index > array.Length))
                throw new ArgumentOutOfRangeException("index must be non-negative");
            if ((array.Length - index) < Count)
                throw new ArgumentException("Array plus offset too small");

            foreach (KeyMultiValueSet<TKey, TValue1, TValue2> kdvp in this)
                array[index++] = kdvp;
        }
        private bool VerifyKey(object key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return key is TKey;
        }
        private bool VerifyValues(object value1, object value2)
        {
            if (value1 == null) throw new ArgumentNullException(nameof(value1));
            if (value2 == null) throw new ArgumentNullException(nameof(value2));
            return value1 is TValue1 && value2 is TValue2;
        }
                
        public MultiObjectContainer<TValue1, TValue2> this[TKey key]
        {
            get => Inner[key];
            set => Inner[key] = value;
        }

        #region IMultiValueDictionary<TKey, TValue1, TValue2> Implementation
        ICollection<TKey> IMultiValueDictionary<TKey, TValue1, TValue2>.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection<TKey, TValue1, TValue2>(this);
                return keys;
            }
        }
        ICollection<MultiObjectContainer<TValue1, TValue2>> IMultiValueDictionary<TKey, TValue1, TValue2>.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection<TKey, TValue1, TValue2>(this);
                return values;
            }
        }

        void IMultiValueDictionary<TKey, TValue1, TValue2>.Add(object key, object value1, object value2)
        {
            if (!VerifyKey(key)) throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
            if (!VerifyValues(value1, value2)) throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, and value2 must be of type {typeof(TValue2).Name}");
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
                if (!VerifyKey(key)) throw new ArgumentException($"key must be of type {typeof(TKey).Name}");
                if (!VerifyValues(value.Value1, value.Value2)) throw new ArgumentException($"value1 must be of type {typeof(TValue1).Name}, and value2 must be of type {typeof(TValue2).Name}");
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

        #region IMultiValueDictionary Implementation
        ICollection IMultiValueDictionary.Keys
        {
            get
            {
                if (keys == null)
                    keys = new KeyCollection<TKey, TValue1, TValue2>(this);
                return keys;
            }
        }
        ICollection IMultiValueDictionary.Values
        {
            get
            {
                if (values == null)
                    values = new ValueCollection<TKey, TValue1, TValue2>(this);
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
            if (array == null) throw new ArgumentNullException("array");
            if (array.Rank != 1) throw new ArgumentException("Multi dim not supported");
            if (array.GetLowerBound(0) != 0) throw new ArgumentException("NonZero lower bound");
            if ((index < 0) || (index > array.Length)) throw new ArgumentOutOfRangeException("index must be non-negative");
            if ((array.Length - index) < Count) throw new ArgumentException("Array plus offset too small");

            if (array is KeyMultiValueSet<TKey, TValue1, TValue2>[] pairArray)
                CopyTo(pairArray, index);
            else if (array is DictionaryEntry[])
            {
                DictionaryEntry[] entryArray = array as DictionaryEntry[];
                foreach (TKey key in Keys)
                    entryArray[index++] = new DictionaryEntry(key, this[key]);
            }
            else
            {
                if (!(array is object[] objArray)) throw new ArgumentException("Invalid array type");
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
                    throw new ArgumentException("Invalid array type");
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => Inner.GetEnumerator();
        #endregion IMultiValueDictionary Implementation
        
         [Serializable, StructLayout(LayoutKind.Sequential)]
        internal struct Enumerator : IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2>>, IDictionaryEnumerator
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator _enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2> dict, int getEnumeratorRetType)
            {
                object[] args = { dict.Inner, getEnumeratorRetType };
                _enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator)Activator.CreateInstance(typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator),
                    BindingFlags.NonPublic | BindingFlags.Instance, args, null);
            }

            object IEnumerator.Current => ((IEnumerator)_enumerator).Current;

            KeyMultiValueSet<TKey, TValue1, TValue2> IEnumerator<KeyMultiValueSet<TKey, TValue1, TValue2>>.Current
            {
                get
                {
                    KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2>> kvp = _enumerator.Current;
                    return new KeyMultiValueSet<TKey, TValue1, TValue2>(kvp);
                }
            }

            public void Dispose() => _enumerator.Dispose();

            DictionaryEntry IDictionaryEnumerator.Entry => ((IDictionaryEnumerator)_enumerator).Entry;

            object IDictionaryEnumerator.Key => ((IDictionaryEnumerator)_enumerator).Key;

            public bool MoveNext() => _enumerator.MoveNext();

            void IEnumerator.Reset() => ((IEnumerator)_enumerator).Reset();

            object IDictionaryEnumerator.Value => ((IDictionaryEnumerator)_enumerator).Value;
        }
    }
}