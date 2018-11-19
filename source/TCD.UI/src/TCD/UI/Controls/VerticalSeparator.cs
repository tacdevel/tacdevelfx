/***************************************************************************************************
 * FileName:             VerticalSeparator.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
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