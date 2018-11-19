/***************************************************************************************************
 * FileName:             StackContainerBase.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// The base class for a <see cref="Control"/> that arranges child elements in one orientation.
    /// </summary>
    public abstract class StackContainerBase : MultiChildContainer<Control, StackContainerBase.ControlCollection>
    {
        private bool isPadded;

        internal StackContainerBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) { }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="StackContainerBase"/> has interior isPadded or not.
        /// </summary>
        public bool IsPadded
        {
            get
            {
                isPadded = Libui.BoxPadded(Handle);
                return isPadded;
            }
            set
            {
                if (isPadded != value)
                {
                    Libui.BoxSetPadded(Handle, value);
                    isPadded = value;
                }
            }
        }

        /// <summary>
        /// Represents a collection of child <see cref="Control"/> objects inside of a <see cref="StackContainerBase"/>.
        /// </summary>
        public class ControlCollection : ControlCollectionBase<Control>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ControlCollection"/> class with the specified parent.
            /// </summary>
            /// <param name="owner">The parent <see cref="StackContainerBase"/> of this <see cref="ControlCollection"/>.</param>
            public ControlCollection(StackContainerBase owner) : base(owner) { }

            /// <summary>
            /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            public override void Add(Control child) => Add(child, false);

            /// <summary>
            /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            /// <param name="stretches">Whether or not <paramref name="child"/> stretches the area of the parent <see cref="Control"/></param>
            public void Add(Control child, bool stretches)
            {
                base.Add(child);
                Libui.BoxAppend(Owner.Handle, child.Handle, stretches);
            }

            /// <summary>
            /// <see cref="ControlCollection"/> does not support this method, and will throw a <see cref="NotSupportedException"/>.
            /// </summary>
            /// <param name="index">The zero-based index at which child should be inserted.</param>
            /// <param name="child">The <see cref="Control"/> to insert into the <see cref="ControlCollection"/>.</param>
            public override void Insert(int index, Control child) => throw new NotSupportedException();

            /// <summary>
            /// Removes the first occurrence of a specific <see cref="Control"/> from the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to remove from the <see cref="ControlCollection"/>.</param>
            /// <returns>true if child is successfully removed; otherwise, false. This method also returns false if child was not found in the <see cref="ControlCollection"/>.</returns>
            public new bool Remove(Control child)
            {
                if (base.Remove(child))
                {
                    Libui.BoxDelete(Owner.Handle, child.Index);
                    return true;
                }
                return false;
            }
        }
    }
}