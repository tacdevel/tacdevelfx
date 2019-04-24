/***************************************************************************************************
 * FileName:             KeyEventArgs.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Native;

namespace TCD.UI
{
    /// <summary>
    /// Provides key data for an event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class KeyEventArgs : EventArgs
    {
        internal Libui.uiAreaKeyEvent uiAreaKeyEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        /// <param name="extension">The extension key that was pressed.</param>
        /// <param name="modifier">The single modifier that was pressed</param>
        /// <param name="modifiers">The multiple modifier keys that were pressed.</param>
        /// <param name="up">Whether the key was released or not.</param>
        public KeyEventArgs(char key, ExtensionKey extension, ModifierKey modifier, ModifierKey modifiers, bool up) => uiAreaKeyEvent = new Libui.uiAreaKeyEvent()
        {
            Key = key,
            ExtKey = (Libui.uiExtKey)extension,
            Modifier = (Libui.uiModifiers)modifier,
            Modifiers = (Libui.uiModifiers)modifiers,
            Up = up
        };
    
        internal KeyEventArgs(Libui.uiAreaKeyEvent @event) => uiAreaKeyEvent = @event;

        /// <summary>
        /// Gets the key that was pressed as a string.
        /// </summary>
        public char Key => uiAreaKeyEvent.Key;

        /// <summary>
        /// Gets the pressed extension key.
        /// </summary>
        public ExtensionKey Extension => (ExtensionKey)uiAreaKeyEvent.ExtKey;

        /// <summary>
        /// Gets a single modifier key-press.
        /// </summary>
        public ModifierKey Modifier => (ModifierKey)uiAreaKeyEvent.Modifier;

        /// <summary>
        /// Gets the modifier keys that were pressed.
        /// </summary>
        public ModifierKey Modifiers => (ModifierKey)uiAreaKeyEvent.Modifiers;

        /// <summary>
        /// Gets a value indicating if the key was released.
        /// </summary>
        public bool Up => uiAreaKeyEvent.Up;
    }
}