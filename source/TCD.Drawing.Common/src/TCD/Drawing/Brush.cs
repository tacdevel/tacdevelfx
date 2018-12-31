/***************************************************************************************************
 * FileName:             Brush.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TCD.Native;

namespace TCD.Drawing
{
    /// <summary>
    /// Defines objects used to paint graphical objects. Classes that derive from <see cref="Brush"/> describe how the area is painted.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public abstract class Brush
    {
        internal Libui.uiDrawBrush uiDrawBrush;

        /// <summary>
        /// Initializes a new instance of the <see cref="Brush"/> class.
        /// </summary>
        protected Brush() => uiDrawBrush = new Libui.uiDrawBrush();
    }

    /// <summary>
    /// An abstract class that describes a gradient, composed of gradient stops. Classes that inherit from <see cref="GradientBrush"/> describe different ways of interpreting gradient stops.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public abstract class GradientBrush : Brush
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GradientBrush"/> class.
        /// </summary>
        protected GradientBrush() : base() { }

        // linear: start X
        // radial: start X
        protected internal double X0
        {
            get => uiDrawBrush.x0;
            set => uiDrawBrush.x0 = value;
        }

        // linear: start Y
        // radial: start Y
        protected internal double Y0
        {
            get => uiDrawBrush.y0;
            set => uiDrawBrush.y0 = value;
        }

        // linear: end X
        // radial: outer circle center X
        protected internal double X1
        {
            get => uiDrawBrush.x1;
            set => uiDrawBrush.x1 = value;
        }

        // linear: end Y
        // radial: outer circle center Y
        protected internal double Y1
        {
            get => uiDrawBrush.y1;
            set => uiDrawBrush.y1 = value;
        }

        /// <summary>
        /// Gets or sets the brush's gradient stops.
        /// </summary>
        public List<GradientStop> GradientStops
        {
            set
            {
                if (value != null && value.Count != 0)
                {
                    uiDrawBrush.numStops = (UIntPtr)value.Count;
                    uiDrawBrush.stops = Marshal.UnsafeAddrOfPinnedArrayElement(value.ToArray(), 0);
                }
            }
        }
    }

    /// <summary>
    /// Represents a color inside of a gradient.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct GradientStop
    {
        internal Libui.uiDrawBrushGradientStop uiDrawBrushGradientStop;

        /// <summary>
        /// Initializes a new instance of the <see cref="GradientStop"/> structure.
        /// </summary>
        /// <param name="pos">The specified position.</param>
        /// <param name="color">The specified color.</param>
        public GradientStop(double pos, Color color) => uiDrawBrushGradientStop = new Libui.uiDrawBrushGradientStop()
        {
            pos = pos,
            r = color.R,
            g = color.G,
            b = color.B,
            a = color.A
        };

        /// <summary>
        /// For most <see cref="GradientBrush"/> objects, this defines where the color stop will start or end. (0 = start, 1 = end). For <see cref="RadialGradientBrush"/> objects, 0 is the center at the start point and 1 is the circle with the outer radius and the end center.
        /// </summary>
        public double Position { get => uiDrawBrushGradientStop.pos; set => uiDrawBrushGradientStop.pos = value; }

        /// <summary>
        /// the color of the color stop.
        /// </summary>
        public Color Color
        {
            get => new Color(uiDrawBrushGradientStop.r, uiDrawBrushGradientStop.g, uiDrawBrushGradientStop.b, uiDrawBrushGradientStop.a);
            set
            {
                uiDrawBrushGradientStop.r = value.R;
                uiDrawBrushGradientStop.g = value.G;
                uiDrawBrushGradientStop.b = value.B;
                uiDrawBrushGradientStop.a = value.A;
            }
        }
    }

    /// <summary>
    /// Paints an area with a linear gradient.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class LinearGradientBrush : GradientBrush
    {
        private PointD start = new PointD();
        private PointD end = new PointD(1.0, 1.0);

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearGradientBrush"/> class.
        /// </summary>
        public LinearGradientBrush() : base() => uiDrawBrush.type = Libui.uiDrawBrushType.uiDrawBrushTypeLinearGradient;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinearGradientBrush"/> class with the specified start and end points.
        /// </summary>
        /// <param name="startPoint">The starting point of the gradient.</param>
        /// <param name="endPoint">The ending point of the gradient.</param>
        public LinearGradientBrush(PointD startPoint, PointD endPoint) : this()
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        /// <summary>
        /// Gets or sets the starting two-dimensional coordinates of the linear gradient.
        /// </summary>
        public PointD StartPoint
        {
            get
            {
                start = new PointD(X0, Y0);
                return start;
            }
            set
            {
                if (start != value)
                {
                    X0 = value.X;
                    Y0 = value.Y;
                    start = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the ending two-dimensional coordinates of the linear gradient.
        /// </summary>
        public PointD EndPoint
        {
            get
            {
                end = new PointD(X1, Y1);
                return end;
            }
            set
            {
                if (end != value)
                {
                    X1 = value.X;
                    Y1 = value.Y;
                    end = value;
                }
            }
        }
    }

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
        public RadialGradientBrush() : base() => uiDrawBrush.type = Libui.uiDrawBrushType.uiDrawBrushTypeRadialGradient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadialGradientBrush"/> class with the specified origin, center, and outer radius.
        /// </summary>
        /// <param name="origin">The focal point of the gradient.</param>
        /// <param name="center">The center of the outermost circle of the gradient.</param>
        /// <param name="outerRadius">The radius of the outermost circle of the gradient.</param>
        public RadialGradientBrush(PointD origin, PointD center, double outerRadius) : this()
        {
            Origin = origin;
            Center = center;
            OuterRadius = outerRadius;
        }

        /// <summary>
        /// Gets or sets the location of the two-dimensional focal point that defines the beginning of the gradient.
        /// </summary>
        public PointD Origin
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
            get => uiDrawBrush.outerRadius;
            set => uiDrawBrush.outerRadius = value;
        }
    }

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
        public SolidBrush() : base() => uiDrawBrush.type = Libui.uiDrawBrushType.uiDrawBrushTypeSolid;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolidBrush"/> class of the specified color.
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
                color = new Color(uiDrawBrush.r, uiDrawBrush.g, uiDrawBrush.b, uiDrawBrush.a);
                return color;
            }
            set
            {
                if (color != value)
                {
                    uiDrawBrush.r = value.R;
                    uiDrawBrush.g = value.G;
                    uiDrawBrush.b = value.B;
                    uiDrawBrush.a = value.A;
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