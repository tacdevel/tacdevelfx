/***************************************************************************************************
 * FileName:             FontSizeAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    public sealed class FontSizeAttribute : TextAttribute
    {
        public FontSizeAttribute(double size) => Handle = Libui.uiNewSizeAttribute(size);

        public double Size => Libui.uiAttributeSize(this);
    }
}