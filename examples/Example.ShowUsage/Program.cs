
// Purpose: Illustrates use of ShowUsage()

namespace Example.ShowUsage
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Util;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

    class Program
    {
        static Flag Flag_Verbose    =   new Flag(@"-v", @"--verbose", @"runs with verbose output");

        static Specification[] specifications =
        {
            Specification.Section("Verbosity:"),
            Flag_Verbose,

            Specification.Section("Standard:"),
            UsageUtil.Help,
            UsageUtil.Version,
        };

        static int Main(string[] argv)
        {
            return Invoker.ParseAndInvokeMain(argv, specifications, (Arguments clargs) =>
                {
                    UsageUtil.ShowUsage(clargs.Aliases, System.Console.Out, null);

                    return Invoker.Constants.ExitCode_Success;
                });
        }
    }
}
