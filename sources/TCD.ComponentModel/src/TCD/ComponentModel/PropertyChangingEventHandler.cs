/***************************************************************************************************
 * FileName:             PropertyChangingEventHandler.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.ComponentModel
{
    /// <summary>
    /// Represents the method that will handle the <see cref="INotifyPropertyChanging.PropertyChanging"/> event raised when a property is changing on a component.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="PropertyChangingEventArgs"/> that contains the event data.</param>
    public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e);
}