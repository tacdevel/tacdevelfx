/***************************************************************************************************
 * FileName:             DisposingEventArgs.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides data for the <see cref="IComponent.Disposing"/> event.
    /// </summary>
    public class DisposingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DisposingEventArgs"/> class.
        /// </summary>
        /// <param name="componentName">The name of the disposing component.</param>
        public DisposingEventArgs(string componentName) => ComponentName = componentName;

        /// <summary>
        /// Gets the name of the disposing component.
        /// </summary>
        public virtual string ComponentName { get; }
    }
}