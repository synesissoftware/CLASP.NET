
// Created: 23rd June 2010
// Updated: 13th July 2019

namespace Clasp.Exceptions
{
    using global::System;

    /// <summary>
    ///  Exception thrown to indicate a missing option.
    /// </summary>
    public class MissingOptionException
        : FlagOrOptionArgumentException
    {
        #region constants

        /// <summary>
        ///  Constants class
        /// </summary>
        public static class Constants
        {
            /// <summary>
            ///  The default message used by
            ///  <see cref="Clasp.Exceptions.MissingOptionException"/>
            /// </summary>
            public const string     DefaultMessage  =   @"option not specified";
        }
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the exception type.
        /// </summary>
        /// <param name="optionName">
        /// </param>
        public MissingOptionException(string optionName)
            : base(null, Constants.DefaultMessage, optionName, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the exception from the given
        ///  argument and message
        /// </summary>
        /// <param name="optionName">
        ///  The name of the option. Must <b>not</b> be <c>null</c>
        /// </param>
        /// <param name="message">
        ///  The prefix of the message to be associated with the exception
        /// </param>
        public MissingOptionException(string optionName, string message)
            : base(null, String.IsNullOrWhiteSpace(message) ? Constants.DefaultMessage : message, optionName, null)
        {
        }
        #endregion
    }
}
