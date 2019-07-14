
#pragma warning disable 0618

namespace Test.Unit.ExampleTestCases
{
    using UsageUtil = global::Clasp.Util.UsageUtil;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class _1_tester
    {
        Clasp.Specification[] Specifications =
        {
            Clasp.Specification.Flag("-d", "--debug", "runs in Debug mode"),

            Clasp.Specification.Option("-v", "--verbosity", "specifies the verbosity", "[s]ilent", "[t]erse", "[n]ormal", "[c]hatty", "[v]erbose"),
            Clasp.Specification.Flag("-c", "--verbosity=chatty", null),

            Clasp.Util.UsageUtil.Help,
            Clasp.Util.UsageUtil.Version,
        };

        [TestMethod]
        public void Test_empty_argv()
        {
            string[] argv =
            {};

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(0, args.Options.Count);
            Assert.AreEqual(0, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsFalse(args.CheckOption("--verbosity", out v));

            try
            {
                args.RequireOption("--abcd", out v);
            }
            catch (Clasp.Exceptions.MissingOptionException x)
            {
                Assert.AreEqual("required option --abcd not specified", x.Message);

                Assert.IsNull(x.Argument);
            }

            try
            {
                int v2;

                args.RequireOption("--verbosity", out v2);
            }
            catch (Clasp.Exceptions.MissingOptionException x)
            {
                Assert.AreEqual("required option --verbosity not specified", x.Message);

                Assert.IsNull(x.Argument);
            }
        }

        [TestMethod]
        public void Test_one_flag()
        {
            string[] argv =
            {
                "--debug",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(1, args.Flags.Count);
            Assert.AreEqual(0, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var flag0 = args.Flags[0];

            Assert.AreEqual("--debug", flag0.GivenName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual("--debug", flag0.ResolvedName);
            Assert.AreSame(Specifications[0], flag0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Flag, flag0.Type);
            Assert.IsFalse(flag0.Used);
            Assert.IsNull(flag0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsTrue(args.HasFlag("--debug"));

            Assert.IsTrue(flag0.Used);

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsFalse(args.CheckOption("--verbosity", out v));
        }

        [TestMethod]
        public void Test_one_flag_via_alias()
        {
            string[] argv =
            {
                "-d",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(1, args.Flags.Count);
            Assert.AreEqual(0, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var flag0 = args.Flags[0];

            Assert.AreEqual("-d", flag0.GivenName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual("--debug", flag0.ResolvedName);
            Assert.AreSame(Specifications[0], flag0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Flag, flag0.Type);
            Assert.IsFalse(flag0.Used);
            Assert.IsNull(flag0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsTrue(args.HasFlag("--debug"));

            Assert.IsTrue(flag0.Used);

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsFalse(args.CheckOption("--verbosity", out v));
        }

        [TestMethod]
        public void Test_repeat_flag_via_resolved_name_and_alias()
        {
            string[] argv =
            {
                "--debug",
                "-d",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(2, args.Flags.Count);
            Assert.AreEqual(0, args.Options.Count);
            Assert.AreEqual(2, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var flag0 = args.Flags[0];

            Assert.AreEqual("--debug", flag0.GivenName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual("--debug", flag0.ResolvedName);
            Assert.AreSame(Specifications[0], flag0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Flag, flag0.Type);
            Assert.IsFalse(flag0.Used);
            Assert.IsNull(flag0.Value);

            var flag1 = args.Flags[1];

            Assert.AreEqual("-d", flag1.GivenName);
            Assert.AreEqual(1, flag1.Index);
            Assert.AreEqual("--debug", flag1.ResolvedName);
            Assert.AreSame(Specifications[0], flag1.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Flag, flag1.Type);
            Assert.IsFalse(flag1.Used);
            Assert.IsNull(flag1.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsTrue(args.HasFlag("--debug"));

            Assert.IsTrue(flag0.Used);
            Assert.IsFalse(flag1.Used);

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsFalse(args.CheckOption("--verbosity", out v));
        }

        [TestMethod]
        public void Test_one_option_with_included_value()
        {
            string[] argv =
            {
                "--verbosity=silent",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("--verbosity", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("silent", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("silent", v);

            try
            {
                args.RequireOption("--abcd", out v);

                Assert.Fail("should not get here");
            }
            catch (Clasp.Exceptions.MissingOptionException x)
            {
                Assert.AreEqual("required option --abcd not specified", x.Message);

                Assert.IsNull(x.Argument);
            }

            args.RequireOption("--verbosity", out v);
            Assert.AreEqual("silent", v);

            try
            {
                int v2;

                args.RequireOption("--verbosity", out v2);

                Assert.Fail("should not get here");
            }
            catch (Clasp.Exceptions.InvalidOptionValueException x)
            {
                Assert.AreEqual("invalid value for option --verbosity: 'silent' is not a number", x.Message);

                Assert.IsNotNull(x.Argument);
                Assert.AreEqual("--verbosity", x.Argument.ResolvedName);
            }

            try
            {
                int v2;

                args.RequireValue(0, out v2);

                Assert.Fail("should not get here");
            }
            catch (Clasp.Exceptions.MissingValueException x)
            {
                Assert.AreEqual("required value at index 0 not specified", x.Message);

                Assert.IsNull(x.Argument);
            }
        }

        public void Test_invalid_value()
        {
            string[] argv =
            {
                "abc",
                "123",
                "a456",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string  s;
            int     i;

            args.RequireValue(0, out s);
            Assert.AreEqual("abc", s);

            args.RequireValue(1, out i);
            Assert.AreEqual(123, i);

            try
            {
                args.RequireValue(2, out i);

                Assert.Fail("should not get here");
            }
            catch (Clasp.Exceptions.InvalidValueException x)
            {
                Assert.AreEqual("invalid value at index 2: 'a456' is not a number", x.Message);

                Assert.IsNotNull(x.Argument);
                Assert.AreEqual(2, x.Argument.Index);
                Assert.AreEqual("a456", x.Argument.Value);
            }
        }

        [TestMethod]
        public void Test_one_option_with_separate_value()
        {
            string[] argv =
            {
                "--verbosity",
                "silent",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("--verbosity", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("silent", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("silent", v);
        }

        [TestMethod]
        public void Test_one_option_with_empty_value()
        {
            string[] argv =
            {
                "--verbosity=",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("--verbosity", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("", v);
        }

        [TestMethod]
        public void Test_one_option_with_missing_value()
        {
            string[] argv =
            {
                "--verbosity",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("--verbosity", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.IsNull(option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.IsNull(v);
        }

        [TestMethod]
        public void Test_one_option_with_optionvaluealias()
        {
            string[] argv =
            {
                "-c",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("-c", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("chatty", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("chatty", v);
        }

        [TestMethod]
        public void Test_one_option_and_one_flag_via_short_forms_combined_as_dc()
        {
            string[] argv =
            {
                "-dc",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, Specifications);

            string v;

            Assert.AreSame(Specifications, args.Aliases);
            Assert.AreSame(Specifications, args.Specifications);

            Assert.AreEqual(1, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(2, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var flag0 = args.Flags[0];

            Assert.AreEqual("-dc", flag0.GivenName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual("--debug", flag0.ResolvedName);
            Assert.AreSame(Specifications[0], flag0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Flag, flag0.Type);
            Assert.IsFalse(flag0.Used);
            Assert.IsNull(flag0.Value);

            var option0 = args.Options[0];

            Assert.AreEqual("-dc", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("chatty", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsTrue(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("chatty", v);
        }

        [TestMethod]
        public void Test_one_option_that_has_default_with_empty_value()
        {
            Clasp.Specification[] specifications = (Clasp.Specification[])Specifications.Clone();

            specifications[1] = ((Clasp.OptionSpecification)specifications[1]).WithDefaultValue("terse");

            string[] argv =
            {
                "--verbosity=",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string v;

            Assert.AreSame(specifications, args.Aliases);
            Assert.AreSame(specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("--verbosity", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("terse", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("terse", v);
        }

        [TestMethod]
        public void Test_one_option_that_has_default_with_missing_value()
        {
            Clasp.Specification[] specifications = (Clasp.Specification[])Specifications.Clone();

            specifications[1] = ((Clasp.OptionSpecification)specifications[1]).WithDefaultValue("terse");

            string[] argv =
            {
                "--verbosity",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string v;

            Assert.AreSame(specifications, args.Aliases);
            Assert.AreSame(specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("--verbosity", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("terse", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("terse", v);
        }

        [TestMethod]
        public void Test_one_option_that_has_default_with_missing_value_and_values_designator()
        {
            Clasp.Specification[] specifications = (Clasp.Specification[])Specifications.Clone();

            specifications[1] = ((Clasp.OptionSpecification)specifications[1]).WithDefaultValue("terse");

            string[] argv =
            {
                "--verbosity",
                "--",
            };

            Clasp.Arguments args = new Clasp.Arguments(argv, specifications);

            string v;

            Assert.AreSame(specifications, args.Aliases);
            Assert.AreSame(specifications, args.Specifications);

            Assert.AreEqual(0, args.Flags.Count);
            Assert.AreEqual(1, args.Options.Count);
            Assert.AreEqual(1, args.FlagsAndOptions.Count);
            Assert.AreEqual(0, args.Values.Count);

            var option0 = args.Options[0];

            Assert.AreEqual("--verbosity", option0.GivenName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("--verbosity", option0.ResolvedName);
            Assert.AreSame(Specifications[1], option0.Specification);
            Assert.AreEqual(Clasp.ArgumentType.Option, option0.Type);
            Assert.IsFalse(option0.Used);
            Assert.AreEqual("terse", option0.Value);

            Assert.IsFalse(args.HasFlag("--xyz"));
            Assert.IsFalse(args.HasFlag("--debug"));

            Assert.IsFalse(args.CheckOption("--abcd", out v));
            Assert.IsTrue(args.CheckOption("--verbosity", out v));

            Assert.IsTrue(option0.Used);
            Assert.AreEqual("terse", v);
        }
    }
}
