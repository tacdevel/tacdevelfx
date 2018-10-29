using System.Collections.Generic;
using System.Xml;

namespace TCDFx.Tools.DocGen
{
    internal class XmlDocFile
    {
        private readonly Dictionary<string, XmlDocMember> docs;

        public XmlDocFile(XmlDocument xml)
        {
            docs = new Dictionary<string, XmlDocMember>();
            Xml = xml;

            foreach (XmlNode item in xml.SelectNodes("//doc/members/member"))
            {
                string key = item.Attributes["name"].Value;
                XmlDocMember member = new XmlDocMember(item);

                foreach (XmlNode param in item.SelectNodes("param"))
                    member.SetParameterDescription(param.Attributes["name"].Value, param.InnerText.Trim());

                foreach (XmlNode typeparam in item.SelectNodes("typeparam"))
                    member.SetTypeParameterDescription(typeparam.Attributes["name"].Value, typeparam.InnerText.Trim());

                docs.Add(key, member);
            }
        }

        public XmlDocument Xml { get; }

        public XmlDocMember this[string memberId] => docs.TryGetValue(memberId, out XmlDocMember memberDocs) ? memberDocs : null;
    }
}