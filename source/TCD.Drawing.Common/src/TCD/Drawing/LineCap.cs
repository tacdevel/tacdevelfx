/***************************************************************************************************
 * FileName:             LineCap.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies the available cap styles for the end of <see cref="Path"/>s that are contained in a <see cref="StrokeOptions"/> object.
    /// </summary>
    public enum LineCap : long
    {
        /// <summary>
        /// Specifies the flat line cap.
        /// </summary>
        Flat = 0,

        /// <summary>
        /// Specifies the round line cap.
        /// </summary>
        Round = 1,

        /// <summary>
        /// Specifies the square line cap.
        /// </summary>
        Square = 2
    }
}