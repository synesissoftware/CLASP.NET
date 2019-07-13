
namespace Test.Unit.Exceptions
{
    using global::Clasp;
    using MissingOptionException = global::Clasp.Exceptions.MissingOptionException;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class MissingOptionException_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(MissingOptionException));
        }

        [TestMethod]
        public void test_construct_single_argument()
        {
            MissingOptionException x = new MissingOptionException("--abc");

            Assert.IsNull(x.Argument);
            Assert.AreEqual("option not specified: --abc", x.Message);
        }

        [TestMethod]
        public void test_construct_with_two_arguments()
        {
            MissingOptionException x = new MissingOptionException("--abc", "Missing required option");

            Assert.IsNull(x.Argument);
            Assert.AreEqual("Missing required option: --abc", x.Message);
        }
    }
}
