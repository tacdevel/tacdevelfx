using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TCD.Collections
{
    [Serializable]
    public sealed class ValueCollection<TKey, TValue1, TValue2> : ICollection<MultiObjectContainer<TValue1, TValue2>>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.ValueCollection valueCollection;

        public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2> dict) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.ValueCollection(dict.InnerDictionary);

        public int Count => valueCollection.Count;

        bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
        void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).IsReadOnly;
        void ICollection<MultiObjectContainer<TValue1, TValue2>>.Add(MultiObjectContainer<TValue1, TValue2> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Add(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Clear();
        bool ICollection<MultiObjectContainer<TValue1, TValue2>>.Contains(MultiObjectContainer<TValue1, TValue2> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Contains(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2>>.CopyTo(MultiObjectContainer<TValue1, TValue2>[] array, int index) => valueCollection.CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2>>.Remove(MultiObjectContainer<TValue1, TValue2> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2>>)valueCollection).Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
        IEnumerator<MultiObjectContainer<TValue1, TValue2>> IEnumerable<MultiObjectContainer<TValue1, TValue2>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2>>)valueCollection).GetEnumerator();

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2>>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dict }, null);
            
            public bool MoveNext() => enumerator.MoveNext();
            public void Dispose() => enumerator.Dispose();

            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            MultiObjectContainer<TValue1, TValue2> IEnumerator<MultiObjectContainer<TValue1, TValue2>>.Current => enumerator.Current.Value;
        }
    }

    [Serializable]
    public sealed class ValueCollection<TKey, TValue1, TValue2, TValue3> : ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.ValueCollection valueCollection;

        public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dict) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.ValueCollection(dict.InnerDictionary);

        public int Count => valueCollection.Count;

        bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
        void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).IsReadOnly;
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Add(MultiObjectContainer<TValue1, TValue2, TValue3> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Add(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Clear();
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Contains(MultiObjectContainer<TValue1, TValue2, TValue3> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Contains(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.CopyTo(MultiObjectContainer<TValue1, TValue2, TValue3>[] array, int index) => valueCollection.CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>.Remove(MultiObjectContainer<TValue1, TValue2, TValue3> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
        IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3>> IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3>>)valueCollection).GetEnumerator();

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3>>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dict }, null);
            
            public bool MoveNext() => enumerator.MoveNext();
            public void Dispose() => enumerator.Dispose();

            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            MultiObjectContainer<TValue1, TValue2, TValue3> IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3>>.Current => enumerator.Current.Value;
        }
    }

    [Serializable]
    public sealed class ValueCollection<TKey, TValue1, TValue2, TValue3, TValue4> : ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.ValueCollection valueCollection;

        public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dict) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.ValueCollection(dict.InnerDictionary);

        public int Count => valueCollection.Count;

        bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
        void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).IsReadOnly;
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Add(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Add(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Clear();
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Contains(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Contains(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.CopyTo(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>[] array, int index) => valueCollection.CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Remove(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
        IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>)valueCollection).GetEnumerator();

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dict }, null);
            
            public bool MoveNext() => enumerator.MoveNext();
            public void Dispose() => enumerator.Dispose();

            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Current => enumerator.Current.Value;
        }
    }

    [Serializable]
    public sealed class ValueCollection<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> : ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.ValueCollection valueCollection;

        public ValueCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dict) => valueCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.ValueCollection(dict.InnerDictionary);

        public int Count => valueCollection.Count;

        bool ICollection.IsSynchronized => ((ICollection)valueCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)valueCollection).SyncRoot;
        void ICollection.CopyTo(Array array, int index) => ((ICollection)valueCollection).CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.IsReadOnly => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).IsReadOnly;
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Add(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Add(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Clear() => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Clear();
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Contains(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Contains(item);
        void ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.CopyTo(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>[] array, int index) => valueCollection.CopyTo(array, index);
        bool ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Remove(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> item) => ((ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)valueCollection).GetEnumerator();
        IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.GetEnumerator() => ((IEnumerable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>)valueCollection).GetEnumerator();

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { dict }, null);
            
            public bool MoveNext() => enumerator.MoveNext();
            public void Dispose() => enumerator.Dispose();

            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
            MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> IEnumerator<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Current => enumerator.Current.Value;
        }
    }
}