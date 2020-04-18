using System;
using System.Collections;
using System.Globalization;

namespace TACDevel.Runtime.InteropServices
{
    public static unsafe class Pointer
    {
        public static readonly Pointer<byte> Null = null;

        public static string DefaultFormat { get; set; } = StringFormats.Pointer;
        public static int ArrayCount { get; set; } = 6;

        public static Pointer<T> Increment<T>(Pointer<T> ptr) => Add(ptr, 1);
        public static Pointer<T> Decrement<T>(Pointer<T> ptr) => Subtract(ptr, 1);
        public static Pointer<T> Add<T>(Pointer<T> p, int i) => p.OffsetFast(i);
        public static Pointer<T> Add<T>(Pointer<T> left, long right) => new Pointer<T>((void*)(ToInt64(left) + right));
        public static Pointer<T> Add<T>(Pointer<T> left, Pointer<T> right) => new Pointer<T>((void*)(ToInt64(left) + ToInt64(right)));
        public static Pointer<T> Subtract<T>(Pointer<T> p, int i) => Add(p, -i);
        public static Pointer<T> Subtract<T>(Pointer<T> left, long right) => new Pointer<T>((void*)(ToInt64(left) - right));
        public static Pointer<T> Subtract<T>(Pointer<T> left, Pointer<T> right) => new Pointer<T>((void*)(ToInt64(left) - ToInt64(right)));
        public static int Compare<T>(Pointer<T> left, Pointer<T> right) => ToInt64(left).CompareTo(ToInt64(right));
        public static bool Equals<T>(Pointer<T> left, Pointer<T> right) => left.Address == right.Address;

        public static Pointer<T> FromInt32<T>(int i) => new Pointer<T>((void*)i);
        [CLSCompliant(false)]
        public static Pointer<T> FromUInt32<T>(uint ui) => new Pointer<T>((void*)ui);
        public static Pointer<T> FromInt64<T>(long l) => new Pointer<T>((void*)l);
        [CLSCompliant(false)]
        public static Pointer<T> FromUInt64<T>(ulong ul) => new Pointer<T>((void*)ul);
        [CLSCompliant(false)]
        public static Pointer<T> FromIntPtr<T>(IntPtr ptr) => new Pointer<T>(ptr);
        [CLSCompliant(false)]
        public static Pointer<T> FromPointer<T>(void* ptr) => new Pointer<T>(ptr);
        public static Pointer<T> FromBytePointer<T>(Pointer<byte> ptr) => ToPointer(ptr);
        public static bool ToBoolean<T>(Pointer<T> ptr) => !ptr.IsNil;
        public static int ToInt32<T>(Pointer<T> ptr) => (int)ToPointer(ptr);
        [CLSCompliant(false)]
        public static uint ToUInt32<T>(Pointer<T> ptr) => (uint)ToPointer(ptr);
        public static long ToInt64<T>(Pointer<T> ptr) => (long)ToPointer(ptr);
        [CLSCompliant(false)]
        public static ulong ToUInt64<T>(Pointer<T> ptr) => (ulong)ToPointer(ptr);
        public static IntPtr ToIntPtr<T>(Pointer<T> ptr) => ptr.Address;
        [CLSCompliant(false)]
        public static unsafe void* ToPointer<T>(Pointer<T> ptr) => ptr.Address.ToPointer();
        public static Pointer<byte> ToBytePointer<T>(Pointer<T> ptr) => ToPointer(ptr);
        [CLSCompliant(false)]
        public static unsafe TUnmanaged* ToPointer<T, TUnmanaged>(Pointer<T> ptr) where TUnmanaged : unmanaged => (TUnmanaged*)ToPointer(ptr);

        public static string ToString<T>(Pointer<T> ptr) => ToString(ptr, null);
        public static string ToString<T>(Pointer<T> ptr, string format) => ToString(ptr, format, CultureInfo.CurrentCulture);
        public static string ToString<T>(Pointer<T> ptr, string format, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(format))
                format = DefaultFormat;
            format = format.ToUpperInvariant();

            if (provider == null)
                provider = CultureInfo.CurrentCulture;

            return format switch
            {
                StringFormats.Array => formatArray(),
                StringFormats.Integer => ptr.ToInt64().ToString(provider),
                StringFormats.Object => ToStringSafe(ptr),
                StringFormats.Pointer => $@"0x{ptr.ToInt64().ToString("X", provider)}",
                StringFormats.Both => formatBoth(),
                _ => ToStringSafe(ptr)
            };

            string formatBoth()
            {
                string str = ToStringSafe(ptr);
                string typeNameStr = typeof(T) == typeof(char) ? "char*" : typeof(T).Name;
                return $@"[0x{ptr.ToInt64().ToString("X", provider)}]:{typeNameStr}: {(str.Contains(Environment.NewLine, StringComparison.InvariantCulture) ? $"{Environment.NewLine}str" : str)}";
            }

            string formatArray()
            {
                string[] a = new string[ArrayCount];
                for (int i = 0; i < a.Length; i++)
                    a[i] = ToStringSafe(ptr.AddressOf(i));
                return string.Join(',', a);
            }
        }

        internal static string ToStringSafe<T>(Pointer<T> ptr)
        {
            if (ptr.IsNull)
                return @"(null)";

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
                return $@"{ptr.Reference} ({long.Parse(ptr.Reference.ToString())})";

            if (!typeof(T).IsValueType)
            {
                Pointer<byte> heapPtr = ptr.ReadPointer();
                string valueStr;

                if (heapPtr.IsNull)
                    valueStr = @"(null)";
                else
                {
                    valueStr = ((typeof(T).GetInterface(nameof(IList)) != null) && !(typeof(T) == typeof(string)))
                        ? $"[{string.Join(',', (IEnumerable)ptr.Reference)}]"
                        : ptr.Reference == null ? @"(null)" : ptr.Reference.ToString();
                }

                return string.Format("{0} ({1})", valueStr, heapPtr.ToString(StringFormats.Pointer));
            }


            return ptr.Reference.ToString();
        }

        private static class StringFormats
        {
            internal const string Object = "O";
            internal const string Array = "RG";
            internal const string Integer = "N";
            internal const string Both = "B";
            internal const string Pointer = "P";
        }
    }

    /*
     * RepoName: RazorSharp
     * RepoURL: https://github.com/Decimation/RazorSharp
     * License: Unknown/None
     * CommitID: dd99e98a6be6232714e02142445f10d84f9b288d
     * FileURLS:
     * https://github.com/Decimation/RazorSharp/blob/master/RazorSharp/Memory/Pointers/Pointer.cs
     * https://github.com/Decimation/RazorSharp/blob/master/RazorSharp/Memory/Pointers/Handle.cs
     */
}