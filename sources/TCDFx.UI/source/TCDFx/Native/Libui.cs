/***************************************************************************************************
 * FileName:             Libui.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

using TCDFx.Runtime;
using TCDFx.Runtime.InteropServices;

namespace TCDFx.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class Libui
    {
        #region Helpers
        private const CallingConvention Convention = CallingConvention.Cdecl;
        private const LayoutKind Layout = LayoutKind.Sequential;
        private static readonly NativeAssembly AssemblyRef =
            (Platform.IsWindows && Platform.Is32Bit) ? new NativeAssembly(@"runtimes\win-x86\native\libui.dll") :
            (Platform.IsWindows && Platform.Is64Bit) ? new NativeAssembly(@"runtimes\win-x64\native\libui.dll") :
            (Platform.IsMacOS && Platform.Is64Bit) ? new NativeAssembly(@"runtimes/osx-x64/native/libui.dylib", @"runtimes/osx-x64/native/libui.A.dylib") :
            ((Platform.IsLinux || Platform.IsFreeBSD) && Platform.Is64Bit) ? new NativeAssembly(@"runtimes/linux-x64/native/libui.so", @"runtimes/linux-x64/native/libui.so.0") :
            throw new PlatformNotSupportedException();
        #endregion

        [StructLayout(Layout)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal struct uiInitError
        {
            internal UIntPtr Size;
            internal char[] Message;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static bool uiInit(IntPtr options, out uiInitError err) => AssemblyRef.LoadFunction<FunctionPointers.uiInit>()(options, out err);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiMain() => AssemblyRef.LoadFunction<FunctionPointers.uiMain>()();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiQuit() => AssemblyRef.LoadFunction<FunctionPointers.uiQuit>()();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiQueueMain(uiQueueMainFuncPtr f, IntPtr data) => AssemblyRef.LoadFunction<FunctionPointers.uiQueueMain>()(f, data);

        [UnmanagedFunctionPointer(Convention)]
        internal delegate void uiQueueMainFuncPtr(IntPtr data);

        [UnmanagedFunctionPointer(Convention)]
        internal delegate void uiEventHandler(IntPtr sender, IntPtr args, IntPtr data);

        [StructLayout(Layout)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal struct uiEventOptions
        {
            internal UIntPtr Size;
            internal bool Global;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static IntPtr uiNewEvent(uiEventOptions options) => AssemblyRef.LoadFunction<FunctionPointers.uiNewEvent>()(options);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiEventFree(IntPtr e) => AssemblyRef.LoadFunction<FunctionPointers.uiEventFree>()(e);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static int uiEventAddHandler(IntPtr e, uiEventHandler handler, IntPtr sender, IntPtr data) => AssemblyRef.LoadFunction<FunctionPointers.uiEventAddHandler>()(e, handler, sender, data);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiEventDeleteHandler(IntPtr e, int id) => AssemblyRef.LoadFunction<FunctionPointers.uiEventDeleteHandler>()(e, id);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiEventFire(IntPtr e, IntPtr sender, IntPtr args) => AssemblyRef.LoadFunction<FunctionPointers.uiEventFire>()(e, sender, args);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static bool uiEventHandlerBlocked(IntPtr e, int id) => AssemblyRef.LoadFunction<FunctionPointers.uiEventHandlerBlocked>()(e, id);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiEventSetHandlerBlocked(IntPtr e, int id, bool blocked) => AssemblyRef.LoadFunction<FunctionPointers.uiEventSetHandlerBlocked>()(e, id, blocked);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiEventInvalidateSender(IntPtr e, IntPtr sender) => AssemblyRef.LoadFunction<FunctionPointers.uiEventInvalidateSender>()(e, sender);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static uint uiControType() => AssemblyRef.LoadFunction<FunctionPointers.uiControType>()();

        [StructLayout(Layout)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal struct uiControlVTable
        {
            internal UIntPtr Size;
            internal uiControlVTableInitFuncPtr Init;
            internal uiControlVTableFreeFuncPtr Free;
        }

        [UnmanagedFunctionPointer(Convention)]
        internal delegate bool uiControlVTableInitFuncPtr(IntPtr c, IntPtr implData, IntPtr initData);

        [UnmanagedFunctionPointer(Convention)]
        internal delegate void uiControlVTableFreeFuncPtr(IntPtr c, IntPtr implData);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static uint uiRegisterControlType(string name, uiControlVTable vtable, IntPtr osVtable, UIntPtr implDataSize) => AssemblyRef.LoadFunction<FunctionPointers.uiRegisterControlType>()(name, vtable, osVtable, implDataSize);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static IntPtr uiCheckControlType(IntPtr c, uint type) => AssemblyRef.LoadFunction<FunctionPointers.uiCheckControlType>()(c, type);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static IntPtr uiNewControl(uint type, IntPtr initData) => AssemblyRef.LoadFunction<FunctionPointers.uiNewControl>()(type, initData);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiControlFree(IntPtr c) => AssemblyRef.LoadFunction<FunctionPointers.uiControlFree>()(c);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static void uiControlSetParent(IntPtr c, IntPtr parent) => AssemblyRef.LoadFunction<FunctionPointers.uiControlSetParent>()(c, parent);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static IntPtr uiControlImplData(IntPtr c) => AssemblyRef.LoadFunction<FunctionPointers.uiControlImplData>()(c);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        internal static IntPtr uiControlOnFree() => AssemblyRef.LoadFunction<FunctionPointers.uiControlOnFree>()();

        private static class FunctionPointers
        {
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiInit(IntPtr options, out uiInitError err);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiMain();
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiQuit();
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiQueueMain(uiQueueMainFuncPtr f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewEvent(uiEventOptions options);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEventFree(IntPtr e);
            [UnmanagedFunctionPointer(Convention)] internal delegate int uiEventAddHandler(IntPtr e, uiEventHandler handler, IntPtr sender, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEventDeleteHandler(IntPtr e, int id);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEventFire(IntPtr e, IntPtr sender, IntPtr args);
            [UnmanagedFunctionPointer(Convention)] internal delegate bool uiEventHandlerBlocked(IntPtr e, int id);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEventSetHandlerBlocked(IntPtr e, int id, bool blocked);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiEventInvalidateSender(IntPtr e, IntPtr sender);
            [UnmanagedFunctionPointer(Convention)] internal delegate uint uiControType();
            [UnmanagedFunctionPointer(Convention)] internal delegate uint uiRegisterControlType(string name, uiControlVTable vtable, IntPtr osVtable, UIntPtr implDataSize);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiCheckControlType(IntPtr c, uint type);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiNewControl(uint type, IntPtr initData);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlFree(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate void uiControlSetParent(IntPtr c, IntPtr parent);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiControlImplData(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] internal delegate IntPtr uiControlOnFree();
        }
    }
}