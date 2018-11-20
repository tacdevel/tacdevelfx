/***************************************************************************************************
 * FileName:             LibuiEx.cs
 * Date:                 20181120
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Drawing;
using TCD.InteropServices;

namespace TCD.Native
{
    internal static class LibuiEx
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct AreaHandler
        {
            private IntPtr draw;
            private IntPtr mouseEvent;
            private IntPtr mouseCrossed;
            private IntPtr dragBroken;
            private IntPtr keyEvent;

            public AreaHandlerDrawCallback Draw
            {
                set => draw = Marshal.GetFunctionPointerForDelegate(value);
            }

            public AreaHandlerMouseEventCallback MouseEvent
            {
                set => mouseEvent = Marshal.GetFunctionPointerForDelegate(value);
            }

            public AreaHandlerMouseCrossedCallback MouseCrossed
            {
                set => mouseCrossed = Marshal.GetFunctionPointerForDelegate(value);
            }

            public AreaHandlerDragBrokenCallback DragBroken
            {
                set => dragBroken = Marshal.GetFunctionPointerForDelegate(value);
            }

            public AreaHandlerKeyEventCallback KeyEvent
            {
                set => keyEvent = Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        #region ColorPicker
        internal static void ColorButtonColor(IntPtr b, out double red, out double green, out double blue, out double alpha) => Libui.LoadFunction<Signatures.uiColorButtonColor>()(b, out red, out green, out blue, out alpha);
        internal static void ColorButtonSetColor(IntPtr b, double red, double green, double blue, double alpha) => Libui.LoadFunction<Signatures.uiColorButtonSetColor>()(b, red, green, blue, alpha);
        internal static void ColorButtonOnChanged(IntPtr b, ColorButtonOnChangedCallback f, IntPtr data) => Libui.LoadFunction<Signatures.uiColorButtonOnChanged>()(b, f, data);
        internal static IntPtr NewColorButton() => Libui.LoadFunction<Signatures.uiNewColorButton>()();
        [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void ColorButtonOnChangedCallback(IntPtr w, IntPtr data);
        #endregion ColorPicker
        #region Context
        internal static void DrawStroke(IntPtr context, IntPtr path, ref Brush brush, ref StrokeOptions strokeParam) => Libui.LoadFunction<Signatures.uiDrawStroke>()(context, path, ref brush, ref strokeParam);
        internal static void DrawFill(IntPtr context, IntPtr path, ref Brush brush) => Libui.LoadFunction<Signatures.uiDrawFill>()(context, path, ref brush);
        internal static void DrawTransform(IntPtr context, Matrix matrix) => Libui.LoadFunction<Signatures.uiDrawTransform>()(context, matrix);
        internal static void DrawClip(IntPtr context, IntPtr path) => Libui.LoadFunction<Signatures.uiDrawClip>()(context, path);
        internal static void DrawSave(IntPtr context) => Libui.LoadFunction<Signatures.uiDrawSave>()(context);
        internal static void DrawRestore(IntPtr context) => Libui.LoadFunction<Signatures.uiDrawRestore>()(context);
        #endregion Context
        #region FontPicker
        internal static void FontButtonFont(IntPtr b, out Font desc) => Libui.LoadFunction<Signatures.uiFontButtonFont>()(b, out desc);
        internal static void FontButtonOnChanged(IntPtr b, FontButtonOnChangedCallback f, IntPtr data) => Libui.LoadFunction<Signatures.uiFontButtonOnChanged>()(b, f, data);
        internal static IntPtr NewFontButton() => Libui.LoadFunction<Signatures.uiNewFontButton>()();
        internal static void FreeFontButtonFont(Font desc) => Libui.LoadFunction<Signatures.uiFreeFontButtonFont>()(desc);
        [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void FontButtonOnChangedCallback(IntPtr w, IntPtr data);
        #endregion FontPicker
        #region Matrix
        internal static void DrawMatrixSetIdentity(Matrix matrix) => Libui.LoadFunction<Signatures.uiDrawMatrixSetIdentity>()(matrix);
        internal static void DrawMatrixTranslate(Matrix matrix, double x, double y) => Libui.LoadFunction<Signatures.uiDrawMatrixTranslate>()(matrix, x, y);
        internal static void DrawMatrixScale(Matrix matrix, double xCenter, double yCenter, double x, double y) => Libui.LoadFunction<Signatures.uiDrawMatrixScale>()(matrix, xCenter, yCenter, x, y);
        internal static void DrawMatrixRotate(Matrix matrix, double x, double y, double amount) => Libui.LoadFunction<Signatures.uiDrawMatrixRotate>()(matrix, x, y, amount);
        internal static void DrawMatrixSkew(Matrix matrix, double x, double y, double xamount, double yamount) => Libui.LoadFunction<Signatures.uiDrawMatrixSkew>()(matrix, x, y, xamount, yamount);
        internal static void DrawMatrixMultiply(Matrix dest, Matrix src) => Libui.LoadFunction<Signatures.uiDrawMatrixMultiply>()(dest, src);
        internal static bool DrawMatrixInvertible(Matrix matrix) => Libui.LoadFunction<Signatures.uiDrawMatrixInvertible>()(matrix);
        internal static int DrawMatrixInvert(Matrix matrix) => Libui.LoadFunction<Signatures.uiDrawMatrixInvert>()(matrix);
        internal static void DrawMatrixTransformPoint(Matrix matrix, out double x, out double y) => Libui.LoadFunction<Signatures.uiDrawMatrixTransformPoint>()(matrix, out x, out y);
        internal static void DrawMatrixTransformSize(Matrix matrix, out double x, out double y) => Libui.LoadFunction<Signatures.uiDrawMatrixTransformSize>()(matrix, out x, out y);
        #endregion Matrix
        #region Path
        internal static IntPtr DrawNewPath(FillMode fillMode) => Libui.LoadFunction<Signatures.uiDrawNewPath>()(fillMode);
        internal static void DrawFreePath(IntPtr p) => Libui.LoadFunction<Signatures.uiDrawFreePath>()(p);
        internal static void DrawPathNewFigure(IntPtr p, double x, double y) => Libui.LoadFunction<Signatures.uiDrawPathNewFigure>()(p, x, y);
        internal static void DrawPathNewFigureWithArc(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative) => Libui.LoadFunction<Signatures.uiDrawPathNewFigureWithArc>()(p, xCenter, yCenter, radius, startAngle, sweep, negative);
        internal static void DrawPathLineTo(IntPtr p, double x, double y) => Libui.LoadFunction<Signatures.uiDrawPathLineTo>()(p, x, y);
        internal static void DrawPathArcTo(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative) => Libui.LoadFunction<Signatures.uiDrawPathArcTo>()(p, xCenter, yCenter, radius, startAngle, sweep, negative);
        internal static void DrawPathBezierTo(IntPtr p, double c1x, double c1y, double c2x, double c2y, double endX, double endY) => Libui.LoadFunction<Signatures.uiDrawPathBezierTo>()(p, c1x, c1y, c2x, c2y, endX, endY);
        internal static void DrawPathCloseFigure(IntPtr p) => Libui.LoadFunction<Signatures.uiDrawPathCloseFigure>()(p);
        internal static void DrawPathAddRectangle(IntPtr p, double x, double y, double width, double height) => Libui.LoadFunction<Signatures.uiDrawPathAddRectangle>()(p, x, y, width, height);
        internal static void DrawPathEnd(IntPtr p) => Libui.LoadFunction<Signatures.uiDrawPathEnd>()(p);
        #endregion Path
        #region Surface
        internal static void AreaSetSize(IntPtr a, int width, int height) => Libui.LoadFunction<Signatures.uiAreaSetSize>()(a, width, height);
        internal static void AreaQueueRedrawAll(IntPtr a) => Libui.LoadFunction<Signatures.uiAreaQueueRedrawAll>()(a);
        internal static void AreaScrollTo(IntPtr a, double x, double y, double width, double height) => Libui.LoadFunction<Signatures.uiAreaScrollTo>()(a, x, y, width, height);
        internal static void AreaBeginUserWindowMove(IntPtr a) => Libui.LoadFunction<Signatures.uiAreaBeginUserWindowMove>()(a);
        internal static void AreaBeginUserWindowResize(IntPtr a, WindowEdge edge) => Libui.LoadFunction<Signatures.uiAreaBeginUserWindowResize>()(a, edge);
        internal static IntPtr NewArea(AreaHandler ah) => Libui.LoadFunction<Signatures.uiNewArea>()(ah);
        internal static IntPtr NewScrollingArea(AreaHandler ah, int width, int height) => Libui.LoadFunction<Signatures.uiNewScrollingArea>()(ah, width, height);
        [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void AreaHandlerDrawCallback(AreaHandler handler, IntPtr area, DrawEventArgs args);
        [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void AreaHandlerMouseEventCallback(AreaHandler handler, IntPtr area, MouseEventArgs args);
        [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void AreaHandlerMouseCrossedCallback(AreaHandler handler, IntPtr area, bool left);
        [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void AreaHandlerDragBrokenCallback(AreaHandler handler, IntPtr area);
        [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void AreaHandlerKeyEventCallback(AreaHandler handler, IntPtr area, KeyEventArgs args);
        #endregion Surface

        // Keep the delegates in this class in order with libui\ui.h
        // so it is somewhat easier to see what is missing.
        private static class Signatures
        {
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAreaSetSize(IntPtr a, int width, int height);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAreaQueueRedrawAll(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAreaScrollTo(IntPtr a, double x, double y, double width, double height);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAreaBeginUserWindowMove(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAreaBeginUserWindowResize(IntPtr a, WindowEdge edge);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewArea(AreaHandler ah);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewScrollingArea(AreaHandler ah, int width, int height);

            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiDrawNewPath(FillMode fillMode);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawFreePath(IntPtr p);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathNewFigure(IntPtr p, double x, double y);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathNewFigureWithArc(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathLineTo(IntPtr p, double x, double y);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathArcTo(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathBezierTo(IntPtr p, double c1x, double c1y, double c2x, double c2y, double endX, double endY);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathCloseFigure(IntPtr p);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathAddRectangle(IntPtr p, double x, double y, double width, double height);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawPathEnd(IntPtr p);

            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawStroke(IntPtr context, IntPtr path, ref Brush brush, ref StrokeOptions strokeParam);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawFill(IntPtr context, IntPtr path, ref Brush brush);

            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixSetIdentity(Matrix matrix);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixTranslate(Matrix matrix, double x, double y);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixScale(Matrix matrix, double xCenter, double yCenter, double x, double y);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixRotate(Matrix matrix, double x, double y, double amount);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixSkew(Matrix matrix, double x, double y, double xamount, double yamount);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixMultiply(Matrix dest, Matrix src);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate bool uiDrawMatrixInvertible(Matrix matrix);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate int uiDrawMatrixInvert(Matrix matrix);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixTransformPoint(Matrix matrix, out double x, out double y);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawMatrixTransformSize(Matrix matrix, out double x, out double y);

            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawTransform(IntPtr context, Matrix matrix);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawClip(IntPtr context, IntPtr path);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawSave(IntPtr context);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawRestore(IntPtr context);

            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiFontButtonFont(IntPtr b, out Font desc);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiFontButtonOnChanged(IntPtr b, FontButtonOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewFontButton();
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiFreeFontButtonFont(Font desc);

            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiColorButtonColor(IntPtr b, out double red, out double green, out double blue, out double alpha);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiColorButtonSetColor(IntPtr b, double red, double green, double blue, double alpha);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiColorButtonOnChanged(IntPtr b, ColorButtonOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewColorButton();
        }
    }
}