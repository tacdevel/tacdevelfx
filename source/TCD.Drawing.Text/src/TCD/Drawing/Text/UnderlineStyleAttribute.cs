/***************************************************************************************************
 * FileName:             UnderlineStyleAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    public sealed class UnderlineStyleAttribute : TextAttribute
    {
        public UnderlineStyleAttribute(UnderlineStyle style) => Handle = Libui.uiNewUnderlineAttribute(style);

        public UnderlineStyle UnderlineStyle => Libui.uiAttributeUnderline(this);
    }
}