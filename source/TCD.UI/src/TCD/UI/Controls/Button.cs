/****************************************************************************
 * FileName:   Button.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180925
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a basic button control with text.
    /// </summary>
    public class Button : Control
    {
        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class with the specified text.
        /// </summary>
        /// <param name="text">The text to be displayed by this <see cref="Button"/>.</param>
        public Button(string text) : base(new SafeControlHandle(Libui.NewButton(text)))
        {
            this.text = text;
            InitializeEvents();
        }

        /// <summary>
        /// Occurs when this <see cref="Button"/> is clicked.
        /// </summary>
        public event Event<Button> Click;

        /// <summary>
        /// Gets or sets the text within this <see cref="Button"/>.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.ButtonText(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.ButtonSetText(Handle, value);
                text = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="Click"/> event.
        /// </summary>
        protected virtual void OnClick(Button sender) => Click?.Invoke(sender);

        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ButtonOnClicked(Handle, (button, data) => { OnClick(this); }, IntPtr.Zero);
        }
    }
}