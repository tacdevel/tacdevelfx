using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// Represents a control that contains multiple <see cref="TabPage"/> objects that share the same space on the screen.
    /// </summary>
    public class TabContainer : MultiChildContainer<TabPage, TabContainer.TabPageCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabContainer"/> class.
        /// </summary>
        public TabContainer() : base(new SafeControlHandle(Libui.NewTab()), true) { }

        /// <summary>
        /// Represents a collection of child <see cref="TabPage"/> objects inside of a <see cref="TabContainer"/>.
        /// </summary>
        public class TabPageCollection : ControlCollectionBase<TabPage>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ControlList"/> class with the specified parent.
            /// </summary>
            /// <param name="owner">The parent <see cref="StackContainer"/> of this <see cref="ControlList"/>.</param>
            public TabPageCollection(TabContainer owner) : base(owner) { }

            /// <summary>
            /// Adds a <see cref="TabPage"/> to the end of the <see cref="ControlList"/>.
            /// </summary>
            /// <param name="child">The <see cref="TabPage"/> to be added to the end of the <see cref="ControlList"/>.</param>
            public override void Add(TabPage child)
            {
                base.Add(child);
                Libui.TabAppend(Owner.Handle, child.Name, child.Handle);
                child.DelayRender();
            }

            /// <summary>
            /// Adds a <see cref="TabPage"/> to the <see cref="ControlList"/> at the specified index.
            /// </summary>
            /// <param name="index">The zero-based index at which child should be inserted.</param>
            /// <param name="child">The <see cref="TabPage"/> to insert into the <see cref="ControlList"/>.</param>
            public override void Insert(int index, TabPage child)
            {
                base.Insert(index, child);
                Libui.TabInsertAt(Owner.Handle, child.Name, index, child.Handle);
                child.DelayRender();
            }

            /// <summary>
            /// Removes the first occurrence of a specific <see cref="TabPage"/> from the <see cref="ControlList"/>.
            /// </summary>
            /// <param name="child">The <see cref="TabPage"/> to remove from the <see cref="ControlList"/>.</param>
            /// <returns>true if child is successfully removed; otherwise, false. This method also returns false if child was not found in the <see cref="ControlList"/>.</returns>
            public new bool Remove(TabPage child)
            {
                if (base.Remove(child))
                {
                    Libui.TabDelete(Owner.Handle, child.Index);
                    return true;
                }
                return false;
            }
        }
    }
}