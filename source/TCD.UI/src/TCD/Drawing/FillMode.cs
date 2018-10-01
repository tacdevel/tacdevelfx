/****************************************************************************
 * FileName:   FillMode.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180930
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

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