/***************************************************************************************************
 * FileName:             DisposedEventArgs.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides data for the <see cref="IComponent.Disposed"/> event.
    /// </summary>
    public class DisposedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposedEventArgs"/> class.
        /// </summary>
        /// <param name="componentName">The name of the disposed component.</param>
        public DisposedEventArgs(string componentName) => ComponentName = componentName;

        /// <summary>
        /// Gets the name of the disposed component.
        /// </summary>
        public virtual string ComponentName { get; }
    }
}