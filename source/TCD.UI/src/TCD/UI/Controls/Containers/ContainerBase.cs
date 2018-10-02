/****************************************************************************
 * FileName:   ContainerBase.cs
 * Assembly:   TCD.UI.dll
 * Package:    TCD.UI
 * Date:       20181001
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using TCD.SafeHandles;

namespace TCD.UI.Controls.Containers
{
    public abstract class ContainerBase : Control
    {
        internal ContainerBase(SafeControlHandle handle, bool cacheable) : base(handle, cacheable) { }
    }
}