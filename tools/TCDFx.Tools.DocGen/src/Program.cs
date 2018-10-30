using System;
using System.IO;
using System.Reflection;
using System.Xml;
using TCDFx.Tools.DocGen;

internal partial class Program
{
    internal static string TargetAssembly { get; private set; }
    internal static string XmlDocFilePath { get; private set; }
    internal static string OutputPath { get; private set; }
    internal static bool MultiPage { get; private set; } = true;

    private static int Main(string[] unused)
    {
        ErrorCode code = ParseArguments();
        if (code != ErrorCode.Success)
        {
            PrintHelpText();
            return (int)code;
        }

        try
        {
            // Load the assembly.
            Assembly asm = Assembly.LoadFile(TargetAssembly);

            // Load XML document.
            XmlDocFile xmlDocs = null;
            if (XmlDocFilePath != null)
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(XmlDocFilePath);
                xmlDocs = new XmlDocFile(xml);
            }

            // Initialize the generator.
            MarkdownGenerator generator;
            if (MultiPage)
                generator = new MultiPageGenerator(xmlDocs, asm);
            else
                generator = new SinglePageGenerator(xmlDocs, asm);

            // Create output path if it doesn't exist.
            if (!Directory.Exists(Path.GetDirectoryName(OutputPath)))
                Directory.CreateDirectory(OutputPath);

            // Generate documentation.
            generator.Generate(OutputPath);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return (int)ErrorCode.Unknown;
        }

        return (int)ErrorCode.Success;
    }

    private static ErrorCode ParseArguments()
    {
        Arguments args = new Arguments(Arguments.SplitCommandLine());

        if (args.Count < 2) return ErrorCode.MissingArguments;

        if (!args.Exists("asm")) return ErrorCode.MissingArgument;
        if (string.IsNullOrWhiteSpace(args.Single("asm"))) return ErrorCode.NullArgument;
        if (!File.Exists(Path.GetFullPath(args.Single("asm")))) return ErrorCode.InvalidArgument;
        TargetAssembly = Path.GetFullPath(args.Single("asm"));

        if (!args.Exists("out")) return ErrorCode.MissingArgument;
        if (string.IsNullOrWhiteSpace(args.Single("out"))) return ErrorCode.NullArgument;
        OutputPath = Path.GetDirectoryName(args.Single("out"));

        if (args.Exists("xml") && args.IsTrue("noxml")) return ErrorCode.InvalidArgument;
        if (args.Exists("xml"))
        {
            if (string.IsNullOrWhiteSpace(args.Single("xml"))) return ErrorCode.NullArgument;
            if (!File.Exists(Path.GetFullPath(args.Single("xml")))) return ErrorCode.InvalidArgument;
            XmlDocFilePath = Path.GetFullPath(args.Single("xml"));
        }
        else if (args.IsTrue("noxml"))
            XmlDocFilePath = null;
        else return ErrorCode.MissingArgument;

        if (args.IsTrue("onepage"))
            MultiPage = false;
        return ErrorCode.Success;
    }

    private static void PrintHelpText()
    {
        Console.WriteLine();
        Console.WriteLine($"TCDFx Documentation Generator ({typeof(Program).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version})");
        Console.WriteLine();
        Console.WriteLine(@"Syntax:                    docgen [arguments] [flags]");
        Console.WriteLine();
        Console.WriteLine(@"Description:               Generates Markdown API documentation from .NET Core");
        Console.WriteLine(@"                           assemblies, and optionally, the corresponding XML");
        Console.WriteLine(@"                           documentation.");
        Console.WriteLine();
        Console.WriteLine(@"Arguments:");
        Console.WriteLine();
        Console.WriteLine(@"  -asm:<path-to-assembly>  The absolute or relative path of the target assembly.");
        Console.WriteLine();
        Console.WriteLine(@"  -out:<output-path>       The absolute or relative output path where all markdown");
        Console.WriteLine(@"                           will be generated.");
        Console.WriteLine();
        Console.WriteLine(@"  -xml:<path-to-xmldoc>    The absolute or relative path to the pre-generated XML");
        Console.WriteLine(@"                           documentation file.");
        Console.WriteLine();
        Console.WriteLine(@"Flags:");
        Console.WriteLine();
        Console.WriteLine(@"  -noxml                   Specifies not to use XML documentation.");
        Console.WriteLine(@"                           (Omit the '-xml' argument when using this option.");
        Console.WriteLine();
        Console.WriteLine(@"  -onepage                 Specifies to generate the documentation as a single page.");
    }
}