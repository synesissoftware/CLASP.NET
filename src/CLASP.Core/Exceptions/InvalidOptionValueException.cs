
// Created: 23rd June 2010
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;
    using ExceptionUtil = global::Clasp.Internal.ExceptionUtil;

    using global::System;
    using global::System.Text;

    /// <summary>
    ///  Exception thrown when an option value cannot be elicited as a
    ///  given type.
    /// </summary>
    public class InvalidOptionValueException
        : InvalidOptionException
    {
        #region fields

        private readonly Type   m_expectedType;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="option">
        ///  The option argument associated with the condition that caused
        ///  the exception to be thrown. May be <c>null</c>
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        public InvalidOptionValueException(IArgument option, Type expectedType)
            : this(option, expectedType, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="option">
        ///  The option argument associated with the condition that caused
        ///  the exception to be thrown. May be <c>null</c>
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        /// <param name="qualifiers">
        ///  Optional qualifiers
        /// </param>
        public InvalidOptionValueException(IArgument option, Type expectedType, Exception innerException, params string[] qualifiers)
            : base(option, MakeMessage_(option, expectedType, innerException, null, qualifiers), innerException)
        {
            m_expectedType = expectedType;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The expected type of the option's value.
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

        private static string MakeMessage_(IArgument arg, Type expectedType, Exception innerException, string message, string[] qualifiers)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                StringBuilder sb = new StringBuilder();

                if (0 == qualifiers.Length)
                {
                    sb.AppendFormat(@"invalid value '{1}' for option {0}", arg.ResolvedName, arg.Value);
                }
                else
                {
                    sb.AppendFormat(@"invalid value for option {0}", arg.ResolvedName);
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

            return message;
        }
        #endregion
    }
}
