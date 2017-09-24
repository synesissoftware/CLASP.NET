﻿
// Created: 23rd June 2010
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Exception thrown when an option value cannot be elicited as a
    ///  given type.
    /// </summary>
    public class InvalidOptionValueException
        : FlagOrOptionArgumentException
    {
        #region constants

        private const string    MessagePrompt   =   "invalid value for option";
        #endregion

        #region fields

        private readonly Type   _expectedType;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="option">
        ///  The option argument associated with the condition that caused
        ///  the exception to be thrown. May be <code>null</code>.
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        public InvalidOptionValueException(IArgument option, Type expectedType)
            : base(option, MessagePrompt, null, null)
        {
            _expectedType = expectedType;
        }

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="option">
        ///  The option argument associated with the condition that caused
        ///  the exception to be thrown. May be <code>null</code>.
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <code>null</code>.
        /// </param>
        public InvalidOptionValueException(IArgument option, Type expectedType, FormatException innerException)
            : base(option, MessagePrompt, null, innerException)
        {
            _expectedType = expectedType;
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
                return _expectedType;
            }
        }
        #endregion
    }
}
