using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TACDevel.Runtime.InteropServices
{
    public unsafe struct Pointer<T> : IComparable, IComparable<Pointer<T>>, IEquatable<Pointer<T>>, IFormattable
    {
        private void* _value;

        [CLSCompliant(false)]
        public Pointer(void* value) => _value = value;
        public Pointer(IntPtr value) : this(value.ToPointer()) { }

        public ref T this[int index] => ref AsRef(index);

        public bool IsNull => this == Pointer.Null;
        public bool IsNil => Reference is null;
        public IntPtr Address
        {
            get => (IntPtr)_value;
            set => _value = (void*)value;
        }
        public ref T Reference => ref AsRef();
        public T Value
        {
            get => Read();
            set => Write(value);
        }
        public int ElementSize => Unsafe.SizeOf<T>();

        public ref T AsRef(int elemOffset = 0) => ref Unsafe.AsRef<T>(OffsetFast(elemOffset));
        public Pointer<T> AddressOf(int index) => OffsetFast(index);

        public Pointer<byte> ReadPointer(int elemOffset = 0) => ReadPointer<byte>(elemOffset);
        public Pointer<TType> ReadPointer<TType>(int elemOffset = 0) => Cast<Pointer<TType>>().Read(elemOffset);
        public T Read(int elemOffset = 0) => Unsafe.Read<T>(OffsetFast(elemOffset));
        public object Read<TAny>(int elemOffset = 0) => GetMethod<TAny>(nameof(Read), out object ptr).Invoke(ptr, new object[] { elemOffset });
        public void WritePointer<TType>(Pointer<TType> ptr, int elemOffset = 0) => Cast<Pointer<TType>>().Write(ptr, elemOffset);
        public void Write(T value, int elemOffset = 0) => Unsafe.Write(OffsetFast(elemOffset), value);
        public void Write<TAny>(object value, int elemOffset = 0) => GetMethod<TAny>(nameof(Write), out object ptr).Invoke(ptr, new object[] { value, elemOffset });
        public void WriteAll(IEnumerable<T> enumerable)
        {
            int i = 0;
            using IEnumerator<T> enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
                this[i++] = enumerator.Current;
        }
        public void WriteAll(params T[] values)
        {
            if (values == null) throw new ArgumentNullException();
            if (values.Length <= 0) throw new ArgumentException();
            for (int i = 0; i < values.Length; i++)
                this[i] = values[i];
        }

        public T[] Copy(int elemCnt) => Copy(0, elemCnt);
        public T[] Copy(int startIndex, int elemCnt)
        {
            T[] rg = new T[elemCnt];
            for (int i = startIndex; i < elemCnt + startIndex; i++)
                rg[i - startIndex] = this[i];
            return rg;
        }

        public byte[] CopyBytes(int elemCnt) => Cast().Copy(elemCnt);
        public byte[] CopyBytes(int startIndex, int elemCnt) => Cast().Copy(startIndex, elemCnt);

        public void Clear(int elemCnt = 1)
        {
            for (int i = 0; i < elemCnt; i++)
                this[i] = default;
        }
        public void ClearBytes(int byteCnt)
        {
            Pointer<byte> bytePtr = Cast();
            for (int i = 0; i < byteCnt; i++)
                bytePtr[i] = default;
        }

        public int OffsetIndex(Pointer<byte> current)
        {
            long delta = current.ToInt64() - ToInt64();
            return (int)delta / ElementSize;
        }

        public Pointer<byte> Cast() => Cast<byte>();
        public Pointer<TNew> Cast<TNew>() => new Pointer<TNew>(Address);
        public object CastTo<TAny>() => GetType().GetMethods().First(f => f.Name == nameof(Cast) && f.IsGenericMethod).MakeGenericMethod(typeof(TAny)).Invoke(this, null);

        public Pointer<T> Increment() => Pointer.Increment(this);
        public Pointer<T> Decrement() => Pointer.Decrement(this);
        public Pointer<T> Add(int i) => Pointer.Add(this, i);
        public Pointer<T> Add(long l) => Pointer.Add(this, l);
        public Pointer<T> Add(Pointer<T> p) => Pointer.Add(this, p);
        public Pointer<T> Subtract(int i) => Pointer.Subtract(this, i);
        public Pointer<T> Subtract(long l) => Pointer.Subtract(this, l);
        public Pointer<T> Subtract(Pointer<T> p) => Pointer.Subtract(this, p);
        public int CompareTo(Pointer<T> p) => Pointer.Compare(this, p);
        public int CompareTo(object obj) => Pointer.Compare(this, (Pointer<T>)obj);

        public bool ToBoolean() => Pointer.ToBoolean(this);
        public int ToInt32() => Pointer.ToInt32(this);
        [CLSCompliant(false)]
        public uint ToUInt32() => Pointer.ToUInt32(this);
        public long ToInt64() => Pointer.ToInt64(this);
        [CLSCompliant(false)]
        public ulong ToUInt64() => Pointer.ToUInt64(this);
        public IntPtr ToIntPtr() => Pointer.ToIntPtr(this);
        [CLSCompliant(false)]
        public void* ToPointer() => Pointer.ToPointer(this);
        public Pointer<byte> ToBytePointer() => Pointer.ToBytePointer(this);
        [CLSCompliant(false)]
        public unsafe TUnmanaged* ToPointer<TUnmanaged>() where TUnmanaged : unmanaged => Pointer.ToPointer<T, TUnmanaged>(this);

        public IEnumerator<T> GetEnumerator(int elemCount)
        {
            for (int i = 0; i < elemCount; i++)
                yield return this[i];
        }

        public bool Equals(Pointer<T> p) => Pointer.Equals(this, p);
        public override bool Equals(object obj) => obj is null ? false : obj is Pointer<T> pointer && Equals(pointer);
        public override string ToString() => Pointer.ToString(this, Pointer.DefaultFormat, CultureInfo.CurrentCulture);
        public string ToString(string format) => Pointer.ToString(this, format, CultureInfo.CurrentCulture);
        public string ToString(string format, IFormatProvider formatProvider) => Pointer.ToString(this, format, formatProvider);
        public override int GetHashCode() => unchecked((int)(long)_value);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void* OffsetFast(int elemCnt) => (void*)((long)_value + (elemCnt * ElementSize));

        private MethodInfo GetMethod<TAny>(string name, out object ptr)
        {
            ptr = CastTo<TAny>();
            return ptr.GetType().GetMethod(name);
        }

        public static Pointer<T> operator ++(Pointer<T> p) => p.Increment();
        public static Pointer<T> operator --(Pointer<T> p) => p.Decrement();
        public static Pointer<T> operator +(Pointer<T> left, int right) => left.Add(right);
        public static Pointer<T> operator +(Pointer<T> left, long right) => left.Add(right);
        public static Pointer<T> operator +(Pointer<T> left, Pointer<T> right) => left.Add(right);
        public static Pointer<T> operator -(Pointer<T> left, int right) => left.Subtract(right);
        public static Pointer<T> operator -(Pointer<T> left, long right) => left.Subtract(right);
        public static Pointer<T> operator -(Pointer<T> left, Pointer<T> right) => left.Subtract(right);
        public static bool operator >(Pointer<T> left, Pointer<T> right) => left.CompareTo(right) == -1;
        public static bool operator <(Pointer<T> left, Pointer<T> right) => left.CompareTo(right) == 1;
        public static bool operator >=(Pointer<T> left, Pointer<T> right) => (left.CompareTo(right) == -1) || (left.CompareTo(right) == 0);
        public static bool operator <=(Pointer<T> left, Pointer<T> right) => (left.CompareTo(right) == 1) || (left.CompareTo(right) == 0);
        public static bool operator ==(Pointer<T> left, Pointer<T> right) => left.Equals(right);
        public static bool operator !=(Pointer<T> left, Pointer<T> right) => !left.Equals(right);

        public static explicit operator bool(Pointer<T> p) => Pointer.ToBoolean(p);
        public static explicit operator int(Pointer<T> p) => Pointer.ToInt32(p);
        public static explicit operator Pointer<T>(int i) => Pointer.FromInt32<T>(i);
        [CLSCompliant(false)]
        public static explicit operator uint(Pointer<T> p) => Pointer.ToUInt32(p);
        [CLSCompliant(false)]
        public static explicit operator Pointer<T>(uint ui) => Pointer.FromUInt32<T>(ui);
        public static explicit operator long(Pointer<T> p) => Pointer.ToInt64(p);
        public static explicit operator Pointer<T>(long l) => Pointer.FromInt64<T>(l);
        [CLSCompliant(false)]
        public static explicit operator ulong(Pointer<T> p) => Pointer.ToUInt64(p);
        [CLSCompliant(false)]
        public static explicit operator Pointer<T>(ulong ul) => Pointer.FromUInt64<T>(ul);
        [CLSCompliant(false)]
#pragma warning disable CA2225 // Operator overloads have named alternates
        public static explicit operator void*(Pointer<T> p) => Pointer.ToPointer(p);
#pragma warning restore CA2225 // Operator overloads have named alternates
        [CLSCompliant(false)]
        public static implicit operator Pointer<T>(void* p) => Pointer.FromPointer<T>(p);
        public static explicit operator IntPtr(Pointer<T> p) => Pointer.ToIntPtr(p);
        public static implicit operator Pointer<T>(IntPtr p) => Pointer.FromIntPtr<T>(p);
        public static explicit operator Pointer<byte>(Pointer<T> p) => Pointer.ToBytePointer(p);
        public static implicit operator Pointer<T>(Pointer<byte> p) => Pointer.FromBytePointer<T>(p);
    }
}