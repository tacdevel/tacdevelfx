using System;
using TCD.UI;

namespace SimpleWindowExample
{
    internal class Program
    {
        [STAThread]
        public static void Main() => new Application().Run(new Window());
    }
}