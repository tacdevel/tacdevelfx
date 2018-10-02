/****************************************************************************
 * FileName:   HorizontalSeparator.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// Represents a control that is used to separate user-interface (UI) content horizontally.
    /// </summary>
    public class HorizontalSeparator : SeparatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalSeparator"/> class.
        /// </summary>
        public HorizontalSeparator() : base(new SafeControlHandle(Libui.NewHorizontalSeparator())) { }
    }
}