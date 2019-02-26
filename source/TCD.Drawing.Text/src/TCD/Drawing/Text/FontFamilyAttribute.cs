/***************************************************************************************************
 * FileName:             FontFamilyAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class FontFamilyAttribute : TextAttribute
    {
        public FontFamilyAttribute(string family) => Handle = Libui.uiNewFamilyAttribute(family);

        public string FontFamily => Libui.uiAttributeFamily(this);
    }
}