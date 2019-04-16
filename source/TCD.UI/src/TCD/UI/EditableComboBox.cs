/***************************************************************************************************
 * FileName:             EditableComboBox.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a selection control with a drop-down list that can be shown or hidden by clicking the arrow on the control, and can be typed into.
    /// </summary>
    public class EditableComboBox : Control
    {
        private string text = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        public EditableComboBox() : base(new SafeControlHandle(Libui.Call<Libui.uiNewEditableCombobox>()())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Text"/> property is changed.
        /// </summary>
        public event EventHandler TextChanged;

        /// <summary>
        /// Gets or sets the text of this <see cref="EditableComboBox"/>.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.Call<Libui.uiEditableComboboxText>()(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiEditableComboboxSetText>()(Handle, value);
                text = value;
            }
        }

        /// <summary>
        /// Adds a drop-down item to this <see cref="EditableComboBox"/>.
        /// </summary>
        /// <param name="item">The item to add to this control.</param>
        public void Add(string item)
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiEditableComboboxAppend>()(Handle, item);
        }

        /// <summary>
        /// Adds drop-down items to this <see cref="EditableComboBox"/>.
        /// </summary>
        /// <param name="items">The items to add to this control</param>
        public void Add(params string[] items)
        {
            foreach (string s in items)
            {
                Add(s);
            }
        }

        /// <summary>
        /// Called when the <see cref="TextChanged"/> event is raised.
        /// </summary>
        protected virtual void OnTextChanged(EditableComboBox sender, EventArgs e) => TextChanged?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="EditableComboBox"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiEditableComboboxOnChanged>()(Handle, (box, data) => OnTextChanged(this, EventArgs.Empty), IntPtr.Zero);
        }
    }
}