using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TCDFx.Tools.DocGen
{
    internal static class Utilities
    {
        private const string Indent = "    ";

        private static readonly Dictionary<Type, string> primitiveNames = new Dictionary<Type, string>
            {
                { typeof(char), "char" },
                { typeof(string), "string" },
                { typeof(bool), "bool" },
                { typeof(int), "int" },
                { typeof(long), "long" },
                { typeof(float), "float" },
                { typeof(double), "double" },
                { typeof(byte), "byte" },
                { typeof(sbyte), "sbyte" },
                { typeof(decimal), "decimal" },
                { typeof(short), "short" },
                { typeof(uint), "uint" },
                { typeof(ulong), "ulong" },
                { typeof(ushort), "ushort" },
                { typeof(object), "object" },
                { typeof(void), "void" }
            };

        private static readonly Dictionary<string, string> operations = new Dictionary<string, string>
            {
                { "op_Implicit", "" },
                { "op_Explicit", "" },
                { "op_Addition", "+" },
                { "op_Subtraction", "-" },
                { "op_Multiply", "*" },
                { "op_Division", "/" },
                { "op_Modulus", "%" },
                { "op_ExclusiveOr", "^" },
                { "op_BitwiseAnd", "&" },
                { "op_BitwiseOr", "|" },
                { "op_LogicalAnd", "&&" },
                { "op_LogicalOr", "||" },
                { "op_LogicalNot", "!" },
                { "op_Assign", "=" },
                { "op_LeftShift", "<<" },
                { "op_RightShift", ">>" },
                { "op_SignedRightShift", "" },
                { "op_UnsignedRightShift", "" },
                { "op_Equality", "==" },
                { "op_GreaterThan", ">" },
                { "op_LessThan", "<" },
                { "op_Inequality", "!=" },
                { "op_GreaterThanOrEqual", ">=" },
                { "op_LessThanOrEqual", "<=" },
                { "op_MultiplicationAssignment", "*=" },
                { "op_SubtractionAssignment", "-=" },
                { "op_ExclusiveOrAssignment", "^=" },
                { "op_LeftShiftAssignment", "<<=" },
                { "op_ModulusAssignment", "%=" },
                { "op_AdditionAssignment", "+=" },
                { "op_BitwiseAndAssignment", "&=" },
                { "op_BitwiseOrAssignment", "|=" },
                { "op_Comma", "," },
                { "op_DivisionAssignment", "/=" },
                { "op_Decrement", "--" },
                { "op_Increment", "++" },
                { "op_UnaryNegation", "-" },
                { "op_UnaryPlus", "+" },
                { "op_OnesComplement", "~" }
            };

        public static string GetTypeTitle(Type type, bool includeNamespace = false)
        {
            string name = GetDisplayTitle(type, false);
            StringBuilder sb = new StringBuilder();

            if (type.IsEnum)
                sb.Append($"{name} enum");
            else if (type.IsInterface)
                sb.Append($"{name} interface");
            else if (type.IsValueType)
                sb.Append($"{name} struct");
            else if (type.IsSubclassOf(typeof(Delegate)))
                sb.Append($"{name} delegate");
            else
                sb.Append($"{name} class");

            if (includeNamespace)
                sb.Append($" ({type.Namespace})");

            return sb.ToString();
        }

        [Obsolete]
        public static string ChangeExtension(string path, string extension)
        {
            int dotIndex = path.LastIndexOf(".", StringComparison.Ordinal);
            int slashIndex = path.LastIndexOf("/", StringComparison.Ordinal);
            if (dotIndex < slashIndex) return $"{path}.{extension}";
            string pathNoExt = dotIndex > -1 ? path.Substring(0, dotIndex) : path;
            return $"{pathNoExt}.{extension}";
        }

        public static string GetInheritanceString(Type type)
        {
            if (type == typeof(object)) return string.Empty;
            List<Type> lstBases = new List<Type>();
            Type t = type;
            do
            {
                lstBases.Add(t);
                t = t.BaseType;
            } while (t != typeof(object) && t != null);
            lstBases.Reverse();
            StringBuilder sb = new StringBuilder();
            sb.Append("Object \u2192 ");
            for (int i = 0; i < lstBases.Count; i++)
            {
                if (i > 0) sb.Append(" \u2192 ");
                sb.Append(GetDisplayTitle(lstBases[i], false));
            }
            return sb.ToString();
        }

        public static string GetOperatorSymbol(string operatorMethodName) => operations.TryGetValue(operatorMethodName, out string symbol) ? symbol : string.Empty;

        public static string GetBuiltinName(Type type) => primitiveNames.TryGetValue(type, out string name) ? name : type.Name;

        public static string GetURLTitle(Type type)
        {
            if (type.IsGenericType)
                return $"{type.Name.Substring(0, type.Name.IndexOf("`"))}-{type.GetGenericArguments().Length}";
            return type.Name;
        }

        public static string GetIdentifier(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            return value.Contains("`") ? value.Substring(0, value.IndexOf("`")) : value;
        }

        public static string GetClassSignature(Type type)
        {
            StringBuilder sb = new StringBuilder();
            Type subclass = type.BaseType;
            bool inherits = subclass != null && subclass != typeof(object);
            Type[] interfaces = type.GetInterfaces();

            if (type.IsPublic) sb.Append("public ");

            if (!type.IsEnum && !type.IsInterface)
            {
                if (type.IsSealed)
                    sb.Append(type.IsAbstract ? "static " : "sealed ");
                else if (type.IsAbstract)
                    sb.Append("abstract ");
            }

            if (type.IsSubclassOf(typeof(Delegate)))
            {
                MethodInfo method = type.GetMethod("Invoke");
                sb.Append($"delegate {GetBuiltinName(method.ReturnType)} {GetMethodSignature(method)};");
                return sb.ToString();
            }

            string rawName = type.Name;
            if (rawName.Contains("`")) rawName = rawName.Substring(0, rawName.IndexOf("`"));

            if (type.IsInterface)
                sb.Append("interface ");
            else if (type.IsEnum)
                sb.Append("enum ");
            else if (type.IsValueType)
                sb.Append("struct ");
            else
                sb.Append("class ");

            sb.Append(rawName);

            if (!type.IsEnum)
            {
                if (inherits || interfaces.Length > 0)
                    sb.Append(" : ");

                if (inherits)
                    sb.Append(GetDisplayTitle(subclass));

                if (interfaces.Length > 0)
                {
                    if (inherits) sb.Append(", ");
                    for (int i = 0; i < interfaces.Length; i++)
                    {
                        if (i > 0) sb.Append(", ");
                        sb.Append(GetDisplayTitle(interfaces[i]));
                    }
                }
            }

            return sb.ToString();
        }

        public static string GetFieldSignature(FieldInfo field, bool fullDefinition = false)
        {
            StringBuilder sb = new StringBuilder();
            if (fullDefinition)
            {
                if (field.IsPublic)
                    sb.Append("public ");
                else if (field.IsFamily)
                    sb.Append("protected ");

                if (field.IsLiteral)
                    sb.Append("const ");
                else
                {
                    if (field.IsStatic)
                        sb.Append("static ");
                    if (field.IsInitOnly)
                        sb.Append("readonly ");
                }

                sb.Append(GetDisplayTitle(field.FieldType, false));
                sb.Append(" ");
            }

            sb.Append(field.Name);
            if (fullDefinition)
            {
                if (field.IsLiteral)
                    sb.Append($" = {GetConstantValueString(field.GetRawConstantValue())}");
                sb.Append(";");
            }

            return sb.ToString();
        }

        public static string GetPropertySignature(PropertyInfo property, bool includeKeywords = true, bool includeParamNames = true, bool includeBody = false)
        {
            StringBuilder sb = new StringBuilder();
            bool canRead = property.CanRead;
            bool canWrite = property.CanWrite;
            MethodInfo getter = property.GetGetMethod(true);
            MethodInfo setter = property.GetSetMethod(true);
            bool isGetterProtected = (canRead && !getter.IsPublic && !getter.IsPrivate);
            bool isSetterProtected = (canWrite && !setter.IsPublic && !setter.IsPrivate);
            bool hasPublicAccessor = (canRead && getter.IsPublic) || (canWrite && setter.IsPublic);
            bool hasProtectedAccessor = isGetterProtected || isSetterProtected;
            bool isProtectedProperty = !hasPublicAccessor && hasProtectedAccessor;
            bool isStatic = canRead && getter.IsStatic || canWrite && setter.IsStatic;
            bool isAbstract = canRead && getter.IsAbstract || canWrite && setter.IsAbstract;
            bool isOverride = property.DeclaringType != property.ReflectedType;
            bool isVirtual = canRead && getter.IsVirtual || canWrite && setter.IsVirtual;

            if (includeKeywords)
            {
                if (hasPublicAccessor)
                    sb.Append("public ");
                else if (isProtectedProperty)
                    sb.Append("protected ");

                if (isStatic)
                    sb.Append("static ");
                else if (isAbstract)
                    sb.Append("abstract ");

                if (isOverride)
                    sb.Append("override ");
                else if (isVirtual)
                    sb.Append("virtual ");

                sb.Append(GetDisplayTitle(property.PropertyType, false));
                sb.Append(" ");
            }

            ParameterInfo[] ip = property.GetIndexParameters();
            if (ip.Length > 0)
            {
                sb.Append("this[");
                for (int i = 0; i < ip.Length; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(GetDisplayTitle(ip[i], includeParamNames));
                }
                sb.Append("]");
            }
            else
                sb.Append(property.Name);

            if (includeBody)
            {
                sb.Append("\n{\n");

                // Print getter
                if (canRead)
                {
                    sb.Append(Indent);
                    // Add protected keyword if property has public accessor
                    if (hasPublicAccessor && isGetterProtected)
                        sb.Append("protected ");
                    sb.Append("get;\n");
                }

                // Print setter
                if (canWrite)
                {
                    sb.Append(Indent);
                    // Add protected keyword if property has public accessor
                    if (hasPublicAccessor & isSetterProtected)
                        sb.Append("protected ");
                    sb.Append("set;\n");
                }

                sb.Append("}");
            }

            return sb.ToString();
        }

        public static string GetMethodSignature(MethodBase method, bool includeKeywords = false, bool includeParamNames = true)
        {
            StringBuilder sb = new StringBuilder();

            string rawName;
            bool isConversion = false;

            if (method.DeclaringType.IsSubclassOf(typeof(Delegate)))
            {
                rawName = method.DeclaringType.Name;
                if (rawName.Contains("`")) rawName = rawName.Substring(0, rawName.IndexOf("`"));
            }
            if (method.IsConstructor)
            {
                rawName = GetDisplayTitle(method.DeclaringType, false);
            }
            else
            {
                rawName = method.Name;
                MethodInfo m = method as MethodInfo;
                if (rawName.Contains("`")) rawName = rawName.Substring(0, rawName.IndexOf("`"));
                if (rawName.StartsWith("op_") && operations.TryGetValue(rawName, out string opName))
                {
                    if (rawName == "op_Implicit")
                    {
                        rawName = $"implicit operator {GetDisplayTitle(m.ReturnType)}";
                        isConversion = true;
                    }
                    else if (rawName == "op_Explicit")
                    {
                        rawName = $"explicit operator {GetDisplayTitle(m.ReturnType)}";
                        isConversion = true;
                    }
                    else
                    {
                        rawName = $"operator {opName}";
                    }
                }
            }

            if (includeKeywords)
            {
                if (method.IsPublic)
                {
                    sb.Append("public ");
                }
                else if (method.IsFamily || method.IsFamilyOrAssembly)
                {
                    sb.Append("protected ");
                }

                if (method.IsStatic)
                {
                    sb.Append("static ");
                }
                else if (method.IsAbstract)
                {
                    sb.Append("abstract ");
                }
                if (method.DeclaringType != method.ReflectedType)
                {
                    sb.Append("override ");
                }
                else if (method.IsVirtual)
                {
                    sb.Append("virtual ");
                }

                if (!isConversion && method is MethodInfo m)
                {
                    sb.Append($"{GetDisplayTitle(m.ReturnType)} ");
                }
            }

            sb.Append(rawName);

            if (method.IsGenericMethod)
            {
                sb.Append("<");
                Type[] gargs = method.GetGenericArguments();
                for (int i = 0; i < gargs.Length; i++)
                {
                    if (i > 0) sb.Append(", ");
                    sb.Append(gargs[i].Name);
                }
                sb.Append(">");
            }
            sb.Append("(");
            ParameterInfo[] plist = method.GetParameters();
            for (int i = 0; i < plist.Length; i++)
            {
                if (i > 0) sb.Append(", ");
                sb.Append(GetDisplayTitle(plist[i], includeParamNames));
            }
            sb.Append(")");

            return sb.ToString();
        }

        public static string GetDisplayTitle(ParameterInfo parameter, bool includeName = true)
        {
            StringBuilder sb = new StringBuilder();
            if (parameter.IsOut)
            {
                sb.Append("out ");
            }
            else if (parameter.ParameterType.IsByRef)
            {
                sb.Append("ref ");
            }
            else if (parameter.GetCustomAttribute<ParamArrayAttribute>() != null)
            {
                sb.Append("params ");
            }

            sb.Append(GetDisplayTitle(parameter.ParameterType, false));

            if (includeName)
            {
                sb.Append($" {parameter.Name}");

                if (parameter.HasDefaultValue)
                {
                    sb.Append($" = {GetConstantValueString(parameter.RawDefaultValue)}");
                }
            }

            return sb.ToString();
        }

        public static string GetConstantValueString(object value)
        {
            if (value == null) return "null";
            Type valueType = value.GetType();

            if (valueType == typeof(string)) return $"\"{value}\"";
            if (valueType == typeof(char)) return $"'{value}'";
            if (valueType == typeof(float)) return $"{value}f";
            if (valueType == typeof(double)) return $"{value}d";

            return value.ToString();
        }

        public static string GetDisplayTitle(Type type, bool includeNamespace = true)
        {
            StringBuilder sb = new StringBuilder();
            Type elementType = type.GetElementType();
            string bareName = type.IsArray
                ? elementType.Name
                : type.Name;
            Type bareType = type.IsArray ? type.GetElementType() : type;

            if (primitiveNames.TryGetValue(bareType, out string primitiveName))
            {
                sb.Append(primitiveName);
            }
            // e.g. Dictionary<string, int>
            else if (bareType.IsGenericType)
            {
                if (includeNamespace)
                {
                    sb.Append($"{bareType.Namespace}.");
                }
                sb.Append(bareName.Substring(0, bareType.Name.IndexOf("`", StringComparison.InvariantCulture)));
                sb.Append('<');
                Type[] gargs = bareType.GetGenericArguments();
                for (int i = 0; i < gargs.Length; i++)
                {
                    if (i > 0 && gargs.Length > 1)
                        sb.Append(", ");
                    sb.Append(GetDisplayTitle(gargs[i]));
                }
                sb.Append('>');
            }
            // e.g. T
            else if (bareType.IsGenericParameter)
            {
                sb.Append(type.Name);
            }
            // e.g. System.String
            else
            {
                if (includeNamespace) sb.Append($"{bareType.Namespace}.");
                sb.Append(bareName);
            }

            // Handle array notation
            if (type.IsArray)
            {
                if (elementType.IsPointer) sb.Append('*');
                int rank = type.GetArrayRank();
                if (rank == 1)
                    sb.Append("[]");
                else
                {
                    sb.Append('[');
                    sb.Append(new string(',', rank - 1));
                    sb.Append(']');
                }
            }

            if (type.IsPointer) sb.Append('*');

            return sb.ToString();
        }

        public static MemberVisibility GetVisibility(FieldInfo field)
        {
            if (field.IsPrivate) return MemberVisibility.Private;

            if (field.IsPublic) return MemberVisibility.Public;

            if (field.IsFamilyOrAssembly) return MemberVisibility.ProtectedInternal;

            if (field.IsFamily) return MemberVisibility.Protected;

            return MemberVisibility.Internal;
        }

        public static MemberVisibility GetVisibility(MethodBase method)
        {
            if (method.IsPrivate) return MemberVisibility.Private;

            if (method.IsPublic) return MemberVisibility.Public;

            if (method.IsFamilyOrAssembly) return MemberVisibility.ProtectedInternal;

            if (method.IsFamily) return MemberVisibility.Protected;

            return MemberVisibility.Internal;
        }
    }
}