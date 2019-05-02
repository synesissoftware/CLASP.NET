
// Suppresses warning that structure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Binding;
    using Exceptions = global::SynesisSoftware.SystemTools.Clasp.Exceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class EmptyStructure_tester
    {
        #region Structures

        [BoundType]
        internal class EmptyStructure
        {
        }
        #endregion

        #region test methods

        [TestMethod]
        public void Test_EmptyStructure_with_no_arguments()
        {
            string[] argv =
            {
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }

        [TestMethod]
        public void Test_EmptyStructure_with_only_double_slash()
        {
            string[] argv =
            {
                "--",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.UnusedArgumentException))]
        public void Test_EmptyStructure_with_extra_flag()
        {
            string[] argv =
            {
                "-f",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            }
            , ArgumentBindingOptions.None
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsFalse(enteredMain);
        }

        [TestMethod]
        public void Test_EmptyStructure_with_extra_flag_but_ignored()
        {
            string[] argv =
            {
                "-f",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            }
            , ArgumentBindingOptions.IgnoreOtherFlags
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.UnusedArgumentException))]
        public void Test_EmptyStructure_with_extra_option()
        {
            string[] argv =
            {
                "-o=v",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            }
            , ArgumentBindingOptions.None
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsFalse(enteredMain);
        }

        [TestMethod]
        public void Test_EmptyStructure_with_extra_option_but_ignored()
        {
            string[] argv =
            {
                "-o=v",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            }
            , ArgumentBindingOptions.IgnoreOtherOptions
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }
        #endregion

        [TestMethod]
        [ExpectedException(typeof(Exceptions.UnusedArgumentException))]
        public void Test_EmptyStructure_with_extra_value()
        {
            string[] argv =
            {
                "v",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            }
            , ArgumentBindingOptions.None
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsFalse(enteredMain);
        }

        [TestMethod]
        public void Test_EmptyStructure_with_extra_value_but_ignored()
        {
            string[] argv =
            {
                "v",
            };

            int expectedExitCode = 123456;
            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<EmptyStructure>(argv, null, (EmptyStructure args, Arguments args_UNUSED) => {

                enteredMain = true;

                return expectedExitCode;
            }
            , ArgumentBindingOptions.IgnoreExtraValues
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(expectedExitCode, r);
        }
    }
}
