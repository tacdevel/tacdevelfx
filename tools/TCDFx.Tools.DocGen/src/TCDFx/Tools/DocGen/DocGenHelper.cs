using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace TCDFx.Tools.DocGen
{
    // See: https://gist.github.com/lontivero/593fc51f1208555112e0
    internal static class XmlToMarkdown
    {
        internal static string ToMarkDown(this XNode e)
        {
            Dictionary<string, string> templates = new Dictionary<string, string>
                {
                    {"doc", "## {0} ##\n\n{1}\n\n"},
                    {"type", "# {0}\n\n{1}\n\n---\n"},
                    {"field", "##### {0}\n\n{1}\n\n---\n"},
                    {"property", "##### {0}\n\n{1}\n\n---\n"},
                    {"method", "##### {0}\n\n{1}\n\n---\n"},
                    {"event", "##### {0}\n\n{1}\n\n---\n"},
                    {"summary", "{0}\n\n"},
                    {"remarks", "\n\n>{0}\n\n"},
                    {"example", "_C# code_\n\n```c#\n{0}\n```\n\n"},
                    {"seePage", "[[{1}|{0}]]"},
                    {"seeAnchor", "[{1}]({0})"},
                    {"param", "|Name | Description |\n|-----|------|\n|{0}: |{1}|\n" },
                    {"exception", "[[{0}|{0}]]: {1}\n\n" },
                    {"returns", "Returns: {0}\n\n"},
                    {"none", ""}
                };
            Func<string, XElement, string[]> d = new Func<string, XElement, string[]>((att, node) => new[]
                {
                    node.Attribute(att).Value,
                    node.Nodes().ToMarkDown()
                });
            Dictionary<string, Func<XElement, IEnumerable<string>>> methods = new Dictionary<string, Func<XElement, IEnumerable<string>>>
                {
                    {"doc", x=> new[]{
                        x.Element("assembly").Element("name").Value,
                        x.Element("members").Elements("member").ToMarkDown()
                    }},
                    {"type", x=>d("name", x)},
                    {"field", x=> d("name", x)},
                    {"property", x=> d("name", x)},
                    {"method",x=>d("name", x)},
                    {"event", x=>d("name", x)},
                    {"summary", x=> new[]{ x.Nodes().ToMarkDown() }},
                    {"remarks", x => new[]{x.Nodes().ToMarkDown()}},
                    {"example", x => new[]{x.Value.ToCodeBlock()}},
                    {"seePage", x=> d("cref", x) },
                    {"seeAnchor", x=> { string[] xx = d("cref", x); xx[0] = xx[0].ToLower(); return xx; }},
                    {"param", x => d("name", x) },
                    {"exception", x => d("cref", x) },
                    {"returns", x => new[]{x.Nodes().ToMarkDown()}},
                    {"none", x => new string[0]}
                };

            string name;
            if (e.NodeType == XmlNodeType.Element)
            {
                XElement el = (XElement)e;
                name = el.Name.LocalName;
                if (name == "member")
                {
                    switch (el.Attribute("name").Value[0])
                    {
                        case 'F': name = "field"; break;
                        case 'P': name = "property"; break;
                        case 'T': name = "type"; break;
                        case 'E': name = "event"; break;
                        case 'M': name = "method"; break;
                        default: name = "none"; break;
                    }
                }
                if (name == "see")
                {
                    bool anchor = el.Attribute("cref").Value.StartsWith("!:#");
                    name = anchor ? "seeAnchor" : "seePage";
                }
                string[] vals = methods[name](el).ToArray();
                string str = "";
                switch (vals.Length)
                {
                    case 1: str = string.Format(templates[name], vals[0]); break;
                    case 2: str = string.Format(templates[name], vals[0], vals[1]); break;
                    case 3: str = string.Format(templates[name], vals[0], vals[1], vals[2]); break;
                    case 4: str = string.Format(templates[name], vals[0], vals[1], vals[2], vals[3]); break;
                }

                return str;
            }

            if (e.NodeType == XmlNodeType.Text)
                return Regex.Replace(((XText)e).Value.Replace('\n', ' '), @"\s+", " ");

            return "";
        }

        internal static string ToMarkDown(this IEnumerable<XNode> es) => es.Aggregate("", (current, x) => current + x.ToMarkDown());

        private static string ToCodeBlock(this string s)
        {
            string[] lines = s.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int blank = lines[0].TakeWhile(x => x == ' ').Count() - 4;
            return string.Join("\n", lines.Select(x => new string(x.SkipWhile((y, i) => i < blank).ToArray())));
        }
    }
}