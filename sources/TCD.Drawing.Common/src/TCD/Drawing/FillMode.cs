/***************************************************************************************************
 * FileName:             FillMode.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies how the interior of a closed path is filled.
    /// </summary>
    public enum FillMode : long
    {
        /// <summary>
        /// Specifies the winding fill mode.
        /// </summary>
        Winding = 0,

        /// <summary>
        /// Specifies the alternate fill mode.
        /// </summary>
        Alternate = 1
    }
}