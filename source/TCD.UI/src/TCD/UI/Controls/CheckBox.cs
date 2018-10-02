/****************************************************************************
 * FileName:   CheckBox.cs
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

namespace TCD.UI
{
    /// <summary>
    /// Represents a control that a user can set and clear.
    /// </summary>
    public class CheckBox : Control
    {
        private string text;
        private bool @checked = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBox"/> class with the specified text.
        /// </summary>
        /// <param name="text">The text specified by the <see cref="CheckBox"/>.</param>
        public CheckBox(string text) : base(new SafeControlHandle(Libui.NewCheckbox(text)))
        {
            this.text = text;
            InitializeEvents();
        }

        //TODO: Maybe add separate Checked and Unchecked events.
        /// <summary>
        /// Occurs when the <see cref="Checked"/> property is changed.
        /// </summary>
        public event Event<CheckBox> Toggled;

        /// <summary>
        /// Gets or sets the text shown by this <see cref="CheckBox"/>.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.CheckboxText(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.CheckboxSetText(Handle, value);
                text = value;
            }
        }

        /// <summary>
        /// Gets or sets the state of this <see cref="CheckBox"/>.
        /// </summary>
        public bool Checked
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                @checked = Libui.CheckboxChecked(Handle);
                return @checked;
            }
            set
            {
                if (@checked == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.CheckboxSetChecked(Handle, value);
                @checked = value;
            }
        }

        /// <summary>
        /// Called when the <see cref="Toggled"/> event is raised.
        /// </summary>
        protected virtual void OnToggled(CheckBox sender) => Toggled?.Invoke(sender);

        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.CheckboxOnToggled(Handle, (checkbox, data) => { OnToggled(this); }, IntPtr.Zero);
        }
    }
}