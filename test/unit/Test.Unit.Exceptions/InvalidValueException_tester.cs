
namespace Test.Unit.Exceptions
{
    using global::Clasp;
    using IArgument = global::Clasp.Interfaces.IArgument;
    using InvalidValueException = global::Clasp.Exceptions.InvalidValueException;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class InvalidValueException_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(InvalidValueException));
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

            InvalidValueException x = new InvalidValueException(arg, typeof(int));

            Assert.AreSame(arg, x.Argument);
            Assert.AreEqual("invalid value for value argument", x.Message);
            //Assert.AreEqual("invalid value for option --opt1: number expected", x.Message);
        }
    }
}
