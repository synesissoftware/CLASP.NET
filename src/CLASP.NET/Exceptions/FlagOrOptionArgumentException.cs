
// Created: 19th June 2017
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for argument-related exceptions.
    /// </summary>
    [Obsolete("This exception is obsolete, no longer has any children, and will be removed in a future release")]
    public abstract class FlagOrOptionArgumentException
        : ArgumentException
    {
        #region fields

        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="argument">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May be <c>null</c>
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception
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
            : base(argument, "", null)
        {
        }
        #endregion

        #region properties

        #endregion

        #region implementation

        #endregion
    }
}
