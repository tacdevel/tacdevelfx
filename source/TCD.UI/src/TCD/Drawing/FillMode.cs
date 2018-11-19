/***************************************************************************************************
 * FileName:             FillMode.cs
 * Date:                 20180930
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies how the interior of a closed path is filled.
    /// </summary>
    public enum FillMode
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