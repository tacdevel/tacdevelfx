/***************************************************************************************************
 * FileName:             DisposingEventHandler.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCDFx
{
    /// <summary>
    /// Represents the method that will handle the <see cref="IDisposableEx.Disposing"/> event raised when a component is disposing.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    public delegate void DisposingEventHandler(IDisposableEx sender, EventArgs e);
}