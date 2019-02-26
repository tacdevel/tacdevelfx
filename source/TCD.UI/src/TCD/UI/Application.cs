/***************************************************************************************************
 * FileName:             Application.cs
 * Date:                 20180921
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TCD.InteropServices;
using TCD.Native;

namespace TCD.UI
{
    /// <summary>
    /// Encapsulates an application with a graphical user interface (GUI).
    /// </summary>
    public sealed class Application : NativeComponent
    {
        private static bool initialized = false;
        private static Libui.uiInitOptions options = new Libui.uiInitOptions() { Size = UIntPtr.Zero };
        private static readonly object sync = new object();
        private static readonly Queue<Action> queue = new Queue<Action>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        public Application() : base(new IntPtr(1))
        {
            lock (sync)
            {
                if (initialized)
                    throw new ApplicationInitializationException("You cannot have more than one instance of Application at once.");
                Current = this;
                InitializeComponent();
                InitializeEvents();
                initialized = true;
            }
        }

        /// <summary>
        /// Occurs just before an application shuts down.
        /// </summary>
        public event EventHandler<CloseEventArgs> Closing;

        /// <summary>
        /// Gets the current instance of the <see cref="Application"/>.
        /// </summary>
        public static Application Current { get; private set; }

        internal static Window MainWindow { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Application"/> is invalid.
        /// </summary>
        public override bool IsInvalid => !initialized;

        /// <summary>
        /// Starts an application with a user-interface and opens the specified window.
        /// </summary>
        /// <param name="window">The specified window to open.</param>
        /// <returns>0 if successful, else returns -1.</returns>
        public int Run(Window window)
        {
            if (window.IsInvalid) throw new InvalidHandleException();
            MainWindow = window;
            return Run(() => window.Show());
        }

        private int Run(Action action)
        {
            try
            {
                QueueMain(action);
                Libui.Call<Libui.uiMain>()();
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// Queues the specified action to run when possible on the UI thread.
        /// </summary>
        /// <param name="action">The <see cref="Action"/> to run.</param>
        public static void QueueMain(Action action)
        {
            queue.Enqueue(action);
            Libui.Call<Libui.uiQueueMain>()(data =>
            {
                lock (sync)
                {
                    queue.Dequeue().Invoke();
                }
            }, new IntPtr(queue.Count));
        }

        /// <summary>
        /// Shuts down this <see cref="Application"/>.
        /// </summary>
        public void Shutdown() => Libui.Call<Libui.uiQuit>()();

        /// <summary>
        /// Initializes this <see cref="Application"/>.
        /// </summary>
        protected override void InitializeComponent()
        {
            string error = Libui.Call<Libui.uiInit>()(ref options);
            if (!string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine(error);
                Libui.Call<Libui.uiFreeInitError>()(error);
                throw new ApplicationInitializationException(error);
            }
        }

        /// <summary>
        /// Initializes this <see cref="Application"/> object's events.
        /// </summary>
        protected override void InitializeEvents() => Libui.Call<Libui.uiOnShouldQuit>()(data =>
        {
            CloseEventArgs e = new CloseEventArgs();
            Closing.Invoke(this, e);
            return e.Close;
        }, IntPtr.Zero);

        /// <summary>
        /// Performs tasks associated with releasing unmanaged resources.
        /// </summary>
        protected override void ReleaseUnmanagedResources()
        {
            if (!IsInvalid)
                Libui.Call<Libui.uiUnInit>()();
            base.ReleaseUnmanagedResources();
        }
    }

    /// <summary>
    /// The exception that is thrown when an <see cref="Application"/> class fails to initialize.
    /// </summary>
    [Serializable]
    public sealed class ApplicationInitializationException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class.
        /// </summary>
        public ApplicationInitializationException() : this("") { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class with the specified error message.
        /// </summary>
        /// <param name="message">The error message that specifies the reason for the exception.</param>
        public ApplicationInitializationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class with the specified error message
        /// and <see langword="abstract"/> reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that specifies the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public ApplicationInitializationException(string message, Exception inner) : base(message, inner) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationInitializationException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        internal ApplicationInitializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}