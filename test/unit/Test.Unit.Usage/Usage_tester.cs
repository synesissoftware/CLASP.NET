
namespace Test.Unit.Usage
{
    using UsageUtil = global::Clasp.Util.UsageUtil;

    using global::Clasp.Util;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    [TestClass]
    public class Usage_tester
    {
        #region fields

        private Assembly    assembly;
        #endregion

        [TestInitialize]
        public void Setup()
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        [TestMethod]
        public void Test_no_specs()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog [ ... flags and options ... ]",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_no_specs_with_no_flagsandoptions()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ ValuesString = "" }, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_no_specs_with_no_flagsandoptions_but_value()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ ValuesString = "<input-path>" }, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog <input-path>",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_specs_with_default_usage_info()
        {
            Clasp.FlagSpecification Flag_Verbose = new Clasp.FlagSpecification(@"-v", @"--verbose", @"runs with verbose output");

            Clasp.Specification[] specifications =
            {
                Clasp.Specification.Section("Verbosity:"),
                Flag_Verbose,

                Clasp.Specification.Section("Standard:"),
                UsageUtil.Help,
                UsageUtil.Version,
            };

            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog [ ... flags and options ... ]",
                    "flags/options:",
                    " Verbosity:",
                    " -v",
                    " --verbose",
                    "  runs with verbose output",
                    " Standard:",
                    " --help",
                    "  shows this help and terminates",
                    " --version",
                    "  shows version information and terminates",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_specs_with_custom_usage_info()
        {
            Clasp.FlagSpecification Flag_Verbose = new Clasp.FlagSpecification(@"-v", @"--verbose", @"runs with verbose output");

            Clasp.Specification[] specifications =
            {
                Clasp.Specification.Section("Verbosity:"),
                Flag_Verbose,

                Clasp.Specification.Section("Standard:"),
                UsageUtil.Help,
                UsageUtil.Version,
            };

            string[] info_lines =
            {
                "CLASP.NET examples",
                null,
                ":version:",
                "",
            };

            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ InfoLines = info_lines, ValuesString = "<input-path>" }, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "CLASP.NET examples",
                    "myprog version 0.1.2 (build 3)",
                    "USAGE: myprog [ ... flags and options ... ] <input-path>",
                    "flags/options:",
                    " Verbosity:",
                    " -v",
                    " --verbose",
                    "  runs with verbose output",
                    " Standard:",
                    " --help",
                    "  shows this help and terminates",
                    " --version",
                    "  shows version information and terminates",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_specs_with_custom_usage_info_with_values_for_option()
        {
            Clasp.OptionSpecification Option_Verbose = new Clasp.OptionSpecification(@"-v", @"--verbosity", @"sets program verbosity", "silent", "terse", "normal", "chatty", "verbose");

            Clasp.Specification[] specifications =
            {
                Clasp.Specification.Section("Custom:"),
                Option_Verbose,

                Clasp.Specification.Section("Standard:"),
                UsageUtil.Help,
                UsageUtil.Version,
            };

            string[] info_lines =
            {
                "CLASP.NET examples",
                null,
                ":version:",
                "",
            };

            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ InfoLines = info_lines }, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "CLASP.NET examples",
                    "myprog version 0.1.2 (build 3)",
                    "USAGE: myprog [ ... flags and options ... ]",
                    "flags/options:",
                    " Custom:",
                    " -v <value>",
                    " --verbosity=<value>",
                    "  sets program verbosity",
                    "  where <value> one of:",
                    "   silent",
                    "   terse",
                    "   normal",
                    "   chatty",
                    "   verbose",
                    " Standard:",
                    " --help",
                    "  shows this help and terminates",
                    " --version",
                    "  shows version information and terminates",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_specs_with_custom_usage_info_with_values_for_option_with_default_value_with_default_default_indicator()
        {
            Clasp.OptionSpecification Option_Verbose = new Clasp.OptionSpecification(@"-v", @"--verbosity", @"sets program verbosity", "silent", "terse", "normal", "chatty", "verbose").WithDefaultValue("chatty");

            Clasp.Specification[] specifications =
            {
                Clasp.Specification.Section("Custom:"),
                Option_Verbose,

                Clasp.Specification.Section("Standard:"),
                UsageUtil.Help,
                UsageUtil.Version,
            };

            string[] info_lines =
            {
                "CLASP.NET examples",
                null,
                ":version:",
                "",
            };

            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ InfoLines = info_lines }, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "CLASP.NET examples",
                    "myprog version 0.1.2 (build 3)",
                    "USAGE: myprog [ ... flags and options ... ]",
                    "flags/options:",
                    " Custom:",
                    " -v <value>",
                    " --verbosity=<value>",
                    "  sets program verbosity",
                    "  where <value> one of:",
                    "   silent",
                    "   terse",
                    "   normal",
                    "   chatty (default)",
                    "   verbose",
                    " Standard:",
                    " --help",
                    "  shows this help and terminates",
                    " --version",
                    "  shows version information and terminates",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_specs_with_custom_usage_info_with_values_for_option_with_default_value_with_custom_default_indicator()
        {
            Clasp.OptionSpecification Option_Verbose = new Clasp.OptionSpecification(@"-v", @"--verbosity", @"sets program verbosity", "silent", "terse", "normal", "chatty", "verbose").WithDefaultValue("chatty");

            Clasp.Specification[] specifications =
            {
                Clasp.Specification.Section("Custom:"),
                Option_Verbose,

                Clasp.Specification.Section("Standard:"),
                UsageUtil.Help,
                UsageUtil.Version,
            };

            string[] info_lines =
            {
                "CLASP.NET examples",
                null,
                ":version:",
                "",
            };

            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ InfoLines = info_lines, DefaultIndicator = "**DEFAULT**" }, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "CLASP.NET examples",
                    "myprog version 0.1.2 (build 3)",
                    "USAGE: myprog [ ... flags and options ... ]",
                    "flags/options:",
                    " Custom:",
                    " -v <value>",
                    " --verbosity=<value>",
                    "  sets program verbosity",
                    "  where <value> one of:",
                    "   silent",
                    "   terse",
                    "   normal",
                    "   chatty **DEFAULT**",
                    "   verbose",
                    " Standard:",
                    " --help",
                    "  shows this help and terminates",
                    " --version",
                    "  shows version information and terminates",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void Test_specs_with_custom_usage_info_with_values_for_option_with_default_value_with_suppressed_default_indicator()
        {
            Clasp.OptionSpecification Option_Verbose = new Clasp.OptionSpecification(@"-v", @"--verbosity", @"sets program verbosity", "silent", "terse", "normal", "chatty", "verbose").WithDefaultValue("chatty");

            Clasp.Specification[] specifications =
            {
                Clasp.Specification.Section("Custom:"),
                Option_Verbose,

                Clasp.Specification.Section("Standard:"),
                UsageUtil.Help,
                UsageUtil.Version,
            };

            string[] info_lines =
            {
                "CLASP.NET examples",
                null,
                ":version:",
                "",
            };

            string[] argv =
            {
                "--help",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ InfoLines = info_lines, DefaultIndicator = "" }, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "CLASP.NET examples",
                    "myprog version 0.1.2 (build 3)",
                    "USAGE: myprog [ ... flags and options ... ]",
                    "flags/options:",
                    " Custom:",
                    " -v <value>",
                    " --verbosity=<value>",
                    "  sets program verbosity",
                    "  where <value> one of:",
                    "   silent",
                    "   terse",
                    "   normal",
                    "   chatty",
                    "   verbose",
                    " Standard:",
                    " --help",
                    "  shows this help and terminates",
                    " --version",
                    "  shows version information and terminates",
                }
            ,   actual
            );
        }
    }
}
