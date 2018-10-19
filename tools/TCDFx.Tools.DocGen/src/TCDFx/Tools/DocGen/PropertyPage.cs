using System.Reflection;

namespace TCDFx.Tools.DocGen
{
    internal class PropertyPage : MarkdownPage
    {
        private readonly PropertyInfo property;

        public PropertyPage(PropertyInfo property, XmlDocMember member) : base(member)
        {
            this.property = property;
            Title = $"{Utilities.GetPropertySignature(property, false, false, false)} property ({Utilities.GetDisplayTitle(property.DeclaringType)})";
        }

        public override void Render(PageTree parent, MarkdownWriter writer)
        {
            writer.WriteHeader(1, Title);
            writer.WriteParagraph(Member?.Summary ?? "_(No Description)_");
            writer.WriteHeader(2, "Signature");
            writer.WriteCodeBlock("csharp", Utilities.GetPropertySignature(property, true, true, true));
        }
    }
}