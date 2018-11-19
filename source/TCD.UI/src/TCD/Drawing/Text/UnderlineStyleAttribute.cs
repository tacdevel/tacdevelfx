/***************************************************************************************************
 * FileName:             UnderlineStyleAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static LibUISharp.Native.NativeMethods;

namespace LibUISharp.Drawing
{
    public sealed class UnderlineStyleAttribute : TextAttribute
    {
        public UnderlineStyleAttribute(UnderlineStyle style) => Handle = Libui.uiNewUnderlineAttribute(style);

        public UnderlineStyle UnderlineStyle => Libui.uiAttributeUnderline(this);
    }
}