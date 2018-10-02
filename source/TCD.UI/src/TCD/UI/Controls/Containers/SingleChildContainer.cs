/****************************************************************************
 * FileName:   SingleChildContainer.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    public abstract class SingleChildContainer<TControl> : ContainerBase
        where TControl : Control
    {
        private TControl child;

        internal SingleChildContainer(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) { }

        /// <summary>
        /// Sets this <see cref="SingleChildContainer{TControl}"/> object's child <see cref="Control"/>.
        /// </summary>
        public virtual TControl Child
        {
            set
            {
                if (child == null)
                    child = (TControl)Activator.CreateInstance(typeof(TControl), this);
            }
        }

        protected override void ReleaseManagedResources()
        {
            if (child != null)
                child.Dispose();
            base.ReleaseManagedResources();
        }
    }
}