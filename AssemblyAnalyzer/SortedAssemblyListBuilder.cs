using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssemblyAnalyzer
{
    public class SortedAssemblyListBuilder
    {
        private readonly SortedList<string, AssemblyCpuDetails> sortedList = new SortedList<string, AssemblyCpuDetails>();

        public SortedList<string, AssemblyCpuDetails> SortedList
        {
            get { return sortedList; }
        }

        public void BuildAssemblyList(string assemblyBasePath)
        {
            var files = GetAssemblyFiles(assemblyBasePath);
            for (var f = 0; f < files.Count; f++)
            {
                var assemblyFile = files[f];
                var assemblyFilename = Path.GetFileName(assemblyFile);
                ProcessorArchitecture processor;
                try
                {
                    var assembly = AssemblyName.GetAssemblyName(assemblyFile);
                    processor = assembly.ProcessorArchitecture;
                }
                catch (Exception ex)
                {
                    processor = ProcessorArchitecture.None;
                }

                var key = string.Concat(processor, f);
                sortedList.Add(key, new AssemblyCpuDetails
                {
                    Filename = Path.GetFileName(assemblyFile),
                    Architecture = processor
                });
            }
        }

        private static List<string> GetAssemblyFiles(string assemblyBasePath)
        {
            var files = new List<string>();
            files.AddRange(Directory.GetFiles(assemblyBasePath, "*.dll"));
            files.AddRange(Directory.GetFiles(assemblyBasePath, "*.exe"));
            return files;
        }
    }
}