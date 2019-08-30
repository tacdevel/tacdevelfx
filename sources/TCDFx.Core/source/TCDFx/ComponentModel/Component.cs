/***************************************************************************************************
 * FileName:             Component.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace TCDFx.ComponentModel
{
    /// <summary>
    /// Provides the base implementation for the <see cref="IComponent"/> interface.
    /// </summary>
    public abstract class Component : Disposable, IComponent
    {
        private static readonly Dictionary<Guid, IComponent> componentCache = new Dictionary<Guid, IComponent>();

#nullable enable
        /// <summary>
        /// Initializes a new instance if the <see cref="Component"/> class.
        /// </summary>
        protected Component()
        {
            UID = GenerateNewUID();
            componentCache.Add(UID, this);
        }
#nullable disable

        /// <inheritdoc />
        public event EventHandler<Component, PropertyChangedEventArgs> PropertyChanged;

        /// <inheritdoc />
        public event EventHandler<Component, PropertyChangingEventArgs> PropertyChanging;

        /// <inheritdoc />
        public Guid UID { get; }

        /// <inheritdoc />
        public abstract bool IsInvalid { get; }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Raises the <see cref="PropertyChanging"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property that is changing.</param>
        protected virtual void OnPropertyChanging(string propertyName) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

        /// <inheritdoc />
        protected override void ReleaseManagedResources()
        {
            if (!IsInvalid && componentCache.ContainsKey(UID))
                componentCache.Remove(UID);
            base.ReleaseManagedResources();
        }

        internal static void UpdateComponentInCache(Guid uid, Component component)
        {
            if (componentCache.ContainsKey(uid) && componentCache[uid] != component)
                componentCache[uid] = component;
        }

        private static Guid GenerateNewUID()
        {
            Guid uid = Guid.Empty;
            while (uid == Guid.Empty || componentCache.ContainsKey(uid))
            {
                byte[] bytes = new byte[16];
                using (RNGCryptoServiceProvider uidGenerator = new RNGCryptoServiceProvider())
                    uidGenerator.GetBytes(bytes);
                uid = new Guid(bytes);
            }
            return uid;
        }
    }
}