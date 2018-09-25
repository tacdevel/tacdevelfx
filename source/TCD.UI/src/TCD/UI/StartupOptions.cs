/****************************************************************************
 * FileName:   StartupOptions.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180921
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCD.UI
{
    [StructLayout(LayoutKind.Sequential)]
    internal class StartupOptions : IEquatable<StartupOptions>
    {
        private UIntPtr size;

        public StartupOptions() : this(0) { }
        public StartupOptions(uint size) => Size = size;
        public StartupOptions(UIntPtr size) => this.size = size;

        public uint Size
        {
            get => (uint)size;
            private set => size = new UIntPtr(value);
        }

        public bool Equals(StartupOptions options) => size == options.size;

        public override bool Equals(object obj) => (obj is StartupOptions) && Equals((StartupOptions)obj);

        public override int GetHashCode() => this.GenerateHashCode();

        public override string ToString() => size.ToString();
    }
}