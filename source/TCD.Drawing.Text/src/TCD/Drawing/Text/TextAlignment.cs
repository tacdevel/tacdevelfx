/***************************************************************************************************
 * FileName:             TextAlignment.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing.Text
{
    /// <summary>
    /// Specifies whether text is left-aligned, right-aligned, or centered.
    /// </summary>
    public enum TextAlignment : long
    {
        /// <summary>
        /// Text is aligned to the left.
        /// </summary>
        Left = 0,

        /// <summary>
        /// Text is centered.
        /// </summary>
        Center = 1,

        /// <summary>
        /// Text is aligned to the right.
        /// </summary>
        Right = 2
    }
}