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

using uiInitOptions = TCD.UI.StartupOptions;
using uiControl = System.IntPtr;
using uiWindow = System.IntPtr;

namespace TCD.Native
{
    internal static class Libui
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate string uiInit(ref uiInitOptions options);
        internal static string Init(ref uiInitOptions options) => AssemblyRef.Libui.LoadFunction<uiInit>()(ref options);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiUnInit();
        internal static void UnInit() => AssemblyRef.Libui.LoadFunction<uiUnInit>()();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiFreeInitError(string err);
        internal static void FreeInitError(string err) => AssemblyRef.Libui.LoadFunction<uiFreeInitError>()(err);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiMain();
        internal static void Main() => AssemblyRef.Libui.LoadFunction<uiMain>()();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiMainSteps();
        internal static void MainSteps() => AssemblyRef.Libui.LoadFunction<uiMainSteps>()();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiMainStep(bool wait);
        internal static bool MainStep(bool wait) => AssemblyRef.Libui.LoadFunction<uiMainStep>()(wait);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiQuit();
        internal static void Quit() => AssemblyRef.Libui.LoadFunction<uiQuit>()();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiQueueMain(QueueMainCallback f, IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void QueueMainCallback(IntPtr data);
        internal static void QueueMain(QueueMainCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<uiQueueMain>()(f, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiTimer(int milliseconds, TimerCallback f, IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate bool TimerCallback(IntPtr data);
        internal static void Timer(int milliseconds, TimerCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<uiTimer>()(milliseconds, f, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiOnShouldQuit(OnShouldQuitCallback f, IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate bool OnShouldQuitCallback(IntPtr data);
        internal static void OnShouldQuit(OnShouldQuitCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<uiOnShouldQuit>()(f, data);

        //TODO: I need to check for memory leaks, since I don't use this for strings at all.
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiFreeText(string text);
        internal static void FreeText(string text) => AssemblyRef.Libui.LoadFunction<uiFreeText>()(text);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiControlDestroy(uiControl c);
        internal static void ControlDestroy(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlDestroy>()(c);

        //// [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate UIntPtr uiControlHandle(uiControl c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate uiControl uiControlParent(uiControl c);
        internal static uiControl ControlParent(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlParent>()(c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiControlSetParent(uiControl c, uiControl parent);
        internal static void ControlSetParent(uiControl c, uiControl parent) => AssemblyRef.Libui.LoadFunction<uiControlSetParent>()(c, parent);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiControlToplevel(uiControl c);
        internal static bool ControlTopLevel(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlToplevel>()(c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiControlVisible(uiControl c);
        internal static bool ControlVisible(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlVisible>()(c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiControlShow(uiControl c);
        internal static void ControlShow(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlShow>()(c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiControlHide(uiControl c);
        internal static void ControlHide(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlHide>()(c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiControlEnabled(uiControl c);
        internal static bool ControlEnabled(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlEnabled>()(c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiControlEnable(uiControl c);
        internal static void ControlEnable(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlEnable>()(c);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiControlDisable(uiControl c);
        internal static void ControlDisable(uiControl c) => AssemblyRef.Libui.LoadFunction<uiControlDisable>()(c);

        //// [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiControlVerifySetParent(uiControl c, IntPtr parent);
        //// [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiControlEnabledToUser(uiControl c);
        //// [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiAllocControl(UIntPtr n, uint OSsig, uint typesig, string typenamestr);
        //// [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiFreeControl(uiControl c);
        //// [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiUserBugCannotSetParentOnTopLevel(string type);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate string uiWindowTitle(uiWindow w);
        internal static string WindowTitle(uiWindow w) => AssemblyRef.Libui.LoadFunction<uiWindowTitle>()(w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowSetTitle(uiWindow w, string title);
        internal static void WindowSetTitle(uiWindow w, string title) => AssemblyRef.Libui.LoadFunction<uiWindowSetTitle>()(w, title);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowContentSize(uiWindow w, out int width, out int height);
        internal static void WindowContentSize(uiWindow w, out int width, out int height) => AssemblyRef.Libui.LoadFunction<uiWindowContentSize>()(w, out width, out height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowSetContentSize(uiWindow w, int width, int height);
        internal static void WindowSetContentSize(uiWindow w, int width, int height) => AssemblyRef.Libui.LoadFunction<uiWindowSetContentSize>()(w, width, height);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiWindowFullscreen(uiWindow w);
        internal static bool WindowFullscreen(uiWindow w) => AssemblyRef.Libui.LoadFunction<uiWindowFullscreen>()(w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowSetFullscreen(uiWindow w, bool fullscreen);
        internal static void WindowSetFullscreen(uiWindow w, bool fullscreen) => AssemblyRef.Libui.LoadFunction<uiWindowSetFullscreen>()(w, fullscreen);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowOnContentSizeChanged(uiWindow w, WindowOnContentSizeChangedCallback f, IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate void WindowOnContentSizeChangedCallback(IntPtr w, IntPtr data);
        internal static void WindowOnContentSizeChanged(uiWindow w, WindowOnContentSizeChangedCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<uiWindowOnContentSizeChanged>()(w, f, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowOnClosing(uiWindow w, WindowOnClosingCallback f, IntPtr data);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] internal delegate bool WindowOnClosingCallback(IntPtr w, IntPtr data);
        internal static void WindowOnClosing(uiWindow w, WindowOnClosingCallback f, IntPtr data) => AssemblyRef.Libui.LoadFunction<uiWindowOnClosing>()(w, f, data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiWindowBorderless(uiWindow w);
        internal static bool WindowBorderless(uiWindow w) => AssemblyRef.Libui.LoadFunction<uiWindowBorderless>()(w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowSetBorderless(uiWindow w, bool borderless);
        internal static void WindowSetBorderless(uiWindow w, bool borderless) => AssemblyRef.Libui.LoadFunction<uiWindowSetBorderless>()(w, borderless);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowSetChild(uiWindow w, uiControl child);
        internal static void WindowSetChild(uiWindow w, uiControl child) => AssemblyRef.Libui.LoadFunction<uiWindowSetChild>()(w, child);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate bool uiWindowMargined(uiWindow w);
        internal static bool WindowMargined(uiWindow w) => AssemblyRef.Libui.LoadFunction<uiWindowMargined>()(w);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate void uiWindowSetMargined(uiWindow w, bool margined);
        internal static void WindowSetMargined(uiWindow w, bool margined) => AssemblyRef.Libui.LoadFunction<uiWindowSetMargined>()(w, margined);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] private delegate uiWindow uiNewWindow(string title, int width, int height, bool hasMenubar);
        internal static uiWindow NewWindow(string title, int width, int height, bool hasMenubar) => AssemblyRef.Libui.LoadFunction<uiNewWindow>()(title, width, height, hasMenubar);
    }
}