/****************************************************************************
 * FileName:   GradientBrush.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181002
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TCD.Drawing
{
    /// <summary>
    /// An abstract class that describes a gradient, composed of gradient stops. Classes that inherit from <see cref="GradientBrush"/> describe different ways of interpreting gradient stops.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public abstract class GradientBrush : Brush
    {
        // linear: start X
        // radial: start X
        private protected double X0
        {
            get => x0;
            set => x0 = value;
        }

        // linear: start Y
        // radial: start Y
        private protected double Y0
        {
            get => y0;
            set => y0 = value;
        }

        // linear: end X
        // radial: outer circle center X
        private protected double X1
        {
            get => x1;
            set => x1 = value;
        }

        // linear: end Y
        // radial: outer circle center Y
        private protected double Y1
        {
            get => y1;
            set => y1 = value;
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
                    numStops = (UIntPtr)value.Count;
                    stops = Marshal.UnsafeAddrOfPinnedArrayElement(value.ToArray(), 0);
                }
            }
        }
    }
}