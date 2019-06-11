/***************************************************************************************************
 * FileName:             HashingExtensions.cs
 * Copyright:            Copyright © 2017-2019 Thomas Corwin, et al. All Rights Reserved.
 * License:              https://github.com/tom-corwin/tcdfx/blob/master/LICENSE.md
 **************************************************************************************************/

 using System.Reflection;

namespace TCDFx.Numerics.Hashing
{
    /// <summary>
    /// Contains extension methods to hash object values.
    /// </summary>
    public static class ObjectExtensions
    {
        // See: https://github.com/dotnet/corefx/blob/master/src/Common/src/System/Numerics/Hashing/HashHelpers.cs
        /// <summary>
        /// Generates a hash code based on the <paramref name="properties"/> given values.
        /// </summary>
        /// <param name="self">The current object.</param>
        /// <param name="properties">The object's properties to hash.</param>
        /// <returns>A generated hash code.</returns>
        public static int GenerateHashCode(this object self, params object[] properties)
        {
            unchecked
            {
                int hash = self.GetHashCode();
                foreach (object prop in properties)
                {
                    if (prop != null)
                    {
                        uint rol5 = ((uint)hash << 5) | ((uint)hash >> 27);
                        hash = ((int)rol5 + hash) ^ prop.GetHashCode();
                    }
                }
                return hash;
            }
        }

        /// <summary>
        /// Generates a hash code by getting the public, instance properties from this object.
        /// </summary>
        /// <param name="self">The current object.</param>
        /// <returns>A generated hash code.</returns>
        public static int GenerateHashCode(this object self)
        {
            PropertyInfo[] propInfos = self.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
            object[] props = new object[propInfos.Length];
            for (int i = 0; i < propInfos.Length; i++)
            {
                props[i] = propInfos[i].GetValue(self);
            }
            return GenerateHashCode(self, props);
        }
    }
}