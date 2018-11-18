/***************************************************************************************************
 * FileName:             ObjectExtensions.cs
 * Date:                 20180913
 * Copyright:            Copyright © 2017-2018 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tacdevel/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

using System;
using System.Reflection;

namespace TCD
{
    internal static class ObjectExtensions
    {
        public static int GenerateHashCode(this object self)
        {
            PropertyInfo[] props = self.GetType().GetProperties(BindingFlags.Public);
            return unchecked(GetHashFromProperties(self, props));
        }

        public static int GenerateHashCode(this object self, params string[] properties)
        {
            Type type = self.GetType();
            PropertyInfo[] props = new PropertyInfo[properties.Length];

            int i = 0;
            foreach (string prop in properties)
            {
                props[i] = type.GetProperty(prop);
                i++;
            }

            return unchecked(GetHashFromProperties(self, props));
        }

        private static int GetHashFromProperties(object obj, PropertyInfo[] properties)
        {
            unchecked
            {
                int hash = 27;
                foreach (PropertyInfo prop in properties)
                {
                    int propHash = prop.GetValue(obj).GetHashCode();
                    // See: https://github.com/dotnet/corefx/blob/master/src/Common/src/System/Numerics/Hashing/HashHelpers.cs
                    uint rol5 = ((uint)hash << 5) | ((uint)propHash >> 27);
                    hash = ((int)rol5 + hash) ^ propHash;
                }
                return hash;
            }
        }
    }
}