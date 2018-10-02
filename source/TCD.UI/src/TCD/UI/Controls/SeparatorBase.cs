/****************************************************************************
 * FileName:   SeparatorBase.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

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