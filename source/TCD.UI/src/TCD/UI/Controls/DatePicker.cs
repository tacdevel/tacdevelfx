/****************************************************************************
 * FileName:   DatePicker.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
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
}