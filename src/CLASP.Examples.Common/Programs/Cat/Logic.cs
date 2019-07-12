
namespace Clasp.Examples.Common.Programs.Cat
{
    using Printable = global::Clasp.Examples.Common.Text.Printable;

    using global::System;
    using global::System.IO;
    using global::System.Linq;

    /// <summary>
    ///  Program logic for <b>cat</b>
    /// </summary>
    public static class Logic
    {
        public static int Run(Arguments prargs, TextWriter nout, TextWriter crout)
        {
            nout.WriteLine("Display (from flags):");
            if (0 != (CatOptions.NumberNonBlankLines & prargs.Options))
            {
                nout.WriteLine("\twill number non-blank lines");
            }
            else if (0 != (CatOptions.NumberLines & prargs.Options))
            {
                nout.WriteLine("\twill number lines");
            }
            if (0 != (CatOptions.SqueezeEmptyAdjacentLines & prargs.Options))
            {
                nout.WriteLine("\twill squeeze empty adjacent lines");
            }
            nout.WriteLine();

            nout.WriteLine("Behaviour (from options):");
            if (!String.IsNullOrWhiteSpace(prargs.EolSequence))
            {
                nout.WriteLine("\tEOL sequence: {0}", String.Join(", ", prargs.EolSequence.Select((ch) => Printable.CharacterRepr(ch))));
            }
            nout.WriteLine();

            nout.WriteLine("Sources:");
            foreach(string path in prargs.InputPaths)
            {
                if ("-" == path)
                {
                    nout.WriteLine("\tsource: would read from standard input stream");
                }
                else
                {
                    nout.WriteLine("\tsource: would read from the path '{0}'", path);
                }
            }
            nout.WriteLine();

            return 0;
        }
    }
}
