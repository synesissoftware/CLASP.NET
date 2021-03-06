﻿
// Suppresses warning that structure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::Clasp;
    using global::Clasp.Binding;
    using UsageUtil = global::Clasp.Util.UsageUtil;
    using ClaspExceptions = global::Clasp.Exceptions;

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

        [BoundType]
        internal class Point
        {
            [BoundOption(@"--x", AllowNegative=false)]
            public int X;

            [BoundOption(@"--y", AllowNegative=false)]
            public int Y;
        }

        [BoundType]
        internal class PointF
        {
            [BoundOption(@"--x")]
            public float X;

            [BoundOption(@"--y")]
            public float Y;
        }

        [BoundType]
        internal class PointFpos
        {
            [BoundOption(@"--x", NumberConstraints=BoundNumberConstraints.MustBePositive)]
            public float X;

            [BoundOption(@"--y", NumberConstraints=BoundNumberConstraints.MustBePositive)]
            public float Y;
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
            catch (ClaspExceptions.MissingOptionValueException x)
            {
                Assert.AreEqual("missing value for option --verbosity", x.Message);

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
            catch (ClaspExceptions.MissingOptionException x)
            {
                Assert.AreEqual("required option --verbosity not specified", x.Message);

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
            catch (ClaspExceptions.UnusedFlagOrOptionException x)
            {
                Assert.AreEqual("unrecognised option --xyz", x.Message);

                Assert.IsNotNull(x.Argument);
                Assert.AreEqual(ArgumentType.Option, x.Argument.Type);
                Assert.AreEqual("--xyz", x.Argument.ResolvedName);
                Assert.AreEqual("123", x.Argument.Value);
            }

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Int32.MinValue, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_testing_exception_UnusedAsUnused()
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
                , FailureOptions.ReportUnusedAsUnused
                );
            }
            catch (ClaspExceptions.UnusedFlagOrOptionException x)
            {
                Assert.AreEqual("given option --xyz not used", x.Message);

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

        [TestMethod]
        public void Test_numeric_Point_at_12_34()
        {
            string[] argv =
            {
                "--x=12",
                "--y=34",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<Point>(argv, null, (Point pt, Arguments clargs) => {

                Assert.AreEqual(12, pt.X);
                Assert.AreEqual(34, pt.Y);

                enteredMain = true;

                return expectedExitCode;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }

        [TestMethod]
        [ExpectedException(typeof(ClaspExceptions.OptionValueOutOfRangeException))]
        public void Test_numeric_Point_with_negative_X()
        {
            string[] argv =
            {
                "--x=-12",
                "--y=34",
            };

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<Point>(argv, null, (Point pt, Arguments clargs) => {

                return 0;
            }
            , ArgumentBindingOptions.Default
            , ParseOptions.Default
            , FailureOptions.None
            );
        }

        [TestMethod]
        public void Test_numeric_PointF_at_123dot456_7dot89()
        {
            string[] argv =
            {
                "--x=-123.456",
                "--y=7.89",
            };

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<PointF>(argv, null, (PointF pt, Arguments clargs) => {

                Assert.AreEqual(-123.456, pt.X, 0.00001);
                Assert.AreEqual(7.89, pt.Y, 0.00001);

                return 0;
            }
            , ArgumentBindingOptions.Default
            , ParseOptions.Default
            , FailureOptions.None
            );
        }

        [TestMethod]
        public void Test_numeric_PointFpos_at_minus123dot456_7dot89()
        {
            string[] argv =
            {
                "--x=-123.456",
                "--y=7.89",
            };

            try
            {
                Invoker.ParseAndInvokeMainWithBoundArgumentOfType<PointFpos>(argv, null, (PointFpos pt, Arguments clargs) => {

                    Assert.Fail("should not get here");

                    return 0;
                }
                , ArgumentBindingOptions.Default
                , ParseOptions.Default
                , FailureOptions.None
                );
            }
            catch (ClaspExceptions.OptionValueOutOfRangeException x)
            {
                Assert.IsNotNull(x.Argument);
            }
        }
        #endregion
    }
}
