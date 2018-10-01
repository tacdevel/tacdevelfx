/****************************************************************************
 * FileName:   FontStyle.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180930
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

namespace TCD.Drawing
{
    /// <summary>
    /// Specifies the style of a font as normal, italic or oblique.
    /// </summary>
    public enum FontStyle : long
    {
        /// <summary>
        /// Specifies the normal font style.
        /// </summary>
        Normal,

        /// <summary>
        /// Specifies the oblique font style.
        /// </summary>
        Oblique,

        /// <summary>
        /// Specifies the italic font style.
        /// </summary>
        Italic
    }
}