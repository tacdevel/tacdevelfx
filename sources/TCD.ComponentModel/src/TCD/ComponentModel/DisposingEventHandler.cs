/***************************************************************************************************
 * FileName:             DisposingEventHandler.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.ComponentModel
{
    /// <summary>
    /// Represents the method that will handle the <see cref="IComponent.Disposing"/> event raised when a component is disposing.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="DisposingEventArgs"/> that contains the event data.</param>
    public delegate void DisposingEventHandler(object sender, DisposingEventArgs e);
}