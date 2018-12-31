/***************************************************************************************************
 * FileName:             DrawEventArgs.cs
 * Date:                 20181003
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Native;

namespace TCD.Drawing
{
    /// <summary>
    /// Provides drawing data for an event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class DrawEventArgs : EventArgs
    {
        internal Libui.uiAreaDrawParams uiAreaDrawParams;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawEventArgs"/> class with the specified event data.
        /// </summary>
        /// <param name="context">The drawing context.</param>
        /// <param name="clip">The rectangle that has been requested to be redrawn.</param>
        /// <param name="surfaceSize">The current size of the surface.</param>
        public DrawEventArgs(Context context, RectangleD clip, SizeD surfaceSize)
        {
            Context = context;
            uiAreaDrawParams = new Libui.uiAreaDrawParams()
            {
                Context = Context.Surface.Handle,
                AreaWidth = surfaceSize.Width,
                AreaHeight = surfaceSize.Height,
                ClipX = clip.X,
                ClipY = clip.Y,
                ClipWidth = clip.Width,
                ClipHeight = clip.Height
            };
        }

        internal DrawEventArgs(Libui.uiAreaDrawParams param) => uiAreaDrawParams = param;

        /// <summary>
        /// Gets the drawing context.
        /// </summary>
        public Context Context { get; }

        /// <summary>
        /// Gets the clip to be redrawn.
        /// </summary>
        public RectangleD Clip => new RectangleD(uiAreaDrawParams.ClipX, uiAreaDrawParams.ClipY, uiAreaDrawParams.ClipWidth, uiAreaDrawParams.ClipHeight);

        /// <summary>
        /// Gets the surface's current size.
        /// </summary>
        public SizeD SurfaceSize => new SizeD(uiAreaDrawParams.AreaWidth, uiAreaDrawParams.AreaHeight);
    }
}