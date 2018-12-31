/***************************************************************************************************
 * FileName:             CheckBox.cs
 * Date:                 20180925
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a control that a user can set and clear.
    /// </summary>
    public class CheckBox : Control
    {
        private string text;
        private bool @checked = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class with the specified text.
        /// </summary>
        /// <param name="text">The text specified by the <see cref="CheckBox"/>.</param>
        public CheckBox(string text) : base(new SafeControlHandle(Libui.Call<Libui.uiNewCheckbox>()(text)))
        {
            this.text = text;
            InitializeEvents();
        }
        
        /// <summary>
        /// Occurs when the <see cref="Checked"/> property is changed.
        /// </summary>
        public event EventHandler Toggled;

        /// <summary>
        /// Gets or sets the text shown by this <see cref="CheckBox"/>.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.Call<Libui.uiCheckboxText>()(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiCheckboxSetText>()(Handle, value);
                text = value;
            }
        }

        /// <summary>
        /// Gets or sets the state of this <see cref="CheckBox"/>.
        /// </summary>
        public bool Checked
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                @checked = Libui.Call<Libui.uiCheckboxChecked>()(Handle);
                return @checked;
            }
            set
            {
                if (@checked == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiCheckboxSetChecked>()(Handle, value);
                @checked = value;
            }
        }

        /// <summary>
        /// Called when the <see cref="Toggled"/> event is raised.
        /// </summary>
        protected virtual void OnToggled(CheckBox sender, EventArgs e) => Toggled?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="CheckBox"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiCheckboxOnToggled>()(Handle, (checkbox, data) => OnToggled(this, EventArgs.Empty), IntPtr.Zero);
        }
    }
}