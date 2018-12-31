/***************************************************************************************************
 * FileName:             MouseEventArgs.cs
 * Date:                 20181003
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Native;

namespace TCD.Drawing
{
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MouseEventArgs : EventArgs
    {
        internal Libui.uiAreaHandlerMouseEvent uiAreaHandlerMouseEvent;

        public MouseEventArgs(double x, double y, double surfaceWidth, double surfaceHeight, bool up, bool down, int count, ModifierKey modifiers, long held)
        {
            this.x = x;
            this.y = y;
            width = surfaceWidth;
            height = surfaceHeight;
            this.up = up;
            this.down = down;
            this.count = count;
            this.modifiers = modifiers;
            this.held = held;
        }

        public MouseEventArgs(PointD point, SizeD surfaceSize, bool up, bool down, int count, ModifierKey modifiers, long held) : this(point.X, point.Y, surfaceSize.Width, surfaceSize.Height, up, down, count, modifiers, held) { }

        public PointD Point => new PointD(x, y);
        public SizeD SurfaceSize => new SizeD(width, height);
        public bool Up => up;
        public bool Down => down;
        public int Count => count;
        public ModifierKey KeyModifiers => modifiers;
        public long Held1To64 => held;
    }
}