/***************************************************************************************************
 * FileName:             NativeAssembly.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace TCD.InteropServices
{
    /// <summary>
    /// Represents a native (shared) assembly.
    /// </summary>
    public sealed class NativeAssembly : NativeAssemblyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NativeAssembly"/> class.
        /// </summary>
        /// <param name="names">An ordered list of assembly names to attempt to load.</param>
        public NativeAssembly(params string[] names) : base(names) { }

        /// <inheritdoc />
        protected override IEnumerable<string> EnumerateLoadTargets(string name)
        {
            yield return Path.Combine(AppContext.BaseDirectory, name);
            yield return name;
        }
    }
}