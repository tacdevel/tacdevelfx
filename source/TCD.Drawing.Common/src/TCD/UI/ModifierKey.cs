/***************************************************************************************************
 * FileName:             ModifierKey.cs
 * Date:                 20180930
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD.UI
{
    /// <summary>
    /// Represents a modifier key on a keyboard.
    /// </summary>
    [Flags]
    public enum ModifierKey : long
    {
        /// <summary>
        /// The control key.
        /// </summary>
        Ctrl = 1 << 0,

        /// <summary>
        /// The alternate key.
        /// </summary>
        Alt = 1 << 1,

        /// <summary>
        /// The shift key.
        /// </summary>
        Shift = 1 << 2,

        /// <summary>
        /// The super key.
        /// </summary>
        Super = 1 << 3
    }
}