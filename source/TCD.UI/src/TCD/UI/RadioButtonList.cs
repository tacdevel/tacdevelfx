/***************************************************************************************************
 * FileName:             RadioButtonList.cs
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
    /// Represents a control that encapsulates a group of radio buttons.
    /// </summary>
    public class RadioButtonList : Control
    {
        private int index = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonList"/> class.
        /// </summary>
        public RadioButtonList() : base(new SafeControlHandle(Libui.Call<Libui.uiNewRadioButtons>()())) => InitializeEvents();

        /// <summary>
        /// Occurs when the <see cref="SelectedIndex"/> property is changed.
        /// </summary>
        public event EventHandler SelectedIndexChanged;

        /// <summary>
        /// Gets or sets the index of the selected item in the list.
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                index = Libui.Call<Libui.uiRadioButtonsSelected>()(Handle);
                return index;
            }
            set
            {
                if (index == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiRadioButtonsSetSelected>()(Handle, value);
                index = value;
            }
        }

        /// <summary>
        /// Adds a radio button to the end of the list.
        /// </summary>
        /// <param name="item">The text of the radio button to be added to the end of the list.</param>
        public void Add(string item)
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiRadioButtonsAppend>()(Handle, item);
        }

        /// <summary>
        /// Adds radio buttons to the end of the list.
        /// </summary>
        /// <param name="items">The text of the radio buttons to be added to the end of the list.</param>
        public void Add(params string[] items)
        {
            if (items == null)
                Add(string.Empty);
            else
            {
                foreach (string item in items)
                {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Called when the <see cref="SelectedIndexChanged"/> event is raised.
        /// </summary>
        protected virtual void OnSelectedIndexChanged(RadioButtonList sender, EventArgs e) => SelectedIndexChanged?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="RadioButtonList"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiRadioButtonsOnSelected>()(Handle, (btn, data) => OnSelectedIndexChanged(this, EventArgs.Empty), IntPtr.Zero);
        }
    }
}