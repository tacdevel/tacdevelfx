/***************************************************************************************************
 * FileName:             Component.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using TCD.Collections;

namespace TCD.ComponentModel
{
    /// <summary>
    /// Provides the base implementation for the <see cref="IComponent"/> interface.
    /// </summary>
    public abstract class Component : Disposable, IComponent, INotifyPropertyChanged, INotifyPropertyChanging
    {
        private string name = null;
        private bool isNameImmutable = false;
        internal static readonly MultiValueDictionary<string, Type, Component> cache = new MultiValueDictionary<string, Type, Component>();

        /// <summary>
        /// Initializes a new instance if the <see cref="Component"/> class.
        /// </summary>
        protected internal Component() { }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc />
        public event PropertyChangingEventHandler PropertyChanging;

        /// <inheritdoc />
        public virtual string Name
        {
            get => name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                if (isNameImmutable) throw new ArgumentException("Name property has already been set.", nameof(value));
                if (cache.ContainsKey(value)) throw new DuplicateComponentException($"The component '{value}' has already been created.");

                OnPropertyChanging(nameof(Name));
                if (name != value)
                    name = value;
                isNameImmutable = true;
                cache.Add(Name, GetType(), this);
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <inheritdoc />
        public abstract bool IsInvalid { get; }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Raises the <see cref="OnPropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        /// <inheritdoc />
        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid && name != null)
                if (cache.ContainsKey(Name))
                    cache.Remove(Name);
            base.ReleaseManagedResources();
        }
    }
}