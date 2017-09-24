
namespace Test.Scratch.ToolMainVA
{
    using SynesisSoftware.SystemTools.Clasp;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        private static readonly Alias[] Aliases =
        {
            Alias.Flag(null, @"--help", @"shows this help and exits")
        };

        static void Main(string[] args)
        {
            Invoker.InvokeMainVA(args, Aliases, ToolMain);
        }

        private static void ToolMain(Arguments args)
        {
        }
    }
}
