

namespace Test.Unit.CLASP.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

    [TestClass]
    public class Arguments_tester
    {
        [TestMethod]
        public void test_Arguments_type_exists()
        {
            Assert.IsNotNull(typeof(Arguments));
        }

        #region Value tests

        [TestMethod]
        public void Test_Value_1()
        {
            string[] argv = { };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);
        }

        [TestMethod]
        public void Test_Value_2()
        {
            string[] argv = { "" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);
        }

        [TestMethod]
        public void Test_Value_3()
        {
            string[] argv = { "abc" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(1, arguments.Values.Count);

            var value0 = arguments.Values[0];

            Assert.AreEqual(ArgumentType.Value, value0.Type);
            Assert.AreEqual(null, value0.GivenName);
            Assert.AreEqual(null, value0.ResolvedName);
            Assert.AreEqual(0, value0.Index);
            Assert.AreEqual("abc", value0.Value);
            Assert.AreEqual("abc", value0.ToString());
        }

        [TestMethod]
        public void Test_Value_4()
        {
            string[] argv = { "abc", "def", "ghi" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(3, arguments.Values.Count);

            var value0 = arguments.Values[0];

            Assert.AreEqual(ArgumentType.Value, value0.Type);
            Assert.AreEqual(null, value0.GivenName);
            Assert.AreEqual(null, value0.ResolvedName);
            Assert.AreEqual(0, value0.Index);
            Assert.AreEqual("abc", value0.Value);
            Assert.AreEqual("abc", value0.ToString());

            var value1 = arguments.Values[1];

            Assert.AreEqual(ArgumentType.Value, value1.Type);
            Assert.AreEqual(null, value1.GivenName);
            Assert.AreEqual(null, value1.ResolvedName);
            Assert.AreEqual(1, value1.Index);
            Assert.AreEqual("def", value1.Value);
            Assert.AreEqual("def", value1.ToString());

            var value2 = arguments.Values[2];

            Assert.AreEqual(ArgumentType.Value, value2.Type);
            Assert.AreEqual(null, value2.GivenName);
            Assert.AreEqual(null, value2.ResolvedName);
            Assert.AreEqual(2, value2.Index);
            Assert.AreEqual("ghi", value2.Value);
            Assert.AreEqual("ghi", value2.ToString());
        }

        [TestMethod]
        public void Test_Value_5()
        {
            string[] argv = { "abc", "--", "-x" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(2, arguments.Values.Count);

            var value0 = arguments.Values[0];

            Assert.AreEqual(ArgumentType.Value, value0.Type);
            Assert.AreEqual(null, value0.GivenName);
            Assert.AreEqual(null, value0.ResolvedName);
            Assert.AreEqual(0, value0.Index);
            Assert.AreEqual("abc", value0.Value);
            Assert.AreEqual("abc", value0.ToString());

            var value1 = arguments.Values[1];

            Assert.AreEqual(ArgumentType.Value, value1.Type);
            Assert.AreEqual(null, value1.GivenName);
            Assert.AreEqual(null, value1.ResolvedName);
            Assert.AreEqual(2, value1.Index);
            Assert.AreEqual("-x", value1.Value);
            Assert.AreEqual("-x", value1.ToString());
        }

        [TestMethod]
        public void Test_Value_6()
        {
            string[] argv = { "abc", "--", "-" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(2, arguments.Values.Count);

            var value0 = arguments.Values[0];

            Assert.AreEqual(ArgumentType.Value, value0.Type);
            Assert.AreEqual(null, value0.GivenName);
            Assert.AreEqual(null, value0.ResolvedName);
            Assert.AreEqual(0, value0.Index);
            Assert.AreEqual("abc", value0.Value);
            Assert.AreEqual("abc", value0.ToString());

            var value1 = arguments.Values[1];

            Assert.AreEqual(ArgumentType.Value, value1.Type);
            Assert.AreEqual(null, value1.GivenName);
            Assert.AreEqual(null, value1.ResolvedName);
            Assert.AreEqual(2, value1.Index);
            Assert.AreEqual("-", value1.Value);
            Assert.AreEqual("-", value1.ToString());
        }

        [TestMethod]
        public void Test_Value_7()
        {
            string[] argv = { "abc", "--", "--" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(2, arguments.Values.Count);

            var value0 = arguments.Values[0];

            Assert.AreEqual(ArgumentType.Value, value0.Type);
            Assert.AreEqual(null, value0.GivenName);
            Assert.AreEqual(null, value0.ResolvedName);
            Assert.AreEqual(0, value0.Index);
            Assert.AreEqual("abc", value0.Value);
            Assert.AreEqual("abc", value0.ToString());

            var value1 = arguments.Values[1];

            Assert.AreEqual(ArgumentType.Value, value1.Type);
            Assert.AreEqual(null, value1.GivenName);
            Assert.AreEqual(null, value1.ResolvedName);
            Assert.AreEqual(2, value1.Index);
            Assert.AreEqual("--", value1.Value);
            Assert.AreEqual("--", value1.ToString());
        }
        #endregion

        #region Option tests

        [TestMethod]
        public void Test_Option_1_hyphen_as_flag()
        {
            string[] argv = { "-" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-", flag0.GivenName);
            Assert.AreEqual("-", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.IsNull(flag0.Value);
            Assert.AreEqual("-", flag0.ToString());
        }

        [TestMethod]
        public void Test_Option_1_hyphen_as_value()
        {
            string[] argv = { "-" };

            Arguments arguments = new Arguments(argv, ParseOptions.TreatSinglehyphenAsValue);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(1, arguments.Values.Count);

            var value0 = arguments.Values[0];

            Assert.AreEqual(ArgumentType.Value, value0.Type);
            Assert.IsNull(value0.GivenName);
            Assert.IsNull(value0.ResolvedName);
            Assert.AreEqual(0, value0.Index);
            Assert.AreEqual("-", value0.Value);
            Assert.AreEqual("-", value0.ToString());
        }

        [TestMethod]
        public void Test_Option_2()
        {
            string[] argv = { "--x=10" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("--x", option0.GivenName);
            Assert.AreEqual("--x", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("10", option0.Value);
            Assert.AreEqual("--x=10", option0.ToString());
        }

        [TestMethod]
        public void Test_Option_3()
        {
            string[] argv = { "-x=10" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("-x", option0.GivenName);
            Assert.AreEqual("-x", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("10", option0.Value);
            Assert.AreEqual("-x=10", option0.ToString());
        }

        [TestMethod]
        public void Test_Option_4()
        {
            string[] argv = { "--width=10", "--height=20" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(2, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("--width", option0.GivenName);
            Assert.AreEqual("--width", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("10", option0.Value);
            Assert.AreEqual("--width=10", option0.ToString());

            var option1 = arguments.Options[1];

            Assert.AreEqual(ArgumentType.Option, option1.Type);
            Assert.AreEqual("--height", option1.GivenName);
            Assert.AreEqual("--height", option1.ResolvedName);
            Assert.AreEqual(1, option1.Index);
            Assert.AreEqual("20", option1.Value);
            Assert.AreEqual("--height=20", option1.ToString());
        }

        [TestMethod]
        public void Test_Option_5()
        {
            string[] argv = { "--width=10", "--height" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("--width", option0.GivenName);
            Assert.AreEqual("--width", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("10", option0.Value);
            Assert.AreEqual("--width=10", option0.ToString());

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("--height", flag0.GivenName);
            Assert.AreEqual("--height", flag0.ResolvedName);
            Assert.AreEqual(1, flag0.Index);
            Assert.AreEqual(null, flag0.Value);
            Assert.AreEqual("--height", flag0.ToString());
        }

        [TestMethod]
        public void Test_NextOption_1()
        {
            Specification[] specifications = { Specification.Option("--width", null) };

            string[] argv = { "--width", "10" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("--width", option0.GivenName);
            Assert.AreEqual("--width", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("10", option0.Value);
            Assert.AreEqual("--width=10", option0.ToString());
        }

        [TestMethod]
        public void Test_NextOption_2()
        {
            Specification[] specifications = { Specification.Option("--width", "--width") };

            string[] argv = { "--width", "10" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("--width", option0.GivenName);
            Assert.AreEqual("--width", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("10", option0.Value);
            Assert.AreEqual("--width=10", option0.ToString());
        }

        [TestMethod]
        public void Test_NextOption_3()
        {
            Specification[] specifications =
            {
                Specification.Option("--width", null),
                Specification.Option("-W", "--width"),
            };

            string[] argv = { "-W", "10" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("-W", option0.GivenName);
            Assert.AreEqual("--width", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("10", option0.Value);
            Assert.AreEqual("--width=10", option0.ToString());
        }
        #endregion

        #region Flag tests

        [TestMethod]
        public void Test_Flag_1()
        {
            string[] argv = { "-x" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-x", flag0.GivenName);
            Assert.AreEqual("-x", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual(null, flag0.Value);
            Assert.AreEqual("-x", flag0.ToString());
        }

        [TestMethod]
        public void Test_Flag_2()
        {
            string[] argv = { "--X" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("--X", flag0.GivenName);
            Assert.AreEqual("--X", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual(null, flag0.Value);
            Assert.AreEqual("--X", flag0.ToString());
        }

        [TestMethod]
        public void Test_Flag_3()
        {
            string[] argv = { "-x", "--X" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(2, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-x", flag0.GivenName);
            Assert.AreEqual("-x", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual(null, flag0.Value);
            Assert.AreEqual("-x", flag0.ToString());

            var flag1 = arguments.Flags[1];

            Assert.AreEqual(ArgumentType.Flag, flag1.Type);
            Assert.AreEqual("--X", flag1.GivenName);
            Assert.AreEqual("--X", flag1.ResolvedName);
            Assert.AreEqual(1, flag1.Index);
            Assert.AreEqual(null, flag1.Value);
            Assert.AreEqual("--X", flag1.ToString());
        }

        [TestMethod]
        public void Test_Flag_4()
        {
            string[] argv = { "-w", "-h" };

            Arguments arguments = new Arguments(argv);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(2, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-w", flag0.GivenName);
            Assert.AreEqual("-w", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual(null, flag0.Value);
            Assert.AreEqual("-w", flag0.ToString());

            var flag1 = arguments.Flags[1];

            Assert.AreEqual(ArgumentType.Flag, flag1.Type);
            Assert.AreEqual("-h", flag1.GivenName);
            Assert.AreEqual("-h", flag1.ResolvedName);
            Assert.AreEqual(1, flag1.Index);
            Assert.AreEqual(null, flag1.Value);
            Assert.AreEqual("-h", flag1.ToString());
        }

        [TestMethod]
        public void Test_Flag_5()
        {
            Specification[] specifications =
            {
                Specification.Flag("-a", "--append"),
                Specification.Flag("-i", "--ignore-interrupts"),
            };

            string[] argv = { "-a" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-a", flag0.GivenName);
            Assert.AreEqual("--append", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.AreEqual(null, flag0.Value);
            Assert.AreEqual("--append", flag0.ToString());
        }
        #endregion

        #region Specifications tests

        [TestMethod]
        public void Test_Specifications_1()
        {
            Specification[] specifications = { Specification.Flag("-D", "--debug=on") };

            string[] argv = { "-D" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("-D", option0.GivenName);
            Assert.AreEqual("--debug", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("on", option0.Value);
            Assert.AreEqual("--debug=on", option0.ToString());
        }

        [TestMethod]
        public void Test_Specifications_2()
        {
            Specification[] specifications = { };

            string[] argv = { "-classpath" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(9, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(9, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-classpath", flag0.GivenName);
            Assert.AreEqual("-c", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.IsNull(flag0.Value);
            Assert.AreEqual("-c", flag0.ToString());

            var flag1 = arguments.Flags[1];

            Assert.AreEqual(ArgumentType.Flag, flag1.Type);
            Assert.AreEqual("-classpath", flag1.GivenName);
            Assert.AreEqual("-l", flag1.ResolvedName);
            Assert.AreEqual(0, flag1.Index);
            Assert.IsNull(flag1.Value);
            Assert.AreEqual("-l", flag1.ToString());

            var flag2 = arguments.Flags[2];

            Assert.AreEqual(ArgumentType.Flag, flag2.Type);
            Assert.AreEqual("-classpath", flag2.GivenName);
            Assert.AreEqual("-a", flag2.ResolvedName);
            Assert.AreEqual(0, flag2.Index);
            Assert.IsNull(flag2.Value);
            Assert.AreEqual("-a", flag2.ToString());

            var flag3 = arguments.Flags[3];

            Assert.AreEqual(ArgumentType.Flag, flag3.Type);
            Assert.AreEqual("-classpath", flag3.GivenName);
            Assert.AreEqual("-s", flag3.ResolvedName);
            Assert.AreEqual(0, flag3.Index);
            Assert.IsNull(flag3.Value);
            Assert.AreEqual("-s", flag3.ToString());

            var flag4 = arguments.Flags[4];

            Assert.AreEqual(ArgumentType.Flag, flag4.Type);
            Assert.AreEqual("-classpath", flag4.GivenName);
            Assert.AreEqual("-s", flag4.ResolvedName);
            Assert.AreEqual(0, flag4.Index);
            Assert.IsNull(flag4.Value);
            Assert.AreEqual("-s", flag4.ToString());

            var flag5 = arguments.Flags[5];

            Assert.AreEqual(ArgumentType.Flag, flag5.Type);
            Assert.AreEqual("-classpath", flag5.GivenName);
            Assert.AreEqual("-p", flag5.ResolvedName);
            Assert.AreEqual(0, flag5.Index);
            Assert.IsNull(flag5.Value);
            Assert.AreEqual("-p", flag5.ToString());

            var flag6 = arguments.Flags[6];

            Assert.AreEqual(ArgumentType.Flag, flag6.Type);
            Assert.AreEqual("-classpath", flag6.GivenName);
            Assert.AreEqual("-a", flag6.ResolvedName);
            Assert.AreEqual(0, flag6.Index);
            Assert.IsNull(flag6.Value);
            Assert.AreEqual("-a", flag6.ToString());

            var flag7 = arguments.Flags[7];

            Assert.AreEqual(ArgumentType.Flag, flag7.Type);
            Assert.AreEqual("-classpath", flag7.GivenName);
            Assert.AreEqual("-t", flag7.ResolvedName);
            Assert.AreEqual(0, flag7.Index);
            Assert.IsNull(flag7.Value);
            Assert.AreEqual("-t", flag7.ToString());

            var flag8 = arguments.Flags[8];

            Assert.AreEqual(ArgumentType.Flag, flag8.Type);
            Assert.AreEqual("-classpath", flag8.GivenName);
            Assert.AreEqual("-h", flag8.ResolvedName);
            Assert.AreEqual(0, flag8.Index);
            Assert.IsNull(flag8.Value);
            Assert.AreEqual("-h", flag8.ToString());
        }

        [TestMethod]
        public void Test_Specifications_3()
        {
            Specification[] specifications = { Specification.Flag("-c", "--capture") };

            string[] argv = { "-classpath" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(9, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(9, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-classpath", flag0.GivenName);
            Assert.AreEqual("--capture", flag0.ResolvedName);
            Assert.AreEqual(0, flag0.Index);
            Assert.IsNull(flag0.Value);
            Assert.AreEqual("--capture", flag0.ToString());

            var flag1 = arguments.Flags[1];

            Assert.AreEqual(ArgumentType.Flag, flag1.Type);
            Assert.AreEqual("-classpath", flag1.GivenName);
            Assert.AreEqual("-l", flag1.ResolvedName);
            Assert.AreEqual(0, flag1.Index);
            Assert.IsNull(flag1.Value);
            Assert.AreEqual("-l", flag1.ToString());

            var flag2 = arguments.Flags[2];

            Assert.AreEqual(ArgumentType.Flag, flag2.Type);
            Assert.AreEqual("-classpath", flag2.GivenName);
            Assert.AreEqual("-a", flag2.ResolvedName);
            Assert.AreEqual(0, flag2.Index);
            Assert.IsNull(flag2.Value);
            Assert.AreEqual("-a", flag2.ToString());

            var flag3 = arguments.Flags[3];

            Assert.AreEqual(ArgumentType.Flag, flag3.Type);
            Assert.AreEqual("-classpath", flag3.GivenName);
            Assert.AreEqual("-s", flag3.ResolvedName);
            Assert.AreEqual(0, flag3.Index);
            Assert.IsNull(flag3.Value);
            Assert.AreEqual("-s", flag3.ToString());

            var flag4 = arguments.Flags[4];

            Assert.AreEqual(ArgumentType.Flag, flag4.Type);
            Assert.AreEqual("-classpath", flag4.GivenName);
            Assert.AreEqual("-s", flag4.ResolvedName);
            Assert.AreEqual(0, flag4.Index);
            Assert.IsNull(flag4.Value);
            Assert.AreEqual("-s", flag4.ToString());

            var flag5 = arguments.Flags[5];

            Assert.AreEqual(ArgumentType.Flag, flag5.Type);
            Assert.AreEqual("-classpath", flag5.GivenName);
            Assert.AreEqual("-p", flag5.ResolvedName);
            Assert.AreEqual(0, flag5.Index);
            Assert.IsNull(flag5.Value);
            Assert.AreEqual("-p", flag5.ToString());

            var flag6 = arguments.Flags[6];

            Assert.AreEqual(ArgumentType.Flag, flag6.Type);
            Assert.AreEqual("-classpath", flag6.GivenName);
            Assert.AreEqual("-a", flag6.ResolvedName);
            Assert.AreEqual(0, flag6.Index);
            Assert.IsNull(flag6.Value);
            Assert.AreEqual("-a", flag6.ToString());

            var flag7 = arguments.Flags[7];

            Assert.AreEqual(ArgumentType.Flag, flag7.Type);
            Assert.AreEqual("-classpath", flag7.GivenName);
            Assert.AreEqual("-t", flag7.ResolvedName);
            Assert.AreEqual(0, flag7.Index);
            Assert.IsNull(flag7.Value);
            Assert.AreEqual("-t", flag7.ToString());

            var flag8 = arguments.Flags[8];

            Assert.AreEqual(ArgumentType.Flag, flag8.Type);
            Assert.AreEqual("-classpath", flag8.GivenName);
            Assert.AreEqual("-h", flag8.ResolvedName);
            Assert.AreEqual(0, flag8.Index);
            Assert.IsNull(flag8.Value);
            Assert.AreEqual("-h", flag8.ToString());
        }

        [TestMethod]
        public void Test_Specifications_4()
        {
            Specification[] specifications = { Specification.Flag("-D", "--debug=on") };

            string[] argv = { "-D", "-h" };

            Arguments arguments = new Arguments(argv, specifications);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            var option0 = arguments.Options[0];

            Assert.AreEqual(ArgumentType.Option, option0.Type);
            Assert.AreEqual("-D", option0.GivenName);
            Assert.AreEqual("--debug", option0.ResolvedName);
            Assert.AreEqual(0, option0.Index);
            Assert.AreEqual("on", option0.Value);
            Assert.AreEqual("--debug=on", option0.ToString());

            var flag0 = arguments.Flags[0];

            Assert.AreEqual(ArgumentType.Flag, flag0.Type);
            Assert.AreEqual("-h", flag0.GivenName);
            Assert.AreEqual("-h", flag0.ResolvedName);
            Assert.AreEqual(1, flag0.Index);
            Assert.AreEqual(null, flag0.Value);
            Assert.AreEqual("-h", flag0.ToString());
        }
        #endregion
    }
}
