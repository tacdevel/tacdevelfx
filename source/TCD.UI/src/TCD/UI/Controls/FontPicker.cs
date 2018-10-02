/****************************************************************************
 * FileName:   FontPicker.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

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
        public FontPicker() : base(new SafeControlHandle(Libui.NewFontButton())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Font"/> property is changed.
        /// </summary>
        public event Event<FontPicker> FontChanged;

        /// <summary>
        /// Gets the currently selected font.
        /// </summary>
        public Font Font
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.FontButtonFont(Handle, out font);
                return font;
            }
        }

        /// <summary>
        /// Raises the <see cref="FontChanged"/> event.
        /// </summary>
        protected virtual void OnFontChanged(FontPicker sender) => FontChanged?.Invoke(sender);

        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.FontButtonOnChanged(Handle, (button, data) => { OnFontChanged(this); }, IntPtr.Zero);
        }

        protected sealed override void ReleaseUnmanagedResources()
        {
            if (font != null)
            {
                Libui.FreeFontButtonFont(font);
                font = null;
            }
            base.ReleaseUnmanagedResources();
        }
    }
}