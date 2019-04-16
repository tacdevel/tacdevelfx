/***************************************************************************************************
 * FileName:             SeparatorBase.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// The base class for a control that is used to separate user-interface (UI) content.
    /// </summary>
    public abstract class SeparatorBase : Control
    {
        internal SeparatorBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) { }
    }

    /// <summary>
    /// Represents a control that is used to separate user-interface (UI) content horizontally.
    /// </summary>
    public class HorizontalSeparator : SeparatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalSeparator"/> class.
        /// </summary>
        public HorizontalSeparator() : base(new SafeControlHandle(Libui.Call<Libui.uiNewHorizontalSeparator>()())) { }
    }

    /// <summary>
    /// Represents a control that is used to separate user-interface (UI) content vertically.
    /// </summary>
    public class VerticalSeparator : SeparatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalSeparator"/> class.
        /// </summary>
        public VerticalSeparator() : base(new SafeControlHandle(Libui.Call<Libui.uiNewVerticalSeparator>()())) { }
    }
}