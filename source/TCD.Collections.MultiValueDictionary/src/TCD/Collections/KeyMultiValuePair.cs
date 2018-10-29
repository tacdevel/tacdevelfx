using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TCD.Collections
{
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct KeyMultiValueSet<TKey, TValue1, TValue2> : IEquatable<KeyMultiValueSet<TKey, TValue1, TValue2>>
    {
        public KeyMultiValueSet(TKey key, TValue1 value1, TValue2 value2) : this()
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
        }

        public KeyMultiValueSet(KeyValuePair<TKey, MultiObjectContainer<TValue1, TValue2>> kvp) : this(kvp.Key, kvp.Value.Value1, kvp.Value.Value2) { }

        public TKey Key { get; set; }
        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is KeyMultiValueSet<TKey, TValue1, TValue2>)
                return Equals((KeyMultiValueSet<TKey, TValue1, TValue2>)obj);
            return false;
        }

        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2> kmvp) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvp.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvp.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvp.Value2);

        public override int GetHashCode() => this.GenerateHashCode("Key", "Value1", "Value2");

        public override string ToString() => $"[{Key}: {Value1}, {Value2}]";

        public static bool operator ==(KeyMultiValueSet<TKey, TValue1, TValue2> left, KeyMultiValueSet<TKey, TValue1, TValue2> right) => left.Equals(right);
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

        public override bool Equals(object obj)
        {
            if (obj is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>)
                return Equals((KeyMultiValueSet<TKey, TValue1, TValue2, TValue3>)obj);
            return false;
        }

        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3> kmvp) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvp.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvp.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvp.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, kmvp.Value3);

        public override int GetHashCode() => this.GenerateHashCode("Key", "Value1", "Value2", "Value3");

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

        public override bool Equals(object obj)
        {
            if (obj is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>)
                return Equals((KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4>)obj);
            return false;
        }

        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4> kmvp) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvp.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvp.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvp.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, kmvp.Value3) &&
            EqualityComparer<TValue4>.Default.Equals(Value4, kmvp.Value4);

        public override int GetHashCode() => this.GenerateHashCode("Key", "Value1", "Value2", "Value3", "Value4");

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

        public override bool Equals(object obj)
        {
            if (obj is KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>)
                return Equals((KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5>)obj);
            return false;
        }

        public bool Equals(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> kmvp) =>
            EqualityComparer<TKey>.Default.Equals(Key, kmvp.Key) &&
            EqualityComparer<TValue1>.Default.Equals(Value1, kmvp.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, kmvp.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, kmvp.Value3) &&
            EqualityComparer<TValue4>.Default.Equals(Value4, kmvp.Value4) &&
            EqualityComparer<TValue5>.Default.Equals(Value5, kmvp.Value5);

        public override int GetHashCode() => this.GenerateHashCode("Key", "Value1", "Value2", "Value3", "Value4", "Value5");

        public override string ToString() => $"[{Key}: {Value1}, {Value2}, {Value3}, {Value4}, {Value5}]";

        public static bool operator ==(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> right) => left.Equals(right);
        public static bool operator !=(KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> left, KeyMultiValueSet<TKey, TValue1, TValue2, TValue3, TValue4, TValue5> right) => !(left == right);
    }
}