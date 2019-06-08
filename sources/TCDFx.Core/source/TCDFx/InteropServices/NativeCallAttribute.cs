/***************************************************************************************************
 * FileName:             NativeCallAttribute.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Security;

namespace TCDFx.InteropServices
{
    /// <summary>
    /// Indicates that the attributed method is exposed by an native assembly as a static entry point.
    /// </summary>
    [CLSCompliant(false)]
    [SuppressUnmanagedCodeSecurity]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class NativeCallAttribute : Attribute
    {
        /// <summary>
        /// The name of the native method.
        /// </summary>
        public string EntryPoint;

        /// <summary>
        /// Initializes a new instance of the NativeCallAttribute.
        /// </summary>
        /// <param name="assemblyNames">An ordered list of assembly names.</param>
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

        /// <summary>
        /// An ordered list of assembly names.
        /// </summary>
        public string[] AssemblyNames { get; }
    }
}