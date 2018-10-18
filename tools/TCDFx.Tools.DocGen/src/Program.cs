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
            XmlDocument xml = new XmlDocument();
            xml.Load(XmlDocFilePath);
            XmlDocFile xmlDocs = new XmlDocFile(xml);

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
        Console.WriteLine("TCDFx Documentation Generator - Help Text");
        Console.WriteLine("=========================================");
        Console.WriteLine();
        Console.WriteLine(@"Usage: dotnet docgen -asm:Path\To\Assembly -out:Path\To\Assembly -xml:Path\To\Xml [-noxml] [-onepage]");
        Console.WriteLine();
        Console.WriteLine(@"Options:  -asm        The path to the assembly.");
        Console.WriteLine(@"Options:  -out        The output path.");
        Console.WriteLine(@"Options:  -xml        The path to the xmldoc file.");
        Console.WriteLine(@"Options:  -noxml      Specifies not to use xmldoc. (Omit the '-xml' option when using this option.");
        Console.WriteLine(@"Options:  -onepage    Specifies to generate the documentation as one page.");
    }
}