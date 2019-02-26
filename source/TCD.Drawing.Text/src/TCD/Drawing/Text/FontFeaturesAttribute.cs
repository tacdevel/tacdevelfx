/***************************************************************************************************
 * FileName:             FontFeaturesAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    public sealed class FontFeaturesAttribute : TextAttribute
    {
        public FontFeaturesAttribute(FontFeatures features) => Handle = Libui.uiNewFeaturesAttribute(features);

        public FontFeatures FontFeatures => new FontFeatures(Libui.uiAttributeFeatures(this));
    }
}