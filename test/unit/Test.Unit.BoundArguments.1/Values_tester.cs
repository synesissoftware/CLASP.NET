
// Suppresses warning that SimpleFlagStructure elements never changed
#pragma warning disable 0649

namespace Test.Unit.BoundArguments.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Binding;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Collections.Generic;

    [TestClass]
    public class Values_tester
    {
        #region Structures

        [BoundType]
        internal class SimpleValueStructure_with_string_array
        {
            [BoundValues(Base=0, Minimum=2)]
            public string[] FilePaths;
        }

        [BoundType]
        internal class SimpleValueStructure_with_List_of_string
        {
            [BoundValues(Base=0, Minimum=2)]
            public List<string> FilePaths;
        }
        #endregion

        #region test methods

        [TestMethod]
        public void Test_has_minimum_values_required_for_string_array()
        {
            string[] argv =
            {
                @"--",
                @"file-1",
                @"file-2",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleValueStructure_with_string_array>(argv, null, (SimpleValueStructure_with_string_array ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual(2, ((ICollection<string>)ss.FilePaths).Count);
                Assert.AreEqual(@"file-1", ss.FilePaths[0]);
                Assert.AreEqual(@"file-2", ss.FilePaths[1]);

                return 12345;
            }
            , ArgumentBindingOptions.None
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        public void Test_has_minimum_values_required_for_List_of_string()
        {
            string[] argv =
            {
                @"--",
                @"file-1",
                @"file-2",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleValueStructure_with_List_of_string>(argv, null, (SimpleValueStructure_with_List_of_string ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual(2, ((ICollection<string>)ss.FilePaths).Count);
                Assert.AreEqual(@"file-1", ss.FilePaths[0]);
                Assert.AreEqual(@"file-2", ss.FilePaths[1]);

                return 12345;
            }
            , ArgumentBindingOptions.None
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }

        [TestMethod]
        [ExpectedException(typeof(global::SynesisSoftware.SystemTools.Clasp.Exceptions.MissingValueException))]
        public void Test_has_too_few_values()
        {
            string[] argv =
            {
                @"--",
                @"file-1",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleValueStructure_with_List_of_string>(argv, null, (SimpleValueStructure_with_List_of_string ss, Arguments args_UNUSED) => {

                enteredMain = true;

                return 1;
            }
            , ArgumentBindingOptions.None
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsFalse(enteredMain);
        }

        [TestMethod]
        public void Test_has_too_few_values_but_ignored()
        {
            string[] argv =
            {
                @"--",
                @"file-1",
            };

            bool enteredMain = false;

            int r = Invoker.ParseAndInvokeMainWithBoundArgumentOfType<SimpleValueStructure_with_List_of_string>(argv, null, (SimpleValueStructure_with_List_of_string ss, Arguments args_UNUSED) => {

                enteredMain = true;

                Assert.AreEqual(1, ((ICollection<string>)ss.FilePaths).Count);
                Assert.AreEqual(@"file-1", ss.FilePaths[0]);

                return 12345;
            }
            , ArgumentBindingOptions.IgnoreMissingValues
            , ParseOptions.None
            , FailureOptions.HandleSystemExceptions
            );

            Assert.IsTrue(enteredMain);
            Assert.AreEqual(12345, r);
        }
        #endregion
    }
}
