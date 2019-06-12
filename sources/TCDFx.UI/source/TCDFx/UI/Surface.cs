/***************************************************************************************************
 * FileName:             Surface.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Threading;
using TCD.Drawing;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Implements the basic functionality required by a drawable surface.
    /// </summary>
    public abstract class SurfaceBase : Control
    {
        private static readonly Dictionary<SurfaceBase, Libui.uiAreaHandler> handlerCache = new Dictionary<SurfaceBase, Libui.uiAreaHandler>();
        private static readonly Dictionary<IntPtr, SurfaceBase> surfaceCache = new Dictionary<IntPtr, SurfaceBase>();

        internal SurfaceBase(SurfaceHandler handler, bool scrollable, int width, int height, bool cacheable = true) : base(GetHandle(handler, scrollable, width, height, out Libui.uiAreaHandler outHandler), cacheable)
        {
            handlerCache.Add(this, outHandler);
            surfaceCache.Add(Handle, this);
        }

        /// <summary>
        /// Sets the content size of this <see cref="Surface"/>.
        /// </summary>
        public Size Size
        {
            set
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiAreaSetSize>()(Handle, value.Width, value.Height);
            }
        }

        /// <summary>
        /// Queues a redraw of the surface.
        /// </summary>
        public void QueueRedrawAll()
        {
            Thread.Sleep(200); // Must sleep for 200ms or else crashes
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiAreaQueueRedrawAll>()(Handle);
        }

        public void BeginUserWindowMove()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiAreaBeginUserWindowMove>()(Handle);
        }

        public void BeginUserWindowResize(WindowEdge edge)
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiAreaBeginUserWindowResize>()(Handle, (Libui.uiWindowResizeEdge)edge);
        }

        private static SafeControlHandle GetHandle(SurfaceHandler handler, bool scrollable, int width, int height, out Libui.uiAreaHandler outHandler)
        {
            outHandler = new Libui.uiAreaHandler
            {
                Draw = (nativeHandler, surface, args) =>
                {
                    DrawEventArgs e = new DrawEventArgs(args);
                    handler.Draw(surfaceCache[surface], ref e);
                },
                MouseEvent = (nativeHandler, surface, args) =>
                {
                    MouseEventArgs e = new MouseEventArgs(args);
                    handler.MouseEvent(surfaceCache[surface], ref e);
                },
                MouseCrossed = (nativeHandler, surface, left) => handler.MouseCrossed(surfaceCache[surface], left),
                DragBroken = (nativeHandler, surface) => handler.DragBroken(surfaceCache[surface]),
                KeyEvent = (nativeHandler, surface, args) =>
                {
                    KeyEventArgs e = new KeyEventArgs(args);
                    return handler.KeyEvent(surfaceCache[surface], ref e);
                }
            };

            return !scrollable
                ? new SafeControlHandle(Libui.Call<Libui.uiNewArea>()(outHandler))
                : new SafeControlHandle(Libui.Call<Libui.uiNewScrollingArea>()(outHandler, width, height));
        }
    }

    /// <summary>
    /// Defines the events for a drawable surface.
    /// </summary>
    public abstract class SurfaceHandler
    {
        /// <summary>
        /// Called when the surface is created or resized.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="args">The event data.</param>
        public virtual void Draw(SurfaceBase surface, ref DrawEventArgs args) { }

        /// <summary>
        /// Called when the mouse is moved or clicked over the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="args">The event data.</param>
        public virtual void MouseEvent(SurfaceBase surface, ref MouseEventArgs args) { }

        /// <summary>
        /// Called when the mouse entered or left the surface.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="left">The event data.</param>
        public virtual void MouseCrossed(SurfaceBase surface, bool left) { }

        /// <summary>
        /// Called when a mouse drag is ended.
        /// </summary>
        /// <param name="surface">The surface.</param>
        public virtual void DragBroken(SurfaceBase surface) { }

        /// <summary>
        /// Called when a key is pressed.
        /// </summary>
        /// <param name="surface">The surface.</param>
        /// <param name="args">The event data.</param>
        public virtual bool KeyEvent(SurfaceBase surface, ref KeyEventArgs args) => false;
    }

    /// <summary>
    /// Represents a control with a drawable surface.
    /// </summary>
    public class Surface : SurfaceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Surface"/> class with the specified <see cref="SurfaceHandler"/>.
        /// </summary>
        /// <param name="handler">The specified event handler.</param>
        public Surface(SurfaceHandler handler) : base(handler, false, 0, 0) { }
    }

    /// <summary>
    /// Represents a control with a scrollable, drawable surface.
    /// </summary>
    public class ScrollableSurface : SurfaceBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScrollableSurface"/> class with the specified <see cref="SurfaceHandler"/>, width, and height.
        /// </summary>
        /// <param name="handler">The specified event handler.</param>
        /// <param name="width">The width of the <see cref="ScrollableSurface"/>.</param>
        /// <param name="height">The height of the <see cref="ScrollableSurface"/>.</param>
        public ScrollableSurface(SurfaceHandler handler, int width, int height) : base(handler, true, width, height) { }

        /// <summary>
        /// Scrolls the surface view to the specified location and size.
        /// </summary>
        /// <param name="x">The x-coordinate of the view.</param>
        /// <param name="y">The y-coordinate of the view.</param>
        /// <param name="width">The width of the view.</param>
        /// <param name="height">The height of the view.</param>
        public void ScrollTo(double x, double y, double width, double height)
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiAreaScrollTo>()(Handle, x, y, width, height);
        }
    }
}