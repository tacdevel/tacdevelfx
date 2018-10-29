using System;
using System.Reflection;
using System.Text;

namespace TCDFx.Tools.DocGen
{
    internal static class IDStringExtensions
    {
        public static string GetIDString(this Type self) => $"T:{ConstructIDString(self)}";

        public static string GetIDString(this MethodBase self) => $"M:{ConstructIDString(self)}";

        public static string GetIDString(this FieldInfo self) => $"F:{ConstructIDString(self.DeclaringType)}.{self.Name}";

        public static string GetIDString(this PropertyInfo self)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"P:{ConstructIDString(self.DeclaringType)}.{self.Name}");

            // Check if it's an indexer
            ParameterInfo[] indexParams = self.GetIndexParameters();
            if (indexParams.Length > 0)
            {
                sb.Append("(");
                for (int i = 0; i < indexParams.Length; i++)
                {
                    if (i > 0) sb.Append(",");
                    sb.Append(ConstructIDString(indexParams[i].ParameterType));
                }
                sb.Append(")");
            }

            return sb.ToString();
        }

        private static string ConstructIDString(MethodBase method)
        {
            StringBuilder sb = new StringBuilder();
            string rawName = method.IsConstructor ? "#ctor" : method.Name;
            sb.Append($"{ConstructIDString(method.DeclaringType)}.{rawName}");
            ParameterInfo[] plist = method.GetParameters();
            if (plist.Length > 0)
            {
                sb.Append("(");
                for (int i = 0; i < plist.Length; i++)
                {
                    if (i > 0) sb.Append(",");
                    sb.Append(ConstructIDString(plist[i].ParameterType));
                }
                sb.Append(")");
            }
            return sb.ToString();
        }

        private static string ConstructIDString(Type type)
        {
            StringBuilder sb = new StringBuilder();
            Type elementType = type.GetElementType();
            string bareName = type.IsArray
                ? elementType.Name
                : type.Name;

            // e.g. T
            if (type.IsGenericParameter)
            {
                sb.Append($"``{type.GenericParameterPosition}");
            }
            // e.g. Dictionary<string, int>
            else if (type.IsConstructedGenericType)
            {
                sb.Append($"{type.Namespace}.{bareName.Substring(0, type.Name.IndexOf("`", StringComparison.InvariantCulture))}");
                sb.Append('{');
                for (int i = 0; i < type.GenericTypeArguments.Length; i++)
                {
                    if (i > 0 && type.GenericTypeArguments.Length > 1)
                    {
                        sb.Append(',');
                    }
                    sb.Append(ConstructIDString(type.GenericTypeArguments[i]));
                }
                sb.Append('}');
            }
            // e.g. System.String
            else
            {
                sb.Append($"{type.Namespace}.{bareName}");
            }

            // Handle array notation
            if (type.IsArray)
            {
                if (elementType.IsPointer) sb.Append('*');
                if (elementType.IsByRef) sb.Append('@');
                int rank = type.GetArrayRank();
                if (rank == 1)
                {
                    sb.Append("[]");
                }
                else
                {
                    sb.Append('[');
                    for (int i = 0; i < rank; i++)
                    {
                        if (i > 0) sb.Append(',');
                        sb.Append("0:");
                    }
                    sb.Append(']');
                }
            }

            if (type.IsPointer) sb.Append('*');
            if (type.IsByRef) sb.Append('@');

            return sb.ToString();
        }
    }
}