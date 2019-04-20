
// Purpose: This example illustrates how to use bound arguments along with
// standard flags --help and --version for a program with usage of:
//
//  { --help | --version | [ --verbose ] <input-file-path> }

namespace Test.Scratch.CLASP.BoundStructure.ns_2
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Binding;
    using global::SynesisSoftware.SystemTools.Clasp.Util;

    using global::System;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

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

        internal static readonly Flag       Flag_Verbose    =   new Flag(@"-v", @"--verbose", @"runs with verbose output");

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

                // Options:
                //
                //  1. 

                return Invoker.ParseAndInvokeMain(argv, Specifications, (Arguments clargs) =>
                    {
                        // check '--help'

                        if(clargs.HasFlag(UsageUtil.Help))
                        {
                            UsageUtil.ShowVersion(Specifications, null);

                            Console.Out.WriteLine();
                            Console.Out.WriteLine(@"USAGE: {0} {{ --help | --version | [ --verbose ] <input-file-path> }}", UsageUtil.InferProgramName(null));
                            Console.Out.WriteLine();
                            Console.Out.WriteLine(@"Flags & options:");
                            Console.Out.WriteLine();

                            UsageUtil.ShowUsage(Specifications, Console.Out, null);

                            return 0;
                        }

                        // check '--version'

                        if(clargs.HasFlag(UsageUtil.Version))
                        {
                            UsageUtil.ShowVersion(Specifications, null);

                            return 0;
                        }

                        return Invoker.InvokeMainWithBoundArgumentOfType<ProgramArguments>(clargs, (ProgramArguments prargs, Arguments clargs_) =>
                            {
                                return Invoker.Constants.ExitCode_Success;
                            });
                    });
            }
            catch(Exception x)
            {
                Console.Error.WriteLine(@"{0}: exception ({1}): {2}", UsageUtil.InferProgramName(null), x.GetType().FullName, x.Message);

                return 1;
            }
        }
    }
}
