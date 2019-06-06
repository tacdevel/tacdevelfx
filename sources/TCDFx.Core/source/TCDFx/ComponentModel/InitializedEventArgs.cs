/***************************************************************************************************
 * FileName:             InitializedEventArgs.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCDFx.ComponentModel
{
    /// <summary>
    /// Provides data for the <see cref="INotifyInitialized.Initialized"/> event.
    /// </summary>
    public class InitializedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InitializedEventArgs"/> class.
        /// </summary>
        /// <param name="componentName">The name of the component that was initialized.</param>
        public InitializedEventArgs(string componentName) => ComponentName = componentName;

        /// <summary>
        /// Gets the name of the component that was initialized.
        /// </summary>
        public virtual string ComponentName { get; }
    }
}