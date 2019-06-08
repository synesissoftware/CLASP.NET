
namespace Example.BoundValues.cat
{
    using System;
    using System.Linq;

    class Program
    {
        // This program will be a pretend tool that has the same/compatible
        // CLI to the UNIX tool cat. For pedagogical purposes, it has a
        // limited number of flags, and it requires at least one value,
        // although that can be "-" to indicate reading from stdin

        [Flags]
        enum CatOptions
        {
            None                        =   0x00000000,

            NumberLines                 =   0x00000001,

            NumberNonBlankLines         =   0x00000002,

            SqueezeEmptyAdjacentLines   =   0x00000010,
        }

        [Clasp.Binding.BoundType(ParsingOptions=Clasp.ParseOptions.TreatSinglehyphenAsValue)]
        struct Arguments
        {
            /// <summary>
            ///  Array of input paths, which will be non-<c>null</c> and will
            ///  have at least one element
            /// </summary>
            [Clasp.Binding.BoundValues(Minimum=1)]
            public string[] InputPaths;

            /// <summary>
            ///  Combination of <see cref="CatOptions"/> from CLI flags
            /// </summary>
            [Clasp.Binding.BoundEnumeration(typeof(CatOptions))]
            [Clasp.Binding.BoundEnumerator("--show-lines", (int)CatOptions.NumberLines, Alias="-n", HelpSection="display:", HelpDescription="shows the number of lines")]
            [Clasp.Binding.BoundEnumerator("--show-blanks", (int)CatOptions.NumberNonBlankLines, Alias="-b", HelpSection="display:", HelpDescription="shows the number of non-blank lines")]
            [Clasp.Binding.BoundEnumerator("--squeeze-empty", (int)CatOptions.SqueezeEmptyAdjacentLines, Alias="-s", HelpSection="display:", HelpDescription="squeeze adjacent empty lines into one")]
            public CatOptions Options;

            [Clasp.Binding.BoundOption("--eol-sequence", Alias="-E", HelpSection="behaviour:", HelpDescription="specifies the end-of-line-sequence")]
            public string EolSequence;

            #region implementation
            internal void _suppress_warning_CS0469()
            {
                this.InputPaths = null;
                this.Options = CatOptions.None;
                this.EolSequence = null;
            }
            #endregion
        }

        static int Main(string[] argv)
        {
            try
            {
                var specifications = new Clasp.Specification[] {

                    Clasp.Specification.Section("display:"),

                    Clasp.Specification.Section("behaviour:"),

                    Clasp.Specification.Section("standard:"),

                    Clasp.Util.UsageUtil.Help,
                    Clasp.Util.UsageUtil.Version,
                };

                return Clasp.Invoker.ParseAndInvokeMain<Program.Arguments>(argv, specifications, (Clasp.Arguments clargs) => {

                    if (clargs.HasFlag("--help"))
                    {
                        return Clasp.Util.UsageUtil.ShowBoundUsage<Program.Arguments>(clargs, null);
                    }

                    if (clargs.HasFlag("--version"))
                    {
                        return Clasp.Util.UsageUtil.ShowVersion(clargs, null);
                    }

                    return Clasp.Invoker.InvokeMainWithBoundArgumentOfType<Program.Arguments>(clargs, (Program.Arguments prargs, Clasp.Arguments _) => {

                        Console.Out.WriteLine("Display (from flags):");
                        if(0 != (Program.CatOptions.NumberNonBlankLines & prargs.Options))
                        {
                            Console.Out.WriteLine("\twill number non-blank lines");
                        }
                        else if(0 != (Program.CatOptions.NumberLines & prargs.Options))
                        {
                            Console.Out.WriteLine("\twill number lines");
                        }
                        if(0 != (Program.CatOptions.SqueezeEmptyAdjacentLines & prargs.Options))
                        {
                            Console.Out.WriteLine("\twill squeeze empty adjacent lines");
                        }
                        Console.Out.WriteLine();

                        Console.Out.WriteLine("Behaviour (from options):");
                        if(!String.IsNullOrWhiteSpace(prargs.EolSequence))
                        {
                            Console.Out.WriteLine("\tEOL sequence: {0}", prargs.EolSequence.Select((ch) => (int)ch));
                        }
                        Console.Out.WriteLine();

                        Console.Out.WriteLine("Sources:");
                        foreach(string path in prargs.InputPaths)
                        {
                            if("-" == path)
                            {
                                Console.Out.WriteLine("\tsource: would read from standard input stream");
                            }
                            else
                            {
                                Console.Out.WriteLine("\tsource: would read from the path '{0}'", path);
                            }
                        }
                        Console.Out.WriteLine();

                        return 0;
                    });
                });
            }
            catch (System.OutOfMemoryException)
            {
                throw;
            }
            catch (System.Exception x)
            {
                System.Console.Error.WriteLine("{0}: exception({1}): {2}", Clasp.Arguments.ProgramName, x.GetType(), x);

                return 1; // Non-normative exit code
            }
        }
    }
}
