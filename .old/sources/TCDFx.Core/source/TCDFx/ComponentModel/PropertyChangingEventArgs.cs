/***************************************************************************************************
 * FileName:             PropertyChangingEventArgs.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCDFx.ComponentModel
{
    /// <summary>
    /// Provides data for the <see cref="IComponent.PropertyChanging"/> event.
    /// </summary>
    public class PropertyChangingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangingEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property whose value is changing.</param>
        public PropertyChangingEventArgs(string propertyName) => PropertyName = propertyName;

        /// <summary>
        /// Gets the name of the property whose value is changing.
        /// </summary>
        public virtual string PropertyName { get; }
    }
}