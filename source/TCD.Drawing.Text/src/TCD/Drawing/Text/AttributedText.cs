/***************************************************************************************************
 * FileName:             AttributedText.cs
  * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using static TCD.Native.NativeMethods;

namespace TCD.Drawing
{
    //TODO: This is a WIP, mostly just a dummy class for now.
    public class AttributedText : Component
    {
        private bool disposed = false;

        public AttributedText(string text) => Handle = Libui.uiNewAttributedString(text);

        public string Text => Libui.uiAttributedStringString(this);

        public long Len() => Libui.uiAttributedStringLen(this).ToUInt32();

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && Handle != IntPtr.Zero)
                    Libui.uiFreeAttributedString(this);
                disposed = true;
                base.Dispose(disposing);
            }
        }
    }
}