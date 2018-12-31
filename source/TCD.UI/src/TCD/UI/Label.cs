/***************************************************************************************************
 * FileName:             Label.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a standard label, which contains and shows text.
    /// </summary>
    public class Label : Control
    {
        private string text;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class with the specified text.
        /// </summary>
        /// <param name="text">The specified text for this <see cref="Label"/>.</param>
        public Label(string text) : base(new SafeControlHandle(Libui.Call<Libui.uiNewLabel>()(text))) => this.text = text;

        /// <summary>
        /// Gets or sets this <see cref="Label"/>'s text.
        /// </summary>
        public string Text
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                text = Libui.Call<Libui.uiLabelText>()(Handle);
                return text;
            }
            set
            {
                if (text == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiLabelSetText>()(Handle, value);
                text = value;
            }
        }
    }
}