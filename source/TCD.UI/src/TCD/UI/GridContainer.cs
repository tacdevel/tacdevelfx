/***************************************************************************************************
 * FileName:             GridContainer.cs
 * Date:                 20181002
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Drawing;
using TCD.InteropServices;
using TCD.Native;
using TCD.UI.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a container control that allows for specific sizes and positions for each child control.
    /// </summary>
    public class GridContainer : MultiChildContainer<Control, GridContainer.ControlCollection>
    {
        private bool isPadded = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridContainer"/> class.
        /// </summary>
        public GridContainer() : base(new SafeControlHandle(Libui.Call<Libui.uiNewGrid>()())) { }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridContainer"/> has interior padding or not.
        /// </summary>
        public bool IsPadded
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                isPadded = Libui.Call<Libui.uiGridPadded>()(Handle);
                return isPadded;
            }
            set
            {
                if (isPadded == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.Call<Libui.uiGridSetPadded>()(Handle, value);
                isPadded = value;
            }
        }

        /// <summary>
        /// Represents a collection of child <see cref="Control"/> objects inside of a <see cref="GridContainer"/>.
        /// </summary>
        public class ControlCollection : ControlCollectionBase<Control>
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ControlCollection"/> class with the specified parent.
            /// </summary>
            /// <param name="owner">The parent <see cref="GridContainer"/> of this <see cref="ControlCollection"/>.</param>
            public ControlCollection(GridContainer owner) : base(owner) { }

            /// <summary>
            /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            public override void Add(Control child) => Add(child, 0, 0, 0, 0, 0, 0, Alignment.Fill);

            /// <summary>
            /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            /// <param name="rect">A <see cref="Rectangle"/> representing the location and size of <paramref name="child"/>.</param>
            /// <param name="expand">A <see cref="Size"/> representing the h and v-expand.</param>
            /// <param name="alignment">The alignment of <paramref name="child"/>.</param>
            public void Add(Control child, Rectangle rect, Size expand, Alignment alignment) => Add(child, rect.Location, rect.Size, expand, alignment);

            /// <summary>
            /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            /// <param name="location">The top-left location of <paramref name="child"/>.</param>
            /// <param name="size">The size of <paramref name="child"/>.</param>
            /// <param name="expand">A <see cref="Size"/> representing the h and v-expand.</param>
            /// <param name="alignment">The alignment of <paramref name="child"/>.</param>
            public void Add(Control child, Point location, Size size, Size expand, Alignment alignment) => Add(child, location.X, location.Y, size.Width, size.Height, expand.Width, expand.Height, alignment);

            /// <summary>
            /// Adds a <see cref="Control"/> to the end of the <see cref="ControlCollection"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to be added to the end of the <see cref="ControlCollection"/>.</param>
            /// <param name="x">The x-coordinate of the child's location.</param>
            /// <param name="y">The y-coordinate of the child's location.</param>
            /// <param name="width">The width of the child.</param>
            /// <param name="height">The height of the child.</param>
            /// <param name="hexpand">The horizontal expand of <paramref name="child"/>.</param>
            /// <param name="vexpand">The vertical expand of <paramref name="child"/>.</param>
            /// <param name="alignment">The alignment of <paramref name="child"/>.</param>
            public void Add(Control child, int x, int y, int width, int height, int hexpand, int vexpand, Alignment alignment)
            {
                if (child == null) throw new ArgumentNullException(nameof(child));
                if (child.IsInvalid) throw new InvalidHandleException();
                if (x < 0) throw new ArgumentOutOfRangeException(nameof(x));
                if (y < 0) throw new ArgumentOutOfRangeException(nameof(y));
                if (width < 0) throw new ArgumentOutOfRangeException(nameof(width));
                if (height < 0) throw new ArgumentOutOfRangeException(nameof(height));
                if (Owner.IsInvalid) throw new InvalidHandleException();
                if (Contains(child)) throw new InvalidOperationException("Cannot add the same control more than once.");
                if (child.TopLevel) throw new ArgumentException("Cannot add a top-level control to a ControlCollectionBase.");

                ToLibuiAligns(alignment, out Libui.uiAlign halign, out Libui.uiAlign valign);
                Libui.Call<Libui.uiGridAppend>()(Owner.Handle, child.Handle, x, y, width, height, hexpand, halign, vexpand, valign);
                base.Add(child);
            }

            /// <summary>
            /// <see cref="ControlCollection"/> does not support this method, and will throw a <see cref="NotSupportedException"/>.
            /// Use <see cref="Insert(Control, Control, RelativeAlignment, int, int, int, int, Alignment)"/> or <see cref="Insert(Control, Control, RelativeAlignment, Size, Size, Alignment)"/> instead.
            /// </summary>
            /// <param name="index">The zero-based index at which child should be inserted.</param>
            /// <param name="child">The <see cref="Control"/> to insert into the <see cref="ControlCollection"/>.</param>
            public override void Insert(int index, Control child) => throw new NotSupportedException();

            /// <summary>
            /// Adds a <see cref="Control"/> to the <see cref="ControlCollection"/> at the specified index.
            /// </summary>
            /// <param name="existing">The existing control at which child should be inserted.</param>
            /// <param name="child">The <see cref="Control"/> to insert into the <see cref="ControlCollection"/>.</param>
            /// <param name="relativeAlignment">The relative placement of the child control to the existing one.</param>
            /// <param name="size">The size of <paramref name="child"/>.</param>
            /// <param name="expand">A <see cref="Size"/> representing the h and v-expand.</param>
            /// <param name="alignment">The alignment of <paramref name="child"/> in the container.</param>
            public void Insert(Control existing, Control child, RelativeAlignment relativeAlignment, Size size, Size expand, Alignment alignment) => Insert(existing, child, relativeAlignment, size.Width, size.Height, expand.Width, expand.Height, alignment);

            /// <summary>
            /// Adds a <see cref="Control"/> to the <see cref="ControlCollection"/> at the specified index.
            /// </summary>
            /// <param name="existing">The existing control at which child should be inserted.</param>
            /// <param name="child">The <see cref="Control"/> to insert into the <see cref="ControlCollection"/>.</param>
            /// <param name="relativeAlignment">The relative placement of the child control to the existing one.</param>
            /// <param name="width">The width of the child.</param>
            /// <param name="height">The height of the child.</param>
            /// <param name="hexpand">The horizontal expand of <paramref name="child"/>.</param>
            /// <param name="vexpand">The vertical expand of <paramref name="child"/>.</param>
            /// <param name="alignment">The alignment of <paramref name="child"/>.</param>
            public void Insert(Control existing, Control child, RelativeAlignment relativeAlignment, int width, int height, int hexpand, int vexpand, Alignment alignment)
            {
                if (width < 0) throw new ArgumentOutOfRangeException(nameof(width));
                if (height < 0) throw new ArgumentOutOfRangeException(nameof(height));
                if (existing == null) throw new ArgumentNullException(nameof(existing));
                if (existing.IsInvalid) throw new InvalidHandleException();
                if (child == null) throw new ArgumentNullException(nameof(child));
                if (child.IsInvalid) throw new InvalidHandleException();
                if (child.TopLevel) throw new ArgumentException("Cannot add a top-level control to a ControlCollectionBase{TContainer, TCollection, T}.");
                if (Contains(child)) throw new InvalidOperationException("Cannot add the same control more than once.");

                ToLibuiAligns(alignment, out Libui.uiAlign halign, out Libui.uiAlign valign);
                Libui.Call<Libui.uiGridInsertAt>()(Owner.Handle, child.Handle, existing.Handle, (Libui.uiAt)relativeAlignment, width, height, hexpand, halign, vexpand, valign);
                base.Insert(existing.Index, child);
            }

            /// <summary>
            /// <see cref="ControlCollection"/> does not support this method, and will throw a <see cref="NotSupportedException"/>.
            /// </summary>
            /// <param name="child">The <see cref="Control"/> to remove from the <see cref="ControlCollection"/>.</param>
            /// <returns>true if child is successfully removed; otherwise, false. This method also returns false if child was not found in the <see cref="ControlCollection"/>.</returns>
            public override bool Remove(Control child) => throw new NotSupportedException();

            private static void ToLibuiAligns(Alignment a, out Libui.uiAlign hAlign, out Libui.uiAlign vAlign)
            {
                switch (a)
                {
                    case Alignment.Fill:
                        vAlign = Libui.uiAlign.uiAlignFill;
                        hAlign = Libui.uiAlign.uiAlignFill;
                        break;
                    case Alignment.Center:
                        vAlign = Libui.uiAlign.uiAlignCenter;
                        hAlign = Libui.uiAlign.uiAlignCenter;
                        break;
                    case Alignment.Top:
                        vAlign = Libui.uiAlign.uiAlignStart;
                        hAlign = Libui.uiAlign.uiAlignFill;
                        break;
                    case Alignment.TopLeft:
                        vAlign = Libui.uiAlign.uiAlignStart;
                        hAlign = Libui.uiAlign.uiAlignStart;
                        break;
                    case Alignment.TopCenter:
                        vAlign = Libui.uiAlign.uiAlignStart;
                        hAlign = Libui.uiAlign.uiAlignCenter;
                        break;
                    case Alignment.TopRight:
                        vAlign = Libui.uiAlign.uiAlignStart;
                        hAlign = Libui.uiAlign.uiAlignEnd;
                        break;
                    case Alignment.Left:
                        vAlign = Libui.uiAlign.uiAlignFill;
                        hAlign = Libui.uiAlign.uiAlignStart;
                        break;
                    case Alignment.LeftCenter:
                        vAlign = Libui.uiAlign.uiAlignCenter;
                        hAlign = Libui.uiAlign.uiAlignStart;
                        break;
                    case Alignment.Right:
                        vAlign = Libui.uiAlign.uiAlignFill;
                        hAlign = Libui.uiAlign.uiAlignEnd;
                        break;
                    case Alignment.RightCenter:
                        vAlign = Libui.uiAlign.uiAlignCenter;
                        hAlign = Libui.uiAlign.uiAlignEnd;
                        break;
                    case Alignment.Bottom:
                        vAlign = Libui.uiAlign.uiAlignEnd;
                        hAlign = Libui.uiAlign.uiAlignFill;
                        break;
                    case Alignment.BottomLeft:
                        vAlign = Libui.uiAlign.uiAlignEnd;
                        hAlign = Libui.uiAlign.uiAlignStart;
                        break;
                    case Alignment.BottomCenter:
                        vAlign = Libui.uiAlign.uiAlignEnd;
                        hAlign = Libui.uiAlign.uiAlignCenter;
                        break;
                    case Alignment.BottomRight:
                        vAlign = Libui.uiAlign.uiAlignEnd;
                        hAlign = Libui.uiAlign.uiAlignEnd;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(a));
                }
            }
        }
    }
}