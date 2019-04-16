/***************************************************************************************************
 * FileName:             FontPicker.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.Drawing;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// A button that allows a user to select a font.
    /// </summary>
    public class FontPicker : Control
    {
        private Libui.uiFontDescriptor uiFontDescriptor;

        /// <summary>
        /// Initializes a new instance of the <see cref="FontPicker"/> class.
        /// </summary>
        public FontPicker() : base(new SafeControlHandle(Libui.Call<Libui.uiNewFontButton>()())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Font"/> property is changed.
        /// </summary>
        public event EventHandler FontChanged;

        /// <summary>
        /// Gets the currently selected font.
        /// </summary>
        public Font Font
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiFontButtonFont>()(Handle, out uiFontDescriptor);
                return new Font(uiFontDescriptor);
            }
        }

        /// <summary>
        /// Raises the <see cref="FontChanged"/> event.
        /// </summary>
        protected virtual void OnFontChanged(FontPicker sender, EventArgs e) => FontChanged?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="FontPicker"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiFontButtonOnChanged>()(Handle, (button, data) => OnFontChanged(this, EventArgs.Empty), IntPtr.Zero);
        }

        protected sealed override void ReleaseUnmanagedResources()
        {
            if (Font != null)
            {
                Libui.Call<Libui.uiFreeFontButtonFont>()(uiFontDescriptor);
                uiFontDescriptor = new Libui.uiFontDescriptor();
            }
            base.ReleaseUnmanagedResources();
        }
    }
}