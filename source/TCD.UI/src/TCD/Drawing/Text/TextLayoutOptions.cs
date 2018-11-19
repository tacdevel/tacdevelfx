/***************************************************************************************************
 * FileName:             TextLayoutOptions.cs
 * Date:                 20181119
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using static LibUISharp.Native.NativeMethods;

namespace LibUISharp.Drawing
{
    public readonly struct TextLayoutOptions
    {
        internal readonly Libui.uiDrawTextLayoutParams Native;

        public TextLayoutOptions(AttributedText attrText, Font defaultFont, double width, TextAlignment alignment)
        {
            Native = new Libui.uiDrawTextLayoutParams()
            {
                String = attrText,
                DefaultFont = defaultFont.Native,
                Width = width,
                Align = alignment
            };

            Text = attrText;
            DefaultFont = defaultFont;
            Width = width;
            Alignment = alignment;
        }

        public AttributedText Text { get; }
        public Font DefaultFont { get; }
        public double Width { get; }
        public TextAlignment Alignment { get; }
    }
}