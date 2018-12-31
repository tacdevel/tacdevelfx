/***************************************************************************************************
 * FileName:             IMultiValueDictionary.cs
 * Date:                 20181029
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Collections;
using System.Collections.Generic;

namespace TCD.Collections
{
    /// <summary>
    /// Represents a non-generic collection of key/multi-value sets
    /// </summary>
    public interface IMultiValueDictionary : ICollection
    {
        /// <summary>
        /// Gets an <see cref="ICollection"/> containing the keys of the <see cref="IMultiValueDictionary"/>.
        /// </summary>
        ICollection Keys { get; }

        /// <summary>
        /// Gets an <see cref="ICollection"/> containing the values in the <see cref="IMultiValueDictionary"/>.
        /// </summary>
        ICollection Values { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IMultiValueDictionary"/> object has a fixed size.
        /// </summary>
        bool IsFixedSize { get; }

        /// <summary>
        /// Gets a value indicating whether the <see cref="IMultiValueDictionary"/> object is read-only.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Removes all elements from the <see cref="IMultiValueDictionary"/> object.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether the <see cref="IMultiValueDictionary"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IMultiValueDictionary"/>.</param>
        /// <returns><c>true</c> if the <see cref="IMultiValueDictionary"/> contains an element with the key; otherwise, <c>false</c>.</returns>
        bool ContainsKey(object key);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IMultiValueDictionary"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if <c>key</c> was not found in the original <see cref="IMultiValueDictionary"/>.</returns>
        void Remove(object key);
    }

    /// <summary>
    /// Represents a generic collection of key/multi-value sets.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the dictionary.</typeparam>
    /// <typeparam name="TValue2">The type of the second values of the dictionary.</typeparam>
    public interface IMultiValueDictionary<TKey, TValue1, TValue2>
        : ICollection<KeyMultiValueSet<TKey, TValue1, TValue2>>
    {
        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the keys of the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        ICollection<TKey> Keys { get; }

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the values in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        ICollection<MultiObjectContainer<TValue1, TValue2>> Values { get; }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        void Add(object key, object value1, object value2);

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        void Add(TKey key, TValue1 value1, TValue2 value2);

        /// <summary>
        /// Determines whether the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>.</param>
        /// <returns><c>true</c> if the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/> contains an element with the key; otherwise, <c>false</c>.</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if <c>key</c> was not found in the original <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/>.</returns>
        bool Remove(TKey key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose values to get.</param>
        /// <param name="value1">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the object that implements <see cref="IMultiValueDictionary{TKey, TValue1, TValue2}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2);

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2}"/> containing the elements values.</returns>
        MultiObjectContainer<object, object> this[object key] { get; set; }

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2}"/> containing the elements values.</returns>
        MultiObjectContainer<TValue1, TValue2> this[TKey key] { get; set; }
    }

    /// <summary>
    /// Represents a generic collection of key/multi-value sets.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the dictionary.</typeparam>
    /// <typeparam name="TValue2">The type of the second values of the dictionary.</typeparam>
    /// <typeparam name="TValue3">The type of the third values of the dictionary.</typeparam>
    public interface IMultiValueDictionary<TKey, TValue1, TValue2, TValue3>
        : ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>
    {
        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the keys of the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        ICollection<TKey> Keys { get; }

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the values in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3>> Values { get; }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        /// <param name="value3">The object to use as the value of the third element to add.</param>
        void Add(object key, object value1, object value2, object value3);

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        /// <param name="value3">The object to use as the value of the third element to add.</param>
        void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3);

        /// <summary>
        /// Determines whether the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</param>
        /// <returns><c>true</c> if the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> contains an element with the key; otherwise, <c>false</c>.</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if <c>key</c> was not found in the original <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/>.</returns>
        bool Remove(TKey key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose values to get.</param>
        /// <param name="value1">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value3">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value3"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the object that implements <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3);

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2, TValue3}"/> containing the elements values.</returns>
        MultiObjectContainer<object, object, object> this[object key] { get; set; }

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2, TValue3}"/> containing the elements values.</returns>
        MultiObjectContainer<TValue1, TValue2, TValue3> this[TKey key] { get; set; }
    }

    /// <summary>
    /// Represents a generic collection of key/multi-value sets.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the dictionary.</typeparam>
    /// <typeparam name="TValue2">The type of the second values of the dictionary.</typeparam>
    /// <typeparam name="TValue3">The type of the third values of the dictionary.</typeparam>
    /// <typeparam name="TValue4">The type of the fourth values of the dictionary.</typeparam>
    public interface IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4>
        : ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>
    {
        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the keys of the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        ICollection<TKey> Keys { get; }

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the values in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> Values { get; }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        /// <param name="value3">The object to use as the value of the third element to add.</param>
        /// <param name="value4">The object to use as the value of the fourth element to add.</param>
        void Add(object key, object value1, object value2, object value3, object value4);

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        /// <param name="value3">The object to use as the value of the third element to add.</param>
        /// <param name="value4">The object to use as the value of the fourth element to add.</param>
        void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4);

        /// <summary>
        /// Determines whether the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</param>
        /// <returns><c>true</c> if the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> contains an element with the key; otherwise, <c>false</c>.</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if <c>key</c> was not found in the original <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</returns>
        bool Remove(TKey key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose values to get.</param>
        /// <param name="value1">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value3">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value3"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value4">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value4"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the object that implements <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3, out TValue4 value4);

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2, TValue3, TValue4}"/> containing the elements values.</returns>
        MultiObjectContainer<object, object, object, object> this[object key] { get; set; }

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2, TValue3, TValue4}"/> containing the elements values.</returns>
        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> this[TKey key] { get; set; }
    }

    /// <summary>
    /// Represents a generic collection of key/multi-value sets.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue1">The type of the first values in the dictionary.</typeparam>
    /// <typeparam name="TValue2">The type of the second values of the dictionary.</typeparam>
    /// <typeparam name="TValue3">The type of the third values of the dictionary.</typeparam>
    /// <typeparam name="TValue4">The type of the fourth values of the dictionary.</typeparam>
    /// <typeparam name="TValue5">The type of the fifth values of the dictionary.</typeparam>
    public interface IMultiValueDictionary<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>
        : ICollection<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>
    {
        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the keys of the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        ICollection<TKey> Keys { get; }

        /// <summary>
        /// Gets an <see cref="ICollection{T}"/> containing the values in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        ICollection<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> Values { get; }

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        /// <param name="value3">The object to use as the value of the third element to add.</param>
        /// <param name="value4">The object to use as the value of the fourth element to add.</param>
        /// <param name="value5">The object to use as the value of the fifth element to add.</param>
        void Add(object key, object value1, object value2, object value3, object value4, object value5);

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>
        /// </summary>
        /// <param name="key">The object to use as the key of the elements to add.</param>
        /// <param name="value1">The object to use as the value of the first element to add.</param>
        /// <param name="value2">The object to use as the value of the second element to add.</param>
        /// <param name="value3">The object to use as the value of the third element to add.</param>
        /// <param name="value4">The object to use as the value of the fourth element to add.</param>
        /// <param name="value5">The object to use as the value of the fifth element to add.</param>
        void Add(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5);

        /// <summary>
        /// Determines whether the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.</param>
        /// <returns><c>true</c> if the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> contains an element with the key; otherwise, <c>false</c>.</returns>
        bool ContainsKey(TKey key);

        /// <summary>
        /// Removes the element with the specified key from the <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>. This method also returns <c>false</c> if <c>key</c> was not found in the original <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4}"/>.</returns>
        bool Remove(TKey key);

        /// <summary>
        /// Gets the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose values to get.</param>
        /// <param name="value1">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value1"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value2">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value2"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value3">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value3"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value4">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value4"/> parameter. This parameter is passed uninitialized.</param>
        /// <param name="value5">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value5"/> parameter. This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the object that implements <see cref="IMultiValueDictionary{TKey, TValue1, TValue2, TValue3, TValue4, TValue5}"/> contains an element with the specified key; otherwise, <c>false</c>.</returns>
        bool TryGetValues(TKey key, out TValue1 value1, out TValue2 value2, out TValue3 value3, out TValue4 value4, out TValue5 value5);

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2, TValue3, TValue4, TValue5}"/> containing the elements values.</returns>
        MultiObjectContainer<object, object, object, object, object> this[object key] { get; set; }

        /// <summary>
        /// Gets or sets the element values with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns> a <see cref="MultiObjectContainer{TValue1, TValue2, TValue3, TValue4, TValue5}"/> containing the elements values.</returns>
        MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> this[TKey key] { get; set; }
    }
}