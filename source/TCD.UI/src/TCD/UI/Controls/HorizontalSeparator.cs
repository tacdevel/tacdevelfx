/***************************************************************************************************
 * FileName:             HorizontalSeparator.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

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