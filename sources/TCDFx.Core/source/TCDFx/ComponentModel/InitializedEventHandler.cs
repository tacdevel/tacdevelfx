/***************************************************************************************************
 * FileName:             InitializedEventHandler.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.ComponentModel
{
    /// <summary>
    /// Represents the method that will handle the <see cref="INotifyInitialized.Initialized"/> event raised when a component is intialized.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="InitializedEventArgs"/> that contains the event data.</param>
    public delegate void InitializedEventHandler(object sender, InitializedEventArgs e);
}