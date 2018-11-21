/***************************************************************************************************
 * FileName:             FontPicker.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.Drawing;
using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// A button that allows a user to select a font.
    /// </summary>
    public class FontPicker : Control
    {
        private Font font;

        /// <summary>
        /// Initializes a new instance of the <see cref="FontPicker"/> class.
        /// </summary>
        public FontPicker() : base(new SafeControlHandle(LibuiEx.NewFontButton())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Font"/> property is changed.
        /// </summary>
        public event NativeEventHandler<FontPicker> FontChanged;

        /// <summary>
        /// Gets the currently selected font.
        /// </summary>
        public Font Font
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                LibuiEx.FontButtonFont(Handle, out font);
                return font;
            }
        }

        /// <summary>
        /// Raises the <see cref="FontChanged"/> event.
        /// </summary>
        protected virtual void OnFontChanged(FontPicker sender) => FontChanged?.Invoke(sender);

        /// <summary>
        /// Initializes this <see cref="FontPicker"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            LibuiEx.FontButtonOnChanged(Handle, (button, data) => OnFontChanged(this), IntPtr.Zero);
        }

        protected sealed override void ReleaseUnmanagedResources()
        {
            if (font != null)
            {
                LibuiEx.FreeFontButtonFont(font);
                font = null;
            }
            base.ReleaseUnmanagedResources();
        }
    }
}