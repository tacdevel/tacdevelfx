using System.Reflection;

namespace TCDFx.Tools.DocGen
{
    internal class FieldPage : MarkdownPage
    {
        private readonly FieldInfo field;

        public FieldMemberPage(FieldInfo field, XmlDocMember member) : base(member)
        {
            this.field = field;
            Title = $"{Utilities.GetFieldSignature(field, false)} field ({Utilities.GetDisplayTitle(field.DeclaringType)})";
        }

        public override void Render(PageTree parent, MarkdownWriter writer)
        {
            writer.WriteHeader(1, Title);
            writer.WriteParagraph(Member?.Summary ?? "(No Description)");
            writer.WriteHeader(2, "Signature");
            writer.WriteCodeBlock("csharp", Utilities.GetFieldSignature(field, true));
        }
    }
}