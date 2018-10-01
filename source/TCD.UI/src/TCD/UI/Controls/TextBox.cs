/****************************************************************************
 * FileName:   TextBox.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
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
    /// Represents a base implementation of controls that can be used to display and edit one line of text.
    /// </summary>
    public abstract class TextBoxBase : Control
    {
        private string text = null;
        private bool isReadOnly = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBoxBase"/> class.
        /// </summary>
        internal TextBoxBase(SafeControlHandle handle, bool cacheable = true, string text = null, bool isReadOnly = false) : base(handle, cacheable)
        {
            if (text != null)
                Text = text;
            if (this.isReadOnly != isReadOnly)
                IsReadOnly = isReadOnly;
            InitializeEvents();
        }

        /// <summary>
        /// Occurs when the <see cref="Text"/> property is changed.
        /// </summary>
        public event Event<TextBoxBase> TextChanged;

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

        protected override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.EntryOnChanged(Handle, (entry, data) => { OnTextChanged(this); }, IntPtr.Zero);
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
        public TextBox(string text = null, bool isReadOnly = false) : base(new SafeControlHandle(Libui.NewEntry()), true, text, isReadOnly) { }
    }

    /// <summary>
    /// Represents a <see cref="TextBox"/> that displays it's text as password characters.
    /// </summary>
    public class PasswordBox : TextBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordBox"/> class.
        /// </summary>
        public PasswordBox(string text = null, bool isReadOnly = false) : base(new SafeControlHandle(Libui.NewPasswordEntry()), true, text, isReadOnly) { }
    }

    /// <summary>
    /// Represents a <see cref="TextBox"/> that displays a search icon.
    /// </summary>
    public class SearchBox : TextBoxBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchBox"/> class.
        /// </summary>
        public SearchBox(string text = null, bool isReadOnly = false) : base(new SafeControlHandle(Libui.NewSearchEntry()), true, text, isReadOnly) { }
    }
}