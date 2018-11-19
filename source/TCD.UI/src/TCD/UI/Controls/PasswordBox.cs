/***************************************************************************************************
 * FileName:             PasswordBox.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
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