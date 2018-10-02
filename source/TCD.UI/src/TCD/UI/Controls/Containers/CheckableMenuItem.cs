/****************************************************************************
 * FileName:   CheckableMenuItem.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181002
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// Represents a checkable menu child in a <see cref="Menu"/>.
    /// </summary>
    public sealed class CheckableMenuItem : MenuItemBase
    {
        private bool @checked;

        /// <summary>
        /// Initializes a new instance of a <see cref="CheckableMenuItem"/> class from the specified handle with the specified name.
        /// </summary>
        /// <param name="handle">The specified handle.</param>
        /// <param name="name">The menu child's name.</param>
        internal CheckableMenuItem(SafeControlHandle handle, string name) : base(handle) => Name = name;

        /// <summary>
        /// Gets or sets the state of this <see cref="CheckableMenuItem"/>.
        /// </summary>
        public bool Checked
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                @checked = Libui.MenuItemChecked(Handle);
                return @checked;
            }
            set
            {
                if (@checked == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.MenuItemSetChecked(Handle, value);
                @checked = value;
            }
        }

        /// <summary>
        /// Gets this menu child's name.
        /// </summary>
        public string Name { get; }
    }
}