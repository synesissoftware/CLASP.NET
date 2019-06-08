
// Purpose: Illustrates use of ShowBoundUsage()

namespace Example.ShowBoundUsage
{
    using global::Clasp;
    using UsageUtil = global::Clasp.Util.UsageUtil;

    class Program
    {
        struct Arguments
        {
            [Clasp.Binding.BoundFlag(@"--verbose", Alias=@"-v", HelpDescription=@"runs with verbose output", HelpSection=@"Verbosity:")]
            public bool Verbose;

            #region implementation
            internal void _suppress_warning_CS0469()
            {
                this.Verbose = false;
            }
            #endregion
        }

        static Specification[] specifications =
        {
            Specification.Section("Verbosity:"),

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
            return Invoker.ParseAndInvokeMain(argv, specifications, (Clasp.Arguments args) => {

                return UsageUtil.ShowBoundUsage<Program.Arguments>(args, new UsageUtil.UsageParams{ InfoLines = info_lines });
            });
        }
    }
}
