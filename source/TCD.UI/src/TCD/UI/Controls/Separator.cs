/****************************************************************************
 * FileName:   Separator.cs
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
    /// The base class for a control that is used to separate user-interface (UI) content.
    /// </summary>
    public abstract class SeparatorBase : Control
    {
        internal SeparatorBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) { }
    }

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

    /// <summary>
    /// Represents a control that is used to separate user-interface (UI) content vertically.
    /// </summary>
    public class VerticalSeparator : SeparatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalSeparator"/> class.
        /// </summary>
        public VerticalSeparator() : base(new SafeControlHandle(Libui.NewVerticalSeparator())) { }
    }
}