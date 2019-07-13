
// Created: 17th May 2019
// Updated: 13th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using global::System;

    /// <summary>
    ///  Exception thrown when a value cannot be elicited as a
    ///  given type.
    /// </summary>
    public class InvalidValueException
        : ArgumentException
    {
        #region constants

        /// <summary>
        ///  Constants class
        /// </summary>
        public static class Constants
        {
            /// <summary>
            ///  The default message used by
            ///  <see cref="Clasp.Exceptions.MissingValueException"/>
            /// </summary>
            public const string     DefaultMessage  =   @"invalid value for value argument";
        }
        #endregion

        #region fields

        private readonly Type   m_expectedType;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class.
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
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="value">
        ///  The value argument associated with the condition that caused
        ///  the exception to be thrown
        /// </param>
        /// <param name="expectedType">
        ///  The expected type.
        /// </param>
        /// <param name="message">
        ///  The string associated with the exception
        /// </param>
        private InvalidValueException(IArgument value, Type expectedType, string message)
            : base(value, String.IsNullOrWhiteSpace(message) ? Constants.DefaultMessage : message, null)
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
    }
}
