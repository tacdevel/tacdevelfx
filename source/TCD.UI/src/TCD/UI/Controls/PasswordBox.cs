/****************************************************************************
 * FileName:   PasswordBox.cs
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
    /// Represents a <see cref="TextBox"/> that displays it's text as password characters.
    /// </summary>
    public class PasswordBox : TextBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordBox"/> class.
        /// </summary>
        public PasswordBox() : base(new SafeControlHandle(Libui.NewPasswordEntry()), true) { }
    }
}