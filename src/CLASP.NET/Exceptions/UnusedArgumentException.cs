
// Created: 23rd June 2010
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using global::System;
    using global::System.Diagnostics;

    /// <summary>
    ///  Exception thrown to indicate an unused flag/option.
    /// </summary>
    public abstract class UnusedArgumentException
        : ArgumentException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="arg">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May not be <c>null</c>.
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception
        /// </param>
        protected UnusedArgumentException(IArgument arg, string message)
            : base(arg, message, null)
        {
            Debug.Assert(null != arg);
        }
        #endregion
    }
}
