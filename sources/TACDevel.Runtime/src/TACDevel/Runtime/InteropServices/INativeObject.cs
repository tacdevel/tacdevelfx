/***************************************************************************************************
 * FileName:             INativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TACDevel.Runtime.InteropServices
{
    public interface INativeObject : IDisposableEx { }

    /// <summary>
    /// Provides functionality required by all objects that have a native handle.
    /// </summary>
    /// <typeparam name="T">The type of handle.</typeparam>
    public interface INativeObject<T> : IDisposableEx
        where T : unmanaged
    {
        /// <summary>
        /// Gets the native handle representing this native object.
        /// </summary>
        T Handle { get; }
    }
}