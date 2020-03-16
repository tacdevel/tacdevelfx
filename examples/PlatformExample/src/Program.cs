using System;
using TACDevel.Runtime;

namespace PlatformExample
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine($@"Platform: {Platform.PlatformType}");
            Console.WriteLine($@"Architecture: {Platform.Architecture.ToString().ToLowerInvariant()}");
            Console.WriteLine($@"Operating System: {Platform.OperatingSystem}");
            Console.WriteLine($@"Operating System Version: {Platform.Version}");
            Console.WriteLine($@"Runtime ID: {Platform.RuntimeID}");
            Console.WriteLine($@"Runtime ID (Generic): {Platform.GenericRuntimeID}");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            _ = Console.ReadKey();
        }
    }
}