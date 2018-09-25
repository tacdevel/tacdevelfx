/****************************************************************************
 * FileName:   Window.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20180921
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using TCD.Drawing;
using TCD.InteropServices;
using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI
{
    /// <summary>
    /// Represents a native window that makes up an application's user interface.
    /// </summary>
    public partial class Window : Control
    {
        private Control child;
        private bool isMargined, fullscreen, borderless = false;
        private Size size;
        private string title;

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class, with the options of specifying
        /// the window's width, height, title, and whether or not it has a <see cref="Menu"/>.
        /// </summary>
        /// <param name="title">The title at the top of the window.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="hasMenu">Whether or not the window will have a menu.</param>
        public Window(string title = "", int width = 600, int height = 400, bool hasMenu = false, bool isMargined = false, bool fullscreen = false, bool borderless = false) : base(new SafeControlHandle(Libui.NewWindow(title, width, height, hasMenu)))
        {
            this.title = title;
            Console.Title = title;
            size = new Size(width, height);
            HasMenu = hasMenu;
            IsMargined = isMargined;
            Fullscreen = fullscreen;
            Borderless = borderless;
            InitializeEvents();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class, with the options of specifying
        /// the window's size, title, and whether or not it has a <see cref="Menu"/>.
        /// </summary>
        /// <param name="title">The title at the top of the window.</param>
        /// <param name="size">The size of the window.</param>
        /// <param name="hasMenu">Whether or not the window will have a menu.</param>
        public Window(string title, Size size, bool hasMenu = false, bool isMargined = false, bool fullscreen = false, bool borderless = false) : this(title, size.Width, size.Height, hasMenu, isMargined, fullscreen, borderless) { }

        /// <summary>
        /// Occurs when the <see cref="Window"/> is closing.
        /// </summary>
        public event Event<Window, CloseEventData> Closing;

        /// <summary>
        /// Occurs when the <see cref="Size"/> property value changes.
        /// </summary>
        public event Event<Window> SizeChanged;

        /// <summary>
        /// Gets whether or not this <see cref="Window"/> has a menu.
        /// </summary>
        public bool HasMenu { get; }

        /// <summary>
        /// Gets or sets the title of this <see cref="Window"/>.
        /// </summary
        public string Title
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                title = Libui.WindowTitle(Handle);
                return title;
            }
            set
            {
                if (title == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.WindowSetTitle(Handle, value);
                title = value;
            }
        }

        /// <summary>
        /// Gets or sets the content size of this <see cref="Window"/>.
        /// </summary>
        public Size Size
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                Libui.WindowContentSize(Handle, out int w, out int h);
                size = new Size(w, h);
                return size;
            }
            set
            {
                if (size == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.WindowSetContentSize(Handle, value.Width, value.Height);
                size = value;
            }
        }

        /// <summary>
        /// Gets the content width of this <see cref="Window"/>.
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// Gets the content height of this <see cref="Window"/>.
        /// </summary>
        public int Height => Size.Height;

        /// <summary>
        /// Gets or sets whether or not this <see cref="Window"/> fills the entire screen.
        /// </summary>
        public bool Fullscreen
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                fullscreen = Libui.WindowFullscreen(Handle);
                return fullscreen;
            }
            set
            {
                if (fullscreen == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.WindowSetFullscreen(Handle, value);
                fullscreen = value;
            }
        }

        /// <summary>
        /// Gets or sets whether or not this <see cref="Window"/> has borders.
        /// </summary>
        public bool Borderless
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                borderless = Libui.WindowBorderless(Handle);
                return borderless;
            }
            set
            {
                if (borderless == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.WindowSetBorderless(Handle, value);
                borderless = value;
            }
        }

        /// <summary>
        /// Sets the child <see cref="Control"/> of this <see cref="Window"/>.
        /// </summary>
        public Control Child
        {
            set
            {
                if (Handle != null)
                {
                    if (value == null) throw new ArgumentNullException(nameof(value));
                    if (IsInvalid) throw new InvalidHandleException();
                    Libui.WindowSetChild(Handle, value.Handle);
                    child = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether this <see cref="Window"/> has margins between its child control and its border.
        /// </summary>
        public bool IsMargined
        {
            get
            {
                if (IsInvalid) throw new InvalidHandleException();
                isMargined = Libui.WindowMargined(Handle);
                return isMargined;
            }
            set
            {
                if (isMargined == value) return;
                if (IsInvalid) throw new InvalidHandleException();
                Libui.WindowSetMargined(Handle, value);
                isMargined = value;
            }
        }

        /// <summary>
        /// Closes the <see cref="Window"/>.
        /// </summary>
        public void Close()
        {
            Hide();
            Dispose();
        }

        /// <summary>
        /// Raises the <see cref="Closing"/> event.
        /// </summary>
        /// <param name="cancel">A <see cref="bool"/> containing the event data.</param>
        protected virtual void OnClosing(Window sender, CloseEventData e) => Closing?.Invoke(sender, e);

        /// <summary>
        /// Raises the <see cref="SizeChanged"/> event.
        /// </summary>
        protected virtual void OnSizeChanged(Window sender) => SizeChanged?.Invoke(sender);

        protected sealed override void InitializeEvents()
        {
            if (IsInvalid) throw new InvalidHandleException();

            Libui.WindowOnClosing(Handle, (window, data) =>
            {
                CloseEventData e = new CloseEventData();
                OnClosing(this, e);
                if (e.Close)
                {
                    if (this != Application.MainWindow)
                        Close();
                    else
                        Application.Current.Shutdown();
                }
                return e.Close;
            }, IntPtr.Zero);

            Libui.WindowOnContentSizeChanged(Handle, (window, data) => { OnSizeChanged(this); }, IntPtr.Zero);
        }
    }
}