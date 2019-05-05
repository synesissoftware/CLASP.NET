
namespace Test.Unit.CLASP.ns_1
{
    using global::Clasp;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class ArgumentType_tester
    {
        [TestMethod]
        public void test_ArgumentType_type_exists()
        {
            Assert.IsNotNull(typeof(ArgumentType));
        }

        [TestMethod]
        public void test_ArgumentType_enumerators_differ()
        {
            Assert.AreNotEqual(ArgumentType.None, ArgumentType.Flag);
            Assert.AreNotEqual(ArgumentType.None, ArgumentType.Option);
            Assert.AreNotEqual(ArgumentType.None, ArgumentType.Value);

            Assert.AreNotEqual(ArgumentType.Flag, ArgumentType.Option);
            Assert.AreNotEqual(ArgumentType.Flag, ArgumentType.Value);

            Assert.AreNotEqual(ArgumentType.Option, ArgumentType.Value);
        }
    }
}
