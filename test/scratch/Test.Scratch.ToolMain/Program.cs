
namespace Test.Scratch.ToolMain
{
    using global::SynesisSoftware.SystemTools.Clasp;

    using System;

    class Program
    {
        private static readonly Specification[] Specifications =
        {
            Specification.Flag(null, @"--help", @"shows this help and exits")
        };

        static int Main(string[] argv)
        {
            return Invoker.ParseAndInvokeMain(argv, Specifications, ToolMain);
        }

        private static int ToolMain(Arguments args)
        {
            return 0;
        }
    }
}
