
namespace Test.Unit.CLASP.ns_1
{
    using global::Clasp;
    using global::Clasp.Exceptions;
    using global::Clasp.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    [TestClass]
    public class Exceptions_tester
    {
        internal class Argument
            : IArgument
        {
            #region fields

            readonly ArgumentType   m_type;
            readonly int            m_index;
            readonly string         m_resolvedName;
            readonly string         m_giveName;
            readonly string         m_value;
            #endregion

            #region construction
            internal Argument(ArgumentType type, int index, string resolvedName, string givenName, string value)
            {
                m_type          =   type;
                m_index         =   index;
                m_resolvedName  =   resolvedName;
                m_giveName      =   givenName;
                m_value         =   value;
            }
            #endregion

            #region IArgument
            ArgumentType IArgument.Type
            {
                get
                {
                    return m_type;
                }
            }

            string IArgument.ResolvedName
            {
                get
                {
                    return m_resolvedName;
                }
            }

            string IArgument.GivenName
            {
                get
                {
                    return m_giveName;
                }
            }

            string IArgument.Value
            {
                get
                {
                    return m_value;
                }
            }

            int IArgument.Index
            {
                get
                {
                    return m_index;
                }
            }

            Specification IArgument.Specification
            {
                get
                {
                    return null;
                }
            }

            bool IArgument.Used
            {
                get
                {
                    return true;
                }
            }

            void IArgument.Use()
            {
                ;
            }
            #endregion
        }

        [TestMethod]
        public void Test_InvalidOptionValueException()
        {
            Exception x;

            x = new InvalidOptionValueException(new Argument(ArgumentType.Flag, -1, "--before", "-b", null), typeof(DateTime));

            Assert.AreEqual("invalid value '' for option --before", x.Message);


            x = new InvalidOptionValueException(new Argument(ArgumentType.Flag, -1, "--before", null, "xyz"), typeof(DateTime));

            Assert.AreEqual("invalid value 'xyz' for option --before", x.Message);
        }

        [TestMethod]
        public void Test_MissingOptionException()
        {
            Exception x;

            x = new MissingOptionException("--height");

            Assert.AreEqual("required option --height not specified", x.Message);
        }

        [TestMethod]
        public void Test_MissingOptionValueException()
        {
            Exception x;

            x = new MissingOptionValueException(new Argument(ArgumentType.Flag, -1, "--length", "-l", null));

            Assert.AreEqual("missing value for option --length", x.Message);


            x = new MissingOptionValueException(new Argument(ArgumentType.Flag, -1, "--length", null, null));

            Assert.AreEqual("missing value for option --length", x.Message);
        }

        [TestMethod]
        public void Test_MissingValueException()
        {
            Exception x;

            x = new MissingValueException(1);

            Assert.AreEqual("required value at index 1 not specified", x.Message);
        }

#if NON_EXISTENT

        [TestMethod]
        public void Test_UnusedFlagOrOptionException()
        {
            Exception x;

            x = new UnusedFlagOrOptionException(new Argument(ArgumentType.Flag, -1, "--verbose", "-v", null));

            Assert.AreEqual("unused argument: -v", x.Message);


            x = new UnusedFlagOrOptionException(new Argument(ArgumentType.Flag, -1, "--verbose", null, null));

            Assert.AreEqual("unused argument: --verbose", x.Message);


            x = new UnusedFlagOrOptionException(new Argument(ArgumentType.Flag, -1, null, null, "file-path"));

            Assert.AreEqual("unused argument: file-path", x.Message);
        }
#endif
    }
}
