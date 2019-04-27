/***************************************************************************************************
 * FileName:             IComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides functionality required by all components.
    /// </summary>
    public interface IComponent : IDisposableEx
    {
        /// <summary>
        /// Gets or sets the name of this component.
        /// </summary>
        /// <value>The name of this component.</value>
        string Name { get; set; }

        /// <summary>
        /// Gets a value indicating whether this component is invalid.
        /// </summary>
        /// <value><c>true</c> if this component is invalid; otherwise, <c>false</c>.</value>
        bool IsInvalid { get; }
    }
}