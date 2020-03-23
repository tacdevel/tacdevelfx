/***********************************************************************************************************************
 * FileName:            SizeD.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TACDevel.Drawing
{
    /// <summary>
    /// Represents an ordered pair of double-precision floating-point numbers that defines a size in a two-dimensional plane.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SizeD : IEquatable<SizeD>
    {
        /// <summary>
        /// Represents a <see cref="SizeD"/> that has <see cref="Width"/> and <see cref="Height"/> values set to zero.
        /// </summary>
        public static readonly SizeD Empty = new SizeD(0.0, 0.0);

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeD"/> class with the specified dimensions.
        /// </summary>
        /// <param name="w">The width component of the point.</param>
        /// <param name="h">The height component of the point.</param>
        public SizeD(double w, double h)
        {
            Width = w;
            Height = h;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SizeD"/> structure from a <see cref="PointD"/>.
        /// </summary>
        /// <param name="pt">A <see cref="PointD"/> that specifies the dimensions for the new <see cref="SizeD"/>.</param>
        public SizeD(PointD pt) : this(pt.X, pt.X) { }

        /// <summary>
        /// Gets or sets the width of this <see cref="SizeD"/>.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height of this <see cref="SizeD"/>.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="SizeD"/> is empty.
        /// </summary>
        public bool IsEmpty => this == Empty;

        /// <summary>
        /// Adds the specified <see cref="SizeD"/> to the other specified <see cref="SizeD"/>.
        /// </summary>
        /// <param name="sz1">The <see cref="SizeD"/> to be added to.</param>
        /// <param name="sz2">The <see cref="SizeD"/> to add to <paramref name="sz1"/>.</param>
        /// <returns>The <see cref="SizeD"/> that is the result of the addition operation.</returns>
        public static SizeD Add(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width + sz2.Width, sz1.Height + sz2.Height);

        /// <summary>
        /// Subtracts the specified <see cref="SizeD"/> from the other specified <see cref="SizeD"/>.
        /// </summary>
        /// <param name="sz1">The <see cref="SizeD"/> to be subtracted from.</param>
        /// <param name="sz2">The <see cref="SizeD"/> to subtract from <paramref name="sz1"/>.</param>
        /// <returns>The <see cref="SizeD"/> that is the result of the addition operation.</returns>
        public static SizeD Subtract(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width - sz2.Width, sz1.Height - sz2.Height);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public bool Equals(SizeD size) => Width == size.Width && Height == size.Height;

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><see langword="true"/> if the specified object is equal to the current object; otherwise, <see langword="false"/>.
        public override bool Equals(object obj) => !(obj is SizeD) ? false : Equals((SizeD)obj);

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => unchecked(HashCode.Combine(Width, Height));

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => $"[Width: {Width}, Height: {Height}]";

        /// <summary>
        /// Adds the specified <see cref="SizeD"/> to the other specified <see cref="SizeD"/>.
        /// </summary>
        /// <param name="sz1">The <see cref="SizeD"/> to be added to.</param>
        /// <param name="sz2">The <see cref="SizeD"/> to add to <paramref name="sz1"/>.</param>
        /// <returns>The <see cref="SizeD"/> that is the result of the addition operation.</returns>
        public static SizeD operator +(SizeD sz1, SizeD sz2) => Add(sz1, sz2);

        /// <summary>
        /// Subtracts the specified <see cref="SizeD"/> from the other specified <see cref="SizeD"/>.
        /// </summary>
        /// <param name="sz1">The <see cref="SizeD"/> to be subtracted from.</param>
        /// <param name="sz2">The <see cref="SizeD"/> to subtract from <paramref name="sz1"/>.</param>
        /// <returns>The <see cref="SizeD"/> that is the result of the subtraction operation.</returns>
        public static SizeD operator -(SizeD sz1, SizeD sz2) => Subtract(sz1, sz2);

        /// <summary>
        /// Multiplies the specified <see cref="SizeD"/> by the other specified <see cref="SizeD"/>.
        /// </summary>
        /// <param name="sz1">The first <see cref="SizeD"/> to be multiplied.</param>
        /// <param name="sz2">The <see cref="SizeD"/> to be multiplied by <paramref name="sz1"/>.</param>
        /// <returns>The <see cref="SizeD"/> that is the result of the multiplication operation.</returns>
        public static SizeD operator *(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width * sz2.Width, sz1.Height * sz2.Height);

        /// <summary>
        /// Divides the specified <see cref="SizeD"/> by the other specified <see cref="SizeD"/>.
        /// </summary>
        /// <param name="sz1">The <see cref="SizeD"/> to be divided.</param>
        /// <param name="sz2">The <see cref="SizeD"/> to divide <paramref name="sz1"/> by.</param>
        /// <returns>The <see cref="SizeD"/> that is the result of the multiplication operation.</returns>
        public static SizeD operator /(SizeD sz1, SizeD sz2) => new SizeD(sz1.Width / sz2.Width, sz1.Height / sz2.Height);

        /// <summary>
        /// Tests whether two specified <see cref="SizeD"/> structures are equivalent.
        /// </summary>
        /// <param name="left">The <see cref="SizeD"/> that is to the left of the equality operator.</param>
        /// <param name="right">The <see cref="SizeD"/> that is to the right of the equality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="SizeD"/> structures are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(SizeD left, SizeD right) => left.Equals(right);

        /// <summary>
        /// Tests whether two specified <see cref="SizeD"/> structures are different.
        /// </summary>
        /// <param name="left">The <see cref="SizeD"/> that is to the left of the inequality operator.</param>
        /// <param name="right">The <see cref="SizeD"/> that is to the right of the inequality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="SizeD"/> structures are different; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(SizeD left, SizeD right) => !(left == right);

        /// <summary>
        /// Converts the specified <see cref="SizeD"/> structure to a <see cref="PointD"/> structure.
        /// </summary>
        /// <param name="sz">The <see cref="SizeD"/> to be converted.</param>
        /// <returns>The <see cref="PointD"/> that results from the conversion.</returns>
        public static explicit operator PointD(SizeD sz) => new PointD(sz);

        /// <summary>
        /// Converts the specified <see cref="SizeD"/> structure to a <see cref="SizeD"/> structure.
        /// </summary>
        /// <param name="sz">The <see cref="SizeD"/> to be converted.</param>
        /// <returns>The <see cref="SizeD"/> that results from the conversion.</returns>
        public static explicit operator Size(SizeD sz) => Size.Round(sz);
    }
}