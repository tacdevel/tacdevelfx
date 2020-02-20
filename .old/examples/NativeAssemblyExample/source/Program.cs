using System;
using System.Runtime.InteropServices;
using TCDFx.Runtime.InteropServices;

namespace NativeAssemblyExample
{
    //TODO: Make this cross-platform.
    //TODO: Add support for the embedded and Dependency asembly types.
    internal class Program
    {
        static Program() => Kernel32 = new NativeAssembly("kernel32.dll");

        public static void Main()
        {

            Console.WriteLine("Calling 'Beep()'...");
            Kernel32.LoadFunction<Beep>()(1500, 400);
            Console.WriteLine("Done.");

            Console.WriteLine("Calling 'Boop()'...");
            Kernel32.LoadFunction<Boop>("Beep")(1000, 400);
            Console.WriteLine("Done.");
        }

        private static NativeAssembly Kernel32 { get; }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate bool Beep(uint frequency, uint duration);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)] private delegate bool Boop(uint frequency, uint duration);
    }
}