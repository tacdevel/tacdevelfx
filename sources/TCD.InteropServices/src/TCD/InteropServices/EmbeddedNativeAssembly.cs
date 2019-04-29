/***************************************************************************************************
 * FileName:             EmbeddedNativeAssembly.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace TCD.InteropServices
{
    /// <summary>
    /// Represents a native (shared) assembly that is embedded in the calling managed assembly.
    /// </summary>
    public sealed class EmbeddedNativeAssembly : NativeAssemblyBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedNativeAssembly"/> class.
        /// </summary>
        /// <param name="names">An ordered list of assembly names to attempt to load.</param>
        public EmbeddedNativeAssembly(params string[] names) : base(names) { }

        /// <inheritdoc />
        protected override IEnumerable<string> EnumerateLoadTargets(string name)
        {
            yield return ExtractEmbeddedAssembly(name);
        }

        private static string ExtractEmbeddedAssembly(string name)
        {
            Assembly asm = Assembly.GetEntryAssembly();
            string[] resNames = asm.GetManifestResourceNames();
            string resAsmName = name.Replace(".dll", string.Empty).Replace("-", "_").Replace(" ", "_").Replace(".", "_");
            string tempDir = Path.Combine(Path.GetTempPath(), asm.GetName().Name, Platform.RuntimeID);
            string outputAsm = Path.Combine(tempDir, name);

            if (!Directory.Exists(tempDir))
                Directory.CreateDirectory(tempDir);

            Stream asmStream;
            bool resAsmExists = false;
            foreach (string resName in resNames)
                if (resName == resAsmName)
                    resAsmExists = true;
            if (resAsmExists)
                return outputAsm;

            asmStream = asm.GetManifestResourceStream(resAsmName);
            if (!File.Exists(outputAsm))
            {
                byte[] buffer = new byte[8 * 1024];
                int len;
                while ((len = asmStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    using Stream output = File.Create(outputAsm);
                    output.Write(buffer, 0, len);
                }
            }
            return outputAsm;
        }

    }
}