/***************************************************************************************************
 * FileName:             WindowEdge.cs
 * Date:                 20180930
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies the edge of a <see cref="TCD.UI.Window"/> object.
    /// </summary>
    public enum WindowEdge : long
    {
        /// <summary>
        /// Specifies the left edge of a <see cref="TCD.UI.Window"/>.
        /// </summary>
        Left = 0,

        /// <summary>
        /// Specifies the top edge of a <see cref="TCD.UI.Window"/>.
        /// </summary>
        Top = 1,

        /// <summary>
        /// Specifies the right edge of a <see cref="TCD.UI.Window"/>.
        /// </summary>
        Right = 2,

        /// <summary>
        /// Specifies the bottom edge of a <see cref="TCD.UI.Window"/>.
        /// </summary>
        Bottom = 3,

        /// <summary>
        /// Specifies the top-left corner of a <see cref="TCD.UI.Window"/>.
        /// </summary>
        TopLeft = 4,

        /// <summary>
        /// Specifies the top-right corner of the <see cref="TCD.UI.Window"/>.
        /// </summary>
        TopRight = 5,

        /// <summary>
        /// Specifies the bottom-left corner of a <see cref="TCD.UI.Window"/>.
        /// </summary>
        BottomLeft = 6,

        /// <summary>
        /// Specifies the bottom-right corner of a <see cref="TCD.UI.Window"/>.
        /// </summary>
        BottomRight = 7
    }
}