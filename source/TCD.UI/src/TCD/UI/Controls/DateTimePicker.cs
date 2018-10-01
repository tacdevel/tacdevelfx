/****************************************************************************
 * FileName:   DateTimePicker.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180930
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// Implements the basic functonality required by a date-time picker.
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
        public event Event<DateTimePickerBase> DateTimeChanged;

        /// <summary>
        /// Gets or sets the selected date and time.
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.DateTimePickerTime(Handle, out NativeDateTime time);
                dateTime = NativeDateTime.ToDateTime(time);
                return (DateTime)dateTime;
            }
            set
            {
                if (dateTime == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.DateTimePickerSetTime(Handle, NativeDateTime.FromDateTime(value));
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
            Libui.DateTimePickerOnChanged(Handle, (d, data) => { OnDateTimeChanged(this); }, IntPtr.Zero);
        }
    }

    /// <summary>
    /// Represents a control that allows the user to select and display a date.
    /// </summary>
    public class DatePicker : DateTimePickerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatePicker"/> class.
        /// </summary>
        public DatePicker(int? year = null, int? month = null, int? day = null) : base(new SafeControlHandle(Libui.NewDatePicker()))
        {
            DateTime dt = DateTime.Now;
            if (year != null)
                dt = new DateTime((int)year, dt.Month, dt.Day);
            if (month != null)
                dt = new DateTime(dt.Year, (int)month, dt.Day);
            if (day != null)
                dt = new DateTime(dt.Year, dt.Month, (int)day);
            DateTime = dt;
            InitializeEvents();
        }

        /// <summary>
        /// Gets the year component from <see cref="DateTime"/>.
        /// </summary>
        public int Year => DateTime.Year;

        /// <summary>
        /// Gets the month component from <see cref="DateTime"/>.
        /// </summary>
        public int Month => DateTime.Month;

        /// <summary>
        /// Gets the day component from <see cref="DateTime"/>.
        /// </summary>
        public int Day => DateTime.Day;
    }

    /// <summary>
    /// Represents a control that allows the user to select and display a time.
    /// </summary>
    public class TimePicker : DateTimePickerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimePicker"/> class.
        /// </summary>
        public TimePicker(int? hour = null, int? minute = null, int? second = null) : base(new SafeControlHandle(Libui.NewTimePicker()))
        {
            DateTime dt = DateTime.Now;
            if (hour != null)
                dt = new DateTime(dt.Year, dt.Month, dt.Day, (int)hour, dt.Minute, dt.Second);
            if (minute != null)
                dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, (int)minute, dt.Second);
            if (second != null)
                dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, (int)second);
            DateTime = dt;
            InitializeEvents();
        }

        /// <summary>
        /// Gets the hour component from <see cref="DateTime"/>.
        /// </summary>
        public int Hour => DateTime.Hour;

        /// <summary>
        /// Gets the minute component from <see cref="DateTime"/>.
        /// </summary>
        public int Minute => DateTime.Minute;

        /// <summary>
        /// Gets the second component from <see cref="DateTime"/>.
        /// </summary>
        public int Second => DateTime.Second;
    }

    /// <summary>
    /// Represents a control that allows the user to select and display a date and time.
    /// </summary>
    public class DateTimePicker : DateTimePickerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimePicker"/> class.
        /// </summary>
        public DateTimePicker(int? year = null, int? month = null, int? day = null, int? hour = null, int? minute = null, int? second = null) : base(new SafeControlHandle(Libui.NewDateTimePicker()))
        {
            DateTime dt = DateTime.Now;
            if (year != null)
                dt = new DateTime((int)year, dt.Month, dt.Day);
            if (month != null)
                dt = new DateTime(dt.Year, (int)month, dt.Day);
            if (day != null)
                dt = new DateTime(dt.Year, dt.Month, (int)day);
            if (hour != null)
                dt = new DateTime(dt.Year, dt.Month, dt.Day, (int)hour, dt.Minute, dt.Second);
            if (minute != null)
                dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, (int)minute, dt.Second);
            if (second != null)
                dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, (int)second);
            DateTime = dt;
            InitializeEvents();
        }

        /// <summary>
        /// Gets the year component from <see cref="DateTime"/>.
        /// </summary>
        public int Year => DateTime.Year;

        /// <summary>
        /// Gets the month component from <see cref="DateTime"/>.
        /// </summary>
        public int Month => DateTime.Month;

        /// <summary>
        /// Gets the day component from <see cref="DateTime"/>.
        /// </summary>
        public int Day => DateTime.Day;

        /// <summary>
        /// Gets the hour component from <see cref="DateTime"/>.
        /// </summary>
        public int Hour => DateTime.Hour;

        /// <summary>
        /// Gets the minute component from <see cref="DateTime"/>.
        /// </summary>
        public int Minute => DateTime.Minute;

        /// <summary>
        /// Gets the second component from <see cref="DateTime"/>.
        /// </summary>
        public int Second => DateTime.Second;
    }

    //TODO: IEquatable<UIDateTime>, object overrides.
    [StructLayout(LayoutKind.Sequential)]
    internal class NativeDateTime
    {
#pragma warning disable IDE0032 // Use auto property
#pragma warning disable IDE0044 // Add readonly modifier
        private int sec, min, hour, day, mon, year;
        private readonly int wday, yday; // Must be uninitialized.
        private readonly int isdst = -1; //Must be -1.
#pragma warning restore IDE0032 // Use auto property
#pragma warning restore IDE0044 // Add readonly modifier

        public NativeDateTime(int year, int month, int day, int hour, int minute, int second)
        {
            sec = second;
            min = minute;
            this.hour = hour;
            this.day = day;
            mon = month;
            this.year = year;
        }

        public int Second
        {
            get => sec;
            set => sec = value;
        }

        public int Minute
        {
            get => min;
            set => min = value;
        }

        public int Hour
        {
            get => hour;
            set => hour = value;
        }

        public int Day
        {
            get => day;
            set => day = value;
        }

        public int Month
        {
            get => mon;
            set => mon = value;
        }

        public int Year
        {
            get => year;
            set => year = value;
        }

        public static DateTime ToDateTime(NativeDateTime dt) => new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        public static NativeDateTime FromDateTime(DateTime dt) => new NativeDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
    }
}