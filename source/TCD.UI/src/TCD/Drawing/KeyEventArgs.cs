/***************************************************************************************************
 * FileName:             KeyEventArgs.cs
 * Date:                 20181003
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Runtime.InteropServices;

namespace TCD.Drawing
{
    /// <summary>
    /// Provides key data for an event.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public sealed class KeyEventArgs : NativeEventArgs
    {
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0032 // Use auto property
        private char key;
        private ExtensionKey extension;
        private ModifierKey modifier, modifiers;
        private bool up;
#pragma warning restore IDE0032 // Use auto property
#pragma warning restore IDE0044 // Add readonly modifier

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyEventArgs"/> class.
        /// </summary>
        /// <param name="key">The key that was pressed.</param>
        /// <param name="extension">The extension key that was pressed.</param>
        /// <param name="modifier">The single modifier that was pressed</param>
        /// <param name="modifiers">The multiple modifier keys that were pressed.</param>
        /// <param name="up">Whether the key was released or not.</param>
        public KeyEventArgs(char key, ExtensionKey extension, ModifierKey modifier, ModifierKey modifiers, bool up)
        {
            this.key = key;
            this.extension = extension;
            this.modifier = modifier;
            this.modifiers = modifiers;
            this.up = up;
        }

        /// <summary>
        /// Gets the key that was presseed as a string.
        /// </summary>
        public char Key => key;

        /// <summary>
        /// Gets the pressed extension key.
        /// </summary>
        public ExtensionKey Extension => extension;

        /// <summary>
        /// Gets a single modifier key-press.
        /// </summary>
        public ModifierKey Modifier => modifier;

        /// <summary>
        /// Gets the modifier keys that were pressed.
        /// </summary>
        public ModifierKey Modifiers => modifiers;

        /// <summary>
        /// Gets a value indicating if the key was released.
        /// </summary>
        public bool Up => up;
    }
}