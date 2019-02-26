/***************************************************************************************************
 * FileName:             FontStretch.cs
 * Date:                 20180930
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies how a font will be stretched compared to the normal aspect ratio of that font.
    /// </summary>
    public enum FontStretch : long
    {
        /// <summary>
        /// Specifies the ultra-condensed font stretch.
        /// </summary>
        UltraCondensed,

        /// <summary>
        /// Specifies the extra-condensed font stretch.
        /// </summary>
        ExtraCondensed,

        /// <summary>
        /// Specifies the condensed font stretch.
        /// </summary>
        Condensed,

        /// <summary>
        /// Specifies the semi-condensed font stretch.
        /// </summary>
        SemiCondensed,

        /// <summary>
        /// Specifies the normal font stretch.
        /// </summary>
        Normal,

        /// <summary>
        /// Specifies the semi-expanded font stretch.
        /// </summary>
        SemiExpanded,

        /// <summary>
        /// Specifies the expanded font stretch.
        /// </summary>
        Expanded,

        /// <summary>
        /// Specifies the extra-expanded font stretch.
        /// </summary>
        ExtraExpanded,

        /// <summary>
        /// Specifies the ultra-expanded font stretch.
        /// </summary>
        UltraExpanded
    }
}