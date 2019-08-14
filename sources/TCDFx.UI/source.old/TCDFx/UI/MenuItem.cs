/***************************************************************************************************
 * FileName:             MenuItem.cs
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
    /// Represents a base implementation for a menu child contained in a <see cref="Menu"/>.
    /// </summary>
    public abstract class MenuItemBase : Control
    {
        private bool enabled;

        /// <summary>
        /// Initializes a new instance of a <see cref="MenuItemBase"/> class from the specified handle.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        internal MenuItemBase(SafeControlHandle handle) : base(handle) => InitializeEvents();

        /// <summary>
        /// Occurs when the menu child is clicked.
        /// </summary>
        public virtual event Action<IntPtr> Clicked;

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to interaction.
        /// </summary>
        public override bool Enabled
        {
            get => enabled;
            set
            {
                if (enabled == value) return;
                if (value) Enable();
                else Disable();
                enabled = value;
            }
        }

        /// <summary>
        /// Enables this control to accept user-interaction.
        /// </summary>
        public override void Enable()
        {
            if (!enabled)
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiMenuItemEnable>()(Handle);
                enabled = true;
            }
        }

        /// <summary>
        /// Disables the control from accepting user-interaction.
        /// </summary>
        public override void Disable()
        {
            if (enabled)
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiMenuItemDisable>()(Handle);
                enabled = false;
            }
        }

        /// <summary>
        /// Initializes this <see cref="MenuItemBase"/> object's events.
        /// </summary>
        protected override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiMenuItemOnClicked>()(Handle, (child, window, data) => OnClicked(window), IntPtr.Zero);
        }

        /// <summary>
        /// Called when the <see cref="Clicked"/> event is raised.
        /// </summary>
        /// <param name="data">An <see cref="IntPtr"/> that contains the event data.</param>
        protected virtual void OnClicked(IntPtr data) => Clicked?.Invoke(data);
    }

    /// <summary>
    /// Represents a basic child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class MenuItem : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="MenuItem"/> class from the specified handle with the specified name.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        /// <param name="name">The menu child's name.</param>
        internal MenuItem(SafeControlHandle handle, string name) : base(handle) => Name = name;

        /// <summary>
        /// Gets this menu child's name.
        /// </summary>
        public string Name { get; }
    }

    /// <summary>
    /// Represents a checkable menu child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class CheckableMenuItem : MenuItemBase
    {
        private bool @checked;

        /// <summary>
        /// Initializes a new instance of a <see cref="CheckableMenuItem"/> class from the specified handle with the specified name.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        /// <param name="name">The menu child's name.</param>
        internal CheckableMenuItem(SafeControlHandle handle, string name) : base(handle) => Name = name;

        /// <summary>
        /// Gets or sets the state of this <see cref="CheckableMenuItem"/>.
        /// </summary>
        public bool Checked
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                @checked = Libui.Call<Libui.uiMenuItemChecked>()(Handle);
                return @checked;
            }
            set
            {
                if (@checked == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiMenuItemSetChecked>()(Handle, value);
                @checked = value;
            }
        }

        /// <summary>
        /// Gets this menu child's name.
        /// </summary>
        public string Name { get; }
    }

    /// <summary>
    /// Represents a about menu child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class AboutMenuItem : MenuItemBase
    {
        internal AboutMenuItem(SafeControlHandle handle) : base(handle) { }
    }

    /// <summary>
    /// Represents a preferences menu child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class PreferencesMenuItem : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="PreferencesMenuItem"/> class from the specified handle.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        internal PreferencesMenuItem(SafeControlHandle handle) : base(handle) { }
    }

    /// <summary>
    /// Represents a about menu child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class QuitMenuItem : MenuItemBase
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="QuitMenuItem"/> class from the specified handle.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        internal QuitMenuItem(SafeControlHandle handle) : base(handle) { }

        /// <summary>
        /// Initializes this UI component's events.
        /// </summary>
        protected sealed override void InitializeEvents() { }

        /// <summary>
        /// This method does not do anything, and will throw a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="data">An <see cref="IntPtr"/> that contains the event data.</param>
        protected sealed override void OnClicked(IntPtr data) => throw new NotSupportedException();
    }
}