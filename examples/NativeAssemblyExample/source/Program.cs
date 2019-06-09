using System;
using System.Runtime.InteropServices;
using TCD.InteropServices;

namespace NativeAssemblyExample
{
    internal class Program
    {
        public static void Main() => Kernel32.LoadFunction<Beep>()(2000, 400);

        private static NativeAssembly Kernel32 => new NativeAssembly("kernel32.dll");

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate bool Beep(uint frequency, uint duration);
    }
}