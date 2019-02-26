/***************************************************************************************************
 * FileName:             StrokeOptions.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TCD.Native;

namespace TCD.Drawing
{
    //TODO: Basic overrides (ToString, GetHashCode)
    //TODO: IEquatable implementation.
    /// <summary>
    /// Represents a stroke to draw with.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class StrokeOptions
    {
        internal Libui.uiDrawStrokeParams uiDrawStrokeParams;
        private List<double> dashes;

        /// <summary>
        /// The default miter limit.
        /// </summary>
        public const double DefaultMiterLimit = 10.0;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrokeOptions"/> class.
        /// </summary>
        /// 
        public StrokeOptions(LineCap cap, LineJoin join, double thickness, IList<double> dashes, double dashPhase, double miterLimit = DefaultMiterLimit)
        {
            uiDrawStrokeParams = new Libui.uiDrawStrokeParams();

            Cap = cap;
            Join = join;
            Thickness = thickness;
            DashPhase = dashPhase;
            Dashes = (List<double>)dashes;
            MiterLimit = miterLimit;
        }


        /// <summary>
        /// Gets or sets the style of cap at line ends.
        /// </summary>
        public LineCap Cap
        {
            get => (LineCap)uiDrawStrokeParams.Cap;
            set => uiDrawStrokeParams.Cap = (Libui.uiDrawLineCap)value;
        }

        /// <summary>
        /// Gets or sets how two lines connecting at an angle should be joined.
        /// </summary>
        public LineJoin Join
        {
            get => (LineJoin)uiDrawStrokeParams.Join;
            set => uiDrawStrokeParams.Join = (Libui.uiDrawLineJoin)value;
        }

        /// <summary>
        /// Gets or sets the thickness of the stroke.
        /// </summary>
        public double Thickness
        {
            get => uiDrawStrokeParams.Thickness;
            set => uiDrawStrokeParams.Thickness = value;
        }

        /// <summary>
        /// Gets or sets how far to extend a line for the line join.
        /// </summary>
        public double MiterLimit
        {
            get => uiDrawStrokeParams.MiterLimit;
            set => uiDrawStrokeParams.MiterLimit = value;
        }

        /// <summary>
        /// Gets or sets the dashing style specified as an array of numbers.
        /// </summary>
        public List<double> Dashes
        {
            get => dashes;
            set
            {
                if (value != null && value.Count != 0)
                {
                    int length = value.Count;
                    uiDrawStrokeParams.Dashes = Marshal.UnsafeAddrOfPinnedArrayElement(value.ToArray(), 0);
                    uiDrawStrokeParams.NumDashes = (UIntPtr)length;
                    dashes = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the offset to the dashes on the path.
        /// </summary>
        public double DashPhase
        {
            get => uiDrawStrokeParams.DashPhase;
            set => uiDrawStrokeParams.DashPhase = value;
        }
    }
}