/***************************************************************************************************
 * FileName:             ForegroundColorAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    public sealed class ForegroundColorAttribute : TextAttribute
    {
        public ForegroundColorAttribute(Color color) => Handle = Libui.uiNewColorAttribute(color.R, color.G, color.B, color.A);

        public Color Color
        {
            get
            {
                Libui.uiAttributeColor(this, out double r, out double g, out double b, out double a);
                return new Color(r, g, b, a);
            }
        }
    }
}