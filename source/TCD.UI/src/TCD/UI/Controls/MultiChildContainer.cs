/****************************************************************************
 * FileName:   MultiChildContainer.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    public abstract class MultiChildContainer<TControl, TCollection> : ContainerBase
        where TControl : Control
        where TCollection : ControlCollectionBase<TControl>
    {
        private TCollection children;

        internal MultiChildContainer(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) { }

        /// <summary>
        /// Sets this <see cref="MultiChildContainer{TControl, TCollection}"/>'s child <see cref="Control"/> objects.
        /// </summary>
        public virtual TCollection Children
        {
            get
            {
                if (children == null)
                    children = (TCollection)Activator.CreateInstance(typeof(TCollection), this);
                return children;
            }
        }

        protected override void ReleaseManagedResources()
        {
            if (children != null)
                children.Clear();
            base.ReleaseManagedResources();
        }
    }
}