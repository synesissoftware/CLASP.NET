
// Purpose: This example illustrates how to use bound arguments along with
// standard flags --help and --version for a program with usage of:
//
//  { --help | --version | [ --verbose ] <input-file-path> }

namespace Test.Scratch.CLASP.BoundStructure.ns_2
{
    using global::Clasp;
    using global::Clasp.Binding;
    using global::Clasp.Util;

    using global::System;

    [BoundType]
    struct ProgramArguments
    {
        [BoundFlag(Program.Constants.Specifications.Flag_Verbose_ResolvedName)]
        public bool Verbose;

        [BoundValue(0, UsageLabel=@"<input-file-path>")]
        public string InputFilePath;

        #region implementation

        internal void SuppressWarningCS0469()
        {
            Verbose         =   false;
            InputFilePath   =   "";
        }
        #endregion
    }

    class Program
    {
        public static class Constants
        {
            public static class Specifications
            {
                public const string Flag_Verbose_ResolvedName       =   @"--verbose";
            }
        }

        internal static readonly Specification       Flag_Verbose    =   new FlagSpecification(@"-v", @"--verbose", @"runs with verbose output");

        internal static readonly Specification[]    Specifications         =
        {
            Specification.Section("Behaviour:"),
            Flag_Verbose,

            Specification.Section(@"Standard:"),
            UsageUtil.Help,
            UsageUtil.Version,
        };

        static int Main(string[] argv)
        {
            try
            {

                return Invoker.ParseAndInvokeMain(argv, Specifications, (Arguments args) =>
                    {
                        // check '--help'

                        if (args.HasFlag(UsageUtil.Help))
                        {
                            UsageUtil.UsageParams ups = new UsageUtil.UsageParams{ ValuesString = "<input-file-path>" };

                            return UsageUtil.ShowUsage(args, ups, null);
                        }

                        // check '--version'

                        if (args.HasFlag(UsageUtil.Version))
                        {
                            return UsageUtil.ShowVersion();
                        }

                        return Invoker.InvokeMainWithBoundArgumentOfType<ProgramArguments>(args, (ProgramArguments prargs, Arguments clargs_) =>
                            {
                                Console.Out.WriteLine("Verbose: {0}", prargs.Verbose);

                                Console.Out.WriteLine("Input file path: {0}", prargs.InputFilePath);

                                return Invoker.Constants.ExitCode_Success;
                            });
                    });
            }
            catch (Exception x)
            {
                Console.Error.WriteLine(@"{0}: exception ({1}): {2}", UsageUtil.InferProgramName(null), x.GetType().FullName, x.Message);

                return 1;
            }
        }
    }
}
