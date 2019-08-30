/***************************************************************************************************
 * FileName:             IComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCDFx.ComponentModel
{
    /// <summary>
    /// Provides functionality required by all components.
    /// </summary>
    public interface IComponent : IDisposableEx
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event EventHandler<Component, PropertyChangedEventArgs> PropertyChanged;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        event EventHandler<Component, PropertyChangingEventArgs> PropertyChanging;

        /// <summary>
        /// Gets the unique identifier (UID) for this component.
        /// </summary>
        Guid UID { get; }

        /// <summary>
        /// Gets a value indicating whether this component is invalid.
        /// </summary>
        /// <value><c>true</c> if this component is invalid; otherwise, <c>false</c>.</value>
        bool IsInvalid { get; }
    }
}