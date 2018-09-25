/****************************************************************************
 * FileName:   ColorPicker.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180925
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using TCD.Drawing;
using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

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
        /// <param name="color">The color of the <see cref="ColorPicker"/>.</param>
        public ColorPicker(Color color = null) : base(new SafeControlHandle(Libui.NewColorButton()))
        {
            if (color != null)
                Color = color;
            InitializeEvents();
        }

        /// <summary>
        /// Occurs when the <see cref="Color"/> property is changed.
        /// </summary>
        public event Event<ColorPicker> ColorChanged;

        /// <summary>
        /// Gets or sets the color selected by the user.
        /// </summary>
        public Color Color
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.ColorButtonColor(Handle, out double red, out double green, out double blue, out double alpha);
                return new Color(red, green, blue, alpha);
            }
            set
            {
                if (color == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.ColorButtonSetColor(Handle, value.R, value.G, value.B, value.A);
                color = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="ColorChanged"/> event.
        /// </summary>
        protected virtual void OnColorChanged(ColorPicker sender) => ColorChanged?.Invoke(sender);

        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ColorButtonOnChanged(Handle, (button, data) => { OnColorChanged(this); }, IntPtr.Zero);
        }
    }
}