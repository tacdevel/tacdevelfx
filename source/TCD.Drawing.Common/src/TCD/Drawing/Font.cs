/***************************************************************************************************
 * FileName:             Font.cs
 * Date:                 20180930
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Native;

namespace TCD.Drawing
{
    //TODO: ToString() overrides.
    /// <summary>
    /// Defines a text font.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class Font : IEquatable<Font>
    {
        internal Libui.uiFontDescriptor uiFontDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> structure
        /// </summary>
        /// <param name="family">The specified font family name.</param>
        /// <param name="size">The size of the font.</param>
        /// <param name="weight">The font weight.</param>
        /// <param name="style">The style of the font.</param>
        /// <param name="stretch">The width of the font.</param>
        public Font(string family, double size, FontWeight weight = FontWeight.Normal, FontStyle style = FontStyle.Normal, FontStretch stretch = FontStretch.Normal) => uiFontDescriptor = new Libui.uiFontDescriptor()
        {
            Family = family,
            Size = size,
            Weight = (Libui.uiTextWeight)weight,
            Italic = (Libui.uiTextItalic)style,
            Stretch = (Libui.uiTextStretch)stretch
        };

        internal Font(Libui.uiFontDescriptor desc) => uiFontDescriptor = desc;

        /// <summary>
        /// Gets the font family of this <see cref="Font"/>.
        /// </summary>
        public string Family => uiFontDescriptor.Family;

        /// <summary>
        /// Gets the size of this <see cref="Font"/>.
        /// </summary>
        public double Size => uiFontDescriptor.Size;

        /// <summary>
        /// Gets the weight of this <see cref="Font"/>.
        /// </summary>
        public FontWeight Weight => (FontWeight)uiFontDescriptor.Weight;

        /// <summary>
        /// Gets the style of this <see cref="Font"/>.
        /// </summary>
        public FontStyle Style => (FontStyle)uiFontDescriptor.Italic;

        /// <summary>
        /// Gets the stretch (width) of this <see cref="Font"/>.
        /// </summary>
        public FontStretch Stretch => (FontStretch)uiFontDescriptor.Stretch;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public override bool Equals(object obj) => !(obj is Font) ? false : Equals((Font)obj);

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="font">The font to compare with the current instance.</param>
        /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
        public bool Equals(Font font) => this == font;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => unchecked(this.GenerateHashCode(Family, Size, Weight, Style, Stretch));

        /// <summary>
        /// Tests whether two specified <see cref="Font"/> structures are equivalent.
        /// </summary>
        /// <param name="left">The <see cref="Font"/> that is to the left of the equality operator.</param>
        /// <param name="right">The <see cref="Font"/> that is to the right of the equality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="Font"/> structures are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Font left, Font right) => left.Family == right.Family && left.Size == right.Size && left.Stretch == right.Stretch && left.Style == right.Style && left.Weight == right.Weight;

        /// <summary>
        /// Tests whether two specified <see cref="Font"/> structures are different.
        /// </summary>
        /// <param name="left">The <see cref="Font"/> that is to the left of the inequality operator.</param>
        /// <param name="right">The <see cref="Font"/> that is to the right of the inequality operator.</param>
        /// <returns><see langword="true"/> if the two <see cref="Font"/> structures are different; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Font left, Font right) => !(left == right);
    }
}