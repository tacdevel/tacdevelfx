/***************************************************************************************************
 * FileName:             PreferencesMenuItem.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// Represents a preferences menu child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class PreferencesMenuItem : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="PreferencesMenuItem"/> class from the specified handle.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        internal PreferencesMenuItem(SafeControlHandle handle) : base(handle) { }
    }
}