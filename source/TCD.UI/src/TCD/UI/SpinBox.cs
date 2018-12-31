/***************************************************************************************************
 * FileName:             SpinBox.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a spin box (also known as an up-down control) that displays numeric values.
    /// </summary>
    public class SpinBox : Control
    {
        private int value = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpinBox"/> class with the specified minimum and maximum values.
        /// </summary>
        /// <param name="min">The minimum this <see cref="SpinBox"/> object's value can be.</param>
        /// <param name="max">The maximum this <see cref="SpinBox"/> object's value can be.</param>
        public SpinBox(int min = 0, int max = 100) : base(new SafeControlHandle(Libui.Call<Libui.uiNewSpinbox>()(min, max)))
        {
            MinimumValue = min;
            MaximumValue = max;
            InitializeEvents();
        }

        /// <summary>
        /// Occurs when the <see cref="Value"/> property is changed.
        /// </summary>
        public event EventHandler ValueChanged;

        /// <summary>
        /// Gets this <see cref="SpinBox"/> object's minimum value.
        /// </summary>
        public int MinimumValue { get; }

        /// <summary>
        /// Gets this <see cref="SpinBox"/> object's maximum value.
        /// </summary>
        public int MaximumValue { get; }

        /// <summary>
        /// Gets or sets the current value of this <see cref="SpinBox"/>.
        /// </summary>
        public int Value
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                value = Libui.Call<Libui.uiSpinboxValue>()(Handle);
                return value;
            }
            set
            {
                if (this.value == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiSpinboxSetValue>()(Handle, value);
                this.value = value;
            }
        }

        /// <summary>
        /// Called when the <see cref="ValueChanged"/> event is raised.
        /// </summary>
        protected virtual void OnValueChanged(SpinBox sender, EventArgs e) => ValueChanged?.Invoke(sender, e);

        /// <summary>
        /// Initializes this <see cref="SpinBox"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.Call<Libui.uiSpinboxOnChanged>()(Handle, (slider, data) => OnValueChanged(this, EventArgs.Empty), IntPtr.Zero);
        }
    }
}