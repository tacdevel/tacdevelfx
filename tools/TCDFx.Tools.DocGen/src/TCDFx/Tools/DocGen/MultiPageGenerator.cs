using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TCDFx.Tools.DocGen
{
    internal class MultiPageGenerator : MarkdownGenerator
    {
        public MultiPageGenerator(XmlDocFile xmlDocs, Assembly asm) : base(xmlDocs, asm)
        {
        }

        public override void Generate(string outputPath)
        {
            PageTree pages = new PageTree("docs");
            List<PageTree> PageTrees = new List<PageTree>();

            foreach (System.Type type in Assembly.GetExportedTypes())
            {
                string typePath = $"{type.Namespace.Replace('.', '/')}/{Utilities.GetURLTitle(type)}";
                XmlDocMember typeData = XmlDocs[type.GetIDString()];

                pages[typePath] = new TypePage(type, typeData);
                PageTrees.Add(pages.GetNode(typePath));

                // Constructors
                ConstructorInfo[] ctors = type.GetConstructors();
                if (ctors.Length > 0)
                {
                    int ctorNum = 1;
                    foreach (ConstructorInfo ctor in ctors)
                    {
                        string ctorPath = $"{typePath}/new/{ctorNum}";
                        XmlDocMember methodData = XmlDocs[ctor.GetIDString()];
                        // TODO: Generate constructor pages
                        ctorNum++;
                    }
                }

                // Method groups
                foreach (IGrouping<string, MethodInfo> methodGroup in type.GetMethods()
                    .Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_"))
                    .GroupBy(m => m.Name))
                {
                    // Path to method group
                    string methodGroupPath = $"{typePath}/{methodGroup.Key}";

                    // Map of reflected methods and documentation
                    Dictionary<MethodInfo, XmlDocMember> methods = new Dictionary<MethodInfo, XmlDocMember>();

                    foreach (MethodInfo method in methodGroup)
                    {
                        XmlDocMember methodData = XmlDocs[method.GetIDString()];
                        methods[method] = methodData;
                    }

                    pages[methodGroupPath] = new MethodGroupPage(type, methodGroup.Key, methods);
                    PageTrees.Add(pages.GetNode(methodGroupPath));
                }

                // Fields
                foreach (FieldInfo field in type.GetFields().Where(f => (f.IsPublic || !f.IsPrivate) && (!f.DeclaringType.IsEnum || !f.IsSpecialName)))
                {
                    string fieldPath = Path.Combine(typePath, field.Name).Replace('\\', '/');
                    XmlDocMember fieldData = XmlDocs[field.GetIDString()];
                    pages[fieldPath] = new FieldPage(field, fieldData);
                    PageTrees.Add(pages.GetNode(fieldPath));
                }

                // Properties and Indexers
                int numIndexers = 0;
                foreach (PropertyInfo property in type.GetProperties())
                {
                    XmlDocMember propData = XmlDocs[property.GetIDString()];

                    string propPath;
                    if (property.GetIndexParameters().Length > 0)
                        propPath = $"{typePath}/this/{++numIndexers}";
                    else
                        propPath = $"{typePath}/{property.Name}";

                    pages[propPath] = new PropertyPage(property, propData);
                    PageTrees.Add(pages.GetNode(propPath));
                }
            }

            // Create a task for each document that needs to be exported, run them all at once
            Task[] exportTasks = new Task[PageTrees.Count];
            for (int i = 0; i < PageTrees.Count; i++)
            {
                PageTree node = PageTrees[i];
                exportTasks[i] = Task.Run(() =>
                {
                    string documentDir = Directory.GetParent($"{outputPath}/{node.Path}").FullName;
                    string documentPath = $"{outputPath}/{node.Path}.md";
                    Directory.CreateDirectory(documentDir);
                    using (MarkdownWriter writer = new MarkdownWriter(documentPath))
                    {
                        node.Page.Render(node, writer);
                    }
                });
            }

            // Wait for all export tasks to finish
            Task.WaitAll(exportTasks);
        }
    }
}