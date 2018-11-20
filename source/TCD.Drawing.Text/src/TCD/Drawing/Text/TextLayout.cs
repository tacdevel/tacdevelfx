/***************************************************************************************************
 * FileName:             TextLayout.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;

namespace TCD.Drawing
{
    [StructLayout(Layout)]
    public struct uiDrawTextLayoutParams
    {
        public IntPtr String;
        public uiFontDescriptor DefaultFont;
        public double Width;
        public TextAlignment Align;
    }

    public class TextLayout : Component
    {
        private bool disposed = false;

        public TextLayout(TextLayoutOptions options)
        {
            Handle = Libui.uiDrawNewTextLayout(options.Native);
            Options = options;
        }

        public TextLayoutOptions Options { get; }

        public SizeD Extents
        {
            get
            {
                Libui.uiDrawTextLayoutExtents(this, out double w, out double h);
                return new SizeD(w, h);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && Handle != IntPtr.Zero)
                    Libui.uiDrawFreeTextLayout(this);
                disposed = true;
                base.Dispose(disposing);
            }
        }
    }
}