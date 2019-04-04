
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
            ///  <see cref="InvalidOptionValueException"/>
            /// </summary>
            public const string     DefaultMessage  =   @"invalid value for option";
        }
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
        ///  the exception to be thrown. May be <c>null</c>.
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        public InvalidOptionValueException(IArgument option, Type expectedType)
            : base(option, Constants.DefaultMessage, null, null)
        {
            _expectedType = expectedType;
        }

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="option">
        ///  The option argument associated with the condition that caused
        ///  the exception to be thrown. May be <c>null</c>.
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        public InvalidOptionValueException(IArgument option, Type expectedType, FormatException innerException)
            : base(option, Constants.DefaultMessage, null, innerException)
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
