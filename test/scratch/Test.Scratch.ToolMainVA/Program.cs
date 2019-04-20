
namespace Test.Scratch.ToolMainVA
{
    using SynesisSoftware.SystemTools.Clasp;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

    class Program
    {
        private static readonly Specification[] Specifications =
        {
            Specification.Flag(null, @"--help", @"shows this help and exits")
        };

        static void Main(string[] argv)
        {
            Invoker.ParseAndInvokeMainVA(argv, Specifications, ToolMain);
        }

        private static void ToolMain(Arguments args)
        {
        }
    }
}
