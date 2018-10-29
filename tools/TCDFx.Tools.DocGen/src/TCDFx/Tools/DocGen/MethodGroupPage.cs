using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TCDFx.Tools.DocGen
{
    internal class MethodGroupPage : MarkdownPage
    {
        private readonly Type type;
        private readonly Dictionary<MethodInfo, XmlDocMember> _methodData;

        public MethodGroupPage(Type type, string name, Dictionary<MethodInfo, XmlDocMember> methodData) : base(null)
        {
            this.type = type;
            _methodData = methodData;
            Title = $"{Utilities.GetDisplayTitle(type)}.{Utilities.GetIdentifier(name)} method";
        }

        public override void Render(PageTree parent, MarkdownWriter writer)
        {
            writer.WriteHeader(1, Title);
            foreach (KeyValuePair<MethodInfo, XmlDocMember> data in _methodData.OrderBy(m => m.Key.GetParameters().Length))
            {
                MethodInfo method = data.Key;
                XmlDocMember docs = data.Value;

                writer.WriteHeader(2, Utilities.GetMethodSignature(method, false, false));
                writer.WriteParagraph(docs?.Summary);
                writer.WriteHeader(3, "Signature");
                writer.WriteCodeBlock("csharp", Utilities.GetMethodSignature(method, true, true));

                if (docs != null)
                {
                    if (docs.HasTypeParameters)
                    {
                        writer.WriteHeader(3, "Type Parameters");
                        foreach (Type tp in method.GetGenericArguments())
                        {
                            string desc = docs?.GetTypeParameterDescription(tp.Name) ?? "_(No Description)_";
                            writer.WriteLine($"- `{tp.Name}`: {desc}");
                        }
                        writer.WriteLine();
                    }

                    if (docs.HasParameters)
                    {
                        writer.WriteHeader(3, "Parameters");
                        foreach (ParameterInfo p in method.GetParameters())
                        {
                            string desc = docs?.GetParameterDescription(p.Name) ?? "_(No Description)_";
                            writer.WriteLine($"- `{p.Name}`: {desc}");
                        }
                        writer.WriteLine();
                    }
                }
            }
        }
    }
}