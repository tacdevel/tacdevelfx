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
    public abstract class NativeAssemblyResolver
    {
        public abstract IEnumerable<string> EnumerateLoadTargets(string name);

        public static NativeAssemblyResolver Default => new DefaultNativeAssemblyResolver();
        public static NativeAssemblyResolver Dependency => new DependencyNativeAssemblyResolver();
        //TODO: public static NativeAssemblyResolver Embedded => new EmbeddedNativeAssemblyResolver();
    }
}