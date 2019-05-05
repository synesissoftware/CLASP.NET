
// Purpose: Illustrates use of ShowUsage()

namespace Example.ShowUsage
{
    using global::Clasp;
    using UsageUtil = global::Clasp.Util.UsageUtil;

    class Program
    {
        static Specification Flag_Verbose = new FlagSpecification(@"-v", @"--verbose", @"runs with verbose output");

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

        static int Main(string[] argv)
        {
            return Invoker.ParseAndInvokeMain(argv, specifications, (Arguments args) =>
                {
                    return UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ InfoLines = info_lines });
                });
        }
    }
}
