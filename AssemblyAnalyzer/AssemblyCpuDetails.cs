using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssemblyAnalyzer
{
    public class AssemblyCpuDetails
    {
        public string Filename { get; set; }

        public ProcessorArchitecture Architecture { get; set; }
    }
}