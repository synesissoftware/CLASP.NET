
namespace Test.Unit.Exceptions
{
    using global::Clasp;
    using IArgument = global::Clasp.Interfaces.IArgument;
    using InvalidOptionValueException = global::Clasp.Exceptions.InvalidOptionValueException;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class InvalidOptionValueException_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(InvalidOptionValueException));
        }

        [TestMethod]
        public void test_construct_two_arguments()
        {
            string[] argv =
            {
                "--opt1=val1",
            };

            Clasp.Arguments arguments = new Arguments(argv);

            IArgument arg = arguments.Options[0];

            InvalidOptionValueException x = new InvalidOptionValueException(arg, typeof(int));

            Assert.AreSame(arg, x.Argument);
            Assert.AreEqual("invalid value for option argument: --opt1", x.Message);
        }

        [TestMethod]
        public void test_construct_with_three_arguments()
        {
            string[] argv =
            {
                "--opt1=val1",
            };

            Clasp.Arguments arguments = new Arguments(argv);

            IArgument arg = arguments.Options[0];

            InvalidOptionValueException x = new InvalidOptionValueException(arg, typeof(int), new SystemException("blah"));

            Assert.AreSame(arg, x.Argument);
            Assert.AreEqual("invalid value for option argument: --opt1", x.Message);
        }
    }
}
