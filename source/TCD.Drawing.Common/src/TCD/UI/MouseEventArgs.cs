/***************************************************************************************************
 * FileName:             MouseEventArgs.cs
 * Date:                 20181003
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Drawing;
using TCD.Native;

namespace TCD.UI
{
    [StructLayout(LayoutKind.Sequential)]
    public sealed class MouseEventArgs : EventArgs
    {
        internal Libui.uiAreaMouseEvent uiAreaMouseEvent;

        public MouseEventArgs(double x, double y, double surfaceWidth, double surfaceHeight, bool up, bool down, int count, ModifierKey modifiers, long held)
        {
            uiAreaMouseEvent = new Libui.uiAreaMouseEvent()
            {
                X = x,
                Y = y,
                AreaWidth = surfaceWidth,
                AreaHeight = surfaceHeight,
                Up = up,
                Down = down,
                Count = count,
                Modifiers = (Libui.uiModifiers)modifiers,
                Held1To64 = (ulong)held
            };
        }

        internal MouseEventArgs(Libui.uiAreaMouseEvent @event) => uiAreaMouseEvent = @event;

        public MouseEventArgs(PointD location, SizeD surfaceSize, bool up, bool down, int count, ModifierKey modifiers, long held) : this(location.X, location.Y, surfaceSize.Width, surfaceSize.Height, up, down, count, modifiers, held) { }

        public PointD Point => new PointD(uiAreaMouseEvent.X, uiAreaMouseEvent.Y);
        public SizeD SurfaceSize => new SizeD(uiAreaMouseEvent.AreaWidth, uiAreaMouseEvent.AreaHeight);
        public bool Up => uiAreaMouseEvent.Up;
        public bool Down => uiAreaMouseEvent.Down;
        public int Count => uiAreaMouseEvent.Count;
        public ModifierKey KeyModifiers => (ModifierKey)uiAreaMouseEvent.Modifiers;
        public long Held1To64 => (long)uiAreaMouseEvent.Held1To64;
    }
}