
// Created: 19th June 2017
// Updated: 5th May 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for argument-related exceptions.
    /// </summary>
    public abstract class FlagOrOptionArgumentException
        : ArgumentException
    {
        #region fields

        private readonly string m_optionName;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="argument">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May be <c>null</c>.
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception.
        /// </param>
        /// <param name="optionName">
        ///  The name of the flag/option.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        /// <param name="qualifiers">
        ///  0+ qualifier strings, each to be separated from the evaluated
        ///  message and each other by the separator <c>": "</c>
        /// </param>
        protected FlagOrOptionArgumentException(IArgument argument, string message, string optionName, Exception innerException, params string[] qualifiers)
            : base(argument, MakeMessage_(argument, message, optionName, qualifiers), innerException)
        {
            m_optionName = optionName;
        }
        #endregion

        #region properties

        #endregion

        #region implementation

        private static string MakeMessage_(IArgument argument, string message, string optionName, params string[] qualifiers)
        {
            if(null != argument)
            {
                if(String.IsNullOrEmpty(optionName))
                {
                    optionName = argument.GivenName;
                }

                if(String.IsNullOrEmpty(optionName))
                {
                    optionName = argument.ResolvedName;
                }

                if(String.IsNullOrEmpty(optionName))
                {
                    optionName = argument.Value;
                }
            }

            if(!String.IsNullOrEmpty(optionName))
            {
                message = String.Format(@"{0}: {1}", message, optionName);
            }

            if(0 != qualifiers.Length)
            {
                message = message + ": " + String.Join(": ", qualifiers);
            }

            return message;
        }
        #endregion
    }
}
