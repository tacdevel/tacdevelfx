/***************************************************************************************************
 * FileName:             MenuItem.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// Represents a basic child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class MenuItem : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="MenuItem"/> class from the specified handle with the specified name.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        /// <param name="name">The menu child's name.</param>
        internal MenuItem(SafeControlHandle handle, string name) : base(handle) => Name = name;

        /// <summary>
        /// Gets this menu child's name.
        /// </summary>
        public string Name { get; }
    }
}