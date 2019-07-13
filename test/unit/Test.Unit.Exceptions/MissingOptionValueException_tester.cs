
namespace Test.Unit.Exceptions
{
    using global::Clasp;
    using IArgument = global::Clasp.Interfaces.IArgument;
    using MissingOptionValueException = global::Clasp.Exceptions.MissingOptionValueException;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class MissingOptionValueException_tester
    {
        [TestMethod]
        public void test_types_exist()
        {
            Assert.IsNotNull(typeof(MissingOptionValueException));
        }

        [TestMethod]
        public void test_construct_single_argument()
        {
            string[] argv =
            {
                "--opt1=",
            };

            Clasp.Arguments arguments = new Arguments(argv);

            IArgument arg = arguments.Options[0];

            MissingOptionValueException x = new MissingOptionValueException(arg);

            Assert.AreSame(arg, x.Argument);
            Assert.AreEqual("missing option value: --opt1", x.Message);
        }
    }
}
