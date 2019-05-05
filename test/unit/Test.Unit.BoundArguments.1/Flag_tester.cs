
// Suppresses warning that structure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::Clasp;
    using global::Clasp.Binding;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class Flag_tester
    {
        #region Structures

        [BoundType]
        internal class SimpleFlagStructure
        {
            [BoundFlag(@"--verbose")]
            public bool Verbose;
        }

        [BoundType(BindingOptions=ArgumentBindingOptions.IgnoreOtherFlags)]
        internal class SimpleFlagStructure_with_ignore
        {
            [BoundFlag(@"--verbose")]
            public bool Verbose;
        }

        [BoundType(BindingOptions=ArgumentBindingOptions.IgnoreOtherFlags, AttributeOptionsHavePrecedence=true)]
        internal class SimpleFlagStructure_with_ignore_and_precedence
        {
            [BoundFlag(@"--verbose")]
            public bool Verbose;
        }
        #endregion

        #region test methods

        [TestMethod]
        public void Test_1_pass_required()
        {
            string[] argv =
            {
                @"--verbose",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure>(argv, null, (SimpleFlagStructure ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    Assert.IsTrue(ss.Verbose);

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
                @"--verbose",
                @"--",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure>(argv, null, (SimpleFlagStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.IsTrue(ss.Verbose);

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_insufficient()
        {
            string[] argv =
            {
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure>(argv, null, (SimpleFlagStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.IsFalse(ss.Verbose);

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus()
        {
            string[] argv =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure>(argv, null, (SimpleFlagStructure ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 12345;
            });

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Invoker.Constants.ExitCode_Failure, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore()
        {
            string[] argv =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure>(argv, null, (SimpleFlagStructure ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    return 12345;
                }, ArgumentBindingOptions.IgnoreOtherFlags);

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure()
        {
            string[] argv =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure_with_ignore>(argv, null, (SimpleFlagStructure_with_ignore ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure_overridden()
        {
            string[] argv =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure_with_ignore>(argv, null, (SimpleFlagStructure_with_ignore ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 12345;
            }
            , ArgumentBindingOptions.None
            );

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Invoker.Constants.ExitCode_Failure, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure_with_precendence()
        {
            string[] argv =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure_with_ignore_and_precedence>(argv, null, (SimpleFlagStructure_with_ignore_and_precedence ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 12345;
            });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure_with_precendence_ignoring_overridden()
        {
            string[] argv =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleFlagStructure_with_ignore_and_precedence>(argv, null, (SimpleFlagStructure_with_ignore_and_precedence ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 12345;
            }
            , ArgumentBindingOptions.None);

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }
        #endregion
    }
}
