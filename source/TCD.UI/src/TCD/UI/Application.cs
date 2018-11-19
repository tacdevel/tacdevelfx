/***************************************************************************************************
 * FileName:             Application.cs
 * Date:                 20180921
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using TCD.InteropServices;
using TCD.Native;

namespace TCD.UI
{
    /// <summary>
    /// Encapulates an application with a graphical user interface (GUI).
    /// </summary>
    public sealed class Application : NativeComponent
    {
        private static object sync = new object();
        private static bool initialized = false;
        private static StartupOptions options = new StartupOptions();
        private static readonly Queue<Action> queue = new Queue<Action>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        public Application() : base(new IntPtr(1))
        {
            lock (sync)
            {
                if (initialized)
                    throw new InvalidOperationException("You cannot have more than one instance of Application at once.");
                Current = this;
                InitializeComponent();
                InitializeEvents();
                initialized = true;
            }
        }

        /// <summary>
        /// Occurs just before an application shuts down.
        /// </summary>
        public event NativeEventHandler<Application, CloseEventArgs> Closing;

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
            return Run(() => { window.Show(); });
        }

        private int Run(Action action)
        {
            try
            {
                QueueMain(action);
                Libui.Main();
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
            Libui.QueueMain(data =>
            {
                lock (sync)
                {
                    Action a = queue.Dequeue();
                    a.Invoke();
                }
            }, new IntPtr(queue.Count));
        }

        private void Steps() => Libui.MainSteps();

        private bool Step(bool wait) => Libui.MainStep(wait);

        /// <summary>
        /// Shuts down this <see cref="Application"/>.
        /// </summary>
        public void Shutdown() => Libui.Quit();

        /// <summary>
        /// Initializes this <see cref="Application"/>.
        /// </summary>
        protected override void InitializeComponent()
        {
            string error = Libui.Init(ref options);

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine(error);
                Libui.FreeInitError(error);
                throw new ApplicationInitializationException(error);
            }
        }

        /// <summary>
        /// Initializes this <see cref="Application"/> object's events.
        /// </summary>
        protected override void InitializeEvents() => Libui.OnShouldQuit(data =>
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
                Libui.UnInit();
            base.ReleaseUnmanagedResources();
        }
    }
}