/***************************************************************************************************
 * FileName:             KeyMultiValueSet.cs
 * Date:                 20181029
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TCD.Collections
{
    /// <summary>
    /// Defines a key/multi-value set with two values that can be set or retrieved.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue1">The type of the first value.</typeparam>
    /// <typeparam name="TValue2">The type of the second value.</typeparam>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public readonly struct KeyMultiValueSet<TKey, TValue1, TValue2> : IEquatable<KeyMultiValueSet<TKey, TValue1, TValue2>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> structure with the specified key and values.
        /// </summary>
        /// <param name="key">The object defined in each key/multi-value set.</param>
        /// <param name="value1">The first definition associated with <paramref name="key"/>.</param>
        /// <param name="value2">The second definition associated with <paramref name="key"/>.</param>
        public KeyMultiValueSet(TKey key, TValue1 value1, TValue2 value2) : this()
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> structure with the specified key and values from a <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <param name="kvp"> The <see cref="KeyValuePair{TKey, TValue}"/> to copy the key and values from.</param>
        public KeyMultiValueSet(KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2>> kvp) : this(kvp.Key, kvp.Value.Value1, kvp.Value.Value2) { }

        /// <summary>
        /// Gets the key in the key/multi-value set.
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Gets the first value in the key/multi-value set.
        /// </summary>
        public TValue1 Value1 { get; }

        /// <summary>
        /// Gets the second value in the key/multi-value set.
        /// </summary>
        public TValue2 Value2 { get; }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj) => obj is KeyMultiValueSet<TKey, TValue1, TValue2> ? Equals((KeyMultiValueSet<TKey, TValue1, TValue2>)obj) : false;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="kmvs">The point to compare with the current instance.</param>
        /// <returns>true if <paramref name="kmvs"/> and this instance represent the same value; otherwise, false.</returns>
        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2> kmvs) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvs.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvs.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvs.Value2);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => this.GenerateHashCode(Key, Value1, Value2);

        /// <summary>
        /// Returns a string representation of the <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/>, using the string representations of the key and values.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"[{Key}: {Value1}, {Value2}]";

        /// <summary>
        /// Tests whether two specified <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> structures are equivalent.
        /// </summary>
        /// <param name="left">The <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> that is to the left of the equality operator.</param>
        /// <param name="right">The <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> that is to the right of the equality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> structures are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(KeyMultiValueSet<TKey, TValue1, TValue2> left, KeyMultiValueSet<TKey, TValue1, TValue2> right) => left.Equals(right);

        /// <summary>
        /// Tests whether two specified <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> structures are inequivalent.
        /// </summary>
        /// <param name="left">The <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> that is to the left of the inequality operator.</param>
        /// <param name="right">The <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> that is to the right of the inequality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="KeyMultiValueSet{TKey, TValue1, TValue2}"/> structures are unequal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(KeyMultiValueSet<TKey, TValue1, TValue2> left, KeyMultiValueSet<TKey, TValue1, TValue2> right) => !(left == right);
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> : IEquatable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>>
    {
        public KeyMultiValueSet(TKey key, TValue1 value1, TValue2 value2, TValue3 value3) : this()
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        public KeyMultiValueSet(KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2, TValue3>> kvp) : this(kvp.Key, kvp.Value.Value1, kvp.Value.Value2, kvp.Value.Value3) { }

        public TKey Key { get; set; }
        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }
        public TValue3 Value3 { get; set; }

        public override bool Equals(object obj) => obj is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> ? Equals((KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>)obj) : false;

        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> kmvs) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvs.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvs.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvs.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, kmvs.Value3);

        public override int GetHashCode() => this.GenerateHashCode(Key, Value1, Value2, Value3);

        public override string ToString() => $"[{Key}: {Value1}, {Value2}, {Value3}]";

        public static bool operator ==(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> right) => left.Equals(right);
        public static bool operator !=(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> right) => !(left == right);
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> : IEquatable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>>
    {
        public KeyMultiValueSet(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4) : this()
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
        }

        public KeyMultiValueSet(KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>> kvp) : this(kvp.Key, kvp.Value.Value1, kvp.Value.Value2, kvp.Value.Value3, kvp.Value.Value4) { }

        public TKey Key { get; set; }
        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }
        public TValue3 Value3 { get; set; }
        public TValue4 Value4 { get; set; }

        public override bool Equals(object obj) => obj is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>
                ? Equals((KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>)obj)
                : false;

        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> kmvs) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvs.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvs.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvs.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, kmvs.Value3) &&
            EqualityComparer<TValue4>.Default.Equals(Value4, kmvs.Value4);

        public override int GetHashCode() => this.GenerateHashCode(Key, Value1, Value2, Value3, Value4);

        public override string ToString() => $"[{Key}: {Value1}, {Value2}, {Value3}, {Value4}]";

        public static bool operator ==(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> right) => left.Equals(right);
        public static bool operator !=(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> right) => !(left == right);
    }

    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> : IEquatable<KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>>
    {
        public KeyMultiValueSet(TKey key, TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5) : this()
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
        }

        public KeyMultiValueSet(KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>> kvp) : this(kvp.Key, kvp.Value.Value1, kvp.Value.Value2, kvp.Value.Value3, kvp.Value.Value4, kvp.Value.Value5) { }

        public TKey Key { get; set; }
        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }
        public TValue3 Value3 { get; set; }
        public TValue4 Value4 { get; set; }
        public TValue5 Value5 { get; set; }

        public override bool Equals(object obj) => obj is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>
                ? Equals((KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>)obj)
                : false;

        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> kmvs) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvs.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvs.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvs.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, kmvs.Value3) &&
            EqualityComparer<TValue4>.Default.Equals(Value4, kmvs.Value4) &&
            EqualityComparer<TValue5>.Default.Equals(Value5, kmvs.Value5);

        public override int GetHashCode() => this.GenerateHashCode(Key, Value1, Value2, Value3, Value4, Value5);

        public override string ToString() => $"[{Key}: {Value1}, {Value2}, {Value3}, {Value4}, {Value5}]";

        public static bool operator ==(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> right) => left.Equals(right);
        public static bool operator !=(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> right) => !(left == right);
    }
}