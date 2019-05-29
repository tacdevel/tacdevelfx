/***************************************************************************************************
 * FileName:             INotifyInitialized.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.ComponentModel
{
    /// <summary>
    /// Notifies clients that a component has been initialized.
    /// </summary>
    public interface INotifyInitialized
    {
        /// <summary>
        /// Occurs when a component has been initialized
        /// </summary>
        event InitializedEventHandler Initialized;
    }
}