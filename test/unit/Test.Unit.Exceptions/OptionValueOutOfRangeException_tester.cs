
namespace Test.Unit.Exceptions
{
    using global::Clasp;
    using IArgument = global::Clasp.Interfaces.IArgument;
    using OptionValueOutOfRangeException = global::Clasp.Exceptions.OptionValueOutOfRangeException;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class OptionValueOutOfRangeException_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(OptionValueOutOfRangeException));
        }

        [TestMethod]
        public void test_construct_three_arguments()
        {
            string[] argv =
            {
                "--opt1=-1",
            };

            Clasp.Arguments arguments = new Arguments(argv);

            IArgument arg = arguments.Options[0];

            OptionValueOutOfRangeException x = new OptionValueOutOfRangeException("must not be negative", arg, typeof(int));

            Assert.AreSame(arg, x.Argument);
            Assert.AreEqual("invalid value for option --opt1: must not be negative", x.Message);
        }
    }
}
