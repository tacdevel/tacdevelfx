using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace TACDevel.Runtime.InteropServices
{
    /// <summary>
    /// Represents a class that can be represented as a native pointer in memory.
    /// </summary>
    /// <typeparam name="T">The type that the pointer represents.</typeparam>
    public unsafe partial struct NativePtr<T>
        : IComparable, IComparable<NativePtr<T>>, IEquatable<NativePtr<T>>, IFormattable
    {
        private void* _value; // This MUST be the only field.

        /// <summary>
        /// Initializes a new instance of the <see cref="NativePtr{T}"/> class.
        /// </summary>
        /// <param name="value">The value represented as a <see cref="void"/> pointer.</param>
        [CLSCompliant(false)]
        public NativePtr(void* value) => _value = value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NativePtr{T}"/> class.
        /// </summary>
        /// <param name="value">The value represented as an <see cref="IntPtr"/>.</param>
        public NativePtr(IntPtr value) : this(value.ToPointer()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativePtr{T}"/> class.
        /// </summary>
        /// <param name="value">The value represented as an <see cref="int"/>.</param>
        public NativePtr(int value) : this((void*)value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativePtr{T}"/> class.
        /// </summary>
        /// <param name="value">The value represented as a <see cref="uint"/>.</param>
        [CLSCompliant(false)]
        public NativePtr(uint value) : this((void*)value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativePtr{T}"/> class.
        /// </summary>
        /// <param name="value">The value represented as a <see cref="long"/>.</param>
        public NativePtr(long value) : this((void*)value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativePtr{T}"/> class.
        /// </summary>
        /// <param name="value">The value represented as a <see cref="ulong"/>.</param>
        [CLSCompliant(false)]
        public NativePtr(ulong value) : this((void*)value) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NativePtr{T}"/> class.
        /// </summary>
        /// <param name="value">
        /// The value represented as a <see cref="NativePtr{T}"/>, where <typeparamref name="T"/> is represented as a
        /// <see cref="byte"/>.
        /// </param>
        public NativePtr(NativePtr<byte> value) : this(value.ToPointer()) { }

        /// <summary>
        /// Gets the reference at the specified index of <see cref="Address"/>.
        /// </summary>
        /// <param name="index">The index of the reference.</param>
        /// <returns>The reference.</returns>
        public ref T this[int index] => ref AsRef(index);

        /// <summary>
        /// Gets a value determining whether <see cref="Address"/> is <see langword="null"/>.
        /// </summary>
        /// <seealso cref="IntPtr.Zero"/>
        public bool IsNull => Address == null;

        /// <summary>
        /// Gets a value determining whether <see cref="Value"/> is <see langword="null"/>.
        /// </summary>
        public bool IsNil => Reference is null;

        /// <summary>
        /// Gets the size of <typeparamref name="T"/>.
        /// </summary>
        public int ElementSize => Unsafe.SizeOf<T>();

        /// <summary>
        /// Gets or sets the address this <see cref="NativePtr{T}"/> points to.
        /// </summary>
        public IntPtr Address
        {
            get => (IntPtr)_value;
            set => _value = (void*)value;
        }

        /// <summary>
        /// Gets the value of this <see cref="NativePtr{T}"/> as a reference of <typeparamref name="T"/>.
        /// </summary>
        public ref T Reference => ref AsRef();

        /// <summary>
        /// Gets for sets the dereferenced value of this <see cref=" NativePtr{T}"/>.
        /// </summary>
        public T Value
        {
            get => Read();
            set => Write(value);
        }

        public ref T AsRef(int index = 0) => ref Unsafe.AsRef<T>(OffsetFast(index));
        public NativePtr<T> AddressOf(int index) => OffsetFast(index);

        public NativePtr<byte> ReadPtr(int offset = 0) => ReadPtr<byte>(offset);
        public NativePtr<TCast> ReadPtr<TCast>(int offset = 0) => Cast<NativePtr<TCast>>().Read(offset);
        public T Read(int elemOffset = 0) => Unsafe.Read<T>(OffsetFast(elemOffset));
        public object Read<TAny>(int elemOffset = 0) => GetMethod<TAny>(nameof(Read), out object ptr).Invoke(ptr, new object[] { elemOffset });
        public void WritePtr<TCast>(NativePtr<TCast> ptr, int offset = 0) => Cast<NativePtr<TCast>>().Write(ptr, offset);
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
            NativePtr<byte> bytePtr = Cast();
            for (int i = 0; i < byteCnt; i++)
                bytePtr[i] = default;
        }

        /// <summary>
        /// Returns the element index of a pointer relative to <see cref="Address"/>.
        /// </summary>
        /// <param name="current">The pointer to return an index from.</param>
        /// <returns>The element index.</returns>
        public int OffsetIndex(NativePtr<byte> current)
        {
            long delta = current.ToInt64() - ToInt64();
            return (int)delta / ElementSize;
        }

        public NativePtr<byte> Cast() => Cast<byte>();
        public NativePtr<TNew> Cast<TNew>() => new NativePtr<TNew>(Address);
        public object CastTo<TAny>() => GetType().GetMethods().First(f => f.Name == nameof(Cast) && f.IsGenericMethod).MakeGenericMethod(typeof(TAny)).Invoke(this, null);

        public IEnumerator<T> GetEnumerator(int elemCount)
        {
            for (int i = 0; i < elemCount; i++)
                yield return this[i];
        }

        /// <summary>
        /// Compares this <see cref="NativePtr{T}"/> to a specified <see cref="NativePtr{T}"/> and returns an indication of their relative values.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int CompareTo(NativePtr<T> p) => ToInt64().CompareTo(p.ToInt64());
        public int CompareTo(object obj) => CompareTo((NativePtr<T>)obj);

        /// <summary>
        /// Determines whether the specified <see cref="NativePtr{T}"/> is equal to this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="obj">The <see cref="NativePtr{T}"/> to compare with te current <see cref="NativePtr{T}"/>.</param>
        /// <returns><see langword="true"/> if the specified <see cref="NativePtr{T}"/> is equal to this <see cref="NativePtr{T}"/>; otherwise, <see langword="false"/>.</returns>
        public bool Equals(NativePtr<T> p) => Address == p.Address;

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this <see cref="NativePtr{T}"/>.</param>
        /// <returns><see langword="true"/> if the specified <see cref="object"/> is equal to this <see cref="NativePtr{T}"/>; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj) => obj is null ? false : obj is NativePtr<T> pointer && Equals(pointer);

        /// <summary>
        /// Returns the hash code for this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this <see cref="NativePtr{T}"/>.</returns>
        public override int GetHashCode() => unchecked((int)(long)_value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void* OffsetFast(int elemCnt) => (void*)((long)_value + (elemCnt * ElementSize));
        private static string ToStringSafe(NativePtr<T> ptr, IFormatProvider formatProvider)
        {
            if (ptr.IsNull) return @"(null)";
            if (ptr.IsNil) return @"(nil)";

            bool isInteger = Type.GetTypeCode(typeof(T)) switch
            {
                TypeCode.Byte => true,
                TypeCode.SByte => true,
                TypeCode.UInt16 => true,
                TypeCode.Int16 => true,
                TypeCode.UInt32 => true,
                TypeCode.Int32 => true,
                TypeCode.UInt64 => true,
                TypeCode.Int64 => true,
                _ => false
            };

            if (isInteger)
                return $@"{ptr.Reference}";

            if (!typeof(T).IsValueType)
            {
                NativePtr<byte> heapPtr = ptr.ReadPtr();
                string valueStr = heapPtr.IsNull
                    ? @"(null)"
                    : ((typeof(T).GetInterface(nameof(IList)) != null) && !(typeof(T) == typeof(string)))
                        ? $"[{string.Join(',', (IEnumerable)ptr.Reference)}]"
                        : ptr.Reference == null ? @"(null)" : ptr.Reference.ToString();
                return string.Format(formatProvider, "{0} ({1})", valueStr, heapPtr.ToString(StringFormats.Pointer, formatProvider));
            }
            return ptr.Reference.ToString();
        }
        private MethodInfo GetMethod<TAny>(string name, out object ptr)
        {
            ptr = CastTo<TAny>();
            return ptr.GetType().GetMethod(name);
        }

        public static bool operator ==(NativePtr<T> left, NativePtr<T> right) => left.Equals(right);
        public static bool operator !=(NativePtr<T> left, NativePtr<T> right) => !left.Equals(right);
        public static bool operator >(NativePtr<T> left, NativePtr<T> right) => left.CompareTo(right) == -1;
        public static bool operator <(NativePtr<T> left, NativePtr<T> right) => left.CompareTo(right) == 1;
        public static bool operator >=(NativePtr<T> left, NativePtr<T> right) => (left.CompareTo(right) == -1) || (left.CompareTo(right) == 0);
        public static bool operator <=(NativePtr<T> left, NativePtr<T> right) => (left.CompareTo(right) == 1) || (left.CompareTo(right) == 0);

        private static class StringFormats
        {
            internal const string Default = Pointer;
            internal const string Object = "O";
            internal const string Array = "RG";
            internal const string Integer = "N";
            internal const string Both = "B";
            internal const string Pointer = "P";
        }
    }

    // NativePtr.Arithmetic.cs
    public unsafe partial struct NativePtr<T>
    {
        /// <summary>
        /// Adds an offset of 1 to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the addition of 1 to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Increment() => Add(1);

        /// <summary>
        /// Sbutracts an offset of 1 to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the subtraction of 1 to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Decrement() => Subtract(1);

        /// <summary>
        /// Adds an offset to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="i">The offset to add.</param>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the addition of <paramref name="i"/> to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Add(int i) => OffsetFast(i);

        /// <summary>
        /// Adds an offset to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="l">The offset to add.</param>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the addition of <paramref name="l"/> to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Add(long l) => new NativePtr<T>((void*)(ToInt64() + l));

        /// <summary>
        /// Adds an offset to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="i">The offset to add.</param>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the addition of <paramref name="p"/> to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Add(NativePtr<T> p) => new NativePtr<T>((void*)(ToInt64() + p.ToInt64()));

        /// <summary>
        /// Subtracts an offset to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="i">The offset to subtract.</param>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the subtraction of <paramref name="i"/> to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Subtract(int i) => Add(-i);

        /// <summary>
        /// Subtracts an offset to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="l">The offset to subtract.</param>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the subtraction of <paramref name="l"/> to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Subtract(long l) => new NativePtr<T>((void*)(ToInt64() - l));

        /// <summary>
        /// Subtracts an offset to the value of this <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <param name="i">The offset to subtract.</param>
        /// <returns>A new <see cref="NativePtr{T}"/> that reflects the subtraction of <paramref name="p"/> to this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<T> Subtract(NativePtr<T> p) => new NativePtr<T>((void*)(ToInt64() - p.ToInt64()));
        public static NativePtr<T> operator ++(NativePtr<T> p) => p.Increment();
        public static NativePtr<T> operator --(NativePtr<T> p) => p.Decrement();
        public static NativePtr<T> operator +(NativePtr<T> left, int right) => left.Add(right);
        public static NativePtr<T> operator +(NativePtr<T> left, long right) => left.Add(right);
        public static NativePtr<T> operator +(NativePtr<T> left, NativePtr<T> right) => left.Add(right);
        public static NativePtr<T> operator -(NativePtr<T> left, int right) => left.Subtract(right);
        public static NativePtr<T> operator -(NativePtr<T> left, long right) => left.Subtract(right);
        public static NativePtr<T> operator -(NativePtr<T> left, NativePtr<T> right) => left.Subtract(right);

    }

    // NativePtr.Conversion.cs
    public unsafe partial struct NativePtr<T>
    {

        /// <summary>
        /// Returns a <see cref="bool"/> that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A <see cref="bool"/> that represents this <see cref="NativePtr{T}"/>.</returns>
        public bool ToBoolean() => !IsNil;

        /// <summary>
        /// Returns an <see cref="int"/> that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>An <see cref="int"/> that represents this <see cref="NativePtr{T}"/>.</returns>
        public int ToInt32() => (int)Address.ToPointer();

        /// <summary>
        /// Returns a <see cref="uint"/> that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A <see cref="uint"/> that represents this <see cref="NativePtr{T}"/>.</returns>
        [CLSCompliant(false)]
        public uint ToUInt32() => (uint)Address.ToPointer();

        /// <summary>
        /// Returns a <see cref="long"/> that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A <see cref="long"/> that represents this <see cref="NativePtr{T}"/>.</returns>
        public long ToInt64() => (long)Address.ToPointer();

        /// <summary>
        /// Returns a <see cref="ulong"/> that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A <see cref="ulong"/> that represents this <see cref="NativePtr{T}"/>.</returns>
        [CLSCompliant(false)]
        public ulong ToUInt64() => (ulong)Address.ToPointer();

        /// <summary>
        /// Returns an <see cref="IntPtr"/> that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>An <see cref="IntPtr"/> that represents this <see cref="NativePtr{T}"/>.</returns>
        public IntPtr ToIntPtr() => Address;

        /// <summary>
        /// Returns a <see cref="void"/> pointer that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A <see cref="void"/> pointer that represents this <see cref="NativePtr{T}"/>.</returns>
        [CLSCompliant(false)]
        public void* ToPointer() => Address.ToPointer();

        /// <summary>
        /// Returns a <see cref="NativePtr{T}"/>, where <typeparamref name="T"/> is <see cref="byte"/>, that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A <see cref="NativePtr{T}"/>, where <typeparamref name="T"/> is <see cref="byte"/>, that represents this <see cref="NativePtr{T}"/>.</returns>
        public NativePtr<byte> ToBytePointer() => ToPointer();

        /// <summary>
        /// Returns a pointer of the specified type that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <typeparam name="TUnmanaged">The type of pointer.</typeparam>
        /// <returns>A pointer of the specified type that represents this current <see cref="NativePtr{T}"/>.</returns>
        [CLSCompliant(false)]
        public unsafe TUnmanaged* ToPointer<TUnmanaged>() where TUnmanaged : unmanaged => (TUnmanaged*)ToPointer();

        /// <summary>
        /// Returns a <see cref="string"/> that represents this current <see cref="NativePtr{T}"/>.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this <see cref="NativePtr{T}"/>.</returns>
        public override string ToString() => ToString(null, CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns a <see cref="string"/> that represents this current <see cref="NativePtr{T}"/> using the specified format.
        /// </summary>
        /// <param name="format">The format to use. A null reference will use the default format.</param>
        /// <returns>A <see cref="string"/> that represents this <see cref="NativePtr{T}"/> in the specified format.</returns>
        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns a <see cref="string"/> that represents this current <see cref="NativePtr{T}"/> using the specified format.
        /// </summary>
        /// <param name="format">The format to use. A null reference will use the default format.</param>
        /// <param name="formatProvider">The provider to use to format the value. A null reference will use <see cref="CultureInfo.CurrentCulture"/>.</param>
        /// <returns>A <see cref="string"/> that represents this <see cref="NativePtr{T}"/> in the specified format.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = StringFormats.Default;
            format = format.ToUpperInvariant();

            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            NativePtr<T> self = this;

            return format switch
            {
                StringFormats.Array => formatArray(),
                StringFormats.Integer => ToInt64().ToString(formatProvider),
                StringFormats.Object => ToStringSafe(this, formatProvider),
                StringFormats.Pointer => $@"0x{ToInt64().ToString("X", formatProvider)}",
                StringFormats.Both => formatBoth(),
                _ => ToStringSafe(this, formatProvider)
            };

            string formatBoth()
            {
                string str = ToStringSafe(self, formatProvider);
                string typeNameStr = typeof(T) == typeof(char) ? "char*" : typeof(T).Name;
                return $@"[0x{self.ToInt64().ToString("X", formatProvider)}]:{typeNameStr}: {(str.Contains(Environment.NewLine, StringComparison.InvariantCulture) ? $"{Environment.NewLine}str" : str)}";
            }

            string formatArray()
            {
                string[] a = new string[6];
                for (int i = 0; i < a.Length; i++)
                    a[i] = ToStringSafe(self.AddressOf(i), formatProvider);
                return string.Join(',', a);
            }
        }

        public static explicit operator bool(NativePtr<T> p) => p.ToBoolean();
        public static explicit operator int(NativePtr<T> p) => p.ToInt32();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static explicit operator NativePtr<T>(int i) => new NativePtr<T>(i);
        [CLSCompliant(false)]
        public static explicit operator uint(NativePtr<T> p) => p.ToUInt32();
        [CLSCompliant(false)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static explicit operator NativePtr<T>(uint ui) => new NativePtr<T>(ui);
        public static explicit operator long(NativePtr<T> p) => p.ToInt64();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static explicit operator NativePtr<T>(long l) => new NativePtr<T>(l);
        [CLSCompliant(false)]
        public static explicit operator ulong(NativePtr<T> p) => p.ToUInt64();
        [CLSCompliant(false)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static explicit operator NativePtr<T>(ulong ul) => new NativePtr<T>(ul);
        [CLSCompliant(false)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static explicit operator void*(NativePtr<T> p) => p.ToPointer();
        [CLSCompliant(false)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static implicit operator NativePtr<T>(void* p) => new NativePtr<T>(p);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static explicit operator IntPtr(NativePtr<T> p) => p.ToIntPtr();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static implicit operator NativePtr<T>(IntPtr p) => new NativePtr<T>(p);
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static explicit operator NativePtr<byte>(NativePtr<T> p) => p.ToBytePointer();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225:Operator overloads have named alternates", Justification = "<Pending>")]
        public static implicit operator NativePtr<T>(NativePtr<byte> p) => new NativePtr<T>(p);
    }
}