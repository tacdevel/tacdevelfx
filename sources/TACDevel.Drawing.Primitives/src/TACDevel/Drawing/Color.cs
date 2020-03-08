/***************************************************************************************************
 * FileName:             Color.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TACDevel.Drawing
{
    /// <summary>
    /// Represents an ARGB (alpha, red, green, blue) color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Color : IEquatable<Color>
    {
        /// <summary>
        /// Represents a color that is <see langword="null"/>.
        /// </summary>
        public static readonly Color Empty = new Color();

        /// <summary>
        /// Initializes a new <see cref="Color"/> structure from an argb value.
        /// </summary>
        /// <param name="argb">An argb value.</param>
        public Color(long argb) : this(((argb >> 16) & 0xFF) / 255.0, ((argb >> 8) & 0xFF) / 255.0, (argb & 0xFF) / 255.0, ((argb >> 24) & 0xFF) / 255.0) { }

        /// <summary>
        /// Initializes a new <see cref="Color"/> structure from red, green, blue, and optionally alpha component values.
        /// </summary>
        /// <param name="r">The red value.</param>
        /// <param name="g">The green value.</param>
        /// <param name="b">The blue value.</param>
        /// <param name="a">The alpha value.</param>
        public Color(double r, double g, double b, double a = 1.0)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Gets the red component for this <see cref="Color"/> structure.
        /// </summary>
        public double R { get; }

        /// <summary>
        /// Gets the green component for this <see cref="Color"/> structure.
        /// </summary>
        public double G { get; }

        /// <summary>
        /// Gets the blue component for this <see cref="Color"/> structure.
        /// </summary>
        public double B { get; }

        /// <summary>
        /// Gets the alpha component for this <see cref="Color"/> structure.
        /// </summary>
        public double A { get; }

        /// <summary>
        /// Specifies whether this <see cref="Color"/> structure is uninitialized.
        /// </summary>
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public bool Equals(Color other) => R == other.R && G == other.G && B == other.B && A == other.A;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public override bool Equals(object obj) => !(obj is Color) ? false : Equals((Color)obj);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => HashCode.Combine(R, G, B, A);

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"[R: {R}, G: {G}, B: {B}, A: {A}]";

        /// <summary>
        /// Tests whether two specified <see cref="Color"/> structures are equivalent.
        /// </summary>
        /// <param name="left">The <see cref="Color"/> that is to the left of the equality operator.</param>
        /// <param name="right">The <see cref="Color"/> that is to the right of the equality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="Color"/> structures are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Color left, Color right) => left.Equals(right);

        /// <summary>
        /// Tests whether two specified <see cref="Color"/> structures are different.
        /// </summary>
        /// <param name="left">The <see cref="Color"/> that is to the left of the inequality operator.</param>
        /// <param name="right">The <see cref="Color"/> that is to the right of the inequality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="Color"/> structures are different; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Color left, Color right) => !(left == right);
    }
}