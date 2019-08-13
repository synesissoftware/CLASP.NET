
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using Cat = Clasp.Examples.Common.Programs.Cat;
    using UsageUtil = global::Clasp.Util.UsageUtil;

    using global::Clasp.Binding;
    using global::Clasp.Util;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    [TestClass]
    public class BoundUsage_tester
    {
        #region types

        [BoundType]
        internal class PathAndVerboseArguments
        {
            [BoundFlag(@"--verbose", Alias=@"-v", HelpDescription=@"makes the output verbose", HelpSection=@"behaviour")]
            public bool Verbose;

            [BoundValue(0, ValuesStringFragment="<path>")]
            public string Path;
        }

        [BoundType]
        internal class TwoPathValuesArguments1
        {
            [BoundValue(0, ValuesStringFragment="<path-1>")]
            public string Path1;

            [BoundValue(1, ValuesStringFragment="<path-2>")]
            public string Path2;
        }

        [BoundType]
        internal class TwoPathValuesArguments2
        {
            [BoundValue(1, ValuesStringFragment="<path-2>")]
            public string Path2;

            [BoundValue(0, ValuesStringFragment="<path-1>")]
            public string Path1;
        }

        [BoundType]
        internal class ComplexValuesArguments
        {
            [BoundValue(1, ValuesStringFragment="<path-2>")]
            public string Path2;

            [BoundValue(0, ValuesStringFragment="<path-1>")]
            public string Path1;

            [BoundValues(Minimum=3, Maximum=7, ValuesStringFragment="<path-4> [ ... <path-9>]")]
            public string[] PathsN;

            [BoundValue(2, ValuesStringFragment="<path-3>")]
            public string Path3;
        }
        #endregion

        #region fields

        private Assembly    assembly;
        #endregion

        [TestInitialize]
        public void Setup()
        {
            assembly = Assembly.GetExecutingAssembly();
        }

        [TestMethod]
        public void test_PathAndVerboseArguments_Usage()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Specification[] Specifications = {

                Clasp.Specification.Section(@"behaviour"),
            };

            Clasp.Arguments args = new Clasp.Arguments(argv);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowBoundUsage<PathAndVerboseArguments>(args, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Select((line) => line.Trim()).Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog [ ... flags and options ... ] <path>",
                    "flags/options:",
                    "-v",
                    "--verbose",
                    "makes the output verbose",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void test_TwoPathValuesArguments1_Usage()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Specification[] Specifications = {

                Clasp.Specification.Section(@"behaviour"),
            };

            Clasp.Arguments args = new Clasp.Arguments(argv);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowBoundUsage<TwoPathValuesArguments1>(args, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Select((line) => line.Trim()).Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog <path-1> <path-2>",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void test_TwoPathValuesArguments2_Usage()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Specification[] Specifications = {

                Clasp.Specification.Section(@"behaviour"),
            };

            Clasp.Arguments args = new Clasp.Arguments(argv);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowBoundUsage<TwoPathValuesArguments2>(args, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Select((line) => line.Trim()).Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog <path-1> <path-2>",
                }
            ,   actual
            );
        }

        [TestMethod]
        public void test_ComplexValuesArguments_Usage()
        {
            string[] argv =
            {
                "--help",
            };

            Clasp.Specification[] Specifications = {

                Clasp.Specification.Section(@"behaviour"),
            };

            Clasp.Arguments args = new Clasp.Arguments(argv);

            string s = Diagnosticism.Testing.Assist.ExecuteAroundWriter((writer) => {

                Dictionary<string, object> options = new Dictionary<string, object>();

                options[UsageUtil.Constants.OptionKeys.Writer] = writer;
                options[UsageUtil.Constants.OptionKeys.Assembly] = assembly;
                options[UsageUtil.Constants.OptionKeys.Separator] = " ";
                options[UsageUtil.Constants.OptionKeys.ProgramName] = "myprog";

                UsageUtil.ShowBoundUsage<ComplexValuesArguments>(args, options);
            });

            string[] lines = s.Split(Environment.NewLine.ToCharArray());

            var actual = lines.Select((line) => line.Trim()).Where((line) => 0 != line.Length).ToArray();

            CollectionAssert.AreEqual(
                new string[]{
                    "USAGE: myprog <path-1> <path-2> <path-3> <path-4> [ ... <path-9>]",
                }
            ,   actual
            );
        }
    }
}
