/****************************************************************************
 * FileName:   Control.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180921
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System.Collections.Generic;
using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI
{
    public abstract class Control : NativeComponent<SafeControlHandle>
    {
        private readonly bool cacheable;
        private static Dictionary<SafeControlHandle, Control> cache = new Dictionary<SafeControlHandle, Control>();
        private bool enabled, visible = true;

        internal Control(SafeControlHandle handle, bool cacheable = true) : base(handle)
        {
            this.cacheable = cacheable;
            if (cacheable)
                cache.Add(handle, this);
            if (this is Window)
                visible = false;
        }

        /// <summary>
        /// Gets the parent <see cref="Control"/> of this <see cref="Control"/>.
        /// </summary>
        public Control Parent { get; internal set; }

        /// <summary>
        /// Gets the index of this <see cref="Control"/>.
        /// </summary>
        public int Index { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Control"/> can respond to interaction.
        /// </summary>
        public virtual bool Enabled
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                return Libui.ControlEnabled(Handle);
            }
            set
            {
                if (enabled == value) return;
                if (value) Enable();
                else Disable();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Control"/> and all of it's child <see cref="Control"/> objects (if applicable) are displayed.
        /// </summary>
        public virtual bool Visible
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                return Libui.ControlVisible(Handle);
            }
            set
            {
                if (visible == value) return;
                if (value) Show();
                else Hide();
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not this is the top-most <see cref="Control"/>.
        /// </summary>
        public bool TopLevel
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                return Libui.ControlTopLevel(Handle);
            }
        }

        /// <summary>
        /// Enables this <see cref="Control"/> to accept user-interaction.
        /// </summary>
        public virtual void Enable()
        {
            if (enabled) return;
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ControlEnable(Handle);
            enabled = true;
        }

        /// <summary>
        /// Disables the <see cref="Control"/> from accepting user-interaction.
        /// </summary>
        public virtual void Disable()
        {
            if (!enabled) return;
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ControlDisable(Handle);
            enabled = false;
        }

        /// <summary>
        /// Displays this <see cref="Control"/> to the user.
        /// </summary>
        public virtual void Show()
        {
            if (visible) return;
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ControlShow(Handle);
            visible = true;
        }

        /// <summary>
        /// Conceals this <see cref="Control"/> from the user.
        /// </summary>
        public virtual void Hide()
        {
            if (!visible) return;
            if (IsInvalid) throw new InvalidHandleException();
            Libui.ControlHide(Handle);
            visible = false;
        }

        /// <summary>
        /// Performs pre-rendering operations.
        /// </summary>
        protected internal virtual void DelayRender() { }

        /// <summary>
        /// Performs tasks associated with releasing managed resources.
        /// </summary>
        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid)
            {
                if (cacheable)
                    cache.Remove(Handle);
            }
            base.ReleaseManagedResources();
        }
    }
}