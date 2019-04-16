/***************************************************************************************************
 * FileName:             FormContainer.cs
 * Copyright:             Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a container control that lists controls vertically with a corresponding label.
    /// </summary>
    public class FormContainer : MultiChildContainer<Control, FormContainer.ControlCollection>
    {
        private bool isPadded = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormContainer"/> class.
        /// </summary>
        public FormContainer() : base(new SafeControlHandle(Libui.Call<Libui.uiNewForm>()())) { }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="FormContainer"/> has interior padding or not.
        /// </summary>
        public bool IsPadded
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                isPadded = Libui.Call<Libui.uiFormPadded>()(Handle);
                return isPadded;
            }
            set
            {
                if (isPadded == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiFormSetPadded>()(Handle, value);
                isPadded = value;
            }
        }

        /// <summary>
        /// Represents a collection of child <see cref="Control"/> objects inside of a <see cref="FormContainer"/>.
        /// </summary>
        public sealed class ControlCollection : ControlCollectionBase<Control>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ControlCollection"/> class with the specified parent.
            /// </summary>
            /// <param name="owner">The parent <see cref="FormContainer"/> of this <see cref="ControlCollection"/>.</param>
            public ControlCollection(FormContainer owner) : base(owner) { }

            /// <summary>
            /// <see cref="ControlCollection"/> does not support this method, and will throw a <see cref="NotSupportedException"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            public override void Add(Control child) => throw new NotSupportedException();

            /// <summary>
            /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="label">The text beside the child <see cref="Control"/>.</param>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            /// <param name="stretches">Whether or not <paramref name="child"/> stretches the area of the parent <see cref="Control"/></param>
            public void Add(string label, Control child, bool stretches = false)
            {
                if (string.IsNullOrEmpty(label)) throw new ArgumentNullException(nameof(label));
                if (child == null) throw new ArgumentNullException(nameof(child));
                if (child.IsInvalid) throw new InvalidHandleException();
                if (Owner.IsInvalid) throw new InvalidHandleException();
                if (Contains(child)) throw new InvalidOperationException("Cannot add the same control more than once.");
                if (child.TopLevel) throw new ArgumentException("Cannot add a top-level control to a ControlCollectionBase.");

                Libui.Call<Libui.uiFormAppend>()(Owner.Handle, label, child.Handle, stretches);
                base.Add(child);
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
                if (child == null) throw new ArgumentNullException(nameof(child));
                if (child.IsInvalid) throw new InvalidHandleException();
                if (!Contains(child)) return false;

                if (Owner.IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiFormDelete>()(Owner.Handle, child.Index);

                return base.Remove(child) ? true : false;
            }
        }
    }
}