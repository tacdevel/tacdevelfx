/****************************************************************************
 * FileName:   TextBlock.cs
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
    /// Represents a control that can be used to display or edit multiple lines of text.
    /// </summary>
    public class TextBlock : TextBlockBase
    {
        public TextBlock() : base(new SafeControlHandle(Libui.NewMultilineEntry()), true) { }
    }
}