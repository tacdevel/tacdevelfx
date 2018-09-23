/****************************************************************************
 * FileName:   NativeAssemblyResolver.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180919
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

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

        //TODO: public static NativeAssemblyResolver Embedded => new EmbeddedNativeAssemblyResolver();
    }
}