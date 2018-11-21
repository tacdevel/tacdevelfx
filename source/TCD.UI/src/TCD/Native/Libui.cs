/***************************************************************************************************
 * FileName:             Libui.cs
 * Date:                 20181120
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.InteropServices;
using TCD.UI;
using TCD.UI.Controls;
using static TCD.PlatformHelper;

namespace TCD.Native
{
    internal static class Libui
    {
        internal const CallingConvention Convention = CallingConvention.Cdecl;
        
        private static NativeAssembly AssemblyRef
        {
            get
            {
                if (CurrentPlatform == Platform.Windows && OSArchitecture == Architecture.X64)
                    return new NativeAssembly(@"runtimes\win-x64\native\libui.dll");
                else if (CurrentPlatform == Platform.MacOS && OSArchitecture == Architecture.X64)
                    return new NativeAssembly(@"runtimes/osx-x64/native/libui.dylib", @"runtimes/osx-x64/native/libui.A.dylib");
                else if ((CurrentPlatform == Platform.Linux || CurrentPlatform == Platform.FreeBSD) && OSArchitecture == Architecture.X64)
                    return new NativeAssembly(@"runtimes/linux-x64/native/libui.so", @"runtimes/linux-x64/native/libui.so.0");
                else throw new PlatformNotSupportedException();
            }
        }

#if !DEBUG
        internal static T LoadFunction<T>() where T : Delegate => AssemblyRef.LoadFunction<T>(typeof(T).Name);
#else
        internal static T LoadFunction<T>() where T : Delegate
        {
            bool fail = false;

            Console.Write($"[DEBUG] Calling native function '{typeof(T).Name}' from assembly '{nameof(Libui)}'...");

            try
            {
                return AssemblyRef.LoadFunction<T>(typeof(T).Name);
            }
            catch (Exception ex)
            {
                fail = true;
                Console.Write($" Failed.");
                Console.WriteLine();
                Console.WriteLine($"[DEBUG] Exception Type: {ex.GetType().Name}");
                Console.WriteLine($"[DEBUG] Inner Exception Type: {ex.InnerException.GetType().Name}");
                Console.WriteLine($"[DEBUG] Source: {ex.Source}");
                Console.WriteLine($"[DEBUG] Message: {ex.Message}");
                Console.WriteLine($"[DEBUG] Source: {ex.StackTrace}");
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
#endif

        internal enum Align : long
        {
            Fill,
            Start,
            Center,
            End
        }

        [StructLayout(LayoutKind.Sequential)]
        internal sealed class Time
        {
#pragma warning disable IDE0032 // Use auto property
#pragma warning disable IDE0044 // Add readonly modifier
            private int sec, min, hour, day, mon, year;
            private readonly int wday, yday; // Must be uninitialized.
            private readonly int isdst = -1; //Must be -1.
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0032 // Use auto property

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

        #region Application
        internal static string Init(ref StartupOptions options) => LoadFunction<Signatures.uiInit>()(ref options);
        internal static void UnInit() => LoadFunction<Signatures.uiUnInit>()();
        internal static void FreeInitError(string error) => LoadFunction<Signatures.uiFreeInitError>()(error);
        internal static void Main() => LoadFunction<Signatures.uiMain>()();
        internal static void MainSteps() => LoadFunction<Signatures.uiMainSteps>()();
        internal static bool MainStep(bool wait) => LoadFunction<Signatures.uiMainStep>()(wait);
        internal static void Quit() => LoadFunction<Signatures.uiQuit>()();
        internal static void QueueMain(QueueMainCallback f, IntPtr data) => LoadFunction<Signatures.uiQueueMain>()(f, data);
        internal static void Timer(int milliseconds, TimerCallback f, IntPtr data) => LoadFunction<Signatures.uiTimer>()(milliseconds, f, data);
        internal static void OnShouldQuit(OnShouldQuitCallback f, IntPtr data) => LoadFunction<Signatures.uiOnShouldQuit>()(f, data);
        [UnmanagedFunctionPointer(Convention)] internal delegate void QueueMainCallback(IntPtr data);
        [UnmanagedFunctionPointer(Convention)] internal delegate bool TimerCallback(IntPtr data);
        [UnmanagedFunctionPointer(Convention)] internal delegate bool OnShouldQuitCallback(IntPtr data);
        #endregion Application
        #region Control
        internal static void ControlDestroy(IntPtr c) => LoadFunction<Signatures.uiControlDestroy>()(c);
        internal static IntPtr ControlParent(IntPtr c) => LoadFunction<Signatures.uiControlParent>()(c);
        internal static void ControlSetParent(IntPtr c, IntPtr parent) => LoadFunction<Signatures.uiControlSetParent>()(c, parent);
        internal static bool ControlTopLevel(IntPtr c) => LoadFunction<Signatures.uiControlToplevel>()(c);
        internal static bool ControlVisible(IntPtr c) => LoadFunction<Signatures.uiControlVisible>()(c);
        internal static void ControlShow(IntPtr c) => LoadFunction<Signatures.uiControlShow>()(c);
        internal static void ControlHide(IntPtr c) => LoadFunction<Signatures.uiControlHide>()(c);
        internal static bool ControlEnabled(IntPtr c) => LoadFunction<Signatures.uiControlEnabled>()(c);
        internal static void ControlEnable(IntPtr c) => LoadFunction<Signatures.uiControlEnable>()(c);
        internal static void ControlDisable(IntPtr c) => LoadFunction<Signatures.uiControlDisable>()(c);
        #endregion Control
        #region Button
        internal static string ButtonText(IntPtr b) => LoadFunction<Signatures.uiButtonText>()(b);
        internal static void ButtonSetText(IntPtr b, string text) => LoadFunction<Signatures.uiButtonSetText>()(b, text);
        internal static void ButtonOnClicked(IntPtr b, ButtonOnClickedCallback f, IntPtr data) => LoadFunction<Signatures.uiButtonOnClicked>()(b, f, data);
        internal static IntPtr NewButton(string text) => LoadFunction<Signatures.uiNewButton>()(text);
        [UnmanagedFunctionPointer(Convention)] internal delegate void ButtonOnClickedCallback(IntPtr b, IntPtr data);
        #endregion Button
        #region CheckBox
        internal static string CheckboxText(IntPtr c) => LoadFunction<Signatures.uiCheckboxText>()(c);
        internal static void CheckboxSetText(IntPtr c, string text) => LoadFunction<Signatures.uiCheckboxSetText>()(c, text);
        internal static void CheckboxOnToggled(IntPtr c, CheckboxOnToggledCallback f, IntPtr data) => LoadFunction<Signatures.uiCheckboxOnToggled>()(c, f, data);
        internal static bool CheckboxChecked(IntPtr c) => LoadFunction<Signatures.uiCheckboxChecked>()(c);
        internal static void CheckboxSetChecked(IntPtr c, bool @checked) => LoadFunction<Signatures.uiCheckboxSetChecked>()(c, @checked);
        internal static IntPtr NewCheckbox(string text) => LoadFunction<Signatures.uiNewCheckbox>()(text);
        [UnmanagedFunctionPointer(Convention)] internal delegate void CheckboxOnToggledCallback(IntPtr c, IntPtr data);
        #endregion CheckBox
        #region ComboBox
        internal static void ComboboxAppend(IntPtr c, string text) => LoadFunction<Signatures.uiComboboxAppend>()(c, text);
        internal static int ComboboxSelected(IntPtr c) => LoadFunction<Signatures.uiComboboxSelected>()(c);
        internal static void ComboboxSetSelected(IntPtr c, int n) => LoadFunction<Signatures.uiComboboxSetSelected>()(c, n);
        internal static void ComboboxOnSelected(IntPtr c, ComboboxOnSelectedCallback f, IntPtr data) => LoadFunction<Signatures.uiComboboxOnSelected>()(c, f, data);
        internal static IntPtr NewCombobox() => LoadFunction<Signatures.uiNewCombobox>()();
        [UnmanagedFunctionPointer(Convention)] internal delegate void ComboboxOnSelectedCallback(IntPtr c, IntPtr data);
        #endregion ComboBox
        #region DateTimePicker
        internal static void DateTimePickerTime(IntPtr d, out Time time) => LoadFunction<Signatures.uiDateTimePickerTime>()(d, out time);
        internal static void DateTimePickerSetTime(IntPtr d, Time time) => LoadFunction<Signatures.uiDateTimePickerSetTime>()(d, time);
        internal static void DateTimePickerOnChanged(IntPtr d, DateTimePickerOnChangedCallback f, IntPtr data) => LoadFunction<Signatures.uiDateTimePickerOnChanged>()(d, f, data);
        internal static IntPtr NewDateTimePicker() => LoadFunction<Signatures.uiNewDateTimePicker>()();
        internal static IntPtr NewDatePicker() => LoadFunction<Signatures.uiNewDatePicker>()();
        internal static IntPtr NewTimePicker() => LoadFunction<Signatures.uiNewTimePicker>()();
        [UnmanagedFunctionPointer(Convention)] internal delegate void DateTimePickerOnChangedCallback(IntPtr d, IntPtr data);
        #endregion DateTimePicker
        #region EditableComboBox
        internal static void EditableComboboxAppend(IntPtr c, string text) => LoadFunction<Signatures.uiEditableComboboxAppend>()(c, text);
        internal static string EditableComboboxText(IntPtr c) => LoadFunction<Signatures.uiEditableComboboxText>()(c);
        internal static void EditableComboboxSetText(IntPtr c, string text) => LoadFunction<Signatures.uiEditableComboboxSetText>()(c, text);
        internal static void EditableComboboxOnChanged(IntPtr c, EditableComboboxOnChangedCallback f, IntPtr data) => LoadFunction<Signatures.uiEditableComboboxOnChanged>()(c, f, data);
        internal static IntPtr NewEditableCombobox() => LoadFunction<Signatures.uiNewEditableCombobox>()();
        [UnmanagedFunctionPointer(Convention)] internal delegate void EditableComboboxOnChangedCallback(IntPtr c, IntPtr data);
        #endregion EditableComboBox
        #region FormContainer
        internal static void FormAppend(IntPtr f, string label, IntPtr c, bool stretchy) => LoadFunction<Signatures.uiFormAppend>()(f, label, c, stretchy);
        internal static void FormDelete(IntPtr f, int index) => LoadFunction<Signatures.uiFormDelete>()(f, index);
        internal static bool FormPadded(IntPtr f) => LoadFunction<Signatures.uiFormPadded>()(f);
        internal static void FormSetPadded(IntPtr f, bool padded) => LoadFunction<Signatures.uiFormSetPadded>()(f, padded);
        internal static IntPtr NewForm() => LoadFunction<Signatures.uiNewForm>()();
        #endregion FormContainer
        #region GridContainer
        internal static void GridAppend(IntPtr g, IntPtr c, int left, int top, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign) => LoadFunction<Signatures.uiGridAppend>()(g, c, left, top, xspan, yspan, hexpand, halign, vexpand, valign);
        internal static void GridInsertAt(IntPtr g, IntPtr c, IntPtr existing, RelativeAlignment at, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign) => LoadFunction<Signatures.uiGridInsertAt>()(g, c, existing, at, xspan, yspan, hexpand, halign, vexpand, valign);
        internal static bool GridPadded(IntPtr g) => LoadFunction<Signatures.uiGridPadded>()(g);
        internal static void GridSetPadded(IntPtr g, bool padded) => LoadFunction<Signatures.uiGridSetPadded>()(g, padded);
        internal static IntPtr NewGrid() => LoadFunction<Signatures.uiNewGrid>()();
        #endregion GridContainer
        #region GroupContainer
        internal static string GroupTitle(IntPtr g) => LoadFunction<Signatures.uiGroupTitle>()(g);
        internal static void GroupSetTitle(IntPtr g, string title) => LoadFunction<Signatures.uiGroupSetTitle>()(g, title);
        internal static void GroupSetChild(IntPtr g, IntPtr child) => LoadFunction<Signatures.uiGroupSetChild>()(g, child);
        internal static bool GroupMargined(IntPtr g) => LoadFunction<Signatures.uiGroupMargined>()(g);
        internal static void GroupSetMargined(IntPtr g, bool margined) => LoadFunction<Signatures.uiGroupSetMargined>()(g, margined);
        internal static IntPtr NewGroup(string title) => LoadFunction<Signatures.uiNewGroup>()(title);
        #endregion GroupContainer
        #region Label
        internal static string LabelText(IntPtr l) => LoadFunction<Signatures.uiLabelText>()(l);
        internal static void LabelSetText(IntPtr l, string text) => LoadFunction<Signatures.uiLabelSetText>()(l, text);
        internal static IntPtr NewLabel(string text) => LoadFunction<Signatures.uiNewLabel>()(text);
        #endregion Label
        #region Menu
        internal static void MenuItemEnable(IntPtr m) => LoadFunction<Signatures.uiMenuItemEnable>()(m);
        internal static void MenuItemDisable(IntPtr m) => LoadFunction<Signatures.uiMenuItemDisable>()(m);
        internal static void MenuItemOnClicked(IntPtr m, MenuItemOnClickedCallback f, IntPtr data) => LoadFunction<Signatures.uiMenuItemOnClicked>()(m, f, data);
        internal static bool MenuItemChecked(IntPtr m) => LoadFunction<Signatures.uiMenuItemChecked>()(m);
        internal static void MenuItemSetChecked(IntPtr m, bool @checked) => LoadFunction<Signatures.uiMenuItemSetChecked>()(m, @checked);
        internal static IntPtr MenuAppendItem(IntPtr m, string name) => LoadFunction<Signatures.uiMenuAppendItem>()(m, name);
        internal static IntPtr MenuAppendCheckItem(IntPtr m, string name) => LoadFunction<Signatures.uiMenuAppendCheckItem>()(m, name);
        internal static IntPtr MenuAppendQuitItem(IntPtr m) => LoadFunction<Signatures.uiMenuAppendQuitItem>()(m);
        internal static IntPtr MenuAppendPreferencesItem(IntPtr m) => LoadFunction<Signatures.uiMenuAppendPreferencesItem>()(m);
        internal static IntPtr MenuAppendAboutItem(IntPtr m) => LoadFunction<Signatures.uiMenuAppendAboutItem>()(m);
        internal static void MenuAppendSeparator(IntPtr m) => LoadFunction<Signatures.uiMenuAppendSeparator>()(m);
        internal static IntPtr NewMenu(string name) => LoadFunction<Signatures.uiNewMenu>()(name);
        [UnmanagedFunctionPointer(Convention)] internal delegate void MenuItemOnClickedCallback(IntPtr sender, IntPtr window, IntPtr data);
        #endregion Menu
        #region ProgressBar
        internal static int ProgressBarValue(IntPtr p) => LoadFunction<Signatures.uiProgressBarValue>()(p);
        internal static void ProgressBarSetValue(IntPtr p, int n) => LoadFunction<Signatures.uiProgressBarSetValue>()(p, n);
        internal static IntPtr NewProgressBar() => LoadFunction<Signatures.uiNewProgressBar>()();
        #endregion ProgressBar
        #region RadioButtonList
        internal static void RadioButtonsAppend(IntPtr r, string text) => LoadFunction<Signatures.uiRadioButtonsAppend>()(r, text);
        internal static int RadioButtonsSelected(IntPtr r) => LoadFunction<Signatures.uiRadioButtonsSelected>()(r);
        internal static void RadioButtonsSetSelected(IntPtr r, int n) => LoadFunction<Signatures.uiRadioButtonsSetSelected>()(r, n);
        internal static void RadioButtonsOnSelected(IntPtr r, RadioButtonsOnSelectedCallback f, IntPtr data) => LoadFunction<Signatures.uiRadioButtonsOnSelected>()(r, f, data);
        internal static IntPtr NewRadioButtons() => LoadFunction<Signatures.uiNewRadioButtons>()();
        [UnmanagedFunctionPointer(Convention)] internal delegate void RadioButtonsOnSelectedCallback(IntPtr r, IntPtr data);
        #endregion RadioButtonList
        #region Separator
        internal static IntPtr NewHorizontalSeparator() => LoadFunction<Signatures.uiNewHorizontalSeparator>()();
        internal static IntPtr NewVerticalSeparator() => LoadFunction<Signatures.uiNewVerticalSeparator>()();
        #endregion Separator
        #region Slider
        internal static int SliderValue(IntPtr s) => LoadFunction<Signatures.uiSliderValue>()(s);
        internal static void SliderSetValue(IntPtr s, int value) => LoadFunction<Signatures.uiSliderSetValue>()(s, value);
        internal static void SliderOnChanged(IntPtr s, SliderOnChangedCallback f, IntPtr data) => LoadFunction<Signatures.uiSliderOnChanged>()(s, f, data);
        internal static IntPtr NewSlider(int min, int max) => LoadFunction<Signatures.uiNewSlider>()(min, max);
        [UnmanagedFunctionPointer(Convention)] internal delegate void SliderOnChangedCallback(IntPtr s, IntPtr data);
        #endregion Slider
        #region SpinBox
        internal static int SpinboxValue(IntPtr s) => LoadFunction<Signatures.uiSpinboxValue>()(s);
        internal static void SpinboxSetValue(IntPtr s, int value) => LoadFunction<Signatures.uiSpinboxSetValue>()(s, value);
        internal static void SpinboxOnChanged(IntPtr s, SpinboxOnChangedCallback f, IntPtr data) => LoadFunction<Signatures.uiSpinboxOnChanged>()(s, f, data);
        internal static IntPtr NewSpinbox(int min, int max) => LoadFunction<Signatures.uiNewSpinbox>()(min, max);
        [UnmanagedFunctionPointer(Convention)] internal delegate void SpinboxOnChangedCallback(IntPtr s, IntPtr data);
        #endregion SpinBox
        #region StackContainer
        internal static void BoxAppend(IntPtr b, IntPtr child, bool stretchy) => LoadFunction<Signatures.uiBoxAppend>()(b, child, stretchy);
        internal static void BoxDelete(IntPtr b, int index) => LoadFunction<Signatures.uiBoxDelete>()(b, index);
        internal static bool BoxPadded(IntPtr b) => LoadFunction<Signatures.uiBoxPadded>()(b);
        internal static void BoxSetPadded(IntPtr b, bool padded) => LoadFunction<Signatures.uiBoxSetPadded>()(b, padded);
        internal static IntPtr NewHorizontalBox() => LoadFunction<Signatures.uiNewHorizontalBox>()();
        internal static IntPtr NewVerticalBox() => LoadFunction<Signatures.uiNewVerticalBox>()();
        #endregion StackContainer
        #region TabContainer
        internal static void TabAppend(IntPtr t, string name, IntPtr c) => LoadFunction<Signatures.uiTabAppend>()(t, name, c);
        internal static void TabInsertAt(IntPtr t, string name, int before, IntPtr c) => LoadFunction<Signatures.uiTabInsertAt>()(t, name, before, c);
        internal static void TabDelete(IntPtr t, int index) => LoadFunction<Signatures.uiTabDelete>()(t, index);
        internal static int TabNumPages(IntPtr t) => LoadFunction<Signatures.uiTabNumPages>()(t);
        internal static bool TabMargined(IntPtr t, int page) => LoadFunction<Signatures.uiTabMargined>()(t, page);
        internal static void TabSetMargined(IntPtr t, int page, bool margined) => LoadFunction<Signatures.uiTabSetMargined>()(t, page, margined);
        internal static IntPtr NewTab() => LoadFunction<Signatures.uiNewTab>()();
        #endregion TabContainer
        #region TextBlock
        internal static string MultilineEntryText(IntPtr e) => LoadFunction<Signatures.uiMultilineEntryText>()(e);
        internal static void MultilineEntrySetText(IntPtr e, string text) => LoadFunction<Signatures.uiMultilineEntrySetText>()(e, text);
        internal static void MultilineEntryAppend(IntPtr e, string text) => LoadFunction<Signatures.uiMultilineEntryAppend>()(e, text);
        internal static void MultilineEntryOnChanged(IntPtr e, MultilineEntryOnChangedCallback f, IntPtr data) => LoadFunction<Signatures.uiMultilineEntryOnChanged>()(e, f, data);
        internal static bool MultilineEntryReadOnly(IntPtr e) => LoadFunction<Signatures.uiMultilineEntryReadOnly>()(e);
        internal static void MultilineEntrySetReadOnly(IntPtr e, bool @readonly) => LoadFunction<Signatures.uiMultilineEntrySetReadOnly>()(e, @readonly);
        internal static IntPtr NewMultilineEntry() => LoadFunction<Signatures.uiNewMultilineEntry>()();
        internal static IntPtr NewNonWrappingMultilineEntry() => LoadFunction<Signatures.uiNewNonWrappingMultilineEntry>()();
        [UnmanagedFunctionPointer(Convention)] internal delegate void MultilineEntryOnChangedCallback(IntPtr e, IntPtr data);
        #endregion TextBlock
        #region TextBox
        internal static string EntryText(IntPtr e) => LoadFunction<Signatures.uiEntryText>()(e);
        internal static void EntrySetText(IntPtr e, string text) => LoadFunction<Signatures.uiEntrySetText>()(e, text);
        internal static void EntryOnChanged(IntPtr e, EntryOnChangedCallback f, IntPtr data) => LoadFunction<Signatures.uiEntryOnChanged>()(e, f, data);
        internal static bool EntryReadOnly(IntPtr e) => LoadFunction<Signatures.uiEntryReadOnly>()(e);
        internal static void EntrySetReadOnly(IntPtr e, bool @readonly) => LoadFunction<Signatures.uiEntrySetReadOnly>()(e, @readonly);
        internal static IntPtr NewEntry() => LoadFunction<Signatures.uiNewEntry>()();
        internal static IntPtr NewPasswordEntry() => LoadFunction<Signatures.uiNewPasswordEntry>()();
        internal static IntPtr NewSearchEntry() => LoadFunction<Signatures.uiNewSearchEntry>()();
        [UnmanagedFunctionPointer(Convention)] internal delegate void EntryOnChangedCallback(IntPtr e, IntPtr data);
        #endregion TextBox
        #region Window
        internal static string WindowTitle(IntPtr w) => LoadFunction<Signatures.uiWindowTitle>()(w);
        internal static void WindowSetTitle(IntPtr w, string title) => LoadFunction<Signatures.uiWindowSetTitle>()(w, title);
        internal static void WindowContentSize(IntPtr w, out int width, out int height) => LoadFunction<Signatures.uiWindowContentSize>()(w, out width, out height);
        internal static void WindowSetContentSize(IntPtr w, int width, int height) => LoadFunction<Signatures.uiWindowSetContentSize>()(w, width, height);
        internal static bool WindowFullscreen(IntPtr w) => LoadFunction<Signatures.uiWindowFullscreen>()(w);
        internal static void WindowSetFullscreen(IntPtr w, bool fullscreen) => LoadFunction<Signatures.uiWindowSetFullscreen>()(w, fullscreen);
        internal static void WindowOnContentSizeChanged(IntPtr w, WindowOnContentSizeChangedCallback f, IntPtr data) => LoadFunction<Signatures.uiWindowOnContentSizeChanged>()(w, f, data);
        internal static void WindowOnClosing(IntPtr w, WindowOnClosingCallback f, IntPtr data) => LoadFunction<Signatures.uiWindowOnClosing>()(w, f, data);
        internal static bool WindowBorderless(IntPtr w) => LoadFunction<Signatures.uiWindowBorderless>()(w);
        internal static void WindowSetBorderless(IntPtr w, bool borderless) => LoadFunction<Signatures.uiWindowSetBorderless>()(w, borderless);
        internal static void WindowSetChild(IntPtr w, IntPtr child) => LoadFunction<Signatures.uiWindowSetChild>()(w, child);
        internal static bool WindowMargined(IntPtr w) => LoadFunction<Signatures.uiWindowMargined>()(w);
        internal static void WindowSetMargined(IntPtr w, bool margined) => LoadFunction<Signatures.uiWindowSetMargined>()(w, margined);
        internal static IntPtr NewWindow(string title, int width, int height, bool hasMenubar) => LoadFunction<Signatures.uiNewWindow>()(title, width, height, hasMenubar);
        internal static string OpenFile(IntPtr parent) => LoadFunction<Signatures.uiOpenFile>()(parent);
        internal static string SaveFile(IntPtr parent) => LoadFunction<Signatures.uiSaveFile>()(parent);
        internal static void MsgBox(IntPtr parent, string title, string description) => LoadFunction<Signatures.uiMsgBox>()(parent, title, description);
        internal static void MsgBoxError(IntPtr parent, string title, string description) => LoadFunction<Signatures.uiMsgBoxError>()(parent, title, description);
        [UnmanagedFunctionPointer(Convention)] internal delegate void WindowOnContentSizeChangedCallback(IntPtr w, IntPtr data);
        [UnmanagedFunctionPointer(Convention)] internal delegate bool WindowOnClosingCallback(IntPtr w, IntPtr data);
        #endregion Window

        // Keep the delegates in this class in order with
        // libui\ui.h so it's easier to see what is missing.
        private static class Signatures
        {
            [UnmanagedFunctionPointer(Convention)] internal delegate string uiInit(ref StartupOptions options);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiUnInit();
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiFreeInitError(string err);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMain();
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMainSteps();
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiMainStep(bool wait);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiQuit();
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiQueueMain(QueueMainCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiTimer(int milliseconds, TimerCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiOnShouldQuit(OnShouldQuitCallback f, IntPtr data);

            //// [UnmanagedFunctionPointer(Convention)] internal delegate void uiFreeText(string text);

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlDestroy(IntPtr c);
            //// [UnmanagedFunctionPointer(Convention)] internal delegate UIntPtr uiControlHandle(uiControl c);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiControlParent(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlSetParent(IntPtr c, IntPtr parent);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiControlToplevel(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiControlVisible(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlShow(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlHide(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiControlEnabled(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlEnable(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlDisable(IntPtr c);
            //// [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlVerifySetParent(IntPtr c, IntPtr parent);
            //// [UnmanagedFunctionPointer(Convention)] internal delegate bool uiControlEnabledToUser(IntPtr c);
            //// [UnmanagedFunctionPointer(Convention)] internal delegate void uiAllocControl(UIntPtr n, uint OSsig, uint typesig, string typenamestr);
            //// [UnmanagedFunctionPointer(Convention)] internal delegate void uiFreeControl(uiControl c);
            //// [UnmanagedFunctionPointer(Convention)] internal delegate void uiUserBugCannotSetParentOnTopLevel(string type);

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiWindowTitle(IntPtr w);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowSetTitle(IntPtr w, string title);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowContentSize(IntPtr w, out int width, out int height);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowSetContentSize(IntPtr w, int width, int height);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiWindowFullscreen(IntPtr w);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowSetFullscreen(IntPtr w, bool fullscreen);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowOnContentSizeChanged(IntPtr w, WindowOnContentSizeChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowOnClosing(IntPtr w, WindowOnClosingCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiWindowBorderless(IntPtr w);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowSetBorderless(IntPtr w, bool borderless);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowSetChild(IntPtr w, IntPtr child);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiWindowMargined(IntPtr w);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiWindowSetMargined(IntPtr w, bool margined);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewWindow(string title, int width, int height, bool hasMenubar);

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiButtonText(IntPtr b);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiButtonSetText(IntPtr b, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiButtonOnClicked(IntPtr b, ButtonOnClickedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewButton(string text);

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiBoxAppend(IntPtr b, IntPtr child, bool stretchy);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiBoxDelete(IntPtr b, int index);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiBoxPadded(IntPtr b);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiBoxSetPadded(IntPtr b, bool padded);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewHorizontalBox();
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewVerticalBox();

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiCheckboxText(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiCheckboxSetText(IntPtr c, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiCheckboxOnToggled(IntPtr c, CheckboxOnToggledCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiCheckboxChecked(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiCheckboxSetChecked(IntPtr c, bool @checked);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewCheckbox(string text);

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiEntryText(IntPtr e);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEntrySetText(IntPtr e, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEntryOnChanged(IntPtr e, EntryOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiEntryReadOnly(IntPtr e);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEntrySetReadOnly(IntPtr e, bool @readonly);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewEntry();
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewPasswordEntry();
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewSearchEntry();

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiLabelText(IntPtr l);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiLabelSetText(IntPtr l, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewLabel(string text);

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiTabAppend(IntPtr t, string name, IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiTabInsertAt(IntPtr t, string name, int before, IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiTabDelete(IntPtr t, int index);
            [UnmanagedFunctionPointer(Convention)] internal delegate int uiTabNumPages(IntPtr t);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiTabMargined(IntPtr t, int page);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiTabSetMargined(IntPtr t, int page, bool margined);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewTab();

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiGroupTitle(IntPtr g);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiGroupSetTitle(IntPtr g, string title);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiGroupSetChild(IntPtr g, IntPtr child);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiGroupMargined(IntPtr g);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiGroupSetMargined(IntPtr g, bool margined);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewGroup(string title);

            [UnmanagedFunctionPointer(Convention)] internal delegate int uiSpinboxValue(IntPtr s);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiSpinboxSetValue(IntPtr s, int value);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiSpinboxOnChanged(IntPtr s, SpinboxOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewSpinbox(int min, int max);

            [UnmanagedFunctionPointer(Convention)] internal delegate int uiSliderValue(IntPtr s);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiSliderSetValue(IntPtr s, int value);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiSliderOnChanged(IntPtr s, SliderOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewSlider(int min, int max);

            [UnmanagedFunctionPointer(Convention)] internal delegate int uiProgressBarValue(IntPtr p);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiProgressBarSetValue(IntPtr p, int n);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewProgressBar();

            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewHorizontalSeparator();
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewVerticalSeparator();

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiComboboxAppend(IntPtr c, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate int uiComboboxSelected(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiComboboxSetSelected(IntPtr c, int n);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiComboboxOnSelected(IntPtr c, ComboboxOnSelectedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewCombobox();

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEditableComboboxAppend(IntPtr c, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate string uiEditableComboboxText(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEditableComboboxSetText(IntPtr c, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEditableComboboxOnChanged(IntPtr c, EditableComboboxOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewEditableCombobox();

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiRadioButtonsAppend(IntPtr r, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate int uiRadioButtonsSelected(IntPtr r);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiRadioButtonsSetSelected(IntPtr r, int n);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiRadioButtonsOnSelected(IntPtr r, RadioButtonsOnSelectedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewRadioButtons();

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiDateTimePickerTime(IntPtr d, out Time time);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiDateTimePickerSetTime(IntPtr d, Time time);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiDateTimePickerOnChanged(IntPtr d, DateTimePickerOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewDateTimePicker();
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewDatePicker();
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewTimePicker();

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiMultilineEntryText(IntPtr e);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMultilineEntrySetText(IntPtr e, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMultilineEntryAppend(IntPtr e, string text);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMultilineEntryOnChanged(IntPtr e, MultilineEntryOnChangedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiMultilineEntryReadOnly(IntPtr e);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMultilineEntrySetReadOnly(IntPtr e, bool @readonly);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewMultilineEntry();
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewNonWrappingMultilineEntry();

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMenuItemEnable(IntPtr m);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMenuItemDisable(IntPtr m);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMenuItemOnClicked(IntPtr m, MenuItemOnClickedCallback f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiMenuItemChecked(IntPtr m);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMenuItemSetChecked(IntPtr m, bool @checked);

            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiMenuAppendItem(IntPtr m, string name);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiMenuAppendCheckItem(IntPtr m, string name);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiMenuAppendQuitItem(IntPtr m);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiMenuAppendPreferencesItem(IntPtr m);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiMenuAppendAboutItem(IntPtr m);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMenuAppendSeparator(IntPtr m);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewMenu(string name);

            [UnmanagedFunctionPointer(Convention)] internal delegate string uiOpenFile(IntPtr parent);
            [UnmanagedFunctionPointer(Convention)] internal delegate string uiSaveFile(IntPtr parent);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMsgBox(IntPtr parent, string title, string description);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMsgBoxError(IntPtr parent, string title, string description);

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiFormAppend(IntPtr f, string label, IntPtr c, bool stretchy);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiFormDelete(IntPtr f, int index);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiFormPadded(IntPtr f);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiFormSetPadded(IntPtr f, bool padded);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewForm();

            [UnmanagedFunctionPointer(Convention)] internal delegate void uiGridAppend(IntPtr g, IntPtr c, int left, int top, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiGridInsertAt(IntPtr g, IntPtr c, IntPtr existing, RelativeAlignment at, int xspan, int yspan, int hexpand, Align halign, int vexpand, Align valign);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiGridPadded(IntPtr g);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiGridSetPadded(IntPtr g, bool padded);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewGrid();

            //TODO:  internal delegate IntPtr uiNewImage(double width, double height);
            //TODO:  internal delegate void uiFreeImage(IntPtr i);
            //TODO:  internal delegate void uiImageAppend(IntPtr i, IntPtr pixels, int pixelWidth, int pixelHeight, int byteStride);

            //TODO: uiTable
        }
    }
}