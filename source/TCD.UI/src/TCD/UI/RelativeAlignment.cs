/***************************************************************************************************
 * FileName:             RelativeAlignment.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.UI
{
    /// <summary>
    /// Specifies how contents are positioned in relation to other content.
    /// </summary>
    public enum RelativeAlignment : long
    {
        /// <summary>
        /// The contents are positioned before the other content.
        /// </summary>
        Leading = 0,

        /// <summary>
        /// The contents are positioned above the other content.
        /// </summary>
        Top = 1,

        /// <summary>
        /// The contents are positioned after the other content.
        /// </summary>
        Trailing = 2,

        /// <summary>
        /// The contents are positioned under the other content.
        /// </summary>
        Bottom = 3
    }
}