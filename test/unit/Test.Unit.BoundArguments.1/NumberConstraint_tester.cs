
// Suppresses warning that structure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Binding;
    using ClaspExceptions = global::SynesisSoftware.SystemTools.Clasp.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class NumberConstraint_tester
    {
        #region Structures

        [BoundType]
        internal class Rectangle_non_negative_whole_numbers
        {
            [BoundOption(@"--height", NumberConstraints=BoundNumberConstraints.MustBeIntegral | BoundNumberConstraints.MustBeNonNegative)]
            public int Height;

            [BoundOption(@"--width", NumberConstraints=BoundNumberConstraints.MustBeIntegral | BoundNumberConstraints.MustBeNonNegative)]
            public int Width;
        }

        [BoundType]
        internal class Rectangle_non_negative_rounded_numbers
        {
            [BoundOption(@"--height", NumberConstraints=BoundNumberConstraints.MustBeNonNegative, NumberTruncate=NumberTruncate.ToNearest)]
            public int Height;

            [BoundOption(@"--width", NumberConstraints=BoundNumberConstraints.MustBeNonNegative, NumberTruncate=NumberTruncate.ToNearest)]
            public int Width;
        }
        #endregion

        #region test methods

        [TestMethod]
        public void Test_nonnegative_and_integral_with_valid_values()
        {
            string[] argv =
            {
                "--height=1",
                "--width=2",
            };

            bool enteredMain = false;
            int expectedExitCode = 123456;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<Rectangle_non_negative_whole_numbers>(argv, null, (Rectangle_non_negative_whole_numbers rectangle, Arguments clargs) => {

                Assert.AreEqual(1, rectangle.Height);
                Assert.AreEqual(2, rectangle.Width);

                enteredMain = true;

                return expectedExitCode;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }

        [TestMethod]
        public void Test_whole_Rectangle_with_negative_height()
        {
            string[] argv =
            {
                "--height=-12",
                "--width=34",
            };

            try
            {
                Invoker.ParseAndInvokeMainWithBoundArgumentOfType<Rectangle_non_negative_whole_numbers>(argv, null, (Rectangle_non_negative_whole_numbers rect, Arguments clargs) => {

                    Assert.Fail("should not get here");

                    return 0;
                }
                , ArgumentBindingOptions.Default
                , ParseOptions.Default
                , FailureOptions.None
                );
            }
            catch(ClaspExceptions.OptionValueOutOfRangeException x)
            {
                Assert.IsNotNull(x.Argument);
                Assert.AreEqual(0, x.Argument.Index);
                Assert.AreEqual("--height", x.Argument.ResolvedName);

                Assert.IsNotNull(x.ExpectedType);
                Assert.AreEqual(typeof(int), x.ExpectedType);

                Assert.AreEqual("invalid value for option: --height: must not be negative", x.Message);
            }
        }

        [TestMethod]
        public void Test_whole_Rectangle_with_fractional_height()
        {
            string[] argv =
            {
                "--height=12.5",
                "--width=34",
            };

            try
            {
                Invoker.ParseAndInvokeMainWithBoundArgumentOfType<Rectangle_non_negative_whole_numbers>(argv, null, (Rectangle_non_negative_whole_numbers rect, Arguments clargs) =>
                {

                    return 0;
                }
                , ArgumentBindingOptions.Default
                , ParseOptions.Default
                , FailureOptions.None
                );
            }
            catch(ClaspExceptions.InvalidOptionValueException x)
            {
                Assert.IsNotNull(x.Argument);
                Assert.AreEqual(0, x.Argument.Index);
                Assert.AreEqual("--height", x.Argument.ResolvedName);

                Assert.IsNotNull(x.ExpectedType);
                Assert.AreEqual(typeof(int), x.ExpectedType);

                Assert.AreEqual("invalid value for option: --height: whole number required", x.Message);
            }
        }

        [TestMethod]
        public void Test_rounded_Rectangle_with_fractional_height()
        {
            string[] argv =
            {
                "--height=11.5",
                "--width=15.48",
            };

            Invoker.ParseAndInvokeMainWithBoundArgumentOfType<Rectangle_non_negative_rounded_numbers>(argv, null, (Rectangle_non_negative_rounded_numbers rect, Arguments clargs) =>
            {
                Assert.AreEqual(12, rect.Height);
                Assert.AreEqual(15, rect.Width);

                return 0;
            }
            , ArgumentBindingOptions.Default
            , ParseOptions.Default
            , FailureOptions.None
            );
        }
        #endregion
    }
}
