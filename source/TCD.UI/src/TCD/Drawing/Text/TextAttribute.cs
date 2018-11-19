/***************************************************************************************************
 * FileName:             TextAttribute.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using static LibUISharp.Native.NativeMethods;

namespace LibUISharp.Drawing
{
    public abstract class TextAttribute : NativeComponent<SafeTextAttributeHandle>
    {
        private bool disposed = false;

        internal Libui.uiAttributeType Type => Libui.uiAttributeGetType(this);

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && Handle != IntPtr.Zero)
                    Libui.uiFreeAttribute(this);
                disposed = true;
                base.Dispose(disposing);
            }
        }
    }
}