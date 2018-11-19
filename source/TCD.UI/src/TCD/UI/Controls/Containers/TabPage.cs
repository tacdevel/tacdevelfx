/***************************************************************************************************
 * FileName:             TabPage.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// Represents a single tab page in a <see cref="TabContainer"/>.
    /// </summary>
    public class TabPage : SingleChildContainer<Control>
    {
        private Control child;
        private bool initialized = false;
        private bool isMargined = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabPage"/> class with the specified name and child <see cref="Control"/>.
        /// </summary>
        /// <param name="name">The name for this <see cref="TabPage"/>.</param>
        /// <param name="child">The child <see cref="Control"/> contained in this <see cref="TabPage"/>.</param>
        public TabPage(string name, Control child) : base(child.Handle, false)
        {
            Name = name;
            Child = child;
        }

        /// <summary>
        /// Gets the name of this <see cref="TabPage"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Sets the child contained in this <see cref="TabPage"/>.
        /// </summary>
        public override Control Child
        {
            set
            {
                if (child != value)
                    child = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not this <see cref="TabPage"/> has outer isMargined.
        /// </summary>
        public bool IsMargined
        {
            get
            {
                if (Parent.Handle != null)
                {
                    isMargined = Libui.TabMargined(Parent.Handle, Index);
                    initialized = true;
                }
                return isMargined;
            }
            set
            {
                if (isMargined != value)
                {
                    if (Parent.Handle != null)
                        Libui.TabSetMargined(Parent.Handle, Index, value);
                    isMargined = value;
                }
            }
        }

        /// <summary>
        /// Performs pre-rendering operations.
        /// </summary>
        protected internal override void DelayRender()
        {
            if (!initialized && isMargined)
                Libui.TabSetMargined(Parent.Handle, Index, isMargined);
        }
    }
}