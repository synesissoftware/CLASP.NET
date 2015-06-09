
namespace Test.Unit.CLASP._1
{
    using global::SynesisSoftware.SystemTools.Clasp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class Arguments_tester
    {
        [TestMethod]
        public void test_Arguments_type_exists()
        {
            Assert.IsNotNull(typeof(Arguments));
        }

        #region Value Tests
        [TestMethod]
        public void Test_Value_1()
        {
            string[] args = { };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);
        }

        [TestMethod]
        public void Test_Value_2()
        {
            string[] args = { "" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);
        }

        [TestMethod]
        public void Test_Value_3()
        {
            string[] args = { "abc" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(1, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[0].Type);
            Assert.AreEqual(null, arguments.Values[0].GivenName);
            Assert.AreEqual(null, arguments.Values[0].ResolvedName);
            Assert.AreEqual(0, arguments.Values[0].Index);
            Assert.AreEqual("abc", arguments.Values[0].Value);
        }

        [TestMethod]
        public void Test_Value_4()
        {
            string[] args = { "abc", "def", "ghi" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(3, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[0].Type);
            Assert.AreEqual(null, arguments.Values[0].GivenName);
            Assert.AreEqual(null, arguments.Values[0].ResolvedName);
            Assert.AreEqual(0, arguments.Values[0].Index);
            Assert.AreEqual("abc", arguments.Values[0].Value);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[1].Type);
            Assert.AreEqual(null, arguments.Values[1].GivenName);
            Assert.AreEqual(null, arguments.Values[1].ResolvedName);
            Assert.AreEqual(1, arguments.Values[1].Index);
            Assert.AreEqual("def", arguments.Values[1].Value);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[2].Type);
            Assert.AreEqual(null, arguments.Values[2].GivenName);
            Assert.AreEqual(null, arguments.Values[2].ResolvedName);
            Assert.AreEqual(2, arguments.Values[2].Index);
            Assert.AreEqual("ghi", arguments.Values[2].Value);
        }

        [TestMethod]
        public void Test_Value_5()
        {
            string[] args = { "abc", "--", "-x" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(2, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[0].Type);
            Assert.AreEqual(null, arguments.Values[0].GivenName);
            Assert.AreEqual(null, arguments.Values[0].ResolvedName);
            Assert.AreEqual(0, arguments.Values[0].Index);
            Assert.AreEqual("abc", arguments.Values[0].Value);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[1].Type);
            Assert.AreEqual(null, arguments.Values[1].GivenName);
            Assert.AreEqual(null, arguments.Values[1].ResolvedName);
            Assert.AreEqual(2, arguments.Values[1].Index);
            Assert.AreEqual("-x", arguments.Values[1].Value);
        }

        [TestMethod]
        public void Test_Value_6()
        {
            string[] args = { "abc", "--", "-" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(2, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[0].Type);
            Assert.AreEqual(null, arguments.Values[0].GivenName);
            Assert.AreEqual(null, arguments.Values[0].ResolvedName);
            Assert.AreEqual(0, arguments.Values[0].Index);
            Assert.AreEqual("abc", arguments.Values[0].Value);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[1].Type);
            Assert.AreEqual(null, arguments.Values[1].GivenName);
            Assert.AreEqual(null, arguments.Values[1].ResolvedName);
            Assert.AreEqual(2, arguments.Values[1].Index);
            Assert.AreEqual("-", arguments.Values[1].Value);
        }

        [TestMethod]
        public void Test_Value_7()
        {
            string[] args = { "abc", "--", "--" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(0, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(2, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[0].Type);
            Assert.AreEqual(null, arguments.Values[0].GivenName);
            Assert.AreEqual(null, arguments.Values[0].ResolvedName);
            Assert.AreEqual(0, arguments.Values[0].Index);
            Assert.AreEqual("abc", arguments.Values[0].Value);

            Assert.AreEqual(ArgumentType.Value, arguments.Values[1].Type);
            Assert.AreEqual(null, arguments.Values[1].GivenName);
            Assert.AreEqual(null, arguments.Values[1].ResolvedName);
            Assert.AreEqual(2, arguments.Values[1].Index);
            Assert.AreEqual("--", arguments.Values[1].Value);
        }
        #endregion

        #region Option Tests
        [TestMethod]
        public void Test_Option_1()
        {
            string[] args = { "-" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("-", arguments.Options[0].GivenName);
            Assert.AreEqual("-", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.IsNull(arguments.Options[0].Value);
        }

        [TestMethod]
        public void Test_Option_2()
        {
            string[] args = { "--x=10" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("--x", arguments.Options[0].GivenName);
            Assert.AreEqual("--x", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("10", arguments.Options[0].Value);
        }

        [TestMethod]
        public void Test_Option_3()
        {
            string[] args = { "-x=10" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("-x", arguments.Options[0].GivenName);
            Assert.AreEqual("-x", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("10", arguments.Options[0].Value);
        }

        [TestMethod]
        public void Test_Option_4()
        {
            string[] args = { "--width=10", "--height=20" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(2, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("--width", arguments.Options[0].GivenName);
            Assert.AreEqual("--width", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("10", arguments.Options[0].Value);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[1].Type);
            Assert.AreEqual("--height", arguments.Options[1].GivenName);
            Assert.AreEqual("--height", arguments.Options[1].ResolvedName);
            Assert.AreEqual(1, arguments.Options[1].Index);
            Assert.AreEqual("20", arguments.Options[1].Value);
        }

        [TestMethod]
        public void Test_Option_5()
        {
            string[] args = { "--width=10", "--height" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("--width", arguments.Options[0].GivenName);
            Assert.AreEqual("--width", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("10", arguments.Options[0].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("--height", arguments.Flags[0].GivenName);
            Assert.AreEqual("--height", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(1, arguments.Flags[0].Index);
            Assert.AreEqual(null, arguments.Flags[0].Value);
        }

        [TestMethod]
        public void Test_NextOption_1()
        {
            Alias[] aliases = { Alias.Option("--width", null) };

            string[] args = { "--width", "10" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("--width", arguments.Options[0].GivenName);
            Assert.AreEqual("--width", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("10", arguments.Options[0].Value);
        }

        [TestMethod]
        public void Test_NextOption_2()
        {
            Alias[] aliases = { Alias.Option("--width", "--width") };

            string[] args = { "--width", "10" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("--width", arguments.Options[0].GivenName);
            Assert.AreEqual("--width", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("10", arguments.Options[0].Value);
        }

        [TestMethod]
        public void Test_NextOption_3()
        {
            Alias[] aliases =
            {
                Alias.Option("--width", null),
                Alias.Option("-W", "--width"),
            };

            string[] args = { "-W", "10" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("-W", arguments.Options[0].GivenName);
            Assert.AreEqual("--width", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("10", arguments.Options[0].Value);
        }
        #endregion

        #region Flag Tests
        [TestMethod]
        public void Test_Flag_1()
        {
            string[] args = { "-x" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("-x", arguments.Flags[0].GivenName);
            Assert.AreEqual("-x", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[0].Index);
            Assert.AreEqual(null, arguments.Flags[0].Value);
        }

        [TestMethod]
        public void Test_Flag_2()
        {
            string[] args = { "--X" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("--X", arguments.Flags[0].GivenName);
            Assert.AreEqual("--X", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[0].Index);
            Assert.AreEqual(null, arguments.Flags[0].Value);
        }

        [TestMethod]
        public void Test_Flag_3()
        {
            string[] args = { "-x", "--X" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(2, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("-x", arguments.Flags[0].GivenName);
            Assert.AreEqual("-x", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[0].Index);
            Assert.AreEqual(null, arguments.Flags[0].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[1].Type);
            Assert.AreEqual("--X", arguments.Flags[1].GivenName);
            Assert.AreEqual("--X", arguments.Flags[1].ResolvedName);
            Assert.AreEqual(1, arguments.Flags[1].Index);
            Assert.AreEqual(null, arguments.Flags[1].Value);
        }

        [TestMethod]
        public void Test_Flag_4()
        {
            string[] args = { "-w", "-h" };

            Arguments arguments = new Arguments(args);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(2, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("-w", arguments.Flags[0].GivenName);
            Assert.AreEqual("-w", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[0].Index);
            Assert.AreEqual(null, arguments.Flags[0].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[1].Type);
            Assert.AreEqual("-h", arguments.Flags[1].GivenName);
            Assert.AreEqual("-h", arguments.Flags[1].ResolvedName);
            Assert.AreEqual(1, arguments.Flags[1].Index);
            Assert.AreEqual(null, arguments.Flags[1].Value);
        }

        [TestMethod]
        public void Test_Flag_5()
        {
            Alias[] aliases =
            {
                Alias.Flag("-a", "--append"),
                Alias.Flag("-i", "--ignore-interrupts"),
            };

            string[] args = { "-a" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("-a", arguments.Flags[0].GivenName);
            Assert.AreEqual("--append", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[0].Index);
            Assert.AreEqual(null, arguments.Flags[0].Value);
        }
        #endregion

        #region Aliases Tests
        [TestMethod]
        public void Test_Aliases_1()
        {
            Alias[] aliases = { Alias.Flag("-D", "--debug=on") };

            string[] args = { "-D" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(1, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(0, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("-D", arguments.Options[0].GivenName);
            Assert.AreEqual("--debug", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("on", arguments.Options[0].Value);
        }

        [TestMethod]
        public void Test_Aliases_2()
        {
            Alias[] aliases = { };

            string[] args = { "-classpath" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(9, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(9, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("-classpath", arguments.Flags[0].GivenName);
            Assert.AreEqual("-c", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[0].Index);
            Assert.IsNull(arguments.Flags[0].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[1].Type);
            Assert.AreEqual("-classpath", arguments.Flags[1].GivenName);
            Assert.AreEqual("-l", arguments.Flags[1].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[1].Index);
            Assert.IsNull(arguments.Flags[1].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[2].Type);
            Assert.AreEqual("-classpath", arguments.Flags[2].GivenName);
            Assert.AreEqual("-a", arguments.Flags[2].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[2].Index);
            Assert.IsNull(arguments.Flags[2].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[3].Type);
            Assert.AreEqual("-classpath", arguments.Flags[3].GivenName);
            Assert.AreEqual("-s", arguments.Flags[3].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[3].Index);
            Assert.IsNull(arguments.Flags[3].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[4].Type);
            Assert.AreEqual("-classpath", arguments.Flags[4].GivenName);
            Assert.AreEqual("-s", arguments.Flags[4].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[4].Index);
            Assert.IsNull(arguments.Flags[4].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[5].Type);
            Assert.AreEqual("-classpath", arguments.Flags[5].GivenName);
            Assert.AreEqual("-p", arguments.Flags[5].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[5].Index);
            Assert.IsNull(arguments.Flags[5].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[6].Type);
            Assert.AreEqual("-classpath", arguments.Flags[6].GivenName);
            Assert.AreEqual("-a", arguments.Flags[6].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[6].Index);
            Assert.IsNull(arguments.Flags[6].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[7].Type);
            Assert.AreEqual("-classpath", arguments.Flags[7].GivenName);
            Assert.AreEqual("-t", arguments.Flags[7].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[7].Index);
            Assert.IsNull(arguments.Flags[7].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[8].Type);
            Assert.AreEqual("-classpath", arguments.Flags[8].GivenName);
            Assert.AreEqual("-h", arguments.Flags[8].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[8].Index);
            Assert.IsNull(arguments.Flags[8].Value);
        }

        [TestMethod]
        public void Test_Aliases_3()
        {
            Alias[] aliases = { Alias.Flag("-c", "--capture") };

            string[] args = { "-classpath" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(9, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(0, arguments.Options.Count);
            Assert.AreEqual(9, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("-classpath", arguments.Flags[0].GivenName);
            Assert.AreEqual("--capture", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[0].Index);
            Assert.IsNull(arguments.Flags[0].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[1].Type);
            Assert.AreEqual("-classpath", arguments.Flags[1].GivenName);
            Assert.AreEqual("-l", arguments.Flags[1].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[1].Index);
            Assert.IsNull(arguments.Flags[1].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[2].Type);
            Assert.AreEqual("-classpath", arguments.Flags[2].GivenName);
            Assert.AreEqual("-a", arguments.Flags[2].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[2].Index);
            Assert.IsNull(arguments.Flags[2].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[3].Type);
            Assert.AreEqual("-classpath", arguments.Flags[3].GivenName);
            Assert.AreEqual("-s", arguments.Flags[3].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[3].Index);
            Assert.IsNull(arguments.Flags[3].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[4].Type);
            Assert.AreEqual("-classpath", arguments.Flags[4].GivenName);
            Assert.AreEqual("-s", arguments.Flags[4].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[4].Index);
            Assert.IsNull(arguments.Flags[4].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[5].Type);
            Assert.AreEqual("-classpath", arguments.Flags[5].GivenName);
            Assert.AreEqual("-p", arguments.Flags[5].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[5].Index);
            Assert.IsNull(arguments.Flags[5].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[6].Type);
            Assert.AreEqual("-classpath", arguments.Flags[6].GivenName);
            Assert.AreEqual("-a", arguments.Flags[6].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[6].Index);
            Assert.IsNull(arguments.Flags[6].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[7].Type);
            Assert.AreEqual("-classpath", arguments.Flags[7].GivenName);
            Assert.AreEqual("-t", arguments.Flags[7].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[7].Index);
            Assert.IsNull(arguments.Flags[7].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[8].Type);
            Assert.AreEqual("-classpath", arguments.Flags[8].GivenName);
            Assert.AreEqual("-h", arguments.Flags[8].ResolvedName);
            Assert.AreEqual(0, arguments.Flags[8].Index);
            Assert.IsNull(arguments.Flags[8].Value);
        }

        [TestMethod]
        public void Test_Aliases_4()
        {
            Alias[] aliases = { Alias.Flag("-D", "--debug=on") };

            string[] args = { "-D", "-h" };

            Arguments arguments = new Arguments(args, aliases);

            Assert.AreEqual(2, arguments.FlagsAndOptions.Count);
            Assert.AreEqual(1, arguments.Options.Count);
            Assert.AreEqual(1, arguments.Flags.Count);
            Assert.AreEqual(0, arguments.Values.Count);

            Assert.AreEqual(ArgumentType.Option, arguments.Options[0].Type);
            Assert.AreEqual("-D", arguments.Options[0].GivenName);
            Assert.AreEqual("--debug", arguments.Options[0].ResolvedName);
            Assert.AreEqual(0, arguments.Options[0].Index);
            Assert.AreEqual("on", arguments.Options[0].Value);

            Assert.AreEqual(ArgumentType.Flag, arguments.Flags[0].Type);
            Assert.AreEqual("-h", arguments.Flags[0].GivenName);
            Assert.AreEqual("-h", arguments.Flags[0].ResolvedName);
            Assert.AreEqual(1, arguments.Flags[0].Index);
            Assert.AreEqual(null, arguments.Flags[0].Value);
        }
        #endregion
    }
}
