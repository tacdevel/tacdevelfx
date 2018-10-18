using System;
using System.Linq;
using System.Text;

namespace TCDFx.Tools.DocGen
{
    internal class TypePage : MarkdownPage
    {
        public Type Type { get; }

        public TypePage(Type type, XmlDocMember member) : base(member)
        {
            Type = type;

            string name = Utilities.GetDisplayTitle(type, false);

            if (type.IsEnum)
                Title = $"{name} Enum";
            else if (type.IsInterface)
                Title = $"{name} Interface";
            else if (type.IsValueType)
                Title = $"{name} Struct";
            else if (type.IsSubclassOf(typeof(Delegate)))
                Title = $"{name} Delegate";
            else
                Title = $"{name} Class";
        }

        public override void Render(PageTree parent, MarkdownWriter writer)
        {
            writer.WriteHeader(1, Title);
            writer.WriteParagraph($"**Namespace:** {Type.Namespace}");
            string inheritance = Utilities.GetInheritanceString(Type);
            if (!string.IsNullOrEmpty(inheritance))
                writer.WriteParagraph($"**Inheritance:** {inheritance}");
            writer.WriteParagraph(Member?.Summary ?? "(No Description)");
            writer.WriteHeader(2, "Signature");
            writer.WriteCodeBlock("csharp", Utilities.GetClassSignature(Type));

            System.Reflection.MethodInfo[] methods = Type.GetMethods().OrderBy(m => m.Name).ToArray();
            if (methods.Length > 0)
            {
                writer.WriteHeader(2, "Methods");
                foreach (IGrouping<string, System.Reflection.MethodInfo> methodGroup in methods.Where(m => !m.IsSpecialName).GroupBy(m => m.Name))
                {
                    StringBuilder sbLink = new StringBuilder($"- [{Utilities.GetIdentifier(methodGroup.Key)}]({Utilities.GetURLTitle(Type)}/{Utilities.GetIdentifier(methodGroup.Key)}.md)");
                    bool isStatic = methodGroup.All(m => m.IsStatic);
                    if (isStatic) sbLink.Append(" (static)");
                    writer.WriteLine(sbLink.ToString());
                }
            }

            System.Reflection.PropertyInfo[] props = Type.GetProperties()
                .Where(p => p.GetIndexParameters().Length == 0)
                .OrderBy(p => p.Name)
                .ToArray();
            if (props.Length > 0)
            {
                writer.WriteHeader(2, "Properties");
                foreach (System.Reflection.PropertyInfo prop in props)
                {
                    StringBuilder sbLink = new StringBuilder($"- [{Utilities.GetIdentifier(prop.Name)}]({Utilities.GetURLTitle(Type)}/{Utilities.GetIdentifier(prop.Name)}.md)");
                    bool isStatic = prop.CanRead && prop.GetGetMethod(true).IsStatic || prop.CanWrite && prop.GetSetMethod(true).IsStatic;
                    if (isStatic) sbLink.Append(" (static)");
                    writer.WriteLine(sbLink.ToString());
                }
            }

            System.Reflection.FieldInfo[] fields = Type.GetFields()
                .Where(f => !f.IsSpecialName)
                .OrderBy(f => f.Name)
                .ToArray();
            if (fields.Length > 0)
            {
                writer.WriteHeader(2, "Fields");
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    StringBuilder sbLink = new StringBuilder($"- [{Utilities.GetIdentifier(field.Name)}]({Utilities.GetURLTitle(Type)}/{Utilities.GetIdentifier(field.Name)}.md)");
                    if (field.IsStatic) sbLink.Append(" (static)");
                    writer.WriteLine(sbLink.ToString());
                }
            }

            // TODO: Events
            /*
            if (events.Length > 0)
            {
                writer.WriteHeader(2, "Events");
                foreach (System.Reflection.EventInfo event in events)
                {
                }
            }
            */

            System.Reflection.MethodInfo[] operators = Type.GetMethods()
                .Where(m => m.Name.StartsWith("op_") && m.Name != "op_Explicit" && m.Name != "op_Implicit")
                .ToArray();
            if (operators.Length > 0)
            {
                writer.WriteHeader(2, "Operators");
                foreach (System.Reflection.MethodInfo op in operators)
                    writer.WriteLine($"- [{Utilities.GetOperatorSymbol(op.Name)}]({Utilities.GetURLTitle(Type)}/{op.Name}.md)");
            }

            writer.WriteHeader(2, "Conversions");
        }
    }
}