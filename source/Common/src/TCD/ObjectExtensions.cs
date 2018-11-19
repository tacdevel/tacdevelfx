/***************************************************************************************************
 * FileName:             ObjectExtensions.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System.Linq;

namespace TCD
{
    internal static class ObjectExtensions
    {
        public static int GenerateHashCode(this object self)
        {
            unchecked
            {
                int hash = 27;
                self.GetType().GetProperties().ToList().ForEach(prop =>
                {
                    object value = prop.GetValue(self, null);
                    if (value != null)
                    {
                        int propHash = value.GetHashCode();
                        // See: https://github.com/dotnet/corefx/blob/master/src/Common/src/System/Numerics/Hashing/HashHelpers.cs
                        uint rol5 = ((uint)hash << 5) | ((uint)propHash >> 27);
                        hash = ((int)rol5 + hash) ^ propHash;
                    }
                });
                return hash;
            }
        }
    }
}