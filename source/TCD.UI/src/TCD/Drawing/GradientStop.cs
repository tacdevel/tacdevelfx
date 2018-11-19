/***************************************************************************************************
 * FileName:             GradientStop.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Runtime.InteropServices;

namespace TCD.Drawing
{
    /// <summary>
    /// Represents a color inside of a gradient.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GradientStop
    {
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0032 // Use auto property
        private double pos, r, g, b, a;
#pragma warning restore IDE0032 // Use auto property
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Initializes a new instance of the <see cref="GradientStop"/> structure.
        /// </summary>
        /// <param name="pos">The specified position.</param>
        /// <param name="color">The specified color.</param>
        public GradientStop(double pos, Color color)
        {
            this.pos = pos;
            r = color.R;
            g = color.G;
            b = color.B;
            a = color.A;
        }

        /// <summary>
        /// For most <see cref="GradientBrush"/> objects, this defines where the color stop will start or end. (0 = start, 1 = end). For <see cref="RadialGradientBrush"/> objects, 0 is the center at the start point and 1 is the circle with the outer radius and the end center.
        /// </summary>
        public double Position { get => pos; set => pos = value; }

        /// <summary>
        /// the color of the color stop.
        /// </summary>
        public Color Color
        {
            get => new Color(r, g, b, a);
            set
            {
                r = value.R;
                g = value.G;
                b = value.B;
                a = value.A;
            }
        }
    }
}