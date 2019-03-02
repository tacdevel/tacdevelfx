/***************************************************************************************************
 * FileName:             FontStyle.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies the style of a font as normal, italic or oblique.
    /// </summary>
    public enum FontStyle : long
    {
        /// <summary>
        /// Specifies the normal font style.
        /// </summary>
        Normal,

        /// <summary>
        /// Specifies the oblique font style.
        /// </summary>
        Oblique,

        /// <summary>
        /// Specifies the italic font style.
        /// </summary>
        Italic
    }
}