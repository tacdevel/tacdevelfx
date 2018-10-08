﻿/****************************************************************************
 * FileName:   SolidBrush.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181002
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

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
    }
}