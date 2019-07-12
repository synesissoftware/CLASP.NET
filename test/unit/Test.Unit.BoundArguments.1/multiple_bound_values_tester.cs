
// Suppresses warning that structure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::Clasp;
    using global::Clasp.Binding;
    using Exceptions = global::Clasp.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class Multiple_bound_values_tester
    {
        #region structures

        [BoundType]
        internal class ValueStruct
        {
            [BoundValue(0)]
            public string Value1;

            [BoundValue(1)]
            public string Value2;
        }
        #endregion

        [TestMethod]
        public void test_required_values_present()
        {
            string[] argv =
            {
                "val1",
                "val2",
            };

            bool enteredMain = false;
            int expectedExitCode = 123456;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<ValueStruct>(argv, null, (ValueStruct vs, Arguments clargs) => {

                Assert.AreEqual("val1", vs.Value1);
                Assert.AreEqual("val2", vs.Value2);

                enteredMain = true;

                return expectedExitCode;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }

        [TestMethod]
        [ExpectedException(typeof(Clasp.Exceptions.MissingValueException))]
        public void test_insufficient_values_present_throw()
        {
            string[] argv =
            {
                "val1",
            };

            Invoker.ParseAndInvokeMainWithBoundArgumentOfType<ValueStruct>(argv, null, (ValueStruct vs, Arguments clargs) => {

                Assert.Fail("should not get here");

                return -1;
            }
            , ArgumentBindingOptions.Default
            , ParseOptions.Default
            , FailureOptions.None
            );

            Assert.Fail("should not get here");
        }

        [TestMethod]
        public void test_insufficient_values_present_ignore()
        {
            string[] argv =
            {
                "val1",
            };

            bool enteredMain = false;
            int expectedExitCode = 123456;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<ValueStruct>(argv, null, (ValueStruct vs, Arguments clargs) => {

                Assert.AreEqual("val1", vs.Value1);
                Assert.IsNull(vs.Value2);

                enteredMain = true;

                return expectedExitCode;
            }
            , ArgumentBindingOptions.IgnoreMissingValues
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }

        [TestMethod]
        [ExpectedException(typeof(Clasp.Exceptions.UnusedArgumentException))]
        public void test_superfluous_values_present_throw()
        {
            string[] argv =
            {
                "val1",
                "val2",
                "val3",
            };

            Invoker.ParseAndInvokeMainWithBoundArgumentOfType<ValueStruct>(argv, null, (ValueStruct vs, Arguments clargs) => {

                Assert.Fail("should not get here");

                return -1;
            }
            , ArgumentBindingOptions.Default
            , ParseOptions.Default
            , FailureOptions.None
            );

            Assert.Fail("should not get here");
        }
    }
}
