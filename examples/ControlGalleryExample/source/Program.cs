using System;
using TCD.UI;

namespace ControlGalleryExample
{
    internal class Program
    {
        [STAThread]
        private static void Main() => new Application().Run(new MainWindow());
    }
}