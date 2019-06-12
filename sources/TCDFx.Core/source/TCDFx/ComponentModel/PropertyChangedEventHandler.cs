/***************************************************************************************************
 * FileName:             PropertyChangedEventHandler.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCDFx.ComponentModel
{
    /// <summary>
    /// Represents the method that will handle the <see cref="INotifyPropertyChanged.PropertyChanged"/> event raised when a property is changed on a component.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PropertyChangedEventArgs"/> that contains the event data.</param>
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}