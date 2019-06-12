/***************************************************************************************************
 * FileName:             INotifyPropertyChanged.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCDFx.ComponentModel
{
    /// <summary>
    /// Notifies clients that a property value has changed.
    /// </summary>
    public interface INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        event EventHandler<Component, PropertyChangedEventArgs> PropertyChanged;
    }
}