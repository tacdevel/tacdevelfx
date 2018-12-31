/***************************************************************************************************
 * FileName:             UnderlineColor.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing.Text
{
    /// <summary>
    /// Identifies the underline color of text.
    /// </summary>
    public enum UnderlineColor : long
    {
        /// <summary>
        /// Custom color.
        /// </summary>
        Custom = 0,

        /// <summary>
        /// Spelling-error color.
        /// </summary>
        Spelling = 1,

        /// <summary>
        /// Grammar-error color.
        /// </summary>
        Grammar = 2,


        /// <summary>
        /// Auxiliary color.
        /// </summary>
        Auxiliary = 3
    }
}