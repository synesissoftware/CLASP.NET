﻿
// Created: 13th July 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using global::System;

    /// <summary>
    ///  Root exception for invalid flag / option arguments
    /// </summary>
    public abstract class InvalidFlagOrOptionException
        : InvalidArgumentException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="arg">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May be <c>null</c>
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        protected InvalidFlagOrOptionException(IArgument arg, string message, Exception innerException)
            : base(arg, message, innerException)
        {
        }
        #endregion
    }
}
