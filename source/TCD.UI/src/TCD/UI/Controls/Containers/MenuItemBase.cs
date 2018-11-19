/***************************************************************************************************
 * FileName:             MenuItemBase.cs
 * Date:                 20181002
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
                Libui.MenuItemEnable(Handle);
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
                Libui.MenuItemDisable(Handle);
                enabled = false;
            }
        }

        protected override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.MenuItemOnClicked(Handle, (child, window, data) => { OnClicked(window); }, IntPtr.Zero);
        }

        /// <summary>
        /// Called when the <see cref="Clicked"/> event is raised.
        /// </summary>
        /// <param name="data">An <see cref="IntPtr"/> that contains the event data.</param>
        protected virtual void OnClicked(IntPtr data) => Clicked?.Invoke(data);
    }
}