/***********************************************************************************************************************
 * FileName:            Program.cs
 * Copyright/License:   https://github.com/tacdevel/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TACDevel;

namespace DisposableExample
{
    /// <summary>
    /// represents an example program using the <see cref="TACDevel.Disposable"/> class as an alternative to
    /// <see cref="System.IDisposable"/>.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Creates a new <see cref="Foo"/> instance that uses an unmanaged resource, and subscribes to it's
        /// inherited <see cref="TACDevel.Disposable.Disposing"/> and <see cref="TACDevel.Disposable.Disposed"/> events.
        /// Then it creates a second <see cref="Foo"/> instance that uses an unmanaged resource and the first instance
        /// of <see cref="Foo"/> as a managed resource, and subscribes to it's inherited
        /// <see cref="TACDevel.Disposable.Disposing"/> and <see cref="TACDevel.Disposable.Disposed"/> events. Lastly,
        /// it disposes both instances of <see cref="Foo"/>.
        /// </summary>
        private static void Main()
        {
            // Create a new instance of the `Foo` class with an unmanaged resource.
            Foo foo1 = new Foo(Marshal.AllocHGlobal(64), null);

            // Attach handlers to both `Disposing` and `Disposed` events of `foo1`.
            foo1.Disposing += (sender, e) => Console.WriteLine(@$"Disposing '{nameof(foo1)}'...");
            foo1.Disposed += (sender, e) => Console.WriteLine(@$"'{nameof(foo1)}' Disposed.");

            // Create a new instance of the `Foo` class with an unmanaged resource and `foo1` as it's managed resource..
            Foo foo2 = new Foo(Marshal.AllocHGlobal(64), foo1);

            // Attach handlers to both `Disposing` and `Disposed` events of `foo2`.
            foo2.Disposing += (sender, e) => Console.WriteLine(@$"Disposing '{nameof(foo2)}'...");
            foo2.Disposed += (sender, e) => Console.WriteLine(@$"'{nameof(foo2)}' Disposed.");

            // Disposes `foo2`, which will automatically dispose of it's resources and `foo1`'s resources.
            foo2.Dispose();

            Console.WriteLine("Press any key to exit...");
            _ = Console.ReadKey();
        }
    }

    /// <summary>
    /// Represents a dummy class that inherits from <see cref="TACDevel.Disposable"/>.
    /// </summary>
    internal class Foo : Disposable
    {
        /// <summary>
        /// Represents the unmanaged resource used by this class.
        /// </summary>
        private IntPtr nativeResource;

        /// <summary>
        /// Represents the managed resource used by this class.
        /// </summary>
        private Disposable managedResource;

        /// <summary>
        /// Initializes a new instance of the <see cref="Foo"/> class.
        /// </summary>
        /// <param name="nativeRes">The native resource that will be used by the new <see cref="Foo"/> class.</param>
        /// <param name="managedRes">The managed resource that will be used by the new <see cref="Foo"/> class.</param>
        public Foo(IntPtr nativeRes, Disposable managedRes)
        {
            nativeResource = nativeRes;
            managedResource = managedRes;
        }

        /// <summary>
        /// Releases the managed resources (<see cref="managedResource"/>) used in this class.
        /// </summary>
        protected override void ReleaseManagedResources()
        {
            // Dispose of our managed resource if it is not null.
            managedResource?.Dispose();

            // Call the base method.
            base.ReleaseManagedResources();
        }

        /// <summary>
        /// Releases the unmanaged resources (<see cref="nativeResource"/>) used in this class.
        /// </summary>
        protected override void ReleaseUnmanagedResources()
        {
            // Dispose of our unmanaged resource if it is not null.
            if (nativeResource != null)
                Marshal.FreeHGlobal(nativeResource);

            // Call the base method.
            base.ReleaseManagedResources();
        }
    }
}