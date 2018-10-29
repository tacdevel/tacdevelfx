using System.Collections;
using System.Collections.Generic;

namespace TCD.Collections
{
    public interface IMultiValueDictionary : ICollection
    {
        ICollection Keys { get; }
        ICollection Values { get; }
        bool IsFixedSize { get; }
        bool IsReadOnly { get; }

        void Clear();
        bool ContainsKey(object key);
        void Remove(object key);
    }

    public interface IMultiValueDictionary<TKey, TValue1, TValue2>
        : ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        ICollection<TKey> Keys { get; }
        ICollection<MultiObjectContainer<TValue1, TValue2>> Values { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        void Add(object key, object value1, object value2);
        void Add(TKey key, TValue1 value1, TValue2 value2);
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2);

        MultiObjectContainer<object, object> this[object key] { get; set; }
        MultiObjectContainer<TValue1, TValue2> this[TKey key] { get; set; }
    }

    public interface IMultiValueDictionary<TKey, TValue1, TValue2, TValue3>
        : IMultiValueDictionary, ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        ICollection<TKey> Keys { get; }
        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>> Values { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        void Add(object key, object value1, object value2, object value3);
        void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3);
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3);

        MultiObjectContainer<object, object, object> this[object key] { get; set; }
        MultiObjectContainer<TValue1, TValue2, TValue3> this[TKey key] { get; set; }
    }

    public interface IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>
        : IMultiValueDictionary, ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        ICollection<TKey> Keys { get; }
        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Values { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        void Add(object key, object value1, object value2, object value3, object value4);
        void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue3 value4);
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3, out TValue3 value4);

        MultiObjectContainer<object, object, object, object> this[object key] { get; set; }
        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> this[TKey key] { get; set; }
    }

    public interface IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>
        : IMultiValueDictionary, ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>
    {
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        ICollection<TKey> Keys { get; }
        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Values { get; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword

        void Add(object key, object value1, object value2, object value3, object value4, object value5);
        void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue3 value4, TValue5 value5);
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3, out TValue3 value4, out TValue5 value5);

        MultiObjectContainer<object, object, object, object, object> this[object key] { get; set; }
        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> this[TKey key] { get; set; }
    }
}