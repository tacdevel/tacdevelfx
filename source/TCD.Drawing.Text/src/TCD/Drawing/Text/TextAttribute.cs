/***************************************************************************************************
 * FileName:             TextAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.InteropServices;
using TCD.Drawing.Text.SafeHandles;
using TCD.Native;

namespace TCD.Drawing.Text
{
    /// //TODO: Caching
    /// <summary>
    /// Stores information about an attribute in an <see cref="AttributedText"/> object.
    /// </summary>
    public abstract class TextAttribute : NativeComponent<SafeTextAttributeHandle>
    {
        internal TextAttribute(SafeTextAttributeHandle handle) :base(handle) { }

        internal Libui.uiAttributeType Type => Libui.Call<Libui.uiAttributeGetType>()(Handle);
    }
}