using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace TCDFx.InteropServices
{
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class NativeCallAttribute : Attribute
    {
        public string? EntryPoint;
        // public CharSet CharSet;
        // public CallingConvention CallingConvention;

        public NativeCallAttribute(params string[] assemblyNames)
        {
            if (assemblyNames == null || assemblyNames.Length == 0)
                throw new NativeCallException("No assembly specified.");

            string[] names = new string[] { };
            int i = 0;
            foreach (string name in assemblyNames)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    names[i] = name;
                    i++;
                }
            }

            AssemblyNames = names;
        }

        public string[] AssemblyNames { get; }
    }

    [SuppressUnmanagedCodeSecurity]
    internal static class NativeCalls
    {
        private static readonly object sync = new object();

        public static void LoadNativeCalls()
        {
            lock (sync)
            {
                MethodInfo[] methods = GetNativeCalls();
                for (int i = 0; i < methods.Length; i++)
                {
                    string[] assemblyNames = methods[i].GetCustomAttribute<NativeCallAttribute>(false).AssemblyNames;
                    foreach (string name in assemblyNames)
                    {
                        //TODO: Load the native assembly
                        //TODO: Load the native function pointer
                        //TODO: Convert the native function pointer into a DynamicMethod (???)
                        //TODO: Call ReplaceMethod(MethodInfo, DynamicMethod)
                    }
                }
            }
        }

        private static MethodInfo[] GetNativeCalls()
        {
            List<MethodInfo> result = new List<MethodInfo>();
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            for (int i = 0; i < assemblies.Length; i++)
            {
                Type[] types = assemblies[i].GetTypes();
                for (int ii = 0; ii < types.Length; ii++)
                {
                    MethodInfo[] methods = types[ii].GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                    for (int iii = 0; iii < methods.Length; iii++)
                    {
                        Attribute attr = methods[iii].GetCustomAttribute<NativeCallAttribute>(false);
                        if (attr != null)
                            result.Add(methods[iii]);
                    }
                }
            }
            return result.ToArray();
        }

        public static unsafe MethodReplacementState ReplaceMethod(MethodInfo targetMethod, DynamicMethod replacementMethod)
        {
            if (!(targetMethod.GetMethodBody() == null && targetMethod.IsStatic))
                throw new NativeCallException($"The replacement of methods not marked 'static extern' is unsupported.");

            RuntimeHelpers.PrepareMethod(targetMethod.MethodHandle);
            RuntimeHelpers.PrepareMethod(replacementMethod.MethodHandle);

            IntPtr target = targetMethod.MethodHandle.Value;
            IntPtr replacement = replacementMethod.MethodHandle.Value + 8;
            if (!targetMethod.IsVirtual)
                target += 8;
            else
            {
                int i = (int)(((*(long*)target) >> 32) & 0xFF);
                IntPtr classStart = *(IntPtr*)(targetMethod.DeclaringType.TypeHandle.Value + (IntPtr.Size == 4 ? 40 : 64));
                target = classStart + (IntPtr.Size * i);
            }

#if DEBUG
            target = *(IntPtr*)target + 1;
            replacement = *(IntPtr*)replacement + 1;

            MethodReplacementState state = new MethodReplacementState(target, new IntPtr(*(int*)target));
            *(int*)target = *(int*)replacement + (int)(long)replacement - (int)(long)target;
            return state;
#else
            MethodReplacementState state = new MethodReplacementState(target, *(IntPtr*)target);
            * (IntPtr*)target = *(IntPtr*)replacement;
            return state;
#endif
        }
    }

    public readonly struct MethodReplacementState : IDisposable
    {

        private readonly IntPtr Location;
        private readonly IntPtr OriginalValue;

        public MethodReplacementState(IntPtr location, IntPtr origValue)
        {
            Location = location;
            OriginalValue = origValue;
        }
        public void Dispose() => Restore();

        public unsafe void Restore() =>
#if DEBUG
        *(int*)Location = (int)OriginalValue;
#else
        *(IntPtr*)Location = OriginalValue;
#endif
    }


    [Serializable]
    public class NativeCallException : Exception
    {
        public NativeCallException() { }
        public NativeCallException(string message) : base(message) { }
        public NativeCallException(string message, Exception inner) : base(message, inner) { }
        protected NativeCallException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}