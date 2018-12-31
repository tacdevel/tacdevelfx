/***************************************************************************************************
 * FileName:             DefaultNativeAssemblyResolver.cs
 * Date:                 20180919
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

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