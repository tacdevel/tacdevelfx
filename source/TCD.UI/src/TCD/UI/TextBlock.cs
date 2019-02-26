/***************************************************************************************************
 * FileName:             TextBlock.cs
 * Date:                 20181001
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
    /// Implements the basic functionality required by a control that can be used to display or edit multiple lines of text.
    /// </summary>
    public abstract class TextBlockBase : Control
    {
        private string text = null;
        private bool isReadOnly = false;

        internal TextBlockBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Text"/> property is changed.
        /// </summary>
        public event EventHandler TextChanged;

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.Call<Libui.uiMultilineEntryText>()(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiMultilineEntrySetText>()(Handle, value);
                text = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the text is read-only or not.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                isReadOnly = Libui.Call<Libui.uiMultilineEntryReadOnly>()(Handle);
                return isReadOnly;
            }
            set
            {
                if (isReadOnly == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiEntrySetReadOnly>()(Handle, value);
                isReadOnly = value;
            }
        }

        /// <summary>
        /// Adds the specified line of text to the end of the text currently contained in this <see cref="TextBlockBase"/>.
        /// </summary>
        /// <param name="line">The line to add.</param>
        public void Add(string line) => Libui.Call<Libui.uiMultilineEntryAppend>()(Handle, line);

        /// <summary>
        /// Adds the specified lines of text to the end of the text currently contained in this <see cref="TextBlockBase"/>.
        /// </summary>
        /// <param name="lines">The lines to add.</param>
        public void Add(params string[] lines)
        {
            foreach (string line in lines)
            {
                Add(line);
            }
        }

        /// <summary>
        /// Called when the <see cref="TextChanged"/> event is raised.
        /// </summary>
        protected virtual void OnTextChanged(TextBlockBase sender, EventArgs e) => TextChanged?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="TextBlockBase"/> object's events.
        /// </summary>
        protected override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiMultilineEntryOnChanged>()(Handle, (entry, data) => OnTextChanged(this, EventArgs.Empty), IntPtr.Zero);
        }
    }

    /// <summary>
    /// Represents a control that can be used to display or edit multiple lines of text.
    /// </summary>
    public class TextBlock : TextBlockBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlock"/> class.
        /// </summary>
        public TextBlock() : base(new SafeControlHandle(Libui.Call<Libui.uiNewMultilineEntry>()()), true) { }
    }

    /// <summary>
    /// Represents a control that can be used to display or edit multiple lines of text that are not wrapped.
    /// </summary>summary>
    public class NonWrappingTextBlock : TextBlockBase
    {
        public NonWrappingTextBlock() : base(new SafeControlHandle(Libui.Call<Libui.uiNewNonWrappingMultilineEntry>()()), true) { }
    }
}