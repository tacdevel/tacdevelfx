/****************************************************************************
 * FileName:   ComboBox.cs
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

namespace TCD.UI.Controls
{
    /// <summary>
    /// Represents a selection control with a drop-down list that can be shown or hidden by clicking the arrow on the control.
    /// </summary>
    public class ComboBox : Control
    {
        private int index = 0;

        /// <summary>
        /// Initalizes a new instance of the <see cref="ComboBox"/> class.
        /// </summary>
        public ComboBox() : base(new SafeControlHandle(Libui.NewCombobox())) => InitializeEvents();

        /// <summary>
        /// Occurs when a drop-down item is selected.
        /// </summary>
        public event Event<ComboBox> ItemSelected;

        /// <summary>
        /// Gets or sets the selected item by index.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                index = Libui.ComboboxSelected(Handle);
                return index;
            }
            set
            {
                if (index == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.ComboboxSetSelected(Handle, value);
                index = value;
            }
        }

        /// <summary>
        /// Adds a drop-down item to this <see cref="ComboBox"/>.
        /// </summary>
        /// <param name="item">The item to add to this <see cref="ComboBox"/>.</param>
        public void Add(string item)
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ComboboxAppend(Handle, item);
        }

        /// <summary>
        /// Adds drop-down items to this <see cref="ComboBox"/>.
        /// </summary>
        /// <param name="items">The items to add to this <see cref="ComboBox"/></param>
        public void Add(params string[] items)
        {
            foreach (string s in items)
            {
                Add(s);
            }
        }

        /// <summary>
        /// Called when the <see cref="ItemSelected"/> event is raised.
        /// </summary>
        protected virtual void OnItemSelected(ComboBox sender) => ItemSelected?.Invoke(sender);

        /// <summary>
        /// Initializes this <see cref="CheckBox"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ComboboxOnSelected(Handle, (c, data) => { OnItemSelected(this); }, IntPtr.Zero);
        }
    }
}