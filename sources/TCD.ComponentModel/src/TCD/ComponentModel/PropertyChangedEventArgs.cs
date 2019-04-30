/***************************************************************************************************
 * FileName:             PropertyChangedEventArgs.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides data for the <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
    /// </summary>
    public class PropertyChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyChangedEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        public PropertyChangedEventArgs(string propertyName) => PropertyName = propertyName;

        /// <summary>
        /// Gets the name of the property that changed.
        /// </summary>
        public virtual string PropertyName { get; }
    }
}