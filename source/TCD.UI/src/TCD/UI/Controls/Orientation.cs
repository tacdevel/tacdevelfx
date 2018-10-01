/****************************************************************************
 * FileName:   Orientation.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180930
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

namespace TCD.UI
{
    /// <summary>
    /// Defines the different orientations that a <see cref="Control"/> or <see cref="Container"/> can have.
    /// </summary>
    public enum Orientation : int
    {
        /// <summary>
        /// <see cref="Control"/> or <see cref="Container"/> should be horizontally oriented.
        /// </summary>
        Horizontal = 0,

        /// <summary>
        /// <see cref="Control"/> or <see cref="Container"/> should be vertically oriented.
        /// </summary>
        Vertical = 1
    }
}