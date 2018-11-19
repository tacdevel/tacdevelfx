/***************************************************************************************************
 * FileName:             SeparatorBase.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// The base class for a control that is used to separate user-interface (UI) content.
    /// </summary>
    public abstract class SeparatorBase : Control
    {
        internal SeparatorBase(SafeControlHandle handle, bool cacheable = true) : base(handle, cacheable) { }
    }
}