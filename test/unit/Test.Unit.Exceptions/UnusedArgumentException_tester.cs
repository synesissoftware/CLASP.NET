
namespace Test.Unit.Exceptions
{
    using global::Clasp;
    using IArgument = global::Clasp.Interfaces.IArgument;
    using UnusedArgumentException = global::Clasp.Exceptions.UnusedArgumentException;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class UnusedArgumentException_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(UnusedArgumentException));
        }
    }
}
