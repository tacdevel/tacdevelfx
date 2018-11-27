/***************************************************************************************************
 * FileName:             QuitMenuItem.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// Represents a about menu child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class QuitMenuItem : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="QuitMenuItem"/> class from the specified handle.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        internal QuitMenuItem(SafeControlHandle handle) : base(handle) { }

        /// <summary>
        /// Initializes this UI component's events.
        /// </summary>
        protected sealed override void InitializeEvents() { }

        /// <summary>
        /// This method does not do anything, and will throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="data">An <see cref="IntPtr"/> that contains the event data.</param>
        protected sealed override void OnClicked(IntPtr data) => throw new NotSupportedException();
    }
}