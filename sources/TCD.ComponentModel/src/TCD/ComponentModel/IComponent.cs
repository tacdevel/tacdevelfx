/***************************************************************************************************
 * FileName:             IComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides functionality required by all components.
    /// </summary>
    public interface IComponent : IDisposable
    {
        /// <summary>
        /// Occurs when this object is disposing.
        /// </summary>
        event DisposingEventHandler Disposing;

        /// <summary>
        /// Occurs when this object is disposed.
        /// </summary>
        event DisposedEventHandler Disposed;

        /// <summary>
        /// Gets or sets the name of this component.
        /// </summary>
        /// <value>The name of this component.</value>
        string Name { get; }

        /// <summary>
        /// Gets a value indicating whether this component is invalid.
        /// </summary>
        /// <value><c>true</c> if this component is invalid; otherwise, <c>false</c>.</value>
        bool IsInvalid { get; }

        /// <summary>
        /// Safely performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources, invoking the specified action in the event of an exception.
        /// </summary>
        /// <param name="exceptionHandler">The action to be invoked in the event of an exception.</param>
        /// <returns><c>true</c> if properly disposed; otherwise, <c>false</c>.</returns>
        bool SafeDispose(Action<Exception> exceptionHandler = null);
    }
}