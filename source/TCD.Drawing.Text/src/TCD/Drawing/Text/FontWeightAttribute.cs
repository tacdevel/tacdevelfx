/***************************************************************************************************
 * FileName:             FontWeightAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    public sealed class FontWeightAttribute : TextAttribute
    {
        public FontWeightAttribute(FontWeight weight) => Handle = Libui.uiNewWeightAttribute(weight);

        public FontWeight FontWeight => Libui.uiAttributeWeight(this);
    }
}