/***************************************************************************************************
 * FileName:             NativeCalls.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security;
using TCDFx.ComponentModel;

namespace TCDFx.InteropServices
{
    /// <summary>
    /// Provides <see cref="Load"/> for loading methods from native assemblies.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public static class NativeCalls
    {
        private static readonly object sync = new object();

        /// <summary>
        /// Loads all native calls defined via a <see cref="NativeCallAttribute"/>.
        /// </summary>
        public static void Load()
        {
            lock (sync)
            {
                MethodInfo[] funcInfo = GetNativeCalls();

                for (int i = 0; i < funcInfo.Length; i++)
                {
                    NativeCallAttribute attribute = funcInfo[i].GetCustomAttribute<NativeCallAttribute>(false);
                    NativeAssemblyBase nativeAssembly;

                    if (IsAssemblyCached(attribute.AssemblyNames, out NativeAssemblyBase cachedAssembly))
                        nativeAssembly = cachedAssembly;
                    else
                    {
                        if (TryLoadAssembly(attribute.AssemblyNames, out NativeAssemblyBase loadedAssembly, out Exception loadingEx))
                            nativeAssembly = loadedAssembly;
                        else
                            throw loadingEx;
                    }

                    string funcName = attribute.EntryPoint ?? funcInfo[i].Name;
                    IntPtr funcPtr = nativeAssembly.LoadFunctionPointer(funcName);

                    Delegate funcDelegate = GenerateNativeDelegate(funcName, nativeAssembly.Name, funcInfo[i], funcPtr);
                    //TODO: Delegate.GetMethodInfo() vs Delegate.Method
                    MethodInfo funcInfoNew = funcDelegate.GetMethodInfo();

                    ReplaceMethod(funcInfo[i], funcInfoNew);
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

        private static Delegate GenerateNativeDelegate(string funcName, string assemblyName, MethodInfo funcInfo, IntPtr funcPtr)
        {
            Type returnType = funcInfo.ReturnType;
            ParameterInfo[] @params = funcInfo.GetParameters();
            Type[] paramTypes = new Type[] { };

            for (int i = 0; i < @params.Length; i++)
                paramTypes[i] = @params[i].ParameterType;

            DynamicMethod nativeMethod = new DynamicMethod($"{assemblyName}_{funcName}", returnType, paramTypes, funcInfo.Module);
            ILGenerator ilGenerator = nativeMethod.GetILGenerator();

            // Generate the arguments
            for (int i = 0; i < @params.Length; i++)
            {
                //TODO: See if I need separate out code for this...
                if (@params[i].ParameterType.IsByRef || @params[i].IsOut)
                {
                    ilGenerator.Emit(OpCodes.Ldarg, i);
                    ilGenerator.Emit(OpCodes.Ldnull);
                    ilGenerator.Emit(OpCodes.Stind_Ref);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Ldarg, i);
                }
            }

            // Push the funcPtr to the stack
            if (IntPtr.Size == 4)
                ilGenerator.Emit(OpCodes.Ldc_I4, funcPtr.ToInt32());
            else if (IntPtr.Size == 8)
                ilGenerator.Emit(OpCodes.Ldc_I8, funcPtr.ToInt64());
            else throw new PlatformNotSupportedException();

            // Call it and return;
            ilGenerator.EmitCall(OpCodes.Call, funcInfo, null);
            ilGenerator.Emit(OpCodes.Ret);

            Type delegateType = Expression.GetDelegateType((from param in @params select param.ParameterType).Concat(new[] { returnType }).ToArray());
            return nativeMethod.CreateDelegate(delegateType);
        }

        private static bool IsAssemblyCached(string[] assemblyNames, out NativeAssemblyBase cachedAssembly)
        {
            bool result = false;
            cachedAssembly = null;
            foreach (string name in assemblyNames)
            {
                if (!Component.Cache.ContainsKey(Path.GetFileNameWithoutExtension(name)))
                {
                    Type asmType = Component.Cache[Path.GetFileNameWithoutExtension(name)].Value1;
                    if (asmType == typeof(NativeAssembly))
                        cachedAssembly = (NativeAssembly)Component.Cache[Path.GetFileNameWithoutExtension(name)].Value2;
                    else if (asmType == typeof(NativeAssembly))
                        cachedAssembly = (DependencyNativeAssembly)Component.Cache[Path.GetFileNameWithoutExtension(name)].Value2;
                    else if (asmType == typeof(NativeAssembly))
                        cachedAssembly = (EmbeddedNativeAssembly)Component.Cache[Path.GetFileNameWithoutExtension(name)].Value2;
                    result = true;
                    break;
                }
            }
            return result;
        }

        private static bool TryLoadAssembly(string[] assemblyNames, out NativeAssemblyBase loadedAssembly, out Exception exception)
        {
            bool result = false;
            exception = null;
            // This is some super ugly code. There has to be a better way...
            try
            {
                loadedAssembly = new NativeAssembly(assemblyNames);
            }
            catch (Exception ex)
            {
                exception = ex;
                loadedAssembly = null;
            }
            try
            {
                if (exception != null)
                    loadedAssembly = new DependencyNativeAssembly(assemblyNames);
            }
            catch (Exception ex)
            {
                exception = ex;
                loadedAssembly = null;
            }
            try
            {
                if (exception == null)
                    loadedAssembly = new EmbeddedNativeAssembly(assemblyNames);
            }
            catch (Exception ex)
            {
                exception = ex;
                loadedAssembly = null;
            }
            return result;
        }

        // https://stackoverflow.com/a/55026523/9825409
        //TODO: I have no idea if this even works in this situation.
        private static unsafe MethodReplacementState ReplaceMethod(MethodInfo targetMethod, MethodInfo replacementMethod)
        {
            if (!(targetMethod.GetMethodBody() == null && targetMethod.IsStatic))
                throw new NativeCallException($"Only the replacement of methods marked 'static extern' is supported.");

#if DEBUG
            RuntimeHelpers.PrepareMethod(targetMethod.MethodHandle);
            RuntimeHelpers.PrepareMethod(replacementMethod.MethodHandle);
#endif
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

        private readonly struct MethodReplacementState : IDisposable
        {
            private readonly IntPtr Location;
            private readonly IntPtr OriginalValue;

            public MethodReplacementState(IntPtr location, IntPtr origValue)
            {
                Location = location;
                OriginalValue = origValue;
            }
            public void Dispose() => Restore();

            private unsafe void Restore() =>
#if DEBUG
        *(int*)Location = (int)OriginalValue;
#else
        *(IntPtr*)Location = OriginalValue;
#endif
        }
    }
}