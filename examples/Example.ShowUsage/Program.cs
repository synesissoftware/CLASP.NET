
// Purpose: Illustrates use of ShowUsage()

// Created: 14th October 2017
// Updated: 14th October 2017

namespace Example.ShowUsage
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Util;

    class Program
    {
        static Flag Flag_Verbose    =   new Flag(@"-v", @"--verbose", @"runs with verbose output");

        static Alias[] aliases =
        {
            Alias.Section("Verbosity:"),
            Flag_Verbose,

            Alias.Section("Standard:"),
            UsageUtil.Help,
            UsageUtil.Version,
        };

        static int Main(string[] args)
        {
            return Invoker.InvokeMain(args, aliases, (Arguments clargs) =>
                {
                    UsageUtil.ShowUsage(clargs.Aliases, System.Console.Out, null);

                    return Invoker.Constants.ExitCode_Success;
                });
        }
    }
}
