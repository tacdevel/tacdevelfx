/***************************************************************************************************
 * FileName:   ContainerBase.cs
 * Date:       20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    public abstract class ContainerBase : Control
    {
        internal ContainerBase(SafeControlHandle handle, bool cacheable) : base(handle, cacheable) { }
    }
}