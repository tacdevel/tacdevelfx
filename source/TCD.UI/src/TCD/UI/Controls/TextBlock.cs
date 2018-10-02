/****************************************************************************
 * FileName:   TextBlock.cs
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

namespace TCD.UI.Controls
{
    /// <summary>
    /// Implements the basic functonality required by a control that can be used to display or edit multiple lines of text.
    /// </summary>
    public abstract class TextBlockBase : Control
    {
        private string text = null;
        private bool isReadOnly = false;

        internal TextBlockBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="Text"/> property is changed.
        /// </summary>
        public event Event<TextBlockBase> TextChanged;

        /// <summary>
        /// Gets or sets the displayed text.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.MultilineEntryText(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.MultilineEntrySetText(Handle, value);
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
                isReadOnly = Libui.MultilineEntryReadOnly(Handle);
                return isReadOnly;
            }
            set
            {
                if (isReadOnly == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.EntrySetReadOnly(Handle, value);
                isReadOnly = value;
            }
        }

        /// <summary>
        /// Adds the specified line of text to the end of the text currently contained in this <see cref="TextBlockBase"/>.
        /// </summary>
        /// <param name="line">The line to add.</param>
        public void Add(string line) => Libui.MultilineEntryAppend(Handle, line);

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
        protected virtual void OnTextChanged(TextBlockBase sender) => TextChanged?.Invoke(sender);
        
        protected override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.MultilineEntryOnChanged(Handle, (entry, data) => { OnTextChanged(this); }, IntPtr.Zero);
        }
    }

    /// <summary>
    /// Represents a control that can be used to display or edit multiple lines of text.
    /// </summary>
    public class TextBlock : TextBlockBase
    {
        public TextBlock() : base(new SafeControlHandle(Libui.NewMultilineEntry()), true) { }
    }

    /// Represents a control that can be used to display or edit multiple lines of text that are not wrapped.
    public class NonWrappingTextBlock : TextBlockBase
    {
        public NonWrappingTextBlock() : base(new SafeControlHandle(Libui.NewNonWrappingMultilineEntry()), true) { }
    }
}