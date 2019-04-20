
// Purpose: Illustrates use of ShowUsageAndQuit()

namespace Example.ShowUsage
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Util;

    class Program
    {
        static Flag Flag_Verbose    =   new Flag(@"-v", @"--verbose", @"runs with verbose output");

        static Alias[] specifications =
        {
            Alias.Section("Verbosity:"),
            Flag_Verbose,

            Alias.Section("Standard:"),
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
