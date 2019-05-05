﻿
// Created: 23rd June 2010
// Updated: 5th May 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

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
            ///  <see cref="Clasp.Exceptions.InvalidOptionValueException"/>
            /// </summary>
            public const string     DefaultMessage  =   @"invalid value for option";
        }
        #endregion

        #region fields

        private readonly Type   m_expectedType;
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
            m_expectedType = expectedType;
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
        public InvalidOptionValueException(IArgument option, Type expectedType, Exception innerException)
            : base(option, Constants.DefaultMessage, null, innerException)
        {
            m_expectedType = expectedType;
        }

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        protected internal InvalidOptionValueException(IArgument option, Type expectedType, Exception innerException, params string[] qualifiers)
            : base(option, Constants.DefaultMessage, option.ResolvedName, innerException, qualifiers)
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
    }
}
