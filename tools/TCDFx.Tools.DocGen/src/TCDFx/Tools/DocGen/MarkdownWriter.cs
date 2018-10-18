using System.IO;
using System.Linq;
using System.Text;

namespace TCDFx.Tools.DocGen
{
    internal class MarkdownWriter : StreamWriter
    {
        public MarkdownWriter(Stream stream) : base(stream) { }

        public MarkdownWriter(Stream stream, Encoding encoding) : base(stream, encoding) { }

        public MarkdownWriter(Stream stream, Encoding encoding, int bufferSize) : base(stream, encoding, bufferSize) { }

        public MarkdownWriter(Stream stream, Encoding encoding, int bufferSize, bool leaveOpen) : base(stream, encoding, bufferSize, leaveOpen) { }

        public MarkdownWriter(string path) : base(path) { }

        public MarkdownWriter(string path, bool append) : base(path, append) { }

        public MarkdownWriter(string path, bool append, Encoding encoding) : base(path, append, encoding) { }

        public MarkdownWriter(string path, bool append, Encoding encoding, int bufferSize) : base(path, append, encoding, bufferSize) { }

        public void WriteHeader(int headerRank, string content, bool escaped = false) => WriteLine($"{new string('#', headerRank)} {(escaped ? Escape(content) : content)}");

        public void WriteLink(string href, string title) => Write($"[{title}]({href})");

        public void WriteHorizontalRule() => Write("\n***\n");

        public void WriteParagraph(string value, bool escaped = false)
        {
            if (value == null) return;
            Write($"{(escaped ? Escape(value) : value)}\n\n");
        }

        public void WriteInfoBox(string msg, string msgType = "info") => Write($"\n!!! {msgType}\n{(msg.Split('\n').Select(ln => "    " + ln).Aggregate((c, n) => $"{c}\n{n}"))}\n\n");

        public void WriteCodeBlock(string lang, string value) => Write($"```{lang}\n{value}\n```\n");

        private static string Escape(string value)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case '>':
                        sb.Append("\\>");
                        break;
                    case '<':
                        sb.Append("\\<");
                        break;
                    default:
                        sb.Append(value[i]);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}