/***************************************************************************************************
 * FileName:             Button.cs
 * Date:                 20180925
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

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
        public Button(string text) : base(new SafeControlHandle(Libui.Call<Libui.uiNewButton>()(text)))
        {
            this.text = text;
            InitializeEvents();
        }

        /// <summary>
        /// Occurs when this <see cref="Button"/> is clicked.
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Gets or sets the text within this <see cref="Button"/>.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.Call<Libui.uiButtonText>()(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiButtonSetText>()(Handle, value);
                text = value;
            }
        }

        /// <summary>
        /// Raises the <see cref="Click"/> event.
        /// </summary>
        protected virtual void OnClick(Button sender, EventArgs e) => Click?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="Button"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiButtonOnClicked>()(Handle, (button, data) => OnClick(this, EventArgs.Empty), IntPtr.Zero);
        }
    }
}