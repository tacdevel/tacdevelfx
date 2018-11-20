/***************************************************************************************************
 * FileName:             TextBoxBase.cs
 * Date:                 20181001
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
        public event NativeEventHandler<TextBoxBase> TextChanged;

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public virtual string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.EntryText(Handle);
                return text;
            }
            set
            {
                if (text != value)
                {
                    Libui.EntrySetText(Handle, value);
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
                isReadOnly = Libui.EntryReadOnly(Handle);
                return isReadOnly;
            }
            set
            {
                if (isReadOnly != value)
                {
                    Libui.EntrySetReadOnly(Handle, value);
                    isReadOnly = value;
                }
            }
        }

        /// <summary>
        /// Called when the <see cref="TextChanged"/> event is raised.
        /// </summary>
        protected virtual void OnTextChanged(TextBoxBase sender) => TextChanged?.Invoke(sender);

        /// <summary>
        /// Initializes this <see cref="TextBoxBase"/> object's events.
        /// </summary>
        protected override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.EntryOnChanged(Handle, (entry, data) => { OnTextChanged(this); }, IntPtr.Zero);
        }
    }
}