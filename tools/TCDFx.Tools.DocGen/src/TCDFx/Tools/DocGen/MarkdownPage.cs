namespace TCDFx.Tools.DocGen
{
    internal abstract class MarkdownPage
    {
        public MarkdownPage(XmlDocMember member) => Member = member;

        public XmlDocMember Member { get; }
        public string Title { get; protected set; }

        public abstract void Render(PageTree parent, MarkdownWriter writer);
    }
}