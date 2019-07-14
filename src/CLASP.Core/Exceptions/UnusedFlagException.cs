
// Created: 13th July 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using global::System;

    /// <summary>
    ///  Thrown to indicate unused flag(s)
    /// </summary>
    public class UnusedFlagException
        : UnusedFlagOrOptionException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="arg">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May not be <c>null</c>
        /// </param>
        /// <param name="failureOptions">
        ///  The failure options specified to the parse operation
        /// </param>
        public UnusedFlagException(IArgument arg, FailureOptions failureOptions)
            : this(arg, failureOptions, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="arg">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May not be <c>null</c>
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception
        /// </param>
        /// <param name="failureOptions">
        ///  The failure options specified to the parse operation
        /// </param>
        public UnusedFlagException(IArgument arg, FailureOptions failureOptions, string message)
            : base(arg, message, failureOptions, ArgumentType.Flag)
        {
        }
        #endregion
    }
}
