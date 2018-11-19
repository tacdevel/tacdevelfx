/***************************************************************************************************
 * FileName:             SpinBox.cs
 * Date:                 20181001
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
        public SpinBox(int min = 0, int max = 100) : base(new SafeControlHandle(Libui.NewSlider(min, max)))
        {
            MinimumValue = min;
            MaximumValue = max;
            InitializeEvents();
        }

        /// <summary>
        /// Occurs when the <see cref="Value"/> property is changed.
        /// </summary>
        public event NativeEventHandler<SpinBox> ValueChanged;

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
                value = Libui.SpinboxValue(Handle);
                return value;
            }
            set
            {
                if (this.value == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.SpinboxSetValue(Handle, value);
                this.value = value;
            }
        }

        /// <summary>
        /// Called when the <see cref="ValueChanged"/> event is raised.
        /// </summary>
        protected virtual void OnValueChanged(SpinBox sender) => ValueChanged?.Invoke(sender);

        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.SpinboxOnChanged(Handle, (slider, data) => { OnValueChanged(this); }, IntPtr.Zero);
        }
    }
}