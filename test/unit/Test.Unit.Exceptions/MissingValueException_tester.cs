
namespace Test.Unit.Exceptions
{
    using global::Clasp;
    using IArgument = global::Clasp.Interfaces.IArgument;
    using MissingValueException = global::Clasp.Exceptions.MissingValueException;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class MissingValueException_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(MissingValueException));
        }

        [TestMethod]
        public void test_construct_single_argument()
        {
            MissingValueException x = new MissingValueException(1);

            Assert.IsNull(x.Argument);
            Assert.AreEqual(1, x.IndexNotSatisfied);
            Assert.AreEqual("required value at index 1 not specified", x.Message);
        }

        [TestMethod]
        public void test_construct_with_two_arguments()
        {
            MissingValueException x = new MissingValueException(1, "missing required value");

            Assert.IsNull(x.Argument);
            Assert.AreEqual(1, x.IndexNotSatisfied);
            Assert.AreEqual("missing required value", x.Message);
        }
    }
}
