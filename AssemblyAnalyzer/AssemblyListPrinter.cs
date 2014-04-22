﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssemblyAnalyzer
{
    public class AssemblyListPrinter
    {
        public static void PrintAssemblyDetails(SortedAssemblyListBuilder listBuilder)
        {
            var list = listBuilder.SortedList.AsEnumerable();
            foreach (var pair in list)
            {
                Console.WriteLine();
                var foreground = Console.ForegroundColor;
                var color = ConsoleColor.DarkGray;
                switch (pair.Value.Architecture)
                {
                    case ProcessorArchitecture.IA64:
                    case ProcessorArchitecture.Amd64:
                        {
                            color = ConsoleColor.Cyan;
                            break;
                        }
                    case ProcessorArchitecture.MSIL:
                        {
                            color = ConsoleColor.Yellow;
                            break;
                        }
                    case ProcessorArchitecture.X86:
                        {
                            color = ConsoleColor.Green;
                            break;
                        }
                }

                Console.ForegroundColor = color;
                var architecture = (pair.Value.Architecture == ProcessorArchitecture.None)
                    ? "Native"
                    : pair.Value.Architecture.ToString();
                Console.Write(architecture);
                Console.ForegroundColor = foreground;
                Console.Write("\t\t");
                Console.Write(pair.Value.Filename);
            }
            Console.WriteLine();
            Console.WriteLine();

            var x64Count = list.Count(l =>
                    l.Value.Architecture == ProcessorArchitecture.Amd64 ||
                    l.Value.Architecture == ProcessorArchitecture.IA64);
            if (x64Count > 0)
            {
                Console.Write("Path contains x64 (64-bit) libraries.");
            }
            else
            {
                Console.Write("Path contains x86 (32-bit) libraries.");
            }
        }
    }
}