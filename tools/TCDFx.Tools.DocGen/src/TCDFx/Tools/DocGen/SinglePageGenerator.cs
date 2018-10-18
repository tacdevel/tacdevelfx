using System;
using System.Linq;
using System.Reflection;

namespace TCDFx.Tools.DocGen
{
    internal class SinglePageGenerator : MarkdownGenerator
    {
        private const BindingFlags MemberSearchFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        private const string Lang = "csharp";

        public SinglePageGenerator(XmlDocFile xmlDocs, Assembly asm) : base(xmlDocs, asm) { }

        public override void Generate(string outputPath)
        {
            // Write markdown file
            using (MarkdownWriter writer = new MarkdownWriter(outputPath))
            {
                foreach (Type type in Assembly.ExportedTypes.Where(t => !t.Name.StartsWith("_")).OrderBy(t => t.Name))
                {
                    // Pull XML docs for type
                    XmlDocMember typeDocs = XmlDocs[type.GetIDString()];

                    // Header, e.g. "StringBuilder class (System)"
                    writer.WriteHeader(2, Utilities.GetTypeTitle(type, true), true);

                    // Summary and other info
                    PrintObsoleteWarning(type, writer);
                    writer.WriteParagraph($"**Namespace:** {type.Namespace}");
                    writer.WriteParagraph($"**Inheritance:** {Utilities.GetInheritanceString(type)}", true);
                    Summary(typeDocs, writer);

                    // Signature
                    writer.WriteCodeBlock(Lang, Utilities.GetClassSignature(type));

                    // Members
                    WriteConstructors(type, writer);
                    WriteProperties(type, writer);
                    WriteIndexers(type, writer);
                    WriteFields(type, writer);
                    WriteMethods(type, writer);
                }
            }
        }

        private void WriteMethods(Type type, MarkdownWriter writer)
        {
            MethodInfo[] methods = type.GetMethods(MemberSearchFlags)
                // Exclude compiler-generated and internal methods
                .Where(m => !m.IsSpecialName && !m.IsAssembly)
                // Exclude protected methods when class is sealed
                .Where(m => type.IsSealed
                    ? m.IsPublic
                    : !m.IsPrivate)
                // Sort alphabetically...
                .OrderBy(m => m.Name)
                // ... then by parameter count.
                .ThenBy(m => m.GetParameters().Length)
                .ToArray();

            if (methods.Length > 0)
            {
                writer.WriteHeader(3, "Methods");
                foreach (MethodInfo method in methods)
                {
                    XmlDocMember methodDocs = XmlDocs[method.GetIDString()];

                    // Heading
                    writer.WriteHeader(4, Utilities.GetMethodSignature(method, false, false), true);
                    PrintObsoleteWarning(method, writer);
                    Summary(methodDocs, writer);
                    writer.WriteCodeBlock(Lang, Utilities.GetMethodSignature(method, true, true));

                    // Parameters
                    WriteParamList(4, method, writer, methodDocs);
                    Returns(4, methodDocs, writer);
                    Remarks(4, methodDocs, writer);
                }
            }
        }

        private void WriteProperties(Type type, MarkdownWriter writer)
        {
            PropertyInfo[] props = type.GetProperties(MemberSearchFlags)
                // Show protected members if class is not sealed
                .Where(p => type.IsSealed
                    ? (p.CanRead && p.GetMethod.IsPublic) || (p.CanWrite && p.SetMethod.IsPublic)
                    : (p.CanRead && !p.GetMethod.IsPrivate) || (p.CanWrite && !p.SetMethod.IsPrivate))
                // Indexers are technically properties, but we want to handle them separately
                .Where(p => p.GetIndexParameters().Length == 0)
                // Sort alphabetically
                .OrderBy(p => p.Name)
                .ToArray();

            if (props.Length == 0) return;

            writer.WriteHeader(3, "Properties");

            for (int i = 0; i < props.Length; i++)
            {
                XmlDocMember propDocs = XmlDocs[props[i].GetIDString()];
                writer.WriteHeader(4, Utilities.GetPropertySignature(props[i], false, false, false));
                PrintObsoleteWarning(props[i], writer);
                Summary(propDocs, writer);

                writer.WriteCodeBlock(Lang, Utilities.GetPropertySignature(props[i], true, true, true));

                Remarks(5, propDocs, writer);
            }
        }

        private void WriteIndexers(Type type, MarkdownWriter writer)
        {
            PropertyInfo[] indexers = type.GetProperties()
                .Where(p => p.GetIndexParameters().Length > 0)
                .OrderBy(p => p.GetIndexParameters().Length)
                .ToArray();

            if (indexers.Length == 0) return;

            writer.WriteHeader(3, "Indexers");

            for (int i = 0; i < indexers.Length; i++)
            {
                XmlDocMember indexDocs = XmlDocs[indexers[i].GetIDString()];
                writer.WriteHeader(4, Utilities.GetPropertySignature(indexers[i], false, true, false));
                PrintObsoleteWarning(indexers[i], writer);
                Summary(indexDocs, writer);

                writer.WriteCodeBlock(Lang, Utilities.GetPropertySignature(indexers[i], true, true, true));

                Remarks(5, indexDocs, writer);
            }
        }

        private void WriteFields(Type type, MarkdownWriter writer)
        {
            FieldInfo[] fields = type.GetFields()
                .Where(f => !f.IsSpecialName)
                .OrderBy(f => f.Name)
                .ToArray();

            if (fields.Length == 0) return;

            writer.WriteHeader(3, "Fields");

            for (int i = 0; i < fields.Length; i++)
            {
                XmlDocMember fieldDocs = XmlDocs[fields[i].GetIDString()];
                writer.WriteHeader(4, Utilities.GetFieldSignature(fields[i], false));
                PrintObsoleteWarning(fields[i], writer);
                Summary(fieldDocs, writer);

                writer.WriteCodeBlock(Lang, Utilities.GetFieldSignature(fields[i], true));

                Remarks(5, fieldDocs, writer);
            }
        }

        private void PrintObsoleteWarning(MemberInfo member, MarkdownWriter writer)
        {
            ObsoleteAttribute obsAttr = member.GetCustomAttribute<ObsoleteAttribute>();
            if (obsAttr == null) return;
            writer.WriteInfoBox($"**This item is deprecated.**\n{obsAttr.Message}", "warning");
        }

        private void WriteParamList(int rank, MethodBase method, MarkdownWriter writer, XmlDocMember docs)
        {
            ParameterInfo[] plist = method.GetParameters();


            if (method.ContainsGenericParameters)
            {
                Type[] tplist = method.GetGenericArguments();
                writer.WriteParagraph("**Type Parameters**");
                for (int i = 0; i < tplist.Length; i++)
                {
                    writer.WriteLine($"- `{tplist[i].Name}`: {TypeParam(docs, tplist[i].Name)}");
                }
            }

            if (plist.Length > 0)
            {
                writer.WriteParagraph("**Parameters**");
                for (int i = 0; i < plist.Length; i++)
                {
                    writer.WriteLine($"- `{plist[i].Name}`: {Param(docs, plist[i])}");
                }
            }
        }

        private void WriteConstructors(Type type, MarkdownWriter writer)
        {
            // Constructor list
            ConstructorInfo[] ctors = type.GetConstructors();
            if (ctors.Length > 0)
            {
                writer.WriteHeader(3, "Constructors");

                for (int i = 0; i < ctors.Length; i++)
                {
                    // Heading for constructor section
                    XmlDocMember ctorDocs = XmlDocs[ctors[i].GetIDString()];
                    writer.WriteHeader(4, Utilities.GetMethodSignature(ctors[i], false, false));
                    PrintObsoleteWarning(ctors[i], writer);
                    Summary(ctorDocs, writer);

                    // Signature
                    writer.WriteCodeBlock(Lang, Utilities.GetMethodSignature(ctors[i], true, true));

                    // Get constructor's parameters and associated docs
                    WriteParamList(5, ctors[i], writer, ctorDocs);
                    Remarks(5, ctorDocs, writer);
                }
            }
        }

        private static void Summary(XmlDocMember docs, MarkdownWriter writer) => writer.WriteParagraph(docs?.Summary ?? "_No Summary_");

        private static void Remarks(int rank, XmlDocMember docs, MarkdownWriter writer)
        {
            string remarks = docs?.Remarks;
            if (!string.IsNullOrWhiteSpace(remarks))
            {
                writer.WriteHeader(rank, "Remarks");
                writer.WriteParagraph(remarks);
            }
        }

        private static void Returns(int rank, XmlDocMember docs, MarkdownWriter writer)
        {
            string returns = docs?.Returns;
            if (!string.IsNullOrWhiteSpace(returns))
            {
                writer.WriteHeader(rank, "Returns");
                writer.WriteParagraph(returns);
            }
        }

        private static string Param(XmlDocMember docs, ParameterInfo param) => docs?.GetParameterDescription(param.Name) ?? "_No Description_";

        private static string TypeParam(XmlDocMember docs, string name) => docs?.GetTypeParameterDescription(name) ?? "_No Description_";
    }
}