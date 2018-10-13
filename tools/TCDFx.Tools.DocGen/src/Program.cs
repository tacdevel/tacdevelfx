using System;
using System.IO;
using System.Linq;
using TCDFx.Tools.DocGen;

internal class Program
{
    // Usage: docgen {outputPath} {xmlFile1} [xmlFile2] ...
    private static void Main(string[] args)
    {
        ParseArguments(args, out string fullOutPath, out string[] xmlFiles);

        foreach (string file in xmlFiles)
        {
            Console.WriteLine($"[DocGen(INF)]: Parsing {file}...");

            //TODO: Read XmlDoc.
            //TODO: Generate Markdown from XmlDoc.
        }
    }

    private static void ParseArguments(string[] args, out string fullOutPath, out string[] xmlFiles)
    {
        Console.WriteLine("[DocGen(INF)]: Arguments:");
        foreach (string arg in args)
            Console.WriteLine($"                 {arg}");

        if (args.Length < 2)
        {
            Console.WriteLine("[DocGen(ERR)]: Missing required arguments.");
            throw new ArgumentException();
        }

        string path;
        if (Directory.Exists(args[0]))
            path = args[0];
        else if (Directory.Exists(Path.GetFullPath(args[0])))
            path = Path.GetFullPath(args[0]);
        else
        {
            Console.WriteLine($"[DocGen(ERR)]: {args[0]} is not a valid directory.");
            throw new ArgumentException();
        }

        if (Directory.EnumerateFileSystemEntries(path).Any())
        {
            Console.WriteLine($"[DocGen(ERR)]: {args[0]} must be an empty directory.");
            throw new ArgumentException();
        }

        // Chop the / off the end, if found. It's not needed, but will cause issues on some platforms due to producing "//" during path concatenation.
        if (path.EndsWith("/"))
            path = path.Substring(0, path.Length - 1);

        fullOutPath = path;

        string[] files = new string[args.Length - 1];
        for (int i = 1; i < args.Length; i++)
        {
            string file;
            if (File.Exists(args[i]))
                file = args[i];
            else if (File.Exists(Path.GetFullPath(args[i])))
                file = Path.GetFullPath(args[i]);
            else
            {
                Console.WriteLine($"[DocGen(ERR)]: {args[i]} does not exist.");
                throw new ArgumentException();
            }
            files[i - 1] = file;
        }

        xmlFiles = files;
    }
}