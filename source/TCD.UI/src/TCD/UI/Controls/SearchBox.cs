/***************************************************************************************************
 * FileName:             SearchBox.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
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