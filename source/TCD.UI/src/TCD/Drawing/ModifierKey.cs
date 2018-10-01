/****************************************************************************
 * FileName:   ModifierKey.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180930
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;

namespace TCD.Drawing
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