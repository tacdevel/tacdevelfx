/***************************************************************************************************
 * FileName:             HorizontalContainer.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// Represents a <see cref="Control"/> that arranges child elements horizontally.
    /// </summary>
    public class HorizontalContainer : StackContainerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalContainer"/> class.
        /// </summary>
        public HorizontalContainer() : base(new SafeControlHandle(Libui.NewHorizontalBox())) { }
    }
}