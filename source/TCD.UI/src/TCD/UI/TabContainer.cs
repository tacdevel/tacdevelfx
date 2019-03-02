/***************************************************************************************************
 * FileName:             TabContainer.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a control that contains multiple <see cref="TabPage"/> objects that share the same space on the screen.
    /// </summary>
    public class TabContainer : MultiChildContainer<TabPage, TabContainer.TabPageCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabContainer"/> class.
        /// </summary>
        public TabContainer() : base(new SafeControlHandle(Libui.Call<Libui.uiNewTab>()()), true) { }

        /// <summary>
        /// Represents a collection of child <see cref="TabPage"/> objects inside of a <see cref="TabContainer"/>.
        /// </summary>
        public class TabPageCollection : ControlCollectionBase<TabPage>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TabPageCollection"/> class with the specified parent.
            /// </summary>
            /// <param name="owner">The parent <see cref="TabContainer"/> of this <see cref="TabPageCollection"/>.</param>
            public TabPageCollection(TabContainer owner) : base(owner) { }

            /// <summary>
            /// Adds a <see cref="TabPage"/> to the end of the <see cref="TabPageCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="TabPage"/> to be added to the end of the <see cref="TabPageCollection"/>.</param>
            public override void Add(TabPage child)
            {
                base.Add(child);
                Libui.Call<Libui.uiTabAppend>()(Owner.Handle, child.Name, child.Handle);
                child.DelayRender();
            }

            /// <summary>
            /// Adds a <see cref="TabPage"/> to the <see cref="TabPageCollection"/> at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which child should be inserted.</param>
            /// <param name="child">The <see cref="TabPage"/> to insert into the <see cref="TabPageCollection"/>.</param>
            public override void Insert(int index, TabPage child)
            {
                base.Insert(index, child);
                Libui.Call<Libui.uiTabInsertAt>()(Owner.Handle, child.Name, index, child.Handle);
                child.DelayRender();
            }

            /// <summary>
            /// Removes the first occurrence of a specific <see cref="TabPage"/> from the <see cref="TabPageCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="TabPage"/> to remove from the <see cref="TabPageCollection"/>.</param>
            /// <returns>true if child is successfully removed; otherwise, false. This method also returns false if child was not found in the <see cref="TabPageCollection"/>.</returns>
            public new bool Remove(TabPage child)
            {
                if (base.Remove(child))
                {
                    Libui.Call<Libui.uiTabDelete>()(Owner.Handle, child.Index);
                    return true;
                }
                return false;
            }
        }
    }

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
                    isMargined = Libui.Call<Libui.uiTabMargined>()(Parent.Handle, Index);
                    initialized = true;
                }
                return isMargined;
            }
            set
            {
                if (isMargined != value)
                {
                    if (Parent?.Handle != null)
                        Libui.Call<Libui.uiTabSetMargined>()(Parent.Handle, Index, value);
                    isMargined = value;
                }
            }
        }

        /// <summary>
        /// Performs prerendering operations.
        /// </summary>
        protected internal override void DelayRender()
        {
            if (!initialized && isMargined)
                Libui.Call<Libui.uiTabSetMargined>()(Parent.Handle, Index, isMargined);
        }
    }
}