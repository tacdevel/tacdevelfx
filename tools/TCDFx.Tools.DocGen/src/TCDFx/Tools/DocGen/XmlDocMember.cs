using System.Collections.Generic;
using System.Xml;

namespace TCDFx.Tools.DocGen
{
    public sealed class XmlDocMember
    {
        private readonly Dictionary<string, string> param = new Dictionary<string, string>();
        private readonly Dictionary<string, string> typeParam = new Dictionary<string, string>();

        public XmlDocMember(XmlNode node)
        {
            Summary = node.SelectSingleNode("summary")?.InnerText.Trim() ?? "(No Description)";
            Returns = node.SelectSingleNode("returns")?.InnerText.Trim() ?? string.Empty;
            Remarks = node.SelectSingleNode("remarks")?.InnerText.Trim() ?? string.Empty;
        }

        public string Summary { get; set; }
        public string Returns { get; set; }
        public string Remarks { get; set; }

        public bool HasParameters => param.Count > 0;
        public bool HasTypeParameters => typeParam.Count > 0;

        public void SetParameterDescription(string name, string description) => param[name] = description;
        public string GetParameterDescription(string name) => param.TryGetValue(name, out string desc) ? desc : "(No Description)";

        public void SetTypeParameterDescription(string name, string description) => typeParam[name] = description;
        public string GetTypeParameterDescription(string name) => typeParam.TryGetValue(name, out string desc) ? desc : "(No Description)";
    }
}