/***************************************************************************************************
 * FileName:             ProgressBar.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a control that indicates the progress of an operation.
    /// </summary>
    public class ProgressBar : Control
    {
        private int value = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressBar"/> class.
        /// </summary>
        public ProgressBar() : base(new SafeControlHandle(Libui.Call<Libui.uiNewProgressBar>()())) { }

        /// <summary>
        /// Gets or sets the current value of this <see cref="ProgressBar"/>.
        /// </summary>
        public int Value
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                value = Libui.Call<Libui.uiProgressBarValue>()(Handle);
                return value;
            }
            set
            {
                if (this.value == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiProgressBarSetValue>()(Handle, value);
                this.value = value;
            }
        }
    }
}