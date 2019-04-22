
// Suppresses warning that SimpleOptionStructure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Binding;
    using UsageUtil = global::SynesisSoftware.SystemTools.Clasp.Util.UsageUtil;
    using ClaspExceptions = global::SynesisSoftware.SystemTools.Clasp.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Text.RegularExpressions;

    [TestClass]
    public class Option_tester
    {
        #region Structures

        [BoundType]
        internal class SimpleOptionStructure
        {
            [BoundOption(@"--verbosity")]
            public string Verbosity;
        }

        [BoundType]
        internal class SimpleOptionStructure_with_allow_empty
        {
            [BoundOption(@"--verbosity", AllowEmpty=true)]
            public string Verbosity;
        }

        [BoundType(BindingOptions=ArgumentBindingOptions.IgnoreOtherOptions)]
        internal class SimpleOptionStructure_with_ignore
        {
            [BoundOption(@"--verbosity")]
            public string Verbosity;
        }

        [BoundType(BindingOptions=ArgumentBindingOptions.IgnoreOtherOptions, AttributeOptionsHavePrecedence=true)]
        internal class SimpleOptionStructure_with_ignore_and_precedence
        {
            [BoundOption(@"--verbosity")]
            public string Verbosity;
        }
        #endregion

        #region test methods

        // Tests:
        //
        // - one option given for the one required option with a non-empty value
        // - one option given for the one required option with a non-empty value + the values designator "--"
        // - one option given for the one required option with empty value (which is not allowed, by default), testing on the exit code
        // - one option given for the one required option with empty value (which is not allowed, by default), testing on the exception thrown
        // - one option given for the one required option with empty value which is allowed via property
        // - missing option, testing on the exit code
        // - missing option, testing on the exception thrown
        // - surplus option, testing on the exit code
        // - surplus option, testing on the exception thrown
        // - surplus option, which is allowed via property
        //
        // - surplus option, which is allowed via flags

        [TestMethod]
        public void Test_1_pass_required()
        {
            string[] argv =
            {
                @"--verbosity=silent",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual("silent", ss.Verbosity);

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_required_with_values_designator()
        {
            string[] argv =
            {
                @"--verbosity=silent",
                @"--",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual("silent", ss.Verbosity);

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_required_with_empty_value_testing_exit_code()
        {
            string[] argv =
            {
                @"--verbosity=",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual("", ss.Verbosity);

                return 12345;
            });

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Invoker.Constants.ExitCode_Failure, r);
        }

        [TestMethod]
        public void Test_1_pass_required_with_empty_value_testing_exception()
        {
            string[] argv =
            {
                @"--verbosity=",
            };

            bool enteredMain = false;

            int r = Int32.MinValue;

            try
            {
                r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) =>
                {

                    enteredMain = true;

                    Assert.AreEqual("", ss.Verbosity);

                    return 12345;
                }
                , ArgumentBindingOptions.None
                , ParseOptions.None
                , FailureOptions.HandleSystemExceptions
                );

                Assert.Fail("should not get here");
            }
            catch(ClaspExceptions.MissingOptionValueException x)
            {
                Assert.IsTrue(x.Message.Contains("missing option value"));
                Assert.IsTrue(x.Message.Contains("--verbosity"));

                Assert.IsNotNull(x.Argument);
                Assert.AreEqual(ArgumentType.Option, x.Argument.Type);
                Assert.AreEqual("--verbosity", x.Argument.ResolvedName);
            }

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Int32.MinValue, r);
        }

        [TestMethod]
        public void Test_1_pass_required_with_empty_value_that_is_allowed_via_property()
        {
            string[] argv =
            {
                @"--verbosity=",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure_with_allow_empty>(argv, null, (SimpleOptionStructure_with_allow_empty ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual("", ss.Verbosity);

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_insufficient_testing_exit_code()
        {
            string[] argv =
            {
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 12345;
            });

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Invoker.Constants.ExitCode_Failure, r);
        }

        [TestMethod]
        public void Test_1_pass_insufficient_testing_exception()
        {
            string[] argv =
            {
            };

            bool enteredMain = false;

            int r = Int32.MinValue;

            try
            {
                r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) =>
                {

                    enteredMain = true;

                    return 12345;
                }
                , ArgumentBindingOptions.None
                , ParseOptions.None
                , FailureOptions.None
                );
            }
            catch(ClaspExceptions.MissingOptionException x)
            {
                Assert.IsTrue(x.Message.Contains("option not specified"));
                Assert.IsTrue(x.Message.Contains("--verbosity"));

                Assert.IsNull(x.Argument);
            }

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Int32.MinValue, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_testing_exit_code()
        {
            string[] argv =
            {
                @"--verbosity=silent",
                @"--xyz=123",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 12345;
            });

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Invoker.Constants.ExitCode_Failure, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_testing_exception()
        {
            string[] argv =
            {
                @"--verbosity=silent",
                @"--xyz=123",
            };

            bool enteredMain = false;

            int r = Int32.MinValue;

            try
            {
                Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) =>
                {

                    enteredMain = true;

                    return 12345;
                }
                , ArgumentBindingOptions.None
                , ParseOptions.None
                , FailureOptions.None
                );
            }
            catch(ClaspExceptions.UnusedArgumentException x)
            {
                Assert.IsTrue(x.Message.Contains("unrecognised option"));
                Assert.IsTrue(x.Message.Contains("--xyz"));

                Assert.IsNotNull(x.Argument);
                Assert.AreEqual(ArgumentType.Option, x.Argument.Type);
                Assert.AreEqual("--xyz", x.Argument.ResolvedName);
                Assert.AreEqual("123", x.Argument.Value);
            }

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Int32.MinValue, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure()
        {
            string[] argv =
            {
                @"--verbosity=silent",
                @"--xyz=123",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure_with_ignore>(argv, null, (SimpleOptionStructure_with_ignore ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual("silent", ss.Verbosity);

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_flags()
        {
            string[] argv =
            {
                @"--verbosity=silent",
                @"--xyz=123",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleOptionStructure>(argv, null, (SimpleOptionStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual("silent", ss.Verbosity);

                return 12345;
            }
            , ArgumentBindingOptions.IgnoreOtherOptions
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }
        #endregion
    }
}
