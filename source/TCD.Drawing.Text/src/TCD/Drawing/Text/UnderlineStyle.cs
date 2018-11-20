/***************************************************************************************************
 * FileName:             UnderlineStyle.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing.Text
{
    /// <summary>
    /// Identifies the underline style of text.
    /// </summary>
    public enum UnderlineStyle : long
    {
        /// <summary>
        /// No underline.
        /// </summary>
        None = 0,

        /// <summary>
        /// A single line.
        /// </summary>
        Single = 1,

        /// <summary>
        /// A double line.
        /// </summary>
        Double = 2,

        /// <summary>
        /// A wavy or dotted line.
        /// </summary>
        Special = 3
    }
}