using System;
using System.Collections;
using System.Collections.Generic;
using TCDFx.ComponentModel;
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
        public string EntryPoint;

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

        public static void Load()
        {
            lock (sync)
            {
                MethodInfo[] methods = GetNativeCalls();
                for (int i = 0; i < methods.Length; i++)
                {
                    NativeCallAttribute attribute = methods[i].GetCustomAttribute<NativeCallAttribute>(false);
                    string[] assemblyNames = attribute.AssemblyNames;
                    bool isAssemblyLoaded = false;
                    NativeAssemblyBase loadedAssembly = null;
                    foreach (string name in assemblyNames)
                    {
                        if (!Component.Cache.ContainsKey(Path.GetFileNameWithoutExtension(name)))
                        {
                            isAssemblyLoaded = true;
                            Type asmType = Component.Cache[Path.GetFileNameWithoutExtension(name)].Value1;
                            if (asmType == typeof(NativeAssembly))
                                loadedAssembly = (NativeAssembly)Component.Cache[Path.GetFileNameWithoutExtension(name)].Value2;
                            else if (asmType == typeof(NativeAssembly))
                                loadedAssembly = (DependencyNativeAssembly)Component.Cache[Path.GetFileNameWithoutExtension(name)].Value2;
                            else if (asmType == typeof(NativeAssembly))
                                loadedAssembly = (EmbeddedNativeAssembly)Component.Cache[Path.GetFileNameWithoutExtension(name)].Value2;
                            break;
                        }
                    }
                    if (!isAssemblyLoaded)
                    {
                        Exception exception = null;
                        try
                        {
                            loadedAssembly = new NativeAssembly(assemblyNames);
                        }
                        catch (Exception ex)
                        {
                            exception = ex;
                        }
                        try
                        {
                            if (exception != null)
                                loadedAssembly = new DependencyNativeAssembly(assemblyNames);
                        }
                        catch (Exception ex)
                        {
                            exception = ex;
                        }
                        try
                        {
                            if (exception != null)
                                loadedAssembly = new EmbeddedNativeAssembly(assemblyNames);
                        }
                        catch (Exception ex)
                        {
                            exception = ex;
                        }
                        if (exception != null)
                            throw exception;
                    }

                    string funcName = attribute.EntryPoint ?? methods[i].Name;

                    IntPtr funcPtr = loadedAssembly.LoadFunctionPointer(funcName);

                    ParameterInfo[] parameters = methods[i].GetParameters();
                    Type[] parameterTypes = new Type[] { };
                    for (int ii = 0; ii < parameters.Length ; ii++)
                    {
                        parameterTypes[ii] = parameters[ii].ParameterType;
                    }

                    //TODO: Somehow set the body of this DynamicMethod to funcPtr. I really hope I don't have to add custom generic Action<> and Func<> delegates...
                    DynamicMethod nativeMethod = new DynamicMethod(funcName, methods[i].ReturnType, parameterTypes);

                    ReplaceMethod(methods[i], nativeMethod);
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