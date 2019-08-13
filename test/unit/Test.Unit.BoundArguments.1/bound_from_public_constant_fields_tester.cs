
namespace Test.Unit.BoundArguments.ns_1
{
    using global::Clasp.Binding;
    using UsageUtil = global::Clasp.Util.UsageUtil;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    [TestClass]
    public class bound_from_public_constant_fields_tester
    {
        public class Constants
        {
            public class Defaults
            {
                public const int    Count       =   1000;

                public const int    Port        =   6789;
            }

            public class Sections
            {
                public const string Behaviour   =   @"Behaviour:";

                public const string Standard    =   @"Standard:";
            }

            public class Arguments
            {
                public class Count
                {
                    public static readonly string ResolvedName  =   "--count";
                    public const string Alias                   =   "-c";
                    public const string HelpDescription         =   "specifies the number of pings";
                    public const string HelpSection             =   Sections.Behaviour;

                    public const int    DefaultValue            =   Defaults.Count;
                }

                public class Port
                {
                    public const string ResolvedName            =   "--port";
                    public static readonly string Alias         =   "-p";
                    public const string HelpDescription         =   "specifies the port";
                    public const string HelpSection             =   Sections.Behaviour;

                    public const int    DefaultValue            =   Defaults.Port;
                }

                public class Verbose
                {
                    public const string ResolvedName            =   "--verbose";
                    public const string Alias                   =   "-v";
                    public const string HelpDescription         =   "runs with verbose output";
                    public static readonly string HelpSection   =   Sections.Behaviour;
                }
            }
        }

        internal class Arguments
        {
            [BoundValue(0, ValuesStringFragment = "<path>")]
            public string Path;

            [BoundOption(typeof(Constants.Arguments.Count))]
            public int Count;

            [BoundOption(typeof(Constants.Arguments.Port))]
            public int Port;

            [BoundFlag(typeof(Constants.Arguments.Verbose))]
            public bool Verbose;

            #region implementation

            internal void _suppress_0649()
            {
                this.Path = "";
                this.Count = -1;
                this.Port = -1;
                this.Verbose = false;
            }
            #endregion
        }

        #region fields

        private Assembly    assembly;
        #endregion

        [TestInitialize]
        public void Setup()
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        [TestMethod]
        public void test_1()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Specification[] specifications = {

                Clasp.Specification.Section(Constants.Sections.Behaviour),

                Clasp.Specification.Section(Constants.Sections.Standard),

                UsageUtil.Help,
                UsageUtil.Version,
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowBoundUsage<Arguments>(args, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Select((line) => line.Trim()).Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog [ ... flags and options ... ] <path>",
                    "flags/options:",
                    "Behaviour:",
                    "-c <value>",
                    "--count=<value>",
                    "specifies the number of pings",
                    "-p <value>",
                    "--port=<value>",
                    "specifies the port",
                    "-v",
                    "--verbose",
                    "runs with verbose output",
                    "Standard:",
                    "--help",
                    "shows this help and terminates",
                    "--version",
                    "shows version information and terminates",
                }
            , actual
            );
        }
    }
}
