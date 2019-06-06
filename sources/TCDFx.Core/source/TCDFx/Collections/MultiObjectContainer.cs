/***************************************************************************************************
 * FileName:             MultiObjectContainer.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using TCDFx.Numerics.Hashing;

namespace TCDFx.Collections
{
    public class MultiObjectContainer<TValue1, TValue2> : IEquatable<MultiObjectContainer<TValue1, TValue2>>
    {
        public MultiObjectContainer(TValue1 value1, TValue2 value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }

        public override bool Equals(object obj) => obj is MultiObjectContainer<TValue1, TValue2> ? Equals((MultiObjectContainer<TValue1, TValue2>)obj) : false;

        public bool Equals(MultiObjectContainer<TValue1, TValue2> moc) =>
            EqualityComparer<TValue1>.Default.Equals(Value1, moc.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, moc.Value2);

        public override int GetHashCode() => this.GenerateHashCode(Value1, Value2);

        public override string ToString() => $"[{Value1}, {Value2}]";

        public static bool operator ==(MultiObjectContainer<TValue1, TValue2> left, MultiObjectContainer<TValue1, TValue2> right) => left.Equals(right);
        public static bool operator !=(MultiObjectContainer<TValue1, TValue2> left, MultiObjectContainer<TValue1, TValue2> right) => !(left == right);
    }

    public class MultiObjectContainer<TValue1, TValue2, TValue3> : IEquatable<MultiObjectContainer<TValue1, TValue2, TValue3>>
    {
        public MultiObjectContainer(TValue1 value1, TValue2 value2, TValue3 value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }
        public TValue3 Value3 { get; set; }

        public override bool Equals(object obj) => obj is MultiObjectContainer<TValue1, TValue2, TValue3> ? Equals((MultiObjectContainer<TValue1, TValue2, TValue3>)obj) : false;

        public bool Equals(MultiObjectContainer<TValue1, TValue2, TValue3> moc) =>
            EqualityComparer<TValue1>.Default.Equals(Value1, moc.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, moc.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, moc.Value3);

        public override int GetHashCode() => this.GenerateHashCode(Value1, Value2, Value3);

        public override string ToString() => $"[{Value1}, {Value2}, {Value3}]";

        public static bool operator ==(MultiObjectContainer<TValue1, TValue2, TValue3> left, MultiObjectContainer<TValue1, TValue2, TValue3> right) => left.Equals(right);
        public static bool operator !=(MultiObjectContainer<TValue1, TValue2, TValue3> left, MultiObjectContainer<TValue1, TValue2, TValue3> right) => !(left == right);
    }

    public class MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> : IEquatable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>>
    {
        public MultiObjectContainer(TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
        }

        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }
        public TValue3 Value3 { get; set; }
        public TValue4 Value4 { get; set; }

        public override bool Equals(object obj) => obj is MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>
                ? Equals((MultiObjectContainer<TValue1, TValue2, TValue3, TValue4>)obj)
                : false;

        public bool Equals(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> moc) =>
            EqualityComparer<TValue1>.Default.Equals(Value1, moc.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, moc.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, moc.Value3) &&
            EqualityComparer<TValue4>.Default.Equals(Value4, moc.Value4);

        public override int GetHashCode() => this.GenerateHashCode(Value1, Value2, Value3, Value4);

        public override string ToString() => $"[{Value1}, {Value2}, {Value3}, {Value4}]";

        public static bool operator ==(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> left, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> right) => left.Equals(right);
        public static bool operator !=(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> left, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4> right) => !(left == right);
    }

    public class MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> : IEquatable<MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>>
    {
        public MultiObjectContainer(TValue1 value1, TValue2 value2, TValue3 value3, TValue4 value4, TValue5 value5)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
        }

        public TValue1 Value1 { get; set; }
        public TValue2 Value2 { get; set; }
        public TValue3 Value3 { get; set; }
        public TValue4 Value4 { get; set; }
        public TValue5 Value5 { get; set; }

        public override bool Equals(object obj) => obj is MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>
                ? Equals((MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5>)obj)
                : false;

        public bool Equals(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> moc) =>
            EqualityComparer<TValue1>.Default.Equals(Value1, moc.Value1) &&
            EqualityComparer<TValue2>.Default.Equals(Value2, moc.Value2) &&
            EqualityComparer<TValue3>.Default.Equals(Value3, moc.Value3) &&
            EqualityComparer<TValue4>.Default.Equals(Value4, moc.Value4) &&
            EqualityComparer<TValue5>.Default.Equals(Value5, moc.Value5);

        public override int GetHashCode() => this.GenerateHashCode(Value1, Value2, Value3, Value4, Value5);

        public override string ToString() => $"[{Value1}, {Value2}, {Value3}, {Value4}, {Value5}]";

        public static bool operator ==(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> left, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> right) => left.Equals(right);
        public static bool operator !=(MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> left, MultiObjectContainer<TValue1, TValue2, TValue3, TValue4, TValue5> right) => !(left == right);
    }
}