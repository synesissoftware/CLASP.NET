﻿
// Created: 23rd June 2010
// Updated: 4th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Exception thrown when an option value cannot be elicited as a
    ///  given type.
    /// </summary>
    public class InvalidOptionValueException
        : ArgumentException
    {
        #region Fields
        private readonly Type   _expectedType;
        #endregion

        #region Construction
        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="argument">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May be <c>null</c>.
        /// </param>
        /// <param name="optionName">
        ///  The name of the flag/option.
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        public InvalidOptionValueException(IArgument argument, string optionName, Type expectedType)
            : base(argument, null, optionName, null)
        {
            _expectedType = expectedType;
        }

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="argument">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May be <c>null</c>.
        /// </param>
        /// <param name="optionName">
        ///  The name of the flag/option.
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        public InvalidOptionValueException(IArgument argument, string optionName, Type expectedType, FormatException innerException)
            : base(argument, null, optionName, innerException)
        {
            _expectedType = expectedType;
        }
        #endregion

        #region Properties
        /// <summary>
        ///  The expected type of the option's value.
        /// </summary>
        public Type ExpectedType
        {
            get
            { return _expectedType;
            }
        }
        #endregion
    }
}
