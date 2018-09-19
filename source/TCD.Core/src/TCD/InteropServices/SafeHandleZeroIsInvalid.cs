/****************************************************************************
 * FileName:   SafeHandleZeroIsInvalid.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180918
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Runtime.InteropServices;

namespace TCD.InteropServices
{
    public abstract class SafeHandleZeroIsInvalid : SafeHandle
    {
        protected SafeHandleZeroIsInvalid(bool ownsHandle) : base(IntPtr.Zero, ownsHandle) { }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}