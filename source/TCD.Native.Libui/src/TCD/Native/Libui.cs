/***************************************************************************************************
 * FileName:             Libui.cs
 * Date:                 20181205
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.InteropServices;
using static TCD.Platform;

namespace TCD.Native
{
    internal static partial class Libui
    {
        #region Helpers
        private const CallingConvention Convention = CallingConvention.Cdecl;
#pragma warning disable IDE0051 // Remove unused private members
        private const LayoutKind Layout = LayoutKind.Sequential;
#pragma warning restore IDE0051 // Remove unused private members

        static Libui()
        {
            if (CurrentPlatform.Platform == PlatformType.Windows && CurrentPlatform.Architecture == PlatformArch.X64)
                AssemblyRef = new NativeAssembly(@"runtimes\win-x64\native\libui.dll");
            else if (CurrentPlatform.Platform == PlatformType.MacOS && CurrentPlatform.Architecture == PlatformArch.X64)
                AssemblyRef = new NativeAssembly(@"runtimes/osx-x64/native/libui.dylib", @"runtimes/osx-x64/native/libui.A.dylib");
            else if ((CurrentPlatform.Platform == PlatformType.Linux || CurrentPlatform.Platform == PlatformType.FreeBSD) && CurrentPlatform.Architecture == PlatformArch.X64)
                AssemblyRef = new NativeAssembly(@"runtimes/linux-x64/native/libui.so", @"runtimes/linux-x64/native/libui.so.0");
            else throw new PlatformNotSupportedException();

            Console.WriteLine($"{AssemblyRef}");
        }

        private static NativeAssembly AssemblyRef { get; }

        public static T Call<T>() where T : Delegate
#if DEBUG
        {
            bool fail = false;

            try
            {
                Console.Write($"[DEBUG] Loading native function '{typeof(T).Name}' from assembly '{nameof(Libui)}'...");
                return AssemblyRef.LoadFunction<T>(typeof(T).Name);
            }
            catch (Exception ex)
            {
                fail = true;
                Console.Write($" Exception Caught.");
                Console.WriteLine();
                Console.WriteLine($"[DEBUG] Exception Type: {ex.GetType().Name}");
                Console.WriteLine($"[DEBUG] Inner Exception Type: {ex.InnerException.GetType().Name}");
                Console.WriteLine($"[DEBUG] Source: {ex.Source}");
                Console.WriteLine($"[DEBUG] Message: {ex.Message}");
                Console.WriteLine($"[DEBUG] StackTrace: {ex.StackTrace}");
                throw;
            }
            finally
            {
                if (!fail)
                {
                    Console.Write(" Done.");
                    Console.WriteLine();
                }
            }
        }
#else
        => AssemblyRef.LoadFunction<T>(typeof(T).Name);
#endif
        #endregion

        // Keep the members below in order with ui.h so it's easier to see what's missing.
#pragma warning disable IDE0001
#pragma warning disable IDE0044
#pragma warning disable IDE1006
        public const double uiPi = 3.14159265358979323846264338327950288419716939937510582097494459;

        public enum uiForEach : uint
        {
            uiForEachContinue,
            uiForEachStop
        }

        [StructLayout(Layout)]
        public struct uiInitOptions
        {
            public UIntPtr Size;
        }

        [UnmanagedFunctionPointer(Convention)] public delegate string uiInit(ref uiInitOptions options);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiUnInit();
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeInitError(string err);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMain();
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMainSteps();
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiMainStep(bool wait);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiQuit();
        [UnmanagedFunctionPointer(Convention)] public delegate void uiQueueMain([MarshalAs(UnmanagedType.FunctionPtr)] uiQueueMainCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiQueueMainCallback(IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTimer(int milliseconds, [MarshalAs(UnmanagedType.FunctionPtr)] uiTimerCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiTimerCallback(IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiOnShouldQuit([MarshalAs(UnmanagedType.FunctionPtr)] uiOnShouldQuitCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiOnShouldQuitCallback(IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeText(string err);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiControlDestroy(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate UIntPtr uiControlHandle(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiControlParent(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiControlSetParent(IntPtr c, IntPtr parent);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiControlToplevel(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiControlVisible(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiControlShow(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiControlHide(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiControlEnabled(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiControlEnable(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiControlDisable(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAllocControl(UIntPtr n, uint OSsig, uint typesig, string typenamestr);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeControl(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiControlVerifySetParent(IntPtr c, IntPtr parent);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiControlEnabledToUser(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiUserBugCannotSetParentOnTopLevel(string type);

        [UnmanagedFunctionPointer(Convention)] public delegate string uiWindowTitle(IntPtr w);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowSetTitle(IntPtr w, string title);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowContentSize(IntPtr w, out int width, out int height);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowSetContentSize(IntPtr w, int width, int height);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiWindowFullscreen(IntPtr w);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowSetFullscreen(IntPtr w, bool fullscreen);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowOnContentSizeChanged(IntPtr w, [MarshalAs(UnmanagedType.FunctionPtr)] uiWindowOnContentSizeChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowOnContentSizeChangedCallback(IntPtr w, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowOnClosing(IntPtr w, [MarshalAs(UnmanagedType.FunctionPtr)] uiWindowOnClosingCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiWindowOnClosingCallback(IntPtr w, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiWindowBorderless(IntPtr w);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowSetBorderless(IntPtr w, bool borderless);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowSetChild(IntPtr w, IntPtr child);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiWindowMargined(IntPtr w);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiWindowSetMargined(IntPtr w, bool margined);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewWindow(string title, int width, int height, bool hasMenubar);

        [UnmanagedFunctionPointer(Convention)] public delegate string uiButtonText(IntPtr b);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiButtonSetText(IntPtr b, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiButtonOnClicked(IntPtr b, [MarshalAs(UnmanagedType.FunctionPtr)] uiButtonOnClickCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiButtonOnClickCallback(IntPtr b, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewButton(string text);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiBoxAppend(IntPtr b, IntPtr child, bool stretchy);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiBoxDelete(IntPtr b, int index);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiBoxPadded(IntPtr b);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiBoxSetPadded(IntPtr b, bool padded);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewHorizontalBox();
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewVerticalBox();

        [UnmanagedFunctionPointer(Convention)] public delegate string uiCheckboxText(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiCheckboxSetText(IntPtr c, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiCheckboxOnToggled(IntPtr c, [MarshalAs(UnmanagedType.FunctionPtr)] uiCheckboxOnToggledCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiCheckboxOnToggledCallback(IntPtr b, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiCheckboxChecked(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiCheckboxSetChecked(IntPtr c, bool @checked);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewCheckbox(string text);

        [UnmanagedFunctionPointer(Convention)] public delegate string uiEntryText(IntPtr e);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiEntrySetText(IntPtr e, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiEntryOnChanged(IntPtr e, [MarshalAs(UnmanagedType.FunctionPtr)] uiEntryOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiEntryOnChangedCallback(IntPtr b, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiEntryReadOnly(IntPtr e);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiEntrySetReadOnly(IntPtr e, bool @readonly);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewEntry();
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewPasswordEntry();
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewSearchEntry();

        [UnmanagedFunctionPointer(Convention)] public delegate string uiLabelText(IntPtr l);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiLabelSetText(IntPtr l, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewLabel(string text);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiTabAppend(IntPtr t, string name, IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTabInsertAt(IntPtr t, string name, int before, IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTabDelete(IntPtr t, int index);
        [UnmanagedFunctionPointer(Convention)] public delegate int uiTabNumPages(IntPtr t);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiTabMargined(IntPtr t, int page);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTabSetMargined(IntPtr t, int page, bool margined);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTab();

        [UnmanagedFunctionPointer(Convention)] public delegate string uiGroupTitle(IntPtr g);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiGroupSetTitle(IntPtr g, string title);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiGroupSetChild(IntPtr g, IntPtr child);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiGroupMargined(IntPtr g);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiGroupSetMargined(IntPtr g, bool margined);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewGroup(string title);

        [UnmanagedFunctionPointer(Convention)] public delegate int uiSpinboxValue(IntPtr s);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiSpinboxSetValue(IntPtr s, int value);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiSpinboxOnChanged(IntPtr s, [MarshalAs(UnmanagedType.FunctionPtr)] uiSpinboxOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiSpinboxOnChangedCallback(IntPtr s, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewSpinbox(int min, int max);

        [UnmanagedFunctionPointer(Convention)] public delegate int uiSliderValue(IntPtr s);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiSliderSetValue(IntPtr s, int value);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiSliderOnChanged(IntPtr s, [MarshalAs(UnmanagedType.FunctionPtr)] uiSliderOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiSliderOnChangedCallback(IntPtr s, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewSlider(int min, int max);

        [UnmanagedFunctionPointer(Convention)] public delegate int uiProgressBarValue(IntPtr p);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiProgressBarSetValue(IntPtr p, int n);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewProgressBar();

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewHorizontalSeparator();
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewVerticalSeparator();

        [UnmanagedFunctionPointer(Convention)] public delegate void uiComboboxAppend(IntPtr c, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate int uiComboboxSelected(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiComboboxSetSelected(IntPtr c, int n);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiComboboxOnSelected(IntPtr c, [MarshalAs(UnmanagedType.FunctionPtr)] uiComboboxOnSelectedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiComboboxOnSelectedCallback(IntPtr c, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewCombobox();

        [UnmanagedFunctionPointer(Convention)] public delegate void uiEditableComboboxAppend(IntPtr c, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate string uiEditableComboboxText(IntPtr c);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiEditableComboboxSetText(IntPtr c, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiEditableComboboxOnChanged(IntPtr c, [MarshalAs(UnmanagedType.FunctionPtr)] uiEditableComboboxOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiEditableComboboxOnChangedCallback(IntPtr c, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewEditableCombobox();

        [UnmanagedFunctionPointer(Convention)] public delegate void uiRadioButtonsAppend(IntPtr r, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate int uiRadioButtonsSelected(IntPtr r);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiRadioButtonsSetSelected(IntPtr r, int n);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiRadioButtonsOnSelected(IntPtr r, [MarshalAs(UnmanagedType.FunctionPtr)] uiRadioButtonsOnSelectedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiRadioButtonsOnSelectedCallback(IntPtr r, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewRadioButtons();

        [StructLayout(Layout)]
        public struct tm
        {
            public int sec;
            public int min;
            public int hour;
            public int day;
            public int mon;
            public int year;
            public int wday; // Must be uninitialized.
            public int yday; // Must be uninitialized.
            public int isdst; // Must be -1.
        }

        [UnmanagedFunctionPointer(Convention)] public delegate void uiDateTimePickerTime(IntPtr d, out tm time);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDateTimePickerSetTime(IntPtr d, tm time);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDateTimePickerOnChanged(IntPtr d, [MarshalAs(UnmanagedType.FunctionPtr)] uiDateTimePickerOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDateTimePickerOnChangedCallback(IntPtr d, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewDateTimePicker();
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewDatePicker();
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTimePicker();

        [UnmanagedFunctionPointer(Convention)] public delegate string uiMultilineEntryText(IntPtr e);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMultilineEntrySetText(IntPtr e, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMultilineEntryAppend(IntPtr e, string text);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMultilineEntryOnChanged(IntPtr e, [MarshalAs(UnmanagedType.FunctionPtr)] uiMultilineEntryOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMultilineEntryOnChangedCallback(IntPtr e, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiMultilineEntryReadOnly(IntPtr e);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMultilineEntrySetReadOnly(IntPtr e, bool @readonly);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewMultilineEntry();
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewNonWrappingMultilineEntry();

        [UnmanagedFunctionPointer(Convention)] public delegate void uiMenuItemEnable(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMenuItemDisable(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMenuItemOnClicked(IntPtr m, [MarshalAs(UnmanagedType.FunctionPtr)] uiMenuItemOnClickedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMenuItemOnClickedCallback(IntPtr sender, IntPtr window, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiMenuItemChecked(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMenuItemSetChecked(IntPtr m, bool @checked);

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiMenuAppendItem(IntPtr m, string name);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiMenuAppendCheckItem(IntPtr m, string name);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiMenuAppendQuitItem(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiMenuAppendPreferencesItem(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiMenuAppendAboutItem(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMenuAppendSeparator(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewMenu(string name);

        [UnmanagedFunctionPointer(Convention)] public delegate string uiOpenFile(IntPtr parent);
        [UnmanagedFunctionPointer(Convention)] public delegate string uiSaveFile(IntPtr parent);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMsgBox(IntPtr parent, string title, string description);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiMsgBoxError(IntPtr parent, string title, string description);

        [StructLayout(Layout)]
        public struct uiAreaHandler
        {
            private IntPtr draw;
            private IntPtr mouseEvent;
            private IntPtr mouseCrossed;
            private IntPtr dragBroken;
            private IntPtr keyEvent;

            public uiAreaHandlerDraw Draw
            {
                set => draw = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiAreaHandlerMouseEvent MouseEvent
            {
                set => mouseEvent = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiAreaHandlerMouseCrossed MouseCrossed
            {
                set => mouseCrossed = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiAreaHandlerDragBroken DragBroken
            {
                set => dragBroken = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiAreaHandlerKeyEvent KeyEvent
            {
                set => keyEvent = Marshal.GetFunctionPointerForDelegate(value);
            }
        }
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaHandlerDraw(uiAreaHandler ah, IntPtr a, uiAreaDrawParams @params);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaHandlerMouseEvent(uiAreaHandler ah, IntPtr a, uiAreaMouseEvent @params);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaHandlerMouseCrossed(uiAreaHandler ah, IntPtr a, bool left);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaHandlerDragBroken(uiAreaHandler ah, IntPtr a);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiAreaHandlerKeyEvent(uiAreaHandler ah, IntPtr a, uiAreaKeyEvent @params);

        public enum uiWindowResizeEdge : uint
        {
            uiWindowResizeEdgeLeft,
            uiWindowResizeEdgeTop,
            uiWindowResizeEdgeRight,
            uiWindowResizeEdgeBottom,
            uiWindowResizeEdgeTopLeft,
            uiWindowResizeEdgeTopRight,
            uiWindowResizeEdgeBottomLeft,
            uiWindowResizeEdgeBottomRight
        }

        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaSetSize(IntPtr a, int width, int height);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaQueueRedrawAll(IntPtr a);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaScrollTo(IntPtr a, double x, double y, double width, double height);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaBeginUserWindowMove(IntPtr a);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAreaBeginUserWindowResize(IntPtr a, uiWindowResizeEdge edge);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewArea(uiAreaHandler ah);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewScrollingArea(uiAreaHandler ah, int width, int height);

        [StructLayout(Layout)]
        public struct uiAreaDrawParams
        {
            public IntPtr Context;
            public double AreaWidth; //! Only defined for non-scrolling areas.
            public double AreaHeight; //! Only defined for non-scrolling areas.
            public double ClipX;
            public double ClipY;
            public double ClipWidth;
            public double ClipHeight;
        }

        public enum uiDrawBrushType : uint
        {
            uiDrawBrushTypeSolid,
            uiDrawBrushTypeLinearGradient,
            uiDrawBrushTypeRadialGradient,
            uiDrawBrushTypeImage
        }

        public enum uiDrawLineCap : uint
        {
            uiDrawLineCapFlat,
            uiDrawLineCapRound,
            uiDrawLineCapSquare
        }

        public enum uiDrawLineJoin : uint
        {
            uiDrawLineJoinMiter,
            uiDrawLineJoinRound,
            uiDrawLineJoinBevel
        }

        public const double uiDrawDefaultMiterLimit = 10.0;

        public enum uiDrawFillMode : uint
        {
            uiDrawFillModeWinding,
            uiDrawFillModeAlternate
        }

        [StructLayout(Layout)]
        public struct uiDrawMatrix
        {
            public double M11;
            public double M12;
            public double M21;
            public double M22;
            public double M31;
            public double M32;
        }

        [StructLayout(Layout)]
        public struct uiDrawBrush
        {
            [MarshalAs(UnmanagedType.I4)]
            public uiDrawBrushType Type;

            // Solid Brushes
            public double R;
            public double G;
            public double B;
            public double A;

            // Gradient Brushes
            // X0          - Linear: StartX, Radial: StartX
            // Y0          - Linear: StartY, Radial: StartY
            // X1          - Linear: EndX,   Radial: CenterX (of outer circle)
            // Y1          - Linear: EndY,   Radial: CenterY (of outer circle)
            // outerRadius - Linear: Unused, Radial: Used
            public double X0;
            public double Y0;
            public double X1;
            public double Y1;
            public double OuterRadius;
            public IntPtr Stops;
            public UIntPtr NumStops;
        }

        [StructLayout(Layout)]
        public struct uiDrawBrushGradientStop
        {
            public double Pos;
            public double R;
            public double G;
            public double B;
            public double A;
        }

        [StructLayout(Layout)]
        public struct uiDrawStrokeParams
        {
            public uiDrawLineCap Cap;
            public uiDrawLineJoin Join;
            public double Thickness;
            public double MiterLimit;
            public IntPtr Dashes;
            public UIntPtr NumDashes;
            public double DashPhase;
        }

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiDrawNewPath(uiDrawFillMode fillMode);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawFreePath(IntPtr p);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathNewFigure(IntPtr p, double x, double y);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathNewFigureWithArc(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathLineTo(IntPtr p, double x, double y);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathArcTo(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathBezierTo(IntPtr p, double c1x, double c1y, double c2x, double c2y, double endX, double endY);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathCloseFigure(IntPtr p);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathAddRectangle(IntPtr p, double x, double y, double width, double height);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawPathEnd(IntPtr p);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawStroke(IntPtr context, IntPtr path, ref uiDrawBrush brush, ref uiDrawStrokeParams strokeParam);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawFill(IntPtr context, IntPtr path, ref uiDrawBrush brush);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixSetIdentity(uiDrawMatrix matrix);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixTranslate(uiDrawMatrix matrix, double x, double y);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixScale(uiDrawMatrix matrix, double xCenter, double yCenter, double x, double y);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixRotate(uiDrawMatrix matrix, double x, double y, double amount);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixSkew(uiDrawMatrix matrix, double x, double y, double xamount, double yamount);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixMultiply(uiDrawMatrix dest, uiDrawMatrix src);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiDrawMatrixInvertible(uiDrawMatrix matrix);
        [UnmanagedFunctionPointer(Convention)] public delegate int uiDrawMatrixInvert(uiDrawMatrix matrix);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixTransformPoint(uiDrawMatrix matrix, out double x, out double y);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawMatrixTransformSize(uiDrawMatrix matrix, out double x, out double y);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawTransform(IntPtr context, uiDrawMatrix matrix);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawClip(IntPtr context, IntPtr path);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawSave(IntPtr context);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawRestore(IntPtr context);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeAttribute(IntPtr a);

        public enum uiAttributeType : uint
        {
            uiAttributeTypeFamily,
            uiAttributeTypeSize,
            uiAttributeTypeWeight,
            uiAttributeTypeItalic,
            uiAttributeTypeStretch,
            uiAttributeTypeColor,
            uiAttributeTypeBackground,
            uiAttributeTypeUnderline,
            uiAttributeTypeUnderlineColor,
            uiAttributeTypeFeatures
        }

        [UnmanagedFunctionPointer(Convention)] public delegate uiAttributeType uiAttributeGetType(IntPtr a);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewFamilyAttribute(string family);
        [UnmanagedFunctionPointer(Convention)] public delegate string uiAttributeFamily(IntPtr a);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewSizeAttribute(double size);
        [UnmanagedFunctionPointer(Convention)] public delegate double uiAttributeSize(IntPtr a);

        public enum uiTextWeight : uint
        {
            uiTextWeightMinimum = 0,
            uiTextWeightThin = 100,
            uiTextWeightUltraLight = 200,
            uiTextWeightLight = 300,
            uiTextWeightBook = 350,
            uiTextWeightNormal = 400,
            uiTextWeightMedium = 500,
            uiTextWeightSemiBold = 600,
            uiTextWeightBold = 700,
            uiTextWeightUltraBold = 800,
            uiTextWeightHeavy = 900,
            uiTextWeightUltraHeavy = 950,
            uiTextWeightMaximum = 1000
        }

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewWeightAttribute(uiTextWeight weight);
        [UnmanagedFunctionPointer(Convention)] public delegate uiTextWeight uiAttributeWeight(IntPtr a);

        public enum uiTextItalic : uint
        {
            uiTextItalicNormal,
            uiTextItalicOblique,
            uiTextItalicItalic
        }

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewItalicAttribute(uiTextItalic italic);
        [UnmanagedFunctionPointer(Convention)] public delegate uiTextItalic uiAttributeItalic(IntPtr a);

        public enum uiTextStretch : uint
        {
            uiTextStretchUltraCondensed,
            uiTextStretchExtraCondensed,
            uiTextStretchCondensed,
            uiTextStretchSemiCondensed,
            uiTextStretchNormal,
            uiTextStretchSemiExpanded,
            uiTextStretchExpanded,
            uiTextStretchExtraExpanded,
            uiTextStretchUltraExpanded
        }

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewStretchAttribute(uiTextStretch stretch);
        [UnmanagedFunctionPointer(Convention)] public delegate uiTextStretch uiAttributeStretch(IntPtr a);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewColorAttribute(double r, double g, double b, double a);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAttributeColor(IntPtr a, out double r, out double g, out double b, out double alpha);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewBackgroundAttribute(double r, double g, double b, double a);

        public enum uiUnderline : uint
        {
            uiUnderlineNone,
            uiUnderlineSingle,
            uiUnderlineDouble,
            uiUnderlineSuggestion, // wavy or dotted underlines used for spelling/grammar checkers
        }

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewUnderlineAttribute(uiUnderline u);
        [UnmanagedFunctionPointer(Convention)] public delegate uiUnderline uiAttributeUnderline(IntPtr a);

        public enum uiUnderlineColor : uint
        {
            uiUnderlineColorCustom,
            uiUnderlineColorSpelling,
            uiUnderlineColorGrammar,
            uiUnderlineColorAuxiliary, // for instance, the color used by smart replacements on macOS or in Microsoft Office
        }

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewUnderlineColorAttribute(uiUnderlineColor u, double r, double g, double b, double a);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAttributeUnderlineColor(IntPtr a, out uiUnderlineColor u, out double r, out double g, out double b, out double alpha);

        [UnmanagedFunctionPointer(Convention)] public delegate uiForEach uiOpenTypeFeaturesForEachFunc(IntPtr otf, char a, char b, char c, char d, uint value, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewOpenTypeFeatures();
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeOpenTypeFeatures(IntPtr otf);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiOpenTypeFeaturesClone(IntPtr otf);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiOpenTypeFeaturesAdd(IntPtr otf, char a, char b, char c, char d, uint value);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiOpenTypeFeaturesRemove(IntPtr otf, char a, char b, char c, char d);
        [UnmanagedFunctionPointer(Convention)] public delegate int uiOpenTypeFeaturesGet(IntPtr otf, char a, char b, char c, char d, out uint value);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiOpenTypeFeaturesForEach(IntPtr otf, uiOpenTypeFeaturesForEachFunc f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewFeaturesAttribute(IntPtr otf);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiAttributeFeatures(IntPtr a);

        [UnmanagedFunctionPointer(Convention)] public delegate uiForEach uiAttributedStringForEachAttributeFunc(IntPtr s, IntPtr a, UIntPtr start, UIntPtr end, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewAttributedString(string initialString);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeAttributedString(IntPtr s);
        [UnmanagedFunctionPointer(Convention)] public delegate string uiAttributedStringString(IntPtr s);
        [UnmanagedFunctionPointer(Convention)] public delegate UIntPtr uiAttributedStringLen(IntPtr s);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAttributedStringAppendUnattributed(IntPtr s, string str);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAttributedStringInsertAtUnattributed(IntPtr s, string str, UIntPtr at);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAttributedStringDelete(IntPtr s, UIntPtr start, UIntPtr end);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAttributedStringSetAttribute(IntPtr s, IntPtr a, UIntPtr start, UIntPtr end);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiAttributedStringForEachAttribute(IntPtr s, uiAttributedStringForEachAttributeFunc f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate UIntPtr uiAttributedStringNumGraphemes(IntPtr s);
        [UnmanagedFunctionPointer(Convention)] public delegate UIntPtr uiAttributedStringByteIndexToGrapheme(IntPtr s, UIntPtr pos);
        [UnmanagedFunctionPointer(Convention)] public delegate UIntPtr uiAttributedStringGraphemeToByteIndex(IntPtr s, UIntPtr pos);

        [StructLayout(Layout)]
        public struct uiFontDescriptor
        {
            public string Family;
            public double Size;
            public uiTextWeight Weight;
            public uiTextItalic Italic;
            public uiTextStretch Stretch;
        }

        public enum uiDrawTextAlign : uint
        {
            uiDrawTextAlignLeft,
            uiDrawTextAlignCenter,
            uiDrawTextAlignRight
        }

        [StructLayout(Layout)]
        public struct uiDrawTextLayoutParams
        {
            public IntPtr String;
            public uiFontDescriptor DefaultFont;
            public double Width;
            public uiDrawTextAlign Align;
        }

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiDrawNewTextLayout(uiDrawTextLayoutParams param);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawFreeTextLayout(IntPtr tl);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawText(IntPtr c, IntPtr tl, double x, double y);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiDrawTextLayoutExtents(IntPtr tl, out double width, out double height);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiFontButtonFont(IntPtr b, out uiFontDescriptor desc);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFontButtonOnChanged(IntPtr b, uiFontButtonOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFontButtonOnChangedCallback(IntPtr b, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewFontButton();
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeFontButtonFont(uiFontDescriptor desc);

        public enum uiModifiers : uint
        {
            uiModifierCtrl = 1 << 0,
            uiModifierAlt = 1 << 1,
            uiModifierShift = 1 << 2,
            uiModifierSuper = 1 << 3
        }

        [StructLayout(Layout)]
        public struct uiAreaMouseEvent
        {
            public double X;
            public double Y;
            public double AreaWidth;
            public double AreaHeight;
            public bool Down;
            public bool Up;
            public int Count;
            public uiModifiers Modifiers;
            public ulong Held1To64;
        }

        public enum uiExtKey : uint
        {
            uiExtKeyEscape = 1,
            uiExtKeyInsert, // equivalent to "Help" on Apple keyboards
            uiExtKeyDelete,
            uiExtKeyHome,
            uiExtKeyEnd,
            uiExtKeyPageUp,
            uiExtKeyPageDown,
            uiExtKeyUp,
            uiExtKeyDown,
            uiExtKeyLeft,
            uiExtKeyRight,
            uiExtKeyF1, // F1..F12 are guaranteed to be consecutive
            uiExtKeyF2,
            uiExtKeyF3,
            uiExtKeyF4,
            uiExtKeyF5,
            uiExtKeyF6,
            uiExtKeyF7,
            uiExtKeyF8,
            uiExtKeyF9,
            uiExtKeyF10,
            uiExtKeyF11,
            uiExtKeyF12,
            uiExtKeyN0, // numpad keys; independent of Num Lock state
            uiExtKeyN1, // N0..N9 are guaranteed to be consecutive
            uiExtKeyN2,
            uiExtKeyN3,
            uiExtKeyN4,
            uiExtKeyN5,
            uiExtKeyN6,
            uiExtKeyN7,
            uiExtKeyN8,
            uiExtKeyN9,
            uiExtKeyNDot,
            uiExtKeyNEnter,
            uiExtKeyNAdd,
            uiExtKeyNSubtract,
            uiExtKeyNMultiply,
            uiExtKeyNDivide
        }

        [StructLayout(Layout)]
        public struct uiAreaKeyEvent
        {
            public char Key;
            public uiExtKey ExtKey;
            public uiModifiers Modifier;
            public uiModifiers Modifiers;
            public bool Up;
        }

        [UnmanagedFunctionPointer(Convention)] public delegate void uiColorButtonColor(IntPtr b, out double red, out double green, out double blue, out double alpha);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiColorButtonSetColor(IntPtr b, double red, double green, double blue, double alpha);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiColorButtonOnChanged(IntPtr b, uiColorButtonOnChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiColorButtonOnChangedCallback(IntPtr b, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewColorButton();

        [UnmanagedFunctionPointer(Convention)] public delegate void uiFormAppend(IntPtr f, string label, IntPtr c, bool stretchy);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFormDelete(IntPtr f, int index);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiFormPadded(IntPtr f);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFormSetPadded(IntPtr f, bool padded);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewForm();

        public enum uiAlign : uint
        {
            uiAlignFill,
            uiAlignStart,
            uiAlignCenter,
            uiAlignEnd
        }

        public enum uiAt : uint
        {
            uiAtLeading,
            uiAtTop,
            uiAtTrailing,
            uiAtBottom
        }

        [UnmanagedFunctionPointer(Convention)] public delegate void uiGridAppend(IntPtr g, IntPtr c, int left, int top, int xspan, int yspan, int hexpand, uiAlign halign, int vexpand, uiAlign valign);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiGridInsertAt(IntPtr g, IntPtr c, IntPtr existing, uiAt at, int xspan, int yspan, int hexpand, uiAlign halign, int vexpand, uiAlign valign);
        [UnmanagedFunctionPointer(Convention)] public delegate bool uiGridPadded(IntPtr g);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiGridSetPadded(IntPtr g, bool padded);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewGrid();

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewImage(double width, double height);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeImage(IntPtr i);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiImageAppend(IntPtr i, IntPtr pixels, int pixelWidth, int pixelHeight, int byteStride);

        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeTableValue(IntPtr v);

        public enum uiTableValueType : uint
        {
            uiTableValueTypeString,
            uiTableValueTypeImage,
            uiTableValueTypeInt,
            uiTableValueTypeColor
        }

        [UnmanagedFunctionPointer(Convention)] public delegate uiTableValueType uiTableValueGetType(IntPtr v);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTableValueString(string str);
        [UnmanagedFunctionPointer(Convention)] public delegate string uiTableValueString(IntPtr v);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTableValueImage(IntPtr img);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiTableValueImage(IntPtr v);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTableValueInt(int i);
        [UnmanagedFunctionPointer(Convention)] public delegate int uiTableValueInt(IntPtr v);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTableValueColor(double r, double g, double b, double a);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableValueColor(IntPtr v, out double r, out double g, out double b, out double a);

        [StructLayout(Layout)]
        public struct uiTableModelHandler
        {
            private IntPtr numColumns;
            private IntPtr columnType;
            private IntPtr numRows;
            private IntPtr cellValue;
            private IntPtr setCellValue;

            public uiTableModelHandlerNumColumns NumColumns
            {
                set => numColumns = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiTableModelHandlerColumnType ColumnType
            {
                set => columnType = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiTableModelHandlerNumRows NumRows
            {
                set => numRows = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiTableModelHandlerCellValue CellValue
            {
                set => cellValue = Marshal.GetFunctionPointerForDelegate(value);
            }

            public uiTableModelHandlerSetCellValue SetCellValue
            {
                set => setCellValue = Marshal.GetFunctionPointerForDelegate(value);
            }
        }
        [UnmanagedFunctionPointer(Convention)] public delegate int uiTableModelHandlerNumColumns(uiTableModelHandler mh, IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate uiTableValueType uiTableModelHandlerColumnType(uiTableModelHandler mh, IntPtr m, int column);
        [UnmanagedFunctionPointer(Convention)] public delegate int uiTableModelHandlerNumRows(uiTableModelHandler mh, IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiTableModelHandlerCellValue(uiTableModelHandler mh, IntPtr m, int row, int column);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableModelHandlerSetCellValue(uiTableModelHandler mh, IntPtr m, int row, int column, IntPtr v);

        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTableModel(uiTableModelHandler mh);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiFreeTableModel(IntPtr m);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableModelRowInserted(IntPtr m, int newIndex);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableModelRowChanged(IntPtr m, int index);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableModelRowDeleted(uiTableModelHandler m, int oldIndex);

        public const int uiTableModelColumnNeverEditable = -1;
        public const int uiTableModelColumnAlwaysEditable = -2;

        [StructLayout(Layout)]
        public struct uiTableTextColumnOptionalParams
        {
            public int ColorModelColumn;
        }

        [StructLayout(Layout)]
        public struct uiTableParams
        {
            public IntPtr Model;
            public int RowBackgroundColorModelColumn;
        }

        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableAppendTextColumn(IntPtr t, string name, int textModelColumn, int textEditableModelColumn, uiTableTextColumnOptionalParams textParams);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableAppendImageColumn(IntPtr t, string name, int imageModelColumn);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableAppendImageTextColumn(IntPtr t, string name, int imageModelColumn, int textModelColumn, int textEditableModelColumn, uiTableTextColumnOptionalParams textParams);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableAppendCheckboxColumn(IntPtr t, string name, int checkboxModelColumn, int checkboxEditableModelColumn);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableAppendCheckboxTextColumn(IntPtr t, string name, int checkboxModelColumn, int checkboxEditableModelColumn, int textModelColumn, int textEditableModelColumn, uiTableTextColumnOptionalParams textParams);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableAppendProgressBarColumn(IntPtr t, string name, int progressModelColumn);
        [UnmanagedFunctionPointer(Convention)] public delegate void uiTableAppendButtonColumn(IntPtr t, string name, int buttonModelColumn, int buttonClickableModelColumn);
        [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewTable(uiTableParams @params);
#pragma warning restore IDE1006
#pragma warning restore IDE0044
#pragma warning restore IDE0001
    }
}