
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

        [TestMethod]
        public void test_construct_single_argument()
        {
            string[] argv =
            {
                "value1",
            };

            Clasp.Arguments arguments = new Arguments(argv);

            IArgument arg = arguments.Values[0];

            UnusedArgumentException x = new UnusedArgumentException(arg);

            Assert.AreSame(arg, x.Argument);
            Assert.AreEqual("unused argument: value1", x.Message);
        }

        [TestMethod]
        public void test_construct_two_arguments()
        {
            string[] argv =
            {
                "value1",
            };

            Clasp.Arguments arguments = new Arguments(argv);

            IArgument arg = arguments.Values[0];

            UnusedArgumentException x = new UnusedArgumentException(arg, "Unused Arg");

            Assert.AreSame(arg, x.Argument);
            Assert.AreEqual("Unused Arg: value1", x.Message);
        }
    }
}
