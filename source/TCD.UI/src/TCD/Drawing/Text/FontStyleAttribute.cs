/***************************************************************************************************
 * FileName:             FontStyleAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static LibUISharp.Native.NativeMethods;

namespace LibUISharp.Drawing
{
    public sealed class FontStyleAttribute : TextAttribute
    {
        public FontStyleAttribute(FontStyle style) => Handle = Libui.uiNewItalicAttribute(style);

        public FontStyle FontStyle => Libui.uiAttributeItalic(this);
    }
}