
namespace Test.Scratch.ToolMain
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

        static int Main(string[] argv)
        {
            return Invoker.ParseAndInvokeMain(argv, Aliases, ToolMain);
        }

        private static int ToolMain(Arguments args)
        {
            return 0;
        }
    }
}
