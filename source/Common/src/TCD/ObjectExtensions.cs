/***************************************************************************************************
 * FileName:             ObjectExtensions.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

namespace TCD
{
    internal static class ObjectExtensions
    {
        // See: https://github.com/dotnet/corefx/blob/master/src/Common/src/System/Numerics/Hashing/HashHelpers.cs
        public static int GenerateHashCode(this object self, params object[] objs)
        {
            unchecked
            {
                int hash = 27;

                foreach (object obj in objs)
                {
                    if (obj != null)
                    {
                        int objHash = obj.GetHashCode();
                        uint rol5 = ((uint)hash << 5) | ((uint)objHash >> 27);
                        hash = ((int)rol5 + hash) ^ objHash;
                    }
                }

                return hash;
            }
        }
    }
}