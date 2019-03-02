/***************************************************************************************************
 * FileName:             TextAttribute.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.InteropServices;
using TCD.Drawing.Text.SafeHandles;
using TCD.Native;

namespace TCD.Drawing.Text
{
    /// <summary>
    /// Stores information about an attribute in an <see cref="AttributedText"/> object.
    /// </summary>
    public abstract class TextAttribute : NativeComponent<SafeTextAttributeHandle>
    {
        internal TextAttribute(SafeTextAttributeHandle handle) : base(handle) { }

        internal Libui.uiAttributeType Type => Libui.Call<Libui.uiAttributeGetType>()(Handle);
    }
}