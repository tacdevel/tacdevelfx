# License for TACDevel Libraries

<details>
  <summary><b>Copyright Notice(s)</b><sub> (Click to Expand)<sub></summary>
  <br/>
  
  ```
  Copyright © 2017-2020 Thomas Corwin, et al.
  ```

</details>

<details>
  <summary><b>License Terms (MIT)</b><sub> (Click to Expand)<sub></summary>
  <br/>

  ```
  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
  associated documentation files (the "Software"), to deal in the Software without restriction,
  including without limitation the rights to use, copy, modify, merge, publish, distribute,
  sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or
  substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
  NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
  ```

</details>

## Third-Party Notices

Some TACDevel Libraries use third-party code (that is also licensed under the MIT license) and/or other resources that may be distributed under
different copyrights. This file contains a list of third-party notices.

If you notice that we accidentally failed to list a required notice, please bring it to our
attention by either posting an issue, or contacting [tom-corwin](https://github.com/tom-corwin).

***The following notices are provided for informational use only, and have been reformatted.***

<hr/>

### [nativelibraryloader](https://github.com/mellinoe/nativelibraryloader)

*A .NET Standard library for loading native shared libraries and retrieving function pointers.*

<details>
  <summary><b>Code Used</b><sub> (Click to Expand)<sub></summary>
  <br/>

  - [`/NativeLibraryLoader/Kernel32.cs`](https://github.com/mellinoe/nativelibraryloader/blob/586f9738ff12688df8f0662027da8c319aee3841/NativeLibraryLoader/Kernel32.cs)
  - [`/NativeLibraryLoader/Libdl.cs`](https://github.com/mellinoe/nativelibraryloader/blob/586f9738ff12688df8f0662027da8c319aee3841/NativeLibraryLoader/Libdl.cs)
  - [`/NativeLibraryLoader/LibraryLoader.cs`](https://github.com/mellinoe/nativelibraryloader/blob/586f9738ff12688df8f0662027da8c319aee3841/NativeLibraryLoader/LibraryLoader.cs)
  - [`/NativeLibraryLoader/NativeLibrary.cs`](https://github.com/mellinoe/nativelibraryloader/blob/586f9738ff12688df8f0662027da8c319aee3841/NativeLibraryLoader/NativeLibrary.cs)
  - [`/NativeLibraryLoader/PathResolver.cs`](https://github.com/mellinoe/nativelibraryloader/blob/586f9738ff12688df8f0662027da8c319aee3841/NativeLibraryLoader/PathResolver.cs)

</details>

<details>
  <summary><b>Used In</b><sub> (Click to Expand)<sub></summary>
  <br/>

  - [`/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Runtime/InteropServices/NativeAssembly.cs`](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Runtime/InteropServices/NativeAssembly.cs)
  - [`/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Native/Kernel32.cs`](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Native/Kernel32.cs)
  - [`/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Native/Libdl.cs`](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Native/Libdl.cs)

</details>

<details>
  <summary><b>Copyright Notice(s)</b><sub> (Click to Expand)<sub></summary>
  <br/>

  ```
  Copyright (c) 2017 Eric Mellino and contributors to the nativelibraryloader project.
  ```

</details>

<details>
  <summary><b>License Terms (MIT)</b><sub> (Click to Expand)<sub></summary>
  <br/>

  ```
  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
  associated documentation files (the "Software"), to deal in the Software without restriction,
  including without limitation the rights to use, copy, modify, merge, publish, distribute,
  sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or
  substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
  NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
  ```

</details>

### [runtime](https://github.com/dotnet/runtime)

*.NET is a cross-platform runtime for cloud, IoT, and desktop apps.*

<details>
  <summary><b>Code Used</b><sub> (Click to Expand)<sub></summary>
  <br/>

  - [`/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/RuntimeEnvironment.cs`](https://github.com/dotnet/runtime/blob/502d96dbfb1d38fed0dbb7ddf2390f948cd54107/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/RuntimeEnvironment.cs)
  - [`/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Darwin.cs`](https://github.com/dotnet/runtime/blob/502d96dbfb1d38fed0dbb7ddf2390f948cd54107/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Darwin.cs)
  - [`/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Unix.cs`](https://github.com/dotnet/runtime/blob/502d96dbfb1d38fed0dbb7ddf2390f948cd54107/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Unix.cs)
  - [`/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Windows.cs`](https://github.com/dotnet/runtime/blob/502d96dbfb1d38fed0dbb7ddf2390f948cd54107/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/NativeMethods.Windows.cs)
  - [`/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/PlatformApis.cs`](https://github.com/dotnet/runtime/blob/502d96dbfb1d38fed0dbb7ddf2390f948cd54107/src/installer/managed/Microsoft.DotNet.PlatformAbstractions/Native/PlatformApis.cs)

</details>

<details>
  <summary><b>Used In</b><sub> (Click to Expand)<sub></summary>
  <br/>

  - [`/sources/TACDevel.Runtime/src/TACDevel/Runtime/Platform.cs`](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime/src/TACDevel/Runtime/InteropServices/NativeAssembly.cs)
  - [`/sources/TACDevel.Runtime/src/TACDevel/Native/Ntdll.cs`](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime/src/TACDevel/Native/Kernel32.cs)
  - [`/sources/TACDevel.Runtime/src/TACDevel/Native/Libc.cs`](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime/src/TACDevel/Native/Libdl.cs)

</details>

<details>
  <summary><b>Copyright Notice(s)</b><sub> (Click to Expand)<sub></summary>
  <br/>

  ```
  Copyright (c) .NET Foundation and Contributors
  ```

</details>

<details>
  <summary><b>License Terms (MIT)</b><sub> (Click to Expand)<sub></summary>
  <br/>

  ```
  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
  associated documentation files (the "Software"), to deal in the Software without restriction,
  including without limitation the rights to use, copy, modify, merge, publish, distribute,
  sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all copies or
  substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
  NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
  ```

</details>

### [RazorSharp](https://github.com/Decimation/RazorSharp)

*Low-level utilities and tools for working with the CLR and memory.*

<details>
  <summary><b>Code Used</b><sub> (Click to Expand)<sub></summary>
  <br/>

  - [`/RazorSharp/Memory/Pointers/Handle.cs`](https://github.com/Decimation/RazorSharp/blob/dd99e98a6be6232714e02142445f10d84f9b288d/RazorSharp/Memory/Pointers/Handle.cs)
  - [`/RazorSharp/Memory/Pointers/Pointer.cs`](https://github.com/Decimation/RazorSharp/blob/dd99e98a6be6232714e02142445f10d84f9b288d/RazorSharp/Memory/Pointers/Pointer.cs)

</details>

<details>
  <summary><b>Used In</b><sub> (Click to Expand)<sub></summary>
  <br/>

  - [`/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Runtime/InteropServices/Pointer.cs`](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Runtime/InteropServices/Pointer.cs)
  - [``/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Runtime/InteropServices/Pointer`1.cs``](https://github.com/tom-corwin/tacdevlibs/tree/master/sources/TACDevel.Runtime.InteropServices/src/TACDevel/Runtime/InteropServices/Pointer`1.cs)

</details>

<details>
  <summary><b>Copyright Notice(s)</b><sub> (Click to Expand)<sub></summary>
  <br/>

  ```
  Unspecified
  ```

</details>

<details>
  <summary><b>License Terms (Unpecified)</b><sub> (Click to Expand)<sub></summary>
  <br/>

  ```
  ```

</details>