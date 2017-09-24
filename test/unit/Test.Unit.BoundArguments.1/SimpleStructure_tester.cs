
// Suppresses warning that SimpleStructure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Binding;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class SimpleStructure_tester
    {
        [BoundType]
        internal class SimpleStructure
        {
            [BoundFlag(@"--verbose")]
            public bool Verbose;
        }

        [BoundType(ParseOptions=BoundArgumentParseOptions.IgnoreOtherFlags)]
        internal class SimpleStructure_with_ignore
        {
            [BoundFlag(@"--verbose")]
            public bool Verbose;
        }

        [BoundType(ParseOptions=BoundArgumentParseOptions.IgnoreOtherFlags, AttributeOptionsHavePrecedence=true)]
        internal class SimpleStructure_with_ignore_and_precedence
        {
            [BoundFlag(@"--verbose")]
            public bool Verbose;
        }

        [TestMethod]
        public void Test_1_pass_required()
        {
            string[] args =
            {
                @"--verbose",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure>(args, null, (SimpleStructure ss, Arguments args_UNUSED) =>
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
            string[] args =
            {
                @"--verbose",
                @"--",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure>(args, null, (SimpleStructure ss, Arguments args_UNUSED) =>
                {
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
            string[] args =
            {
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure>(args, null, (SimpleStructure ss, Arguments args_UNUSED) =>
                {
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
            string[] args =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure>(args, null, (SimpleStructure ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    return 12345;
                });

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Invoker.Constants.ExitCode_Failure, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore()
        {
            string[] args =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure>(args, null, (SimpleStructure ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    return 12345;
                }, BoundArgumentParseOptions.IgnoreOtherFlags);

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure()
        {
            string[] args =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure_with_ignore>(args, null, (SimpleStructure_with_ignore ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    return 12345;
                });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure_overridden()
        {
            string[] args =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure_with_ignore>(args, null, (SimpleStructure_with_ignore ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    return 12345;
                }, BoundArgumentParseOptions.None);

            Assert.IsFalse(enteredMain);
            Assert.AreEqual(Invoker.Constants.ExitCode_Failure, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure_with_precendence()
        {
            string[] args =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure_with_ignore_and_precedence>(args, null, (SimpleStructure_with_ignore_and_precedence ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    return 12345;
                });

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_1_pass_surplus_but_ignore_via_structure_with_precendence_ignoring_overridden()
        {
            string[] args =
            {
                @"--verbose",
                @"--silent",
            };

            bool enteredMain = false;

            int r = Invoker.InvokeMainAndParseBoundArgumentOfType<SimpleStructure_with_ignore_and_precedence>(args, null, (SimpleStructure_with_ignore_and_precedence ss, Arguments args_UNUSED) =>
                {
                    enteredMain = true;

                    return 12345;
                }, BoundArgumentParseOptions.None);

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

    }
}
