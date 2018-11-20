/***************************************************************************************************
 * FileName:             TextAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.InteropServices;
using TCD.SafeHandles;
using TCD.Native;

namespace TCD.Drawing.Text
{
    //TODO: Caching.
    public abstract class TextAttribute : NativeComponent<SafeTextAttributeHandle>
    {
        internal TextAttribute(SafeTextAttributeHandle handle) :base(handle) { }

        internal LibuiEx.AttributeType Type => LibuiEx.AttributeGetType(Handle);
    }
}