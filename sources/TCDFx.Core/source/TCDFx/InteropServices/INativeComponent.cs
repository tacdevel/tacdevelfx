/***************************************************************************************************
 * FileName:             INativeComponent.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCDFx.ComponentModel;

namespace TCDFx.InteropServices
{
    /// <summary>
    /// Provides functionality required by all components that have a native handle.
    /// </summary>
    /// <typeparam name="T">The type of handle.</typeparam>
    public interface INativeComponent<T> : IComponent, IDisposableEx
    {
        /// <summary>
        /// Gets the native handle representing this native component.
        /// </summary>
        T Handle { get; }
    }
}