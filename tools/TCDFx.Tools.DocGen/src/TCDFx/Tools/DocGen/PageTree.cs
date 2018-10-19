using System;
using System.Collections.Generic;
using System.Linq;

namespace TCDFx.Tools.DocGen
{
    internal sealed class PageTree
    {
        private readonly Dictionary<string, PageTree> children;

        public string Name { get; }
        public string Path { get; }

        public MarkdownPage Page { get; set; }

        public PageTree Parent { get; }

        public PageTree(string name)
        {
            Name = name;
            Path = name;
            children = new Dictionary<string, PageTree>();
            Parent = null;
        }

        private PageTree(string name, PageTree parent)
        {
            Name = name;
            Path = $"{parent.Path}/{name}";
            children = new Dictionary<string, PageTree>();
            Parent = parent;
        }

        public MarkdownPage this[string path]
        {
            get
            {
                PageTree child = this;
                IEnumerable<string> parts = path.Split('/').Select(str => str.Trim());
                foreach (string part in parts)
                {
                    if (string.IsNullOrWhiteSpace(part)) return null;
                    if (!child.children.TryGetValue(part, out child)) return null;
                }
                return child.Page;
            }
            set
            {
                PageTree child = this;
                IEnumerable<string> parts = path.Split('/').Select(str => str.Trim());
                foreach (string part in parts)
                {
                    if (string.IsNullOrWhiteSpace(part)) return;
                    if (child.children.TryGetValue(part, out PageTree foundChild))
                        child = foundChild;
                    else
                    {
                        PageTree oldChild = child;
                        child = child.children[part] = new PageTree(part, oldChild);
                    }
                }
                child.Page = value;
                Console.WriteLine(child.Path);
            }
        }

        public PageTree GetNode(string path)
        {
            PageTree child = this;
            IEnumerable<string> parts = path.Split('/').Select(str => str.Trim());
            foreach (string part in parts)
            {
                if (string.IsNullOrWhiteSpace(part)) return null;
                if (!child.children.TryGetValue(part, out child)) return null;
            }
            return child;
        }
    }
}