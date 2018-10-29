using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace TCD.Collections
{
    [Serializable]
    public sealed class KeyCollection<TKey, TValue1, TValue2> : ICollection<TKey>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.KeyCollection keyCollection;

        public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2> dict) =>
            keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.KeyCollection(dict.InnerDictionary);

        public int Count => keyCollection.Count;

        public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);
        
        void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
        bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
        void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
        void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
        bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
        bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
        bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
        IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct Enumerator : IEnumerator<TKey>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dict }, null);

            public void Dispose() => enumerator.Dispose();
            public bool MoveNext() => enumerator.MoveNext();

            TKey IEnumerator<TKey>.Current => enumerator.Current.Key;
            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
        }
    }

    [Serializable]
    public sealed class KeyCollection<TKey, TValue1, TValue2, TValue3> : ICollection<TKey>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.KeyCollection keyCollection;

        public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dict) =>
            keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.KeyCollection(dict.InnerDictionary);

        public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);

        public int Count => keyCollection.Count;

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
        IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();

        void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
        bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
        void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
        void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
        bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
        bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
        bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct Enumerator : IEnumerator<TKey>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dict }, null);

            public void Dispose() => enumerator.Dispose();
            public bool MoveNext() => enumerator.MoveNext();

            TKey IEnumerator<TKey>.Current => enumerator.Current.Key;
            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
        }
    }

    [Serializable]
    public sealed class KeyCollection<TKey, TValue1, TValue2, TValue3, TValue4> : ICollection<TKey>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.KeyCollection keyCollection;

        public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dict) =>
            keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.KeyCollection(dict.InnerDictionary);

        public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);

        public int Count => keyCollection.Count;

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
        IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();

        void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
        bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
        void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
        void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
        bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
        bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
        bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct Enumerator : IEnumerator<TKey>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dict }, null);

            public void Dispose() => enumerator.Dispose();
            public bool MoveNext() => enumerator.MoveNext();

            TKey IEnumerator<TKey>.Current => enumerator.Current.Key;
            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
        }
    }

    [Serializable]
    public sealed class KeyCollection<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> : ICollection<TKey>, ICollection
    {
        private readonly Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.KeyCollection keyCollection;

        public KeyCollection(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dict) =>
            keyCollection = new Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.KeyCollection(dict.InnerDictionary);

        public void CopyTo(TKey[] array, int index) => keyCollection.CopyTo(array, index);

        public int Count => keyCollection.Count;

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)keyCollection).GetEnumerator();
        IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() => ((IEnumerable<TKey>)keyCollection).GetEnumerator();

        void ICollection.CopyTo(Array array, int index) => ((ICollection)keyCollection).CopyTo(array, index);
        bool ICollection.IsSynchronized => ((ICollection)keyCollection).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)keyCollection).SyncRoot;
        void ICollection<TKey>.Add(TKey item) => ((ICollection<TKey>)keyCollection).Add(item);
        void ICollection<TKey>.Clear() => ((ICollection<TKey>)keyCollection).Clear();
        bool ICollection<TKey>.Contains(TKey item) => ((ICollection<TKey>)keyCollection).Contains(item);
        bool ICollection<TKey>.IsReadOnly => ((ICollection<TKey>)keyCollection).IsReadOnly;
        bool ICollection<TKey>.Remove(TKey item) => ((ICollection<TKey>)keyCollection).Remove(item);

        [Serializable, StructLayout(LayoutKind.Sequential)]
        private struct Enumerator : IEnumerator<TKey>
        {
            private Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator enumerator;

            internal Enumerator(MultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> dict) => enumerator = (Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator)Activator.CreateInstance(
                typeof(Dictionary<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>.Enumerator), BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { dict }, null);

            public void Dispose() => enumerator.Dispose();
            public bool MoveNext() => enumerator.MoveNext();

            TKey IEnumerator<TKey>.Current => enumerator.Current.Key;
            object IEnumerator.Current => enumerator.Current;
            void IEnumerator.Reset() => ((IEnumerator)enumerator).Reset();
        }
    }
}