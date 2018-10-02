/****************************************************************************
 * FileName:   SearchBox.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a <see cref="TextBox"/> that displays a search icon.
    /// </summary>
    public class SearchBox : TextBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBox"/> class.
        /// </summary>
        public SearchBox() : base(new SafeControlHandle(Libui.NewSearchEntry()), true) { }
    }
}