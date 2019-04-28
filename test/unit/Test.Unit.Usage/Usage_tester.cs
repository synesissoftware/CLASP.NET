﻿
namespace Test.Unit.Usage
{
    using Clasp = global::SynesisSoftware.SystemTools.Clasp;
    using UsageUtil = global::SynesisSoftware.SystemTools.Clasp.Util.UsageUtil;

    using global::SynesisSoftware.SystemTools.Clasp.Util;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

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
            Clasp.Flag Flag_Verbose = new Clasp.Flag(@"-v", @"--verbose", @"runs with verbose output");

            Specification[] specifications =
            {
                Specification.Section("Verbosity:"),
                Flag_Verbose,

                Specification.Section("Standard:"),
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
            Clasp.Flag Flag_Verbose = new Clasp.Flag(@"-v", @"--verbose", @"runs with verbose output");

            Specification[] specifications =
            {
                Specification.Section("Verbosity:"),
                Flag_Verbose,

                Specification.Section("Standard:"),
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
            Clasp.Option Option_Verbose = new Clasp.Option(@"-v", @"--verbosity", @"sets program verbosity", "silent", "terse", "normal", "chatty", "verbose");

            Specification[] specifications =
            {
                Specification.Section("Custom:"),
                Option_Verbose,

                Specification.Section("Standard:"),
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
    }
}
