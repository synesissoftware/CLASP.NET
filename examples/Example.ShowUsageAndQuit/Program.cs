
// Purpose: Illustrates use of ShowUsageAndQuit()

namespace Example.ShowUsage
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using UsageUtil = global::SynesisSoftware.SystemTools.Clasp.Util.UsageUtil;

    class Program
    {
        static Specification Flag_Verbose    =   new FlagSpecification(@"-v", @"--verbose", @"runs with verbose output");

        static Specification[] specifications =
        {
            Specification.Section("Verbosity:"),
            Flag_Verbose,

            Specification.Section("Standard:"),
            UsageUtil.Help,
            UsageUtil.Version,
        };

        static string[] info_lines =
        {
            "CLASP.NET examples",
            "",
            ":version:",
            null,
        };

        static void Main(string[] argv)
        {
            Invoker.ParseAndInvokeMainVA(argv, specifications, (Arguments args) =>

                UsageUtil.ShowUsageAndQuit(args, Invoker.Constants.ExitCode_Success, new UsageUtil.UsageParams{ InfoLines = info_lines })
            );
        }
    }
}
