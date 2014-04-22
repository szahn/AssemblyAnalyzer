using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace AssemblyAnalyzer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            InitConsole();
            DisplayHelp();
            try
            {
                var assemblyPath = GetAssemblyPath();
                AnalyzePath(assemblyPath);
            }
            catch (Exception ex)
            {
                var foreground = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = foreground;
            }
            WaitForExit();
        }

        private static void InitConsole()
        {
            Console.Title = ".NET Assembly Analyzer";
            Console.Clear();
        }

        private static void DisplayHelp()
        {
            var help = "Produces a list of assemblies with their CPU architectures in given path."
                       + " This can be useful when trying to determine wheather an IIS site, "
                       + "windows service or application requires 64-bit capability or not.";
            Console.Write(help);
            Console.WriteLine();
        }

        private static string GetAssemblyPath()
        {
            Console.WriteLine("Please enter the assembly root path, or the application path to analyze.");
            Console.Write("Application Path: ");
            var assemblyPath = Console.ReadLine();
            if (string.IsNullOrEmpty(assemblyPath))
            {
                throw new Exception("Invalid assembly path.");
            }

            if (!Directory.Exists(assemblyPath))
            {
                throw new Exception("Assembly path not found.");
            }

            return assemblyPath;
        }

        private static void AnalyzePath(string assemblyBasePath)
        {
            var listBuilder = new SortedAssemblyListBuilder();
            listBuilder.BuildAssemblyList(assemblyBasePath);
            AssemblyListPrinter.PrintAssemblyDetails(listBuilder);
        }

        private static void WaitForExit()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            while (!Console.KeyAvailable)
            {
            }
        }
    }
}