
namespace Test.Unit.Exceptions
{
    using global::Clasp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class abstract_classes_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(Clasp.Exceptions.ClaspException));
            Assert.IsTrue(typeof(Clasp.Exceptions.ClaspException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.ArgumentException));
            Assert.IsTrue(typeof(Clasp.Exceptions.ArgumentException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.FlagOrOptionArgumentException));
            Assert.IsTrue(typeof(Clasp.Exceptions.FlagOrOptionArgumentException).IsAbstract);
        }
    }
}
