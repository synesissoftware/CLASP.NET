
// Created: 17th May 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;
    using ExceptionUtil = global::Clasp.Internal.ExceptionUtil;

    using global::System;
    using global::System.Text;

    /// <summary>
    ///  Exception thrown when a value cannot be elicited as a
    ///  given type.
    /// </summary>
    public class InvalidValueException
        : InvalidArgumentException
    {
        #region fields

        private readonly Type   m_expectedType;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="value">
        ///  The value argument associated with the condition that caused
        ///  the exception to be thrown
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        public InvalidValueException(IArgument value, Type expectedType)
            : this(value, expectedType, null)
        {}

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="value">
        ///  The value argument associated with the condition that caused
        ///  the exception to be thrown
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        /// <param name="qualifiers">
        ///  0+ qualifier strings, each to be separated from the evaluated
        ///  message and each other by the separator <c>": "</c>
        /// </param>
        public InvalidValueException(IArgument value, Type expectedType, Exception innerException, params string[] qualifiers)
            : base(value, MakeMessage_(value, expectedType, innerException, qualifiers), null)
        {
            m_expectedType = expectedType;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The index that had an invalid value
        /// </summary>
        public int Index
        {
            get
            {
                return Argument.Index;
            }
        }

        /// <summary>
        ///  The expected type of the value
        /// </summary>
        public Type ExpectedType
        {
            get
            {
                return m_expectedType;
            }
        }
        #endregion

        #region implementation

        private static string MakeMessage_(IArgument arg, Type expectedType, Exception innerException, string[] qualifiers)
        {
            StringBuilder sb = new StringBuilder();

            if (0 == qualifiers.Length)
            {
                sb.AppendFormat(@"invalid value '{0}'", arg.Value);
            }
            else
            {
                sb.Append(@"invalid value");
            }

            if (null != innerException)
            {
                string xmsg = ExceptionUtil.PrettifyExceptionMessage(innerException);

                if (!String.IsNullOrWhiteSpace(xmsg))
                {
                    sb.Append(@": ");
                    sb.Append(xmsg);
                }
            }

            foreach (string qualifier in qualifiers)
            {
                sb.Append(@": ");
                sb.Append(qualifier);
            }

            return sb.ToString();
        }
        #endregion
    }
}
