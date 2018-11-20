/***************************************************************************************************
 * FileName:             BackgroundColorAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    public sealed class BackgroundColorAttribute : TextAttribute
    {
        public BackgroundColorAttribute(Color color)
        {
            Handle = Libui.uiNewBackgroundAttribute(color.R, color.G, color.B, color.A);
            Color = color;
        }

        public Color Color
        {
            get;
            //{
            //    uiAttributeColor(Handle.DangerousGetHandle(), out double r, out double g, out double b, out double a);
            //    return new Color(r, g, b, a);
            //}
        }
    }
}