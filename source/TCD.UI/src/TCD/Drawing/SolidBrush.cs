/***************************************************************************************************
 * FileName:             SolidBrush.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Runtime.InteropServices;

namespace TCD.Drawing
{
    /// <summary>
    /// Paints an area with a solid color.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class SolidBrush : Brush
    {
        private Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidBrush"/> class.
        /// </summary>
        public SolidBrush() => type = BrushType.Solid;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidBrush"/> class of the specified color..
        /// </summary>
        /// <param name="color">The specified color.</param>
        public SolidBrush(Color color) : this() => Color = color;

        /// <summary>
        /// Gets or sets the <see cref="Drawing.Color"/> of this <see cref="SolidBrush"/>.
        /// </summary>
        public Color Color
        {
            get
            {
                color = new Color(r, g, b, a);
                return color;
            }
            set
            {
                if (color != value)
                {
                    r = value.R;
                    g = value.G;
                    b = value.B;
                    a = value.A;
                    color = value;
                }
            }
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> structure to a <see cref="SolidBrush"/> structure.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to be converted.</param>
        /// <returns>The <see cref="SolidBrush"/> that results from the conversion.</returns>
        public static explicit operator SolidBrush(Color color) => new SolidBrush(color);
    }
}