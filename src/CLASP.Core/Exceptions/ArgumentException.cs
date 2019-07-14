
// Created: 23rd June 2010
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for argument-related exceptions.
    /// </summary>
    public abstract class ArgumentException
        : ClaspException
    {
        #region fields

        #endregion

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
        protected ArgumentException(IArgument arg, string message, Exception innerException)
            : base(arg, message, innerException)
        {
        }
        #endregion

        #region properties

        #endregion

        #region implementation

        #endregion
    }
}
