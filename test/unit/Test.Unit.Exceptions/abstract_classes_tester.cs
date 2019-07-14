
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

#if NON_EXISTENT

            Assert.IsNotNull(typeof(Clasp.Exceptions.FlagOrOptionArgumentException));
            Assert.IsTrue(typeof(Clasp.Exceptions.FlagOrOptionArgumentException).IsAbstract);
#endif

            Assert.IsNotNull(typeof(Clasp.Exceptions.InvalidArgumentException));
            Assert.IsTrue(typeof(Clasp.Exceptions.InvalidArgumentException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.MissingArgumentException));
            Assert.IsTrue(typeof(Clasp.Exceptions.MissingArgumentException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.UnusedArgumentException));
            Assert.IsTrue(typeof(Clasp.Exceptions.UnusedArgumentException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.InvalidFlagOrOptionException));
            Assert.IsTrue(typeof(Clasp.Exceptions.InvalidFlagOrOptionException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.MissingFlagOrOptionException));
            Assert.IsTrue(typeof(Clasp.Exceptions.MissingFlagOrOptionException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.UnusedFlagOrOptionException));
            Assert.IsTrue(typeof(Clasp.Exceptions.UnusedFlagOrOptionException).IsAbstract);

            Assert.IsNotNull(typeof(Clasp.Exceptions.InvalidOptionException));
            Assert.IsTrue(typeof(Clasp.Exceptions.InvalidOptionException).IsAbstract);
        }
    }
}
