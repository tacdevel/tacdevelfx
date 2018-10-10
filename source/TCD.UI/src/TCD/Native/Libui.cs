/****************************************************************************
 * FileName:   Libui.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180921
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Drawing;
using TCD.UI;
using TCD.UI.Controls;

namespace TCD.Native
{
    internal static class Libui
    {
        private const CallingConvention Cdecl = CallingConvention.Cdecl;

        internal enum ForEach : long
        {
            Continue,
            Stop
        }

        internal enum Align : long
        {
            Fill,
            Start,
            Center,
            End
        }

        [StructLayout(LayoutKind.Sequential)]
        internal class Time
        {
#pragma warning disable IDE0032 // Use auto property
#pragma warning disable IDE0044 // Add readonly modifier
            private int sec, min, hour, day, mon, year;
            private readonly int wday, yday; // Must be uninitialized.
            private readonly int isdst = -1; //Must be -1.
#pragma warning restore IDE0032 // Use auto property
#pragma warning restore IDE0044 // Add readonly modifier

            public Time(int year, int month, int day, int hour, int minute, int second)
            {
                sec = second;
                min = minute;
                this.hour = hour;
                this.day = day;
                mon = month;
                this.year = year;
            }

            public int Second
            {
                get => sec;
                set => sec = value;
            }

            public int Minute
            {
                get => min;
                set => min = value;
            }

            public int Hour
            {
                get => hour;
                set => hour = value;
            }

            public int Day
            {
                get => day;
                set => day = value;
            }

            public int Month
            {
                get => mon;
                set => mon = value;
            }

            public int Year
            {
                get => year;
                set => year = value;
            }

            public static DateTime ToDateTime(Time dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            public static Time FromDateTime(DateTime dt) => new Time(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AreaHandler
        {
            private IntPtr draw;
            private IntPtr mouseEvent;
            private IntPtr mouseCrossed;
            private IntPtr dragBroken;
            private IntPtr keyEvent;

            public Libui.AreaHandlerDrawCallback Draw
            {
                set => draw = Marshal.GetFunctionPointerForDelegate(value);
            }

            public Libui.AreaHandlerMouseEventCallback MouseEvent
            {
                set => mouseEvent = Marshal.GetFunctionPointerForDelegate(value);
            }

            public Libui.AreaHandlerMouseCrossedCallback MouseCrossed
            {
                set => mouseCrossed = Marshal.GetFunctionPointerForDelegate(value);
            }

            public Libui.AreaHandlerDragBrokenCallback DragBroken
            {
                set => dragBroken = Marshal.GetFunctionPointerForDelegate(value);
            }

            public Libui.AreaHandlerKeyEventCallback KeyEvent
            {
                set => keyEvent = Marshal.GetFunctionPointerForDelegate(value);
            }
        }

        #region Application
        internal static string Init(ref StartupOptions options) => AssemblyRef.Libui.LoadFunction<Signatures.uiInit>()(ref options);
        internal static void UnInit() => AssemblyRef.Libui.LoadFunction<Signatures.uiUnInit>()();
        internal static void FreeInitError(string error) => AssemblyRef.Libui.LoadFunction<Signatures.uiFreeInitError>()(error);
        internal static void Main() => AssemblyRef.Libui.LoadFunction<Signatures.uiMain>()();
        internal static void MainSteps() => AssemblyRef.Libui.LoadFunction<Signatures.uiMainSteps>()();
        internal static bool MainStep(bool wait) => AssemblyRef.Libui.LoadFunction<Signatures.uiMainStep>()(wait);
        internal static void Quit() => AssemblyRef.Libui.LoadFunction<Signatures.uiQuit>()();
        internal static void QueueMain(QueueMainCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiQueueMain>()(f, data);
        internal static void Timer(int milliseconds, TimerCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiTimer>()(milliseconds, f, data);
        internal static void OnShouldQuit(OnShouldQuitCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiOnShouldQuit>()(f, data);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void QueueMainCallback(IntPtr data);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate bool TimerCallback(IntPtr data);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate bool OnShouldQuitCallback(IntPtr data);
        #endregion Application
        #region Button
        internal static string ButtonText(IntPtr b) => AssemblyRef.Libui.LoadFunction<Signatures.uiButtonText>()(b);
        internal static void ButtonSetText(IntPtr b, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiButtonSetText>()(b, text);
        internal static void ButtonOnClicked(IntPtr b, ButtonOnClickedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiButtonOnClicked>()(b, f, data);
        internal static IntPtr NewButton(string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewButton>()(text);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void ButtonOnClickedCallback(IntPtr b, IntPtr data);
        #endregion
        #region CheckBox
        internal static string CheckboxText(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiCheckboxText>()(c);
        internal static void CheckboxSetText(IntPtr c, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiCheckboxSetText>()(c, text);
        internal static void CheckboxOnToggled(IntPtr c, CheckboxOnToggledCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiCheckboxOnToggled>()(c, f, data);
        internal static bool CheckboxChecked(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiCheckboxChecked>()(c);
        internal static void CheckboxSetChecked(IntPtr c, bool @checked) => AssemblyRef.Libui.LoadFunction<Signatures.uiCheckboxSetChecked>()(c, @checked);
        internal static IntPtr NewCheckbox(string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewCheckbox>()(text);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void CheckboxOnToggledCallback(IntPtr c, IntPtr data);
        #endregion CheckBox
        #region ColorPicker
        internal static void ColorButtonColor(IntPtr b, out double red, out double green, out double blue, out double alpha) => AssemblyRef.Libui.LoadFunction<Signatures.uiColorButtonColor>()(b, out red, out green, out blue, out alpha);
        internal static void ColorButtonSetColor(IntPtr b, double red, double green, double blue, double alpha) => AssemblyRef.Libui.LoadFunction<Signatures.uiColorButtonSetColor>()(b, red, green, blue, alpha);
        internal static void ColorButtonOnChanged(IntPtr b, ColorButtonOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiColorButtonOnChanged>()(b, f, data);
        internal static IntPtr NewColorButton() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewColorButton>()();
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void ColorButtonOnChangedCallback(IntPtr w, IntPtr data);
        #endregion ColorPicker
        #region ComboBox
        internal static void ComboboxAppend(IntPtr c, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiComboboxAppend>()(c, text);
        internal static int ComboboxSelected(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiComboboxSelected>()(c);
        internal static void ComboboxSetSelected(IntPtr c, int n) => AssemblyRef.Libui.LoadFunction<Signatures.uiComboboxSetSelected>()(c, n);
        internal static void ComboboxOnSelected(IntPtr c, ComboboxOnSelectedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiComboboxOnSelected>()(c, f, data);
        internal static IntPtr NewCombobox() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewCombobox>()();
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void ComboboxOnSelectedCallback(IntPtr c, IntPtr data);
        #endregion ComboBox
        #region Context
        internal static void DrawStroke(IntPtr context, IntPtr path, ref Brush brush, ref StrokeOptions strokeParam) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawStroke>()(context, path, ref brush, ref strokeParam);
        internal static void DrawFill(IntPtr context, IntPtr path, ref Brush brush) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawFill>()(context, path, ref brush);
        internal static void DrawTransform(IntPtr context, Matrix matrix) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawTransform>()(context, matrix);
        internal static void DrawClip(IntPtr context, IntPtr path) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawClip>()(context, path);
        internal static void DrawSave(IntPtr context) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawSave>()(context);
        internal static void DrawRestore(IntPtr context) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawRestore>()(context);
        #endregion Context
        #region Control
        internal static void ControlDestroy(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlDestroy>()(c);
        internal static IntPtr ControlParent(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlParent>()(c);
        internal static void ControlSetParent(IntPtr c, IntPtr parent) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlSetParent>()(c, parent);
        internal static bool ControlTopLevel(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlToplevel>()(c);
        internal static bool ControlVisible(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlVisible>()(c);
        internal static void ControlShow(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlShow>()(c);
        internal static void ControlHide(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlHide>()(c);
        internal static bool ControlEnabled(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlEnabled>()(c);
        internal static void ControlEnable(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlEnable>()(c);
        internal static void ControlDisable(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiControlDisable>()(c);
        #endregion Control
        #region DateTimePicker
        internal static void DateTimePickerTime(IntPtr d, out Time time) => AssemblyRef.Libui.LoadFunction<Signatures.uiDateTimePickerTime>()(d, out time);
        internal static void DateTimePickerSetTime(IntPtr d, Time time) => AssemblyRef.Libui.LoadFunction<Signatures.uiDateTimePickerSetTime>()(d, time);
        internal static void DateTimePickerOnChanged(IntPtr d, DateTimePickerOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiDateTimePickerOnChanged>()(d, f, data);
        internal static IntPtr NewDateTimePicker() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewDateTimePicker>()();
        internal static IntPtr NewDatePicker() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewDatePicker>()();
        internal static IntPtr NewTimePicker() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewTimePicker>()();
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void DateTimePickerOnChangedCallback(IntPtr d, IntPtr data);
        #endregion DateTimePicker
        #region EditableComboBox
        internal static void EditableComboboxAppend(IntPtr c, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiEditableComboboxAppend>()(c, text);
        internal static string EditableComboboxText(IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiEditableComboboxText>()(c);
        internal static void EditableComboboxSetText(IntPtr c, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiEditableComboboxSetText>()(c, text);
        internal static void EditableComboboxOnChanged(IntPtr c, EditableComboboxOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiEditableComboboxOnChanged>()(c, f, data);
        internal static IntPtr NewEditableCombobox() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewEditableCombobox>()();
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void EditableComboboxOnChangedCallback(IntPtr c, IntPtr data);
        #endregion EditableComboBox
        #region FontPicker
        internal static void FontButtonFont(IntPtr b, out Font desc) => AssemblyRef.Libui.LoadFunction<Signatures.uiFontButtonFont>()(b, out desc);
        internal static void FontButtonOnChanged(IntPtr b, FontButtonOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiFontButtonOnChanged>()(b, f, data);
        internal static IntPtr NewFontButton() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewFontButton>()();
        internal static void FreeFontButtonFont(Font desc) => AssemblyRef.Libui.LoadFunction<Signatures.uiFreeFontButtonFont>()(desc);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void FontButtonOnChangedCallback(IntPtr w, IntPtr data);
        #endregion FontPicker
        #region FormContainer
        internal static void FormAppend(IntPtr f, string label, IntPtr c, bool stretchy) => AssemblyRef.Libui.LoadFunction<Signatures.uiFormAppend>()(f, label, c, stretchy);
        internal static void FormDelete(IntPtr f, int index) => AssemblyRef.Libui.LoadFunction<Signatures.uiFormDelete>()(f, index);
        internal static bool FormPadded(IntPtr f) => AssemblyRef.Libui.LoadFunction<Signatures.uiFormPadded>()(f);
        internal static void FormSetPadded(IntPtr f, bool padded) => AssemblyRef.Libui.LoadFunction<Signatures.uiFormSetPadded>()(f, padded);
        internal static IntPtr NewForm() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewForm>()();
        #endregion FormContainer
        #region GridContainer
        internal static void GridAppend(IntPtr g, IntPtr c, int left, int top, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign) => AssemblyRef.Libui.LoadFunction<Signatures.uiGridAppend>()(g, c, left, top, xspan, yspan, hexpand, halign, vexpand, valign);
        internal static void GridInsertAt(IntPtr g, IntPtr c, IntPtr existing, RelativeAlignment at, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign) => AssemblyRef.Libui.LoadFunction<Signatures.uiGridInsertAt>()(g, c, existing, at, xspan, yspan, hexpand, halign, vexpand, valign);
        internal static bool GridPadded(IntPtr g) => AssemblyRef.Libui.LoadFunction<Signatures.uiGridPadded>()(g);
        internal static void GridSetPadded(IntPtr g, bool padded) => AssemblyRef.Libui.LoadFunction<Signatures.uiGridSetPadded>()(g, padded);
        internal static IntPtr NewGrid() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewGrid>()();
        #endregion GridContainer
        #region GroupContainer
        internal static string GroupTitle(IntPtr g) => AssemblyRef.Libui.LoadFunction<Signatures.uiGroupTitle>()(g);
        internal static void GroupSetTitle(IntPtr g, string title) => AssemblyRef.Libui.LoadFunction<Signatures.uiGroupSetTitle>()(g, title);
        internal static void GroupSetChild(IntPtr g, IntPtr child) => AssemblyRef.Libui.LoadFunction<Signatures.uiGroupSetChild>()(g, child);
        internal static bool GroupMargined(IntPtr g) => AssemblyRef.Libui.LoadFunction<Signatures.uiGroupMargined>()(g);
        internal static void GroupSetMargined(IntPtr g, bool margined) => AssemblyRef.Libui.LoadFunction<Signatures.uiGroupSetMargined>()(g, margined);
        internal static IntPtr NewGroup(string title) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewGroup>()(title);
        #endregion GroupContainer
        #region Label
        internal static string LabelText(IntPtr l) => AssemblyRef.Libui.LoadFunction<Signatures.uiLabelText>()(l);
        internal static void LabelSetText(IntPtr l, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiLabelSetText>()(l, text);
        internal static IntPtr NewLabel(string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewLabel>()(text);
        #endregion Label
        #region Matrix
        internal static void DrawMatrixSetIdentity(Matrix matrix) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixSetIdentity>()(matrix);
        internal static void DrawMatrixTranslate(Matrix matrix, double x, double y) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixTranslate>()(matrix, x, y);
        internal static void DrawMatrixScale(Matrix matrix, double xCenter, double yCenter, double x, double y) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixScale>()(matrix, xCenter, yCenter, x, y);
        internal static void DrawMatrixRotate(Matrix matrix, double x, double y, double amount) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixRotate>()(matrix, x, y, amount);
        internal static void DrawMatrixSkew(Matrix matrix, double x, double y, double xamount, double yamount) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixSkew>()(matrix, x, y, xamount, yamount);
        internal static void DrawMatrixMultiply(Matrix dest, Matrix src) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixMultiply>()(dest, src);
        internal static bool DrawMatrixInvertible(Matrix matrix) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixInvertible>()(matrix);
        internal static int DrawMatrixInvert(Matrix matrix) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixInvert>()(matrix);
        internal static void DrawMatrixTransformPoint(Matrix matrix, out double x, out double y) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixTransformPoint>()(matrix, out x, out y);
        internal static void DrawMatrixTransformSize(Matrix matrix, out double x, out double y) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawMatrixTransformSize>()(matrix, out x, out y);
        #endregion Matrix
        #region Menu
        internal static void MenuItemEnable(IntPtr m) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuItemEnable>()(m);
        internal static void MenuItemDisable(IntPtr m) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuItemDisable>()(m);
        internal static void MenuItemOnClicked(IntPtr m, MenuItemOnClickedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuItemOnClicked>()(m, f, data);
        internal static bool MenuItemChecked(IntPtr m) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuItemChecked>()(m);
        internal static void MenuItemSetChecked(IntPtr m, bool @checked) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuItemSetChecked>()(m, @checked);
        internal static IntPtr MenuAppendItem(IntPtr m, string name) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuAppendItem>()(m, name);
        internal static IntPtr MenuAppendCheckItem(IntPtr m, string name) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuAppendCheckItem>()(m, name);
        internal static IntPtr MenuAppendQuitItem(IntPtr m) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuAppendQuitItem>()(m);
        internal static IntPtr MenuAppendPreferencesItem(IntPtr m) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuAppendPreferencesItem>()(m);
        internal static IntPtr MenuAppendAboutItem(IntPtr m) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuAppendAboutItem>()(m);
        internal static void MenuAppendSeparator(IntPtr m) => AssemblyRef.Libui.LoadFunction<Signatures.uiMenuAppendSeparator>()(m);
        internal static IntPtr NewMenu(string name) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewMenu>()(name);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void MenuItemOnClickedCallback(IntPtr sender, IntPtr window, IntPtr data);
        #endregion Menu
        #region Path
        internal static IntPtr DrawNewPath(FillMode fillMode) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawNewPath>()(fillMode);
        internal static void DrawFreePath(IntPtr p) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawFreePath>()(p);
        internal static void DrawPathNewFigure(IntPtr p, double x, double y) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathNewFigure>()(p, x, y);
        internal static void DrawPathNewFigureWithArc(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathNewFigureWithArc>()(p, xCenter, yCenter, radius, startAngle, sweep, negative);
        internal static void DrawPathLineTo(IntPtr p, double x, double y) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathLineTo>()(p, x, y);
        internal static void DrawPathArcTo(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathArcTo>()(p, xCenter, yCenter, radius, startAngle, sweep, negative);
        internal static void DrawPathBezierTo(IntPtr p, double c1x, double c1y, double c2x, double c2y, double endX, double endY) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathBezierTo>()(p, c1x, c1y, c2x, c2y, endX, endY);
        internal static void DrawPathCloseFigure(IntPtr p) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathCloseFigure>()(p);
        internal static void DrawPathAddRectangle(IntPtr p, double x, double y, double width, double height) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathAddRectangle>()(p, x, y, width, height);
        internal static void DrawPathEnd(IntPtr p) => AssemblyRef.Libui.LoadFunction<Signatures.uiDrawPathEnd>()(p);
        #endregion Path
        #region ProgressBar
        internal static int ProgressBarValue(IntPtr p) => AssemblyRef.Libui.LoadFunction<Signatures.uiProgressBarValue>()(p);
        internal static void ProgressBarSetValue(IntPtr p, int n) => AssemblyRef.Libui.LoadFunction<Signatures.uiProgressBarSetValue>()(p, n);
        internal static IntPtr NewProgressBar() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewProgressBar>()();
        #endregion ProgressBar
        #region RadioButtonList
        internal static void RadioButtonsAppend(IntPtr r, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiRadioButtonsAppend>()(r, text);
        internal static int RadioButtonsSelected(IntPtr r) => AssemblyRef.Libui.LoadFunction<Signatures.uiRadioButtonsSelected>()(r);
        internal static void RadioButtonsSetSelected(IntPtr r, int n) => AssemblyRef.Libui.LoadFunction<Signatures.uiRadioButtonsSetSelected>()(r, n);
        internal static void RadioButtonsOnSelected(IntPtr r, RadioButtonsOnSelectedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiRadioButtonsOnSelected>()(r, f, data);
        internal static IntPtr NewRadioButtons() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewRadioButtons>()();
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void RadioButtonsOnSelectedCallback(IntPtr r, IntPtr data);
        #endregion RadioButtonList
        #region Separator
        internal static IntPtr NewHorizontalSeparator() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewHorizontalSeparator>()();
        internal static IntPtr NewVerticalSeparator() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewVerticalSeparator>()();
        #endregion Separator
        #region Slider
        internal static int SliderValue(IntPtr s) => AssemblyRef.Libui.LoadFunction<Signatures.uiSliderValue>()(s);
        internal static void SliderSetValue(IntPtr s, int value) => AssemblyRef.Libui.LoadFunction<Signatures.uiSliderSetValue>()(s, value);
        internal static void SliderOnChanged(IntPtr s, SliderOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiSliderOnChanged>()(s, f, data);
        internal static IntPtr NewSlider(int min, int max) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewSlider>()(min, max);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void SliderOnChangedCallback(IntPtr s, IntPtr data);
        #endregion Slider
        #region SpinBox
        internal static int SpinboxValue(IntPtr s) => AssemblyRef.Libui.LoadFunction<Signatures.uiSpinboxValue>()(s);
        internal static void SpinboxSetValue(IntPtr s, int value) => AssemblyRef.Libui.LoadFunction<Signatures.uiSpinboxSetValue>()(s, value);
        internal static void SpinboxOnChanged(IntPtr s, SpinboxOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiSpinboxOnChanged>()(s, f, data);
        internal static IntPtr NewSpinbox(int min, int max) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewSpinbox>()(min, max);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void SpinboxOnChangedCallback(IntPtr s, IntPtr data);
        #endregion SpinBox
        #region StackContainer
        internal static void BoxAppend(IntPtr b, IntPtr child, bool stretchy) => AssemblyRef.Libui.LoadFunction<Signatures.uiBoxAppend>()(b, child, stretchy);
        internal static void BoxDelete(IntPtr b, int index) => AssemblyRef.Libui.LoadFunction<Signatures.uiBoxDelete>()(b, index);
        internal static bool BoxPadded(IntPtr b) => AssemblyRef.Libui.LoadFunction<Signatures.uiBoxPadded>()(b);
        internal static void BoxSetPadded(IntPtr b, bool padded) => AssemblyRef.Libui.LoadFunction<Signatures.uiBoxSetPadded>()(b, padded);
        internal static IntPtr NewHorizontalBox() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewHorizontalBox>()();
        internal static IntPtr NewVerticalBox() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewVerticalBox>()();
        #endregion StackContainer
        #region Surface
        internal static void AreaSetSize(IntPtr a, int width, int height) => AssemblyRef.Libui.LoadFunction<Signatures.uiAreaSetSize>()(a, width, height);
        internal static void AreaQueueRedrawAll(IntPtr a) => AssemblyRef.Libui.LoadFunction<Signatures.uiAreaQueueRedrawAll>()(a);
        internal static void AreaScrollTo(IntPtr a, double x, double y, double width, double height) => AssemblyRef.Libui.LoadFunction<Signatures.uiAreaScrollTo>()(a, x, y, width, height);
        internal static void AreaBeginUserWindowMove(IntPtr a) => AssemblyRef.Libui.LoadFunction<Signatures.uiAreaBeginUserWindowMove>()(a);
        internal static void AreaBeginUserWindowResize(IntPtr a, WindowEdge edge) => AssemblyRef.Libui.LoadFunction<Signatures.uiAreaBeginUserWindowResize>()(a, edge);
        internal static IntPtr NewArea(AreaHandler ah) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewArea>()(ah);
        internal static IntPtr NewScrollingArea(AreaHandler ah, int width, int height) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewScrollingArea>()(ah, width, height);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void AreaHandlerDrawCallback(AreaHandler handler, IntPtr area, DrawEventArgs args);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void AreaHandlerMouseEventCallback(AreaHandler handler, IntPtr area, MouseEventArgs args);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void AreaHandlerMouseCrossedCallback(AreaHandler handler, IntPtr area, bool left);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void AreaHandlerDragBrokenCallback(AreaHandler handler, IntPtr area);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void AreaHandlerKeyEventCallback(AreaHandler handler, IntPtr area, KeyEventArgs args);
        #endregion Surface
        #region TabContainer
        internal static void TabAppend(IntPtr t, string name, IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiTabAppend>()(t, name, c);
        internal static void TabInsertAt(IntPtr t, string name, int before, IntPtr c) => AssemblyRef.Libui.LoadFunction<Signatures.uiTabInsertAt>()(t, name, before, c);
        internal static void TabDelete(IntPtr t, int index) => AssemblyRef.Libui.LoadFunction<Signatures.uiTabDelete>()(t, index);
        internal static int TabNumPages(IntPtr t) => AssemblyRef.Libui.LoadFunction<Signatures.uiTabNumPages>()(t);
        internal static bool TabMargined(IntPtr t, int page) => AssemblyRef.Libui.LoadFunction<Signatures.uiTabMargined>()(t, page);
        internal static void TabSetMargined(IntPtr t, int page, bool margined) => AssemblyRef.Libui.LoadFunction<Signatures.uiTabSetMargined>()(t, page, margined);
        internal static IntPtr NewTab() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewTab>()();
        #endregion TabContainer
        #region TextBlock
        internal static string MultilineEntryText(IntPtr e) => AssemblyRef.Libui.LoadFunction<Signatures.uiMultilineEntryText>()(e);
        internal static void MultilineEntrySetText(IntPtr e, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiMultilineEntrySetText>()(e, text);
        internal static void MultilineEntryAppend(IntPtr e, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiMultilineEntryAppend>()(e, text);
        internal static void MultilineEntryOnChanged(IntPtr e, MultilineEntryOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiMultilineEntryOnChanged>()(e, f, data);
        internal static bool MultilineEntryReadOnly(IntPtr e) => AssemblyRef.Libui.LoadFunction<Signatures.uiMultilineEntryReadOnly>()(e);
        internal static void MultilineEntrySetReadOnly(IntPtr e, bool @readonly) => AssemblyRef.Libui.LoadFunction<Signatures.uiMultilineEntrySetReadOnly>()(e, @readonly);
        internal static IntPtr NewMultilineEntry() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewMultilineEntry>()();
        internal static IntPtr NewNonWrappingMultilineEntry() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewNonWrappingMultilineEntry>()();
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void MultilineEntryOnChangedCallback(IntPtr e, IntPtr data);
        #endregion TextBlock
        #region TextBox
        internal static string EntryText(IntPtr e) => AssemblyRef.Libui.LoadFunction<Signatures.uiEntryText>()(e);
        internal static void EntrySetText(IntPtr e, string text) => AssemblyRef.Libui.LoadFunction<Signatures.uiEntrySetText>()(e, text);
        internal static void EntryOnChanged(IntPtr e, EntryOnChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiEntryOnChanged>()(e, f, data);
        internal static bool EntryReadOnly(IntPtr e) => AssemblyRef.Libui.LoadFunction<Signatures.uiEntryReadOnly>()(e);
        internal static void EntrySetReadOnly(IntPtr e, bool @readonly) => AssemblyRef.Libui.LoadFunction<Signatures.uiEntrySetReadOnly>()(e, @readonly);
        internal static IntPtr NewEntry() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewEntry>()();
        internal static IntPtr NewPasswordEntry() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewPasswordEntry>()();
        internal static IntPtr NewSearchEntry() => AssemblyRef.Libui.LoadFunction<Signatures.uiNewSearchEntry>()();
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void EntryOnChangedCallback(IntPtr e, IntPtr data);
        #endregion TextBox
        #region Window
        internal static string WindowTitle(IntPtr w) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowTitle>()(w);
        internal static void WindowSetTitle(IntPtr w, string title) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowSetTitle>()(w, title);
        internal static void WindowContentSize(IntPtr w, out int width, out int height) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowContentSize>()(w, out width, out height);
        internal static void WindowSetContentSize(IntPtr w, int width, int height) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowSetContentSize>()(w, width, height);
        internal static bool WindowFullscreen(IntPtr w) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowFullscreen>()(w);
        internal static void WindowSetFullscreen(IntPtr w, bool fullscreen) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowSetFullscreen>()(w, fullscreen);
        internal static void WindowOnContentSizeChanged(IntPtr w, WindowOnContentSizeChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowOnContentSizeChanged>()(w, f, data);
        internal static void WindowOnClosing(IntPtr w, WindowOnClosingCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowOnClosing>()(w, f, data);
        internal static bool WindowBorderless(IntPtr w) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowBorderless>()(w);
        internal static void WindowSetBorderless(IntPtr w, bool borderless) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowSetBorderless>()(w, borderless);
        internal static void WindowSetChild(IntPtr w, IntPtr child) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowSetChild>()(w, child);
        internal static bool WindowMargined(IntPtr w) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowMargined>()(w);
        internal static void WindowSetMargined(IntPtr w, bool margined) => AssemblyRef.Libui.LoadFunction<Signatures.uiWindowSetMargined>()(w, margined);
        internal static IntPtr NewWindow(string title, int width, int height, bool hasMenubar) => AssemblyRef.Libui.LoadFunction<Signatures.uiNewWindow>()(title, width, height, hasMenubar);
        internal static string OpenFile(IntPtr parent) => AssemblyRef.Libui.LoadFunction<Signatures.uiOpenFile>()(parent);
        internal static string SaveFile(IntPtr parent) => AssemblyRef.Libui.LoadFunction<Signatures.uiSaveFile>()(parent);
        internal static void MsgBox(IntPtr parent, string title, string description) => AssemblyRef.Libui.LoadFunction<Signatures.uiMsgBox>()(parent, title, description);
        internal static void MsgBoxError(IntPtr parent, string title, string description) => AssemblyRef.Libui.LoadFunction<Signatures.uiMsgBoxError>()(parent, title, description);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate void WindowOnContentSizeChangedCallback(IntPtr w, IntPtr data);
        [UnmanagedFunctionPointer(Cdecl)] internal delegate bool WindowOnClosingCallback(IntPtr w, IntPtr data);
        #endregion Window

        // Keep the delegates in this class in order with libui\ui.h
        private static class Signatures
        {
            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiInit(ref StartupOptions options);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiUnInit();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFreeInitError(string err);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMain();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMainSteps();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiMainStep(bool wait);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiQuit();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiQueueMain(QueueMainCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiTimer(int milliseconds, TimerCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiOnShouldQuit(OnShouldQuitCallback f, IntPtr data);

            //// [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFreeText(string text);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiControlDestroy(IntPtr c);
            //// [UnmanagedFunctionPointer(Cdecl)] internal delegate UIntPtr uiControlHandle(uiControl c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiControlParent(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiControlSetParent(IntPtr c, IntPtr parent);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiControlToplevel(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiControlVisible(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiControlShow(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiControlHide(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiControlEnabled(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiControlEnable(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiControlDisable(IntPtr c);
            //// [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiControlVerifySetParent(IntPtr c, IntPtr parent);
            //// [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiControlEnabledToUser(IntPtr c);
            //// [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAllocControl(UIntPtr n, uint OSsig, uint typesig, string typenamestr);
            //// [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFreeControl(uiControl c);
            //// [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiUserBugCannotSetParentOnTopLevel(string type);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiWindowTitle(IntPtr w);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowSetTitle(IntPtr w, string title);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowContentSize(IntPtr w, out int width, out int height);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowSetContentSize(IntPtr w, int width, int height);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiWindowFullscreen(IntPtr w);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowSetFullscreen(IntPtr w, bool fullscreen);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowOnContentSizeChanged(IntPtr w, WindowOnContentSizeChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowOnClosing(IntPtr w, WindowOnClosingCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiWindowBorderless(IntPtr w);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowSetBorderless(IntPtr w, bool borderless);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowSetChild(IntPtr w, IntPtr child);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiWindowMargined(IntPtr w);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiWindowSetMargined(IntPtr w, bool margined);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewWindow(string title, int width, int height, bool hasMenubar);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiButtonText(IntPtr b);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiButtonSetText(IntPtr b, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiButtonOnClicked(IntPtr b, ButtonOnClickedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewButton(string text);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiBoxAppend(IntPtr b, IntPtr child, bool stretchy);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiBoxDelete(IntPtr b, int index);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiBoxPadded(IntPtr b);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiBoxSetPadded(IntPtr b, bool padded);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewHorizontalBox();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewVerticalBox();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiCheckboxText(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiCheckboxSetText(IntPtr c, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiCheckboxOnToggled(IntPtr c, CheckboxOnToggledCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiCheckboxChecked(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiCheckboxSetChecked(IntPtr c, bool @checked);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewCheckbox(string text);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiEntryText(IntPtr e);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiEntrySetText(IntPtr e, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiEntryOnChanged(IntPtr e, EntryOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiEntryReadOnly(IntPtr e);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiEntrySetReadOnly(IntPtr e, bool @readonly);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewEntry();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewPasswordEntry();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewSearchEntry();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiLabelText(IntPtr l);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiLabelSetText(IntPtr l, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewLabel(string text);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiTabAppend(IntPtr t, string name, IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiTabInsertAt(IntPtr t, string name, int before, IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiTabDelete(IntPtr t, int index);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiTabNumPages(IntPtr t);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiTabMargined(IntPtr t, int page);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiTabSetMargined(IntPtr t, int page, bool margined);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewTab();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiGroupTitle(IntPtr g);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiGroupSetTitle(IntPtr g, string title);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiGroupSetChild(IntPtr g, IntPtr child);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiGroupMargined(IntPtr g);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiGroupSetMargined(IntPtr g, bool margined);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewGroup(string title);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiSpinboxValue(IntPtr s);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiSpinboxSetValue(IntPtr s, int value);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiSpinboxOnChanged(IntPtr s, SpinboxOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewSpinbox(int min, int max);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiSliderValue(IntPtr s);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiSliderSetValue(IntPtr s, int value);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiSliderOnChanged(IntPtr s, SliderOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewSlider(int min, int max);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiProgressBarValue(IntPtr p);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiProgressBarSetValue(IntPtr p, int n);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewProgressBar();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewHorizontalSeparator();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewVerticalSeparator();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiComboboxAppend(IntPtr c, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiComboboxSelected(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiComboboxSetSelected(IntPtr c, int n);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiComboboxOnSelected(IntPtr c, ComboboxOnSelectedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewCombobox();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiEditableComboboxAppend(IntPtr c, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiEditableComboboxText(IntPtr c);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiEditableComboboxSetText(IntPtr c, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiEditableComboboxOnChanged(IntPtr c, EditableComboboxOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewEditableCombobox();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiRadioButtonsAppend(IntPtr r, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiRadioButtonsSelected(IntPtr r);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiRadioButtonsSetSelected(IntPtr r, int n);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiRadioButtonsOnSelected(IntPtr r, RadioButtonsOnSelectedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewRadioButtons();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDateTimePickerTime(IntPtr d, out Time time);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDateTimePickerSetTime(IntPtr d, Time time);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDateTimePickerOnChanged(IntPtr d, DateTimePickerOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewDateTimePicker();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewDatePicker();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewTimePicker();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiMultilineEntryText(IntPtr e);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMultilineEntrySetText(IntPtr e, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMultilineEntryAppend(IntPtr e, string text);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMultilineEntryOnChanged(IntPtr e, MultilineEntryOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiMultilineEntryReadOnly(IntPtr e);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMultilineEntrySetReadOnly(IntPtr e, bool @readonly);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewMultilineEntry();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewNonWrappingMultilineEntry();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMenuItemEnable(IntPtr m);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMenuItemDisable(IntPtr m);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMenuItemOnClicked(IntPtr m, MenuItemOnClickedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiMenuItemChecked(IntPtr m);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMenuItemSetChecked(IntPtr m, bool @checked);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiMenuAppendItem(IntPtr m, string name);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiMenuAppendCheckItem(IntPtr m, string name);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiMenuAppendQuitItem(IntPtr m);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiMenuAppendPreferencesItem(IntPtr m);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiMenuAppendAboutItem(IntPtr m);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMenuAppendSeparator(IntPtr m);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewMenu(string name);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiOpenFile(IntPtr parent);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiSaveFile(IntPtr parent);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMsgBox(IntPtr parent, string title, string description);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiMsgBoxError(IntPtr parent, string title, string description);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAreaSetSize(IntPtr a, int width, int height);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAreaQueueRedrawAll(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAreaScrollTo(IntPtr a, double x, double y, double width, double height);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAreaBeginUserWindowMove(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAreaBeginUserWindowResize(IntPtr a, WindowEdge edge);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewArea(AreaHandler ah);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewScrollingArea(AreaHandler ah, int width, int height);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiDrawNewPath(FillMode fillMode);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawFreePath(IntPtr p);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathNewFigure(IntPtr p, double x, double y);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathNewFigureWithArc(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathLineTo(IntPtr p, double x, double y);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathArcTo(IntPtr p, double xCenter, double yCenter, double radius, double startAngle, double sweep, bool negative);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathBezierTo(IntPtr p, double c1x, double c1y, double c2x, double c2y, double endX, double endY);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathCloseFigure(IntPtr p);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathAddRectangle(IntPtr p, double x, double y, double width, double height);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawPathEnd(IntPtr p);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawStroke(IntPtr context, IntPtr path, ref Brush brush, ref StrokeOptions strokeParam);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawFill(IntPtr context, IntPtr path, ref Brush brush);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixSetIdentity(Matrix matrix);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixTranslate(Matrix matrix, double x, double y);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixScale(Matrix matrix, double xCenter, double yCenter, double x, double y);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixRotate(Matrix matrix, double x, double y, double amount);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixSkew(Matrix matrix, double x, double y, double xamount, double yamount);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixMultiply(Matrix dest, Matrix src);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiDrawMatrixInvertible(Matrix matrix);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiDrawMatrixInvert(Matrix matrix);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixTransformPoint(Matrix matrix, out double x, out double y);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawMatrixTransformSize(Matrix matrix, out double x, out double y);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawTransform(IntPtr context, Matrix matrix);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawClip(IntPtr context, IntPtr path);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawSave(IntPtr context);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawRestore(IntPtr context);

            //TODO: Functions for the following delegates.
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFreeAttribute(IntPtr a);
            //TODO: [UnmanagedFunctionPointer(Cdecl)] internal delegate uiAttributeType uiAttributeGetType(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewFamilyAttribute(string family);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiAttributeFamily(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewSizeAttribute(double size);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate double uiAttributeSize(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewWeightAttribute(FontWeight weight);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate FontWeight uiAttributeWeight(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewItalicAttribute(FontStyle italic);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate FontStyle uiAttributeItalic(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewStretchAttribute(FontStretch stretch);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate FontStretch uiAttributeStretch(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewColorAttribute(double r, double g, double b, double a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAttributeColor(IntPtr a, out double r, out double g, out double b, out double alpha);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewBackgroundAttribute(double r, double g, double b, double a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewUnderlineAttribute(UnderlineStyle u);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate UnderlineStyle uiAttributeUnderline(IntPtr a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewUnderlineColorAttribute(UnderlineColor u, double r, double g, double b, double a);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAttributeUnderlineColor(IntPtr a, out UnderlineColor u, out double r, out double g, out double b, out double alpha);

            //TODO: [UnmanagedFunctionPointer(Cdecl)] internal delegate uiForEach uiOpenTypeFeaturesForEachFunc(IntPtr otf, char a, char b, char c, char d, uint value, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewOpenTypeFeatures();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFreeOpenTypeFeatures(IntPtr otf);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiOpenTypeFeaturesClone(IntPtr otf);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiOpenTypeFeaturesAdd(IntPtr otf, char a, char b, char c, char d, uint value);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiOpenTypeFeaturesRemove(IntPtr otf, char a, char b, char c, char d);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate int uiOpenTypeFeaturesGet(IntPtr otf, char a, char b, char c, char d, out uint value);
            //TODO: [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiOpenTypeFeaturesForEach(IntPtr otf, uiOpenTypeFeaturesForEachFunc f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewFeaturesAttribute(IntPtr otf);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiAttributeFeatures(IntPtr a);

            //TODO: [UnmanagedFunctionPointer(Cdecl)] internal delegate uiForEach uiAttributedStringForEachAttributeFunc(IntPtr s, IntPtr a, UIntPtr start, UIntPtr end, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewAttributedString(string initialString);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFreeAttributedString(IntPtr s);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate string uiAttributedStringString(IntPtr s);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate UIntPtr uiAttributedStringLen(IntPtr s);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAttributedStringAppendUnattributed(IntPtr s, IntPtr str);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAttributedStringInsertAtUnattributed(IntPtr s, IntPtr str, UIntPtr at);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAttributedStringDelete(IntPtr s, UIntPtr start, UIntPtr end);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAttributedStringSetAttribute(IntPtr s, IntPtr a, UIntPtr start, UIntPtr end);
            //TODO: [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiAttributedStringForEachAttribute(IntPtr s, uiAttributedStringForEachAttributeFunc f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate UIntPtr uiAttributedStringNumGraphemes(IntPtr s);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate UIntPtr uiAttributedStringByteIndexToGrapheme(IntPtr s, UIntPtr pos);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate UIntPtr uiAttributedStringGraphemeToByteIndex(IntPtr s, UIntPtr pos);

            //TODO: [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiDrawNewTextLayout(uiDrawTextLayoutParams param);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawFreeTextLayout(IntPtr tl);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawText(IntPtr c, IntPtr tl, double x, double y);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiDrawTextLayoutExtents(IntPtr tl, out double width, out double height);
            //TODO: Functions for the above delegates.

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFontButtonFont(IntPtr b, out Font desc);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFontButtonOnChanged(IntPtr b, FontButtonOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewFontButton();
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFreeFontButtonFont(Font desc);

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiColorButtonColor(IntPtr b, out double red, out double green, out double blue, out double alpha);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiColorButtonSetColor(IntPtr b, double red, double green, double blue, double alpha);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiColorButtonOnChanged(IntPtr b, ColorButtonOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewColorButton();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFormAppend(IntPtr f, string label, IntPtr c, bool stretchy);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFormDelete(IntPtr f, int index);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiFormPadded(IntPtr f);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiFormSetPadded(IntPtr f, bool padded);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewForm();

            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiGridAppend(IntPtr g, IntPtr c, int left, int top, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiGridInsertAt(IntPtr g, IntPtr c, IntPtr existing, RelativeAlignment at, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate bool uiGridPadded(IntPtr g);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate void uiGridSetPadded(IntPtr g, bool padded);
            [UnmanagedFunctionPointer(Cdecl)] internal delegate IntPtr uiNewGrid();

            internal delegate IntPtr uiNewImage(double width, double height);
            internal delegate void uiFreeImage(IntPtr i);
            internal delegate void uiImageAppend(IntPtr i, IntPtr pixels, int pixelWidth, int pixelHeight, int byteStride);

            //TODO: uiTable
        }
    }
}