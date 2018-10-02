/****************************************************************************
 * FileName:   NonWrappingTextBlock.cs
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
    /// Represents a control that can be used to display or edit multiple lines of text that are not wrapped.
    /// </summary>summary>
    public class NonWrappingTextBlock : TextBlockBase
    {
        public NonWrappingTextBlock() : base(new SafeControlHandle(Libui.NewNonWrappingMultilineEntry()), true) { }
    }
}