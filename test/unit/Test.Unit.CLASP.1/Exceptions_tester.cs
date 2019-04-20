
// Created: 
// Updated: 24th September 2017

namespace Test.Unit.CLASP.ns_1
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Exceptions;
    using global::SynesisSoftware.SystemTools.Clasp.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

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
            #endregion
        }

        [TestMethod]
        public void Test_InvalidOptionValueException()
        {
            Exception x;

            x = new InvalidOptionValueException(new Argument(ArgumentType.Flag, -1, "--before", "-b", null), typeof(DateTime));

            Assert.AreEqual("invalid value for option: -b", x.Message);


            x = new InvalidOptionValueException(new Argument(ArgumentType.Flag, -1, "--before", null, null), typeof(DateTime));

            Assert.AreEqual("invalid value for option: --before", x.Message);
        }

        [TestMethod]
        public void Test_MissingOptionException()
        {
            Exception x;

            x = new MissingOptionException("--height");

            Assert.AreEqual("option not specified: --height", x.Message);
        }

        [TestMethod]
        public void Test_MissingOptionValueException()
        {
            Exception x;

            x = new MissingOptionValueException(new Argument(ArgumentType.Flag, -1, "--length", "-l", null));

            Assert.AreEqual("missing option value: -l", x.Message);


            x = new MissingOptionValueException(new Argument(ArgumentType.Flag, -1, "--length", null, null));

            Assert.AreEqual("missing option value: --length", x.Message);
        }

        [TestMethod]
        public void Test_MissingValueException()
        {
            Exception x;

            x = new MissingValueException(1);

            Assert.AreEqual("required value not specified", x.Message);
        }

        [TestMethod]
        public void Test_UnusedArgumentException()
        {
            Exception x;

            x = new UnusedArgumentException(new Argument(ArgumentType.Flag, -1, "--verbose", "-v", null));

            Assert.AreEqual("unused argument: -v", x.Message);


            x = new UnusedArgumentException(new Argument(ArgumentType.Flag, -1, "--verbose", null, null));

            Assert.AreEqual("unused argument: --verbose", x.Message);


            x = new UnusedArgumentException(new Argument(ArgumentType.Flag, -1, null, null, "file-path"));

            Assert.AreEqual("unused argument: file-path", x.Message);
        }
    }
}
