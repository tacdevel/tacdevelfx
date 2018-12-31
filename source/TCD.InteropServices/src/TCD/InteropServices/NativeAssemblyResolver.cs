/***************************************************************************************************
 * FileName:             NativeAssemblyResolver.cs
 * Date:                 20180919
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Collections.Generic;

namespace TCD.InteropServices
{
    /// <summary>
    /// Enumerates possible load targets for a native assembly.
    /// </summary>
    public abstract class NativeAssemblyResolver
    {
        /// <summary>
        /// Returns an enumerator which yields possible library load targets, in priority order.
        /// </summary>
        /// <param name="name">The name of the library to load.</param>
        /// <returns>An enumerator yielding load targets.</returns>
        public abstract IEnumerable<string> EnumerateLoadTargets(string name);

        /// <summary>
        /// Gets the default assembly resolver.
        /// </summary>
        public static NativeAssemblyResolver Default => new DefaultNativeAssemblyResolver();

        /// <summary>
        /// Gets a resolver that enumerates load targets from a dependency.
        /// </summary>
        public static NativeAssemblyResolver Dependency => new DependencyNativeAssemblyResolver();
    }
}