/***************************************************************************************************
 * FileName:             GroupContainer.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a control that creates a container that has a border and a title for user-interface (UI) content.
    /// </summary>
    public class GroupContainer : SingleChildContainer<Control>
    {
        private Control child;
        private string title;
        private bool isMargined = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupContainer"/> class with the specified title.
        /// </summary>
        /// <param name="title">The title of this <see cref="GroupContainer"/>.</param>
        public GroupContainer(string title) : base(new SafeControlHandle(Libui.Call<Libui.uiNewGroup>()(title))) => this.title = title;

        /// <summary>
        /// Gets or sets the title for this <see cref="GroupContainer"/> control.
        /// </summary>
        public string Title
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                title = Libui.Call<Libui.uiGroupTitle>()(Handle);
                return title;
            }
            set
            {
                if (title == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiGroupSetTitle>()(Handle, value);
                title = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not this <see cref="TabPage"/> has outer margins.
        /// </summary>
        public bool IsMargined
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                isMargined = Libui.Call<Libui.uiGroupMargined>()(Handle);
                return isMargined;
            }
            set
            {
                if (isMargined == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiGroupSetMargined>()(Handle, value);
                isMargined = value;
            }
        }

        /// <summary>
        /// Sets this <see cref="GroupContainer"/> object's child <see cref="Control"/>.
        /// </summary>
        public override Control Child
        {
            set
            {
                if (child != value)
                {
                    if (IsInvalid) throw new InvalidHandleException();
                    Libui.Call<Libui.uiGroupSetChild>()(Handle, value.Handle);
                    child = value;
                }
            }
        }
    }
}