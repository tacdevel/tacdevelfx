/****************************************************************************
 * FileName:   DefaultNativeAssemblyResolver.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180919
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace TCD.InteropServices
{
    internal sealed class DefaultNativeAssemblyResolver : NativeAssemblyResolver
    {
        public override IEnumerable<string> EnumerateLoadTargets(string name)
        {
            yield return Path.Combine(AppContext.BaseDirectory, name);
            yield return name;
        }
    }
}