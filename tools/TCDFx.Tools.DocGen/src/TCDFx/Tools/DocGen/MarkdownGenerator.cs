using System.Reflection;

namespace TCDFx.Tools.DocGen
{
    internal abstract class MarkdownGenerator
    {
        public MarkdownGenerator(XmlDocFile docs, Assembly asm)
        {
            XmlDocs = docs;
            Assembly = asm;
        }

        public XmlDocFile XmlDocs { get; }
        public Assembly Assembly { get; }

        public abstract void Generate(string outputPath);
    }
}