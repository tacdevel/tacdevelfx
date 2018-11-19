/***************************************************************************************************
 * FileName:             NonWrappingTextBlock.cs
 * Date:                 20181001
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using TCD.Native;
using TCD.SafeHandles;

namespace TCD.UI.Controls
{
    /// <summary>
    /// Represents a control that can be used to display or edit multiple lines of text that are not wrapped.
    /// </summary>summary>
    public class NonWrappingTextBlock : TextBlockBase
    {
        public NonWrappingTextBlock() : base(new SafeControlHandle(Libui.NewNonWrappingMultilineEntry()), true) { }
    }
}