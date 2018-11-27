/***************************************************************************************************
 * FileName:             DateTimePickerBase.cs
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
    /// Implements the basic functionality required by a date-time picker.
    /// </summary>
    public abstract class DateTimePickerBase : Control
    {
        private DateTime? dateTime = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimePickerBase"/> class.
        /// </summary>
        internal DateTimePickerBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) { }

        /// <summary>
        /// Occurs when the <see cref="DateTime"/> property is changed.
        /// </summary>
        public event NativeEventHandler<DateTimePickerBase> DateTimeChanged;

        /// <summary>
        /// Gets or sets the selected date and time.
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.DateTimePickerTime(Handle, out Libui.Time time);
                dateTime = Libui.Time.ToDateTime(time);
                return (DateTime)dateTime;
            }
            set
            {
                if (dateTime == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.DateTimePickerSetTime(Handle, Libui.Time.FromDateTime(value));
                dateTime = value;
            }
        }

        /// <summary>
        /// Called when the <see cref="DateTimeChanged"/> event is raised.
        /// </summary>
        protected virtual void OnDateTimeChanged(DateTimePickerBase sender) => DateTimeChanged?.Invoke(sender);

        /// <summary>
        /// Initializes this <see cref="DateTimePicker"/> object's events.
        /// </summary>
        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();
            Libui.DateTimePickerOnChanged(Handle, (d, data) => OnDateTimeChanged(this), IntPtr.Zero);
        }
    }
}