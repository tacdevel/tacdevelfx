/***************************************************************************************************
 * FileName:             EditableComboBox.cs
 * Date:                 20180925
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/
 
using System;
using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// Represents a selection control with a drop-down list that can be shown or hidden by clicking the arrow on the control, and can be typed into.
    /// </summary>
    public class EditableComboBox : Control
    {
        private string text = null;

        /// <summary>
        /// Initalizes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        public EditableComboBox() : base(new SafeControlHandle(Libui.NewEditableCombobox())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Text"/> property is changed.
        /// </summary>
        public event NativeEventHandler<EditableComboBox> TextChanged;

        /// <summary>
        /// Gets or sets the text of this <see cref="EditableComboBox"/>.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.EditableComboboxText(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.EditableComboboxSetText(Handle, value);
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
            Libui.EditableComboboxAppend(Handle, item);
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
        protected virtual void OnTextChanged(EditableComboBox sender) => TextChanged?.Invoke(sender);

        /// <summary>
        /// Initializes this <see cref="EditableComboBox"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.EditableComboboxOnChanged(Handle, (box, data) => OnTextChanged(this), IntPtr.Zero);
        }
    }
}