/***************************************************************************************************
 * FileName:             ScrollableSurface.cs
 * Date:                 20181008
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.InteropServices;
using TCD.Native;

namespace TCD.Drawing
{
    /// <summary>
    /// Represents a control with a scrollable, drawable surface.
    /// </summary>
    public class ScrollableSurface : SurfaceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollableSurface"/> class with the specified <see cref="SurfaceHandler"/>, widht, and height.
        /// </summary>
        /// <param name="handler">The specified event handler.</param>
        public ScrollableSurface(SurfaceHandler handler, int width, int height) : base(handler, true, width, height) { }

        /// <summary>
        /// Scrolls the surface view to the specified location and size.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void ScrollTo(double x, double y, double width, double height)
        {
            if (IsInvalid) throw new InvalidHandleException();
            LibuiEx.AreaScrollTo(Handle, x, y, width, height);
        }
    }
}