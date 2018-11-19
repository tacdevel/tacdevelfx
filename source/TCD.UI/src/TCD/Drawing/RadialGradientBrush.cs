/***************************************************************************************************
 * FileName:             RadialGradientBrush.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Runtime.InteropServices;

namespace TCD.Drawing
{
    /// <summary>
    /// Paints an area with a radial gradient. A focal point defines the beginning of the gradient, and a circle defines the end point of the gradient.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class RadialGradientBrush : GradientBrush
    {
        private PointD origin, center = new PointD(0.5, 0.5);

        /// <summary>
        /// Initializes a new instance of the <see cref="RadialGradientBrush"/> class.
        /// </summary>
        public RadialGradientBrush() => type = BrushType.RadialGradient;

        /// <summary>
        /// Gets or sets the location of the two-dimensional focal point that defines the beginning of the gradient.
        /// </summary>
        public PointD GradientOrigin
        {
            get
            {
                origin = new PointD(X0, Y0);
                return origin;
            }
            set
            {
                if (origin != value)
                {
                    X0 = value.X;
                    Y0 = value.Y;
                    origin = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the center of the outermost circle of the radial gradient.
        /// </summary>
        public PointD Center
        {
            get
            {
                center = new PointD(X0, Y0);
                return center;
            }
            set
            {
                if (center != value)
                {
                    X0 = value.X;
                    Y0 = value.Y;
                    center = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the radius of the outermost circle of a radial gradient.
        /// </summary>
        public double OuterRadius
        {
            get => outerRadius;
            set => outerRadius = value;
        }
    }
}