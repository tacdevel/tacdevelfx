/****************************************************************************
 * FileName:   VerticalContainer.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181002
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// Represents a <see cref="Control"/> that arranges child elements vertically.
    /// </summary>
    public class VerticalContainer : StackContainerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalContainer"/> class.
        /// </summary>
        public VerticalContainer() : base(new SafeControlHandle(Libui.NewHorizontalBox())) { }
    }
}