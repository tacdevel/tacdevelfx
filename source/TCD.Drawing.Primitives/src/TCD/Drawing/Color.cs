/****************************************************************************
 * FileName:   Color.cs
 * Assembly:   TCD.Drawing.Primitives.dll
 * Package:    TCD.Drawing.Primitives
 * Date:       20180913
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCD.Drawing
{
    //TODO: Maybe add CYMK/HSL/HSV color(s) too.
    //TODO: ToString() overrides.
    [StructLayout(LayoutKind.Sequential)]
    public sealed class Color : IEquatable<Color>
    {
        public Color(long argb) : this(((argb >> 16) & 0xFF) / 255.0, ((argb >> 8) & 0xFF) / 255.0, (argb & 0xFF) / 255.0, ((argb >> 24) & 0xFF) / 255.0) { }

        public Color(double r, double g, double b, double a = 1.0)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public double R { get; }
        public double G { get; }
        public double B { get; }
        public double A { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Color))
                return false;
            return Equals((Color)obj);
        }

        public bool Equals(Color color) => R == color.R && G == color.G && B == color.B && A == color.A;

        public override int GetHashCode() => unchecked(this.GenerateHashCode());

        public static bool operator ==(Color left, Color right) => left.Equals(right);
        public static bool operator !=(Color left, Color right) => !(left == right);

        //TODO: Move the following operator to SolidBrush.cs in TCD.Drawing.SolidBrush.
        // public static explicit operator SolidBrush(Color color) => new SolidBrush(color);
    }
}