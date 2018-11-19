/***************************************************************************************************
 * FileName:             DatePicker.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

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
        public DatePicker() : base(new SafeControlHandle(Libui.NewDatePicker())) => InitializeEvents();

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