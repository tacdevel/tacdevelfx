/****************************************************************************
 * FileName:   ObjectExtensions.cs
 * Assembly:   TCD.Core.dll
 * Package:    TCD.Core
 * Date:       20180913
 * License:    MIT License
 * LicenseUrl: https://github.com/tacdevel/TDCFx/blob/master/LICENSE.md
 ***************************************************************************/

using System;
using System.Reflection;

namespace TCD
{
    internal static class ObjectExtensions
    {
        public static int GenerateHashCode(this object self)
        {
            Type type = self.GetType();
            PropertyInfo[] props = type.GetProperties();

            unchecked
            {
                int hash = 27;
                foreach (PropertyInfo prop in props)
                {
                    int propHash = prop.GetValue(self).GetHashCode();
                    // See: https://github.com/dotnet/corefx/blob/master/src/Common/src/System/Numerics/Hashing/HashHelpers.cs
                    uint rol5 = ((uint)hash << 5) | ((uint)propHash >> 27);
                    hash = ((int)rol5 + hash) ^ propHash;
                }
                return hash;
            }
        }
    }
}
