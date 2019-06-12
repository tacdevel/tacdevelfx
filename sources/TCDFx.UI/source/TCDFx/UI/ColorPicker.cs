/***************************************************************************************************
 * FileName:             ColorPicker.cs
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
    /// Represents a common button that allows a user to choose a <see cref="Drawing.Color"/>.
    /// </summary>
    public class ColorPicker : Control
    {
        private Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPicker"/> class.
        /// </summary>
        public ColorPicker() : base(new SafeControlHandle(Libui.Call<Libui.uiNewColorButton>()())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Color"/> property is changed.
        /// </summary>
        public event EventHandler ColorChanged;

        /// <summary>
        /// Gets or sets the color selected by the user.
        /// </summary>
        public Color Color
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiColorButtonColor>()(Handle, out double red, out double green, out double blue, out double alpha);
                return new Color(red, green, blue, alpha);
            }
            set
            {
                if (color == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiColorButtonSetColor>()(Handle, value.R, value.G, value.B, value.A);
                color = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="ColorChanged"/> event.
        /// </summary>
        protected virtual void OnColorChanged(ColorPicker sender, EventArgs e) => ColorChanged?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="ColorPicker"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiColorButtonOnChanged>()(Handle, (button, data) => OnColorChanged(this, EventArgs.Empty), IntPtr.Zero);
        }
    }
}