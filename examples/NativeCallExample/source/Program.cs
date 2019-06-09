using TCDFx.InteropServices;

namespace NativeCallExample
{
    internal class Program
    {
        internal static void Main()
        {
            NativeCalls.Load();
            Beep(2000, 400);
        }

        [NativeCall("kernel32.dll")]
        private static extern bool Beep(uint frequency, uint duration);
    }
}