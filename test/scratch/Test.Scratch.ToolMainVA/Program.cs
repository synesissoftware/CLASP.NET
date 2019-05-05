
namespace Test.Scratch.ToolMainVA
{
    using global::Clasp;

    using System;

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
