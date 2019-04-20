
// Purpose: Illustrates use of ShowUsageAndQuit()

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

        static void Main(string[] argv)
        {
            Invoker.ParseAndInvokeMainVA(argv, specifications, (Arguments clargs) =>

                    UsageUtil.ShowUsageAndQuit(clargs, Invoker.Constants.ExitCode_Success, null)
                );
        }
    }
}
