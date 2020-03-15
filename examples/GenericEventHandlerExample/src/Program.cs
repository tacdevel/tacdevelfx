/***********************************************************************************************************************
 * FileName:            Program.cs
 * Copyright/License:   https://github.com/tom-corwin/tacdevlibs/blob/master/LICENSE.md
***********************************************************************************************************************/

using System;
using TACDevel;

namespace GenericEventHandlerExample
{
    /// <summary>
    /// Represents an example program, using the <see cref="TACDevel.GenericEventHandler{TSender, TEventArgs}"/>
    /// as an alternative to <see cref="System.EventHandler"/> and <see cref="System.EventHandler{TEventArgs}"/>.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Creates a new <see cref="Foo"/> class, subscribes to it's <see cref="Foo.BarChanged"/> event, and sets a new
        /// value to it's <see cref="Foo.Bar"/> property. When the <see cref="Foo.Bar"/> property is changed, it will
        /// raise the <see cref="Foo.BarChanged"/> event, writing the old value and new value to the console.
        /// </summary>
        private static void Main()
        {
            // Create a new instance of the `Foo` class.
            Foo foo = new Foo();

            // Attach a new `GenericEventHandler` to the event.
            foo.BarChanged += (sender, e) => Console.WriteLine(@$"Bar value changed from '{e.OldValue}' to '{sender.Bar}'");

            // Set a new value to `foo.Bar`, which will raise it's corresponding event, `BarChanged`.
            foo.Bar = "NewValue";

            Console.WriteLine("Press any key to exit...");
            _ = Console.ReadKey();
        }
    }

    /// <summary>
    /// Represents a dummy class that uses the <see cref="TACDevel.GenericEventHandler{TSender, TArgs}"/> delegate for it's events.
    /// </summary>
    internal class Foo
    {
        /// <summary>
        /// Private field for <see cref="Bar"/>. Defaults to <see cref="string.Empty"/>.
        /// </summary>
        private string _bar = string.Empty;

        /// <summary>
        /// Occurs when the <see cref="Bar"/> property is changed.
        /// </summary>
        public event GenericEventHandler<Foo, BarChangedEventArgs> BarChanged;

        /// <summary>
        /// Gets or sets the dummy string, calling <see cref="OnBarChanged(BarChangedEventArgs)"/> when set.
        /// </summary>
        public string Bar
        {
            get => _bar;
            set
            {
                // Store the old value so we can use it later.
                string old = _bar;

                // Actually set the value.
                _bar = value;

                // Raises event, passing the old value as the parameter.
                OnBarChanged(new BarChangedEventArgs(old));
            }
        }

        /// <summary>
        /// Raises the <see cref="BarChanged"/> event.
        /// </summary>
        /// <param name="e"></param>
        protected void OnBarChanged(BarChangedEventArgs e) => BarChanged?.Invoke(this, e);
    }

    /// <summary>
    /// Provides data for the <see cref="Foo.BarChanged"/> event.
    /// </summary>
    internal class BarChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldVal">The old value of the <see cref="Foo.Bar"/> property.</param>
        public BarChangedEventArgs(string oldValue) => OldValue = oldValue;

        /// <summary>
        /// Gets the old value of the <see cref="Foo.Bar"/> property.
        /// </summary>
        public string OldValue { get; }
    }
}