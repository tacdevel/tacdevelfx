/***************************************************************************************************
 * FileName:             LibuiEx.cs
 * Date:                 20181120
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Runtime.InteropServices;
using TCD.Drawing;
using TCD.Drawing.Text;

namespace TCD.Native
{
    internal static class LibuiEx
    {        
        internal enum ForEach : long
        {
            Continue,
            Stop
        }

        internal enum AttributeType : long
        {
            Family,
            Size,
            Weight,
            Italic,
            Stretch,
            Color,
            Background,
            Underline,
            UnderlineColor,
            Features
        }

        #region TextAttributes
        internal static void FreeAttribute(IntPtr a) => Libui.LoadFunction<Signatures.uiFreeAttribute>()(a);
        internal static AttributeType AttributeGetType(IntPtr a) => Libui.LoadFunction<Signatures.uiAttributeGetType>()(a);
        internal static IntPtr NewFamilyAttribute(string family) => Libui.LoadFunction<Signatures.uiNewFamilyAttribute>()(family);
        internal static string AttributeFamily(IntPtr a) => Libui.LoadFunction<Signatures.uiAttributeFamily>()(a);
        internal static IntPtr NewSizeAttribute(double size) => Libui.LoadFunction<Signatures.uiNewSizeAttribute>()(size);
        internal static double AttributeSize(IntPtr a) => Libui.LoadFunction<Signatures.uiAttributeSize>()(a);
        internal static IntPtr NewWeightAttribute(FontWeight weight) => Libui.LoadFunction<Signatures.uiNewWeightAttribute>()(weight);
        internal static FontWeight AttributeWeight(IntPtr a) => Libui.LoadFunction<Signatures.uiAttributeWeight>()(a);
        internal static IntPtr NewItalicAttribute(FontStyle italic) => Libui.LoadFunction<Signatures.uiNewItalicAttribute>()(italic);
        internal static FontStyle AttributeItalic(IntPtr a) => Libui.LoadFunction<Signatures.uiAttributeItalic>()(a);
        internal static IntPtr NewStretchAttribute(FontStretch stretch) => Libui.LoadFunction<Signatures.uiNewStretchAttribute>()(stretch);
        internal static FontStretch AttributeStretch(IntPtr a) => Libui.LoadFunction<Signatures.uiAttributeStretch>()(a);
        internal static IntPtr NewColorAttribute(double r, double g, double b, double a) => Libui.LoadFunction<Signatures.uiNewColorAttribute>()(r, g, b, a);
        internal static void AttributeColor(IntPtr a, out double r, out double g, out double b, out double alpha) => Libui.LoadFunction<Signatures.uiAttributeColor>()(a, out r, out g, out b, out alpha);
        internal static IntPtr NewBackgroundAttribute(double r, double g, double b, double a) => Libui.LoadFunction<Signatures.uiNewBackgroundAttribute>()(r, g, b, a);
        internal static IntPtr NewUnderlineAttribute(UnderlineStyle u) => Libui.LoadFunction<Signatures.uiNewUnderlineAttribute>()(u);
        internal static UnderlineStyle AttributeUnderline(IntPtr a) => Libui.LoadFunction<Signatures.uiAttributeUnderline>()(a);
        internal static IntPtr NewUnderlineColorAttribute(UnderlineColor u, double r, double g, double b, double a) => Libui.LoadFunction<Signatures.uiNewUnderlineColorAttribute>()(u, r, g, b, a);
        internal static void AttributeGetType(IntPtr a, out UnderlineColor u, out double r, out double g, out double b, out double alpha) => Libui.LoadFunction<Signatures.uiAttributeUnderlineColor>()(a, out u, out r, out g, out b, out alpha);
        #endregion

        // Keep the delegates in this class in order with
        // libui\ui.h so it's easier to see what is missing.
        private static class Signatures
        {
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiFreeAttribute(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate AttributeType uiAttributeGetType(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewFamilyAttribute(string family);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate string uiAttributeFamily(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewSizeAttribute(double size);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate double uiAttributeSize(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewWeightAttribute(FontWeight weight);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate FontWeight uiAttributeWeight(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewItalicAttribute(FontStyle italic);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate FontStyle uiAttributeItalic(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewStretchAttribute(FontStretch stretch);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate FontStretch uiAttributeStretch(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewColorAttribute(double r, double g, double b, double a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAttributeColor(IntPtr a, out double r, out double g, out double b, out double alpha);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewBackgroundAttribute(double r, double g, double b, double a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewUnderlineAttribute(UnderlineStyle u);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate UnderlineStyle uiAttributeUnderline(IntPtr a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewUnderlineColorAttribute(UnderlineColor u, double r, double g, double b, double a);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAttributeUnderlineColor(IntPtr a, out UnderlineColor u, out double r, out double g, out double b, out double alpha);

            //TODO: Functions for the following delegates.
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate ForEach uiOpenTypeFeaturesForEachFunc(IntPtr otf, char a, char b, char c, char d, uint value, IntPtr data);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewOpenTypeFeatures();
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiFreeOpenTypeFeatures(IntPtr otf);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiOpenTypeFeaturesClone(IntPtr otf);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiOpenTypeFeaturesAdd(IntPtr otf, char a, char b, char c, char d, uint value);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiOpenTypeFeaturesRemove(IntPtr otf, char a, char b, char c, char d);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate int uiOpenTypeFeaturesGet(IntPtr otf, char a, char b, char c, char d, out uint value);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiOpenTypeFeaturesForEach(IntPtr otf, uiOpenTypeFeaturesForEachFunc f, IntPtr data);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewFeaturesAttribute(IntPtr otf);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiAttributeFeatures(IntPtr a);

            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate ForEach uiAttributedStringForEachAttributeFunc(IntPtr s, IntPtr a, UIntPtr start, UIntPtr end, IntPtr data);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiNewAttributedString(string initialString);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiFreeAttributedString(IntPtr s);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate string uiAttributedStringString(IntPtr s);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate UIntPtr uiAttributedStringLen(IntPtr s);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAttributedStringAppendUnattributed(IntPtr s, IntPtr str);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAttributedStringInsertAtUnattributed(IntPtr s, IntPtr str, UIntPtr at);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAttributedStringDelete(IntPtr s, UIntPtr start, UIntPtr end);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAttributedStringSetAttribute(IntPtr s, IntPtr a, UIntPtr start, UIntPtr end);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiAttributedStringForEachAttribute(IntPtr s, uiAttributedStringForEachAttributeFunc f, IntPtr data);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate UIntPtr uiAttributedStringNumGraphemes(IntPtr s);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate UIntPtr uiAttributedStringByteIndexToGrapheme(IntPtr s, UIntPtr pos);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate UIntPtr uiAttributedStringGraphemeToByteIndex(IntPtr s, UIntPtr pos);

            // [UnmanagedFunctionPointer(Libui.Convention)] internal delegate IntPtr uiDrawNewTextLayout(uiDrawTextLayoutParams param);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawFreeTextLayout(IntPtr tl);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawText(IntPtr c, IntPtr tl, double x, double y);
            [UnmanagedFunctionPointer(Libui.Convention)] internal delegate void uiDrawTextLayoutExtents(IntPtr tl, out double width, out double height);
            //TODO: Functions for the above delegates.
        }
    }
}