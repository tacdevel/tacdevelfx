/***************************************************************************************************
 * FileName:             CloseEventArgs.cs
 * Date:                 20180921
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD.UI
{
    /// <summary>
    /// Provides data for a closing event.
    /// </summary>
    public class CloseEventArgs : NativeEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CloseEventArgs"/> with the specified event data.
        /// </summary>
        /// <param name="close"><see langword="true"/> to close; <see langword="false"/> to cancel.</param>
        public CloseEventArgs(bool close = true) => Close = close;

        /// <summary>
        /// Gets a value determining whether to close or not.
        /// </summary>
        public bool Close { get; }
    }
}