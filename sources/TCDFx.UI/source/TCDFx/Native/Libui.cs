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

        static Libui()
        {
            if (Platform.PlatformType == PlatformType.Windows && Platform.Architecture == PlatformArch.X64)
                AssemblyRef = new NativeAssembly(@"runtimes\win-x64\native\libui.dll");
            else if (Platform.PlatformType == PlatformType.MacOS && Platform.Architecture == PlatformArch.X64)
                AssemblyRef = new NativeAssembly(@"runtimes/osx-x64/native/libui.dylib", @"runtimes/osx-x64/native/libui.A.dylib");
            else if ((Platform.PlatformType == PlatformType.Linux || Platform.PlatformType == PlatformType.FreeBSD) && Platform.Architecture == PlatformArch.X64)
                AssemblyRef = new NativeAssembly(@"runtimes/linux-x64/native/libui.so", @"runtimes/linux-x64/native/libui.so.0");
            else
                throw new PlatformNotSupportedException();
        }

        private static NativeAssembly AssemblyRef { get; }

        public static T Call<T>() where T : Delegate => AssemblyRef.LoadFunction<T>();
        #endregion

        [StructLayout(Layout)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public struct uiInitError
        {
            public UIntPtr Size;
            public char[] Message;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static bool uiInit(IntPtr options, out uiInitError err) => Call<FunctionPointers.uiInit>()(options, out err);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiMain() => Call<FunctionPointers.uiMain>()();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiQuit() => Call<FunctionPointers.uiQuit>()();

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiQueueMain(uiQueueMainFuncPtr f, IntPtr data) => Call<FunctionPointers.uiQueueMain>()(f, data);

        [UnmanagedFunctionPointer(Convention)]
        public delegate void uiQueueMainFuncPtr(IntPtr data);

        [UnmanagedFunctionPointer(Convention)]
        public delegate void uiEventHandler(IntPtr sender, IntPtr args, IntPtr data);

        [StructLayout(Layout)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public struct uiEventOptions
        {
            public UIntPtr Size;
            public bool Global;
        }

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static IntPtr uiNewEvent(uiEventOptions options) => Call<FunctionPointers.uiNewEvent>()(options);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiEventFree(IntPtr e) => Call<FunctionPointers.uiEventFree>()(e);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static int uiEventAddHandler(IntPtr e, uiEventHandler handler, IntPtr sender, IntPtr data) => Call<FunctionPointers.uiEventAddHandler>()(e, handler, sender, data);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiEventDeleteHandler(IntPtr e, int id) => Call<FunctionPointers.uiEventDeleteHandler>()(e, id);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiEventFire(IntPtr e, IntPtr sender, IntPtr args) => Call<FunctionPointers.uiEventFire>()(e, sender, args);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static bool uiEventHandlerBlocked(IntPtr e, int id) => Call<FunctionPointers.uiEventHandlerBlocked>()(e, id);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiEventSetHandlerBlocked(IntPtr e, int id, bool blocked) => Call<FunctionPointers.uiEventSetHandlerBlocked>()(e, id, blocked);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiEventInvalidateSender(IntPtr e, IntPtr sender) => Call<FunctionPointers.uiEventInvalidateSender>()(e, sender);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static uint uiControType() => Call<FunctionPointers.uiControType>()();

        [StructLayout(Layout)]
        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public struct uiControlVTable
        {
            public UIntPtr Size;
            public uiControlVTableInitFuncPtr Init;
            public uiControlVTableFreeFuncPtr Free;
        }

        [UnmanagedFunctionPointer(Convention)]
        public delegate bool uiControlVTableInitFuncPtr(IntPtr c, IntPtr implData, IntPtr initData);

        [UnmanagedFunctionPointer(Convention)]
        public delegate void uiControlVTableFreeFuncPtr(IntPtr c, IntPtr implData);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static uint uiRegisterControlType(string name, uiControlVTable vtable, IntPtr osVtable, UIntPtr implDataSize) => Call<FunctionPointers.uiRegisterControlType>()(name, vtable, osVtable, implDataSize);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static IntPtr uiCheckControlType(IntPtr c, uint type) => Call<FunctionPointers.uiCheckControlType>()(c, type);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static IntPtr uiNewControl(uint type, IntPtr initData) => Call<FunctionPointers.uiNewControl>()(type, initData);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiControlFree(IntPtr c) => Call<FunctionPointers.uiControlFree>()(c);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static void uiControlSetParent(IntPtr c, IntPtr parent) => Call<FunctionPointers.uiControlSetParent>()(c, parent);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static IntPtr uiControlImplData(IntPtr c) => Call<FunctionPointers.uiControlImplData>()(c);

        [SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "NativeMemberName")]
        public static IntPtr uiControlOnFree() => Call<FunctionPointers.uiControlOnFree>()();

        private static class FunctionPointers
        {
            [UnmanagedFunctionPointer(Convention)] public delegate bool uiInit(IntPtr options, out uiInitError err);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiMain();
            [UnmanagedFunctionPointer(Convention)] public delegate void uiQuit();
            [UnmanagedFunctionPointer(Convention)] public delegate void uiQueueMain(uiQueueMainFuncPtr f, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewEvent(uiEventOptions options);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiEventFree(IntPtr e);
            [UnmanagedFunctionPointer(Convention)] public delegate int uiEventAddHandler(IntPtr e, uiEventHandler handler, IntPtr sender, IntPtr data);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiEventDeleteHandler(IntPtr e, int id);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiEventFire(IntPtr e, IntPtr sender, IntPtr args);
            [UnmanagedFunctionPointer(Convention)] public delegate bool uiEventHandlerBlocked(IntPtr e, int id);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiEventSetHandlerBlocked(IntPtr e, int id, bool blocked);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiEventInvalidateSender(IntPtr e, IntPtr sender);
            [UnmanagedFunctionPointer(Convention)] public delegate uint uiControType();
            [UnmanagedFunctionPointer(Convention)] public delegate uint uiRegisterControlType(string name, uiControlVTable vtable, IntPtr osVtable, UIntPtr implDataSize);
            [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiCheckControlType(IntPtr c, uint type);
            [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiNewControl(uint type, IntPtr initData);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiControlFree(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] public delegate void uiControlSetParent(IntPtr c, IntPtr parent);
            [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiControlImplData(IntPtr c);
            [UnmanagedFunctionPointer(Convention)] public delegate IntPtr uiControlOnFree();
        }
    }
}