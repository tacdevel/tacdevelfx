/***************************************************************************************************
 * FileName:             ColorPicker.cs
 * Date:                 20180925
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
    /// Represents a common button that allows a user to choose a <see cref="Drawing.Color"/>.
    /// </summary>
    public class ColorPicker : Control
    {
        private Color color;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPicker"/> class.
        /// </summary>
        public ColorPicker() : base(new SafeControlHandle(LibuiEx.NewColorButton())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Color"/> property is changed.
        /// </summary>
        public event NativeEventHandler<ColorPicker> ColorChanged;

        /// <summary>
        /// Gets or sets the color selected by the user.
        /// </summary>
        public Color Color
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                LibuiEx.ColorButtonColor(Handle, out double red, out double green, out double blue, out double alpha);
                return new Color(red, green, blue, alpha);
            }
            set
            {
                if (color == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                LibuiEx.ColorButtonSetColor(Handle, value.R, value.G, value.B, value.A);
                color = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="ColorChanged"/> event.
        /// </summary>
        protected virtual void OnColorChanged(ColorPicker sender) => ColorChanged?.Invoke(sender);

        /// <summary>
        /// Initializes this <see cref="ColorPicker"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            LibuiEx.ColorButtonOnChanged(Handle, (button, data) => OnColorChanged(this), IntPtr.Zero);
        }
    }
}