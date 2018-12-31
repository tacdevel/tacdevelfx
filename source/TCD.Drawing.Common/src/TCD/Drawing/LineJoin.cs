/***************************************************************************************************
 * FileName:             LineJoin.cs
 * Date:                 20180930
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies how to join consecutive line or curve segments in a figure that are contained in a <see cref="StrokeOptions"/> object.
    /// </summary>
    public enum LineJoin : long
    {
        /// <summary>
        /// Specifies a mitered line join.
        /// </summary>
        Miter = 0,

        /// <summary>
        /// Specifies a circular line join.
        /// </summary>
        Round = 1,

        /// <summary>
        /// Specifies a beveled line join.
        /// </summary>
        Bevel = 2
    }
}