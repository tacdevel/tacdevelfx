/***************************************************************************************************
 * FileName:             TextBox.cs
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
    /// Represents a base implementation of controls that can be used to display and edit one line of text.
    /// </summary>
    public abstract class TextBoxBase : Control
    {
        private string text = null;
        private bool isReadOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxBase"/> class.
        /// </summary>
        internal TextBoxBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Text"/> property is changed.
        /// </summary>
        public event EventHandler TextChanged;

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public virtual string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.Call<Libui.uiEntryText>()(Handle);
                return text;
            }
            set
            {
                if (text != value)
                {
                    Libui.Call<Libui.uiEntrySetText>()(Handle, value);
                    text = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the text is read-only or not.
        /// </summary>
        public virtual bool IsReadOnly
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                isReadOnly = Libui.Call<Libui.uiEntryReadOnly>()(Handle);
                return isReadOnly;
            }
            set
            {
                if (isReadOnly != value)
                {
                    Libui.Call<Libui.uiEntrySetReadOnly>()(Handle, value);
                    isReadOnly = value;
                }
            }
        }

        /// <summary>
        /// Called when the <see cref="TextChanged"/> event is raised.
        /// </summary>
        protected virtual void OnTextChanged(TextBoxBase sender, EventArgs e) => TextChanged?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="TextBoxBase"/> object's events.
        /// </summary>
        protected override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiEntryOnChanged>()(Handle, (entry, data) => OnTextChanged(this, EventArgs.Empty), IntPtr.Zero);
        }
    }

    /// <summary>
    /// Represents a control that can be used to display or edit text.
    /// </summary>
    public class TextBox : TextBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBox"/> class.
        /// </summary>
        public TextBox() : base(new SafeControlHandle(Libui.Call<Libui.uiNewEntry>()()), true) { }
    }

    /// <summary>
    /// Represents a <see cref="TextBox"/> that displays it's text as password characters.
    /// </summary>
    public class PasswordBox : TextBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordBox"/> class.
        /// </summary>
        public PasswordBox() : base(new SafeControlHandle(Libui.Call<Libui.uiNewPasswordEntry>()()), true) { }
    }

    /// <summary>
    /// Represents a <see cref="TextBox"/> that displays a search icon.
    /// </summary>
    public class SearchBox : TextBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBox"/> class.
        /// </summary>
        public SearchBox() : base(new SafeControlHandle(Libui.Call<Libui.uiNewSearchEntry>()()), true) { }
    }
}