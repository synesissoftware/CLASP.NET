
// Created: 
// Updated: 24th September 2017

namespace Test.Scratch.CLASP.BoundStructure.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Binding;

    using System;

    [BoundType]
    class ArgumentSet
    {
        [BoundFlag("--verbose")]
        public bool Verbose;

        [BoundOption("--length", AllowNegative=false, AllowFraction=false, DefaultValue=0)]
        public int Length;

        [BoundOption("--name", AllowEmpty=false, DefaultValue="")]
        public string Name;

        [BoundOption("--height", DefaultValue=0.0)]
        public double Height;

        internal void DummyMethodToSilenceCS0649()
        {
            Verbose = false;
            Length = 0;
            Name = "";
            Height = 0;
        }

        public override string ToString()
        {
            return String.Format("{{ Name='{0}', Height={1}, Length={2}, Verbose={3} }}", Name, Height, Length, Verbose);
        }
    }

    class Program
    {
        internal static readonly Alias[]    Aliases =
        {
        };

        static int Main(string[] args)
        {
            return Invoker.InvokeMainAndParseBoundArgumentOfType<ArgumentSet>(args, Aliases, ToolMain);
        }

        private static int ToolMain(ArgumentSet argSet, Arguments args)
        {
            Console.Out.WriteLine("argSet={{{0}}}, args={{{1}}}", argSet, args);

            return 0;
        }
    }
}
