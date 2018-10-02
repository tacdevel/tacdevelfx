/****************************************************************************
 * FileName:   GroupContainer.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181002
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    /// <summary>
    /// Represents a control that creates a container that has a border and a title for user-interface (UI) content.
    /// </summary>
    public class GroupContainer : SingleChildContainer<Control>
    {
        private Control child;
        private string title;
        private bool isMargined = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupContainer"/> class with the specified title.
        /// </summary>
        /// <param name="title">The title of this <see cref="GroupContainer"/>.</param>
        public GroupContainer(string title) : base(new SafeControlHandle(Libui.NewGroup(title))) => this.title = title;

        /// <summary>
        /// Gets or sets the title for this <see cref="GroupContainer"/> control.
        /// </summary>
        public string Title
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                title = Libui.GroupTitle(Handle);
                return title;
            }
            set
            {
                if (title == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.GroupSetTitle(Handle, value);
                title = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not this <see cref="TabPage"/> has outer margins.
        /// </summary>
        public bool IsMargined
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                isMargined = Libui.GroupMargined(Handle);
                return isMargined;
            }
            set
            {
                if (isMargined == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.GroupSetMargined(Handle, value);
                isMargined = value;
            }
        }

        /// <summary>
        /// Sets this <see cref="GroupContainer"/> object's child <see cref="Control"/>.
        /// </summary>
        public override Control Child
        {
            set
            {
                if (child != value)
                {
                    if (IsInvalid) throw new InvalidHandleException();
                    Libui.GroupSetChild(Handle, value.Handle);
                    child = value;
                }
            }
        }
    }
}