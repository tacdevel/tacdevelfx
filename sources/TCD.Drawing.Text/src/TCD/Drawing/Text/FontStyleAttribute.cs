/***************************************************************************************************
 * FileName:             FontStyleAttribute.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    public sealed class FontStyleAttribute : TextAttribute
    {
        public FontStyleAttribute(FontStyle style) => Handle = Libui.uiNewItalicAttribute(style);

        public FontStyle FontStyle => Libui.uiAttributeItalic(this);
    }
}