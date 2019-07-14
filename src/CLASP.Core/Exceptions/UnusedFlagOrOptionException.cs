
// Created: 13th July 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using global::System;

    /// <summary>
    ///  Root exception for unused flag / option arguments
    /// </summary>
    public abstract class UnusedFlagOrOptionException
        : UnusedArgumentException
    {
        #region fields

        private readonly FailureOptions m_failureOptions;
        #endregion

        #region construction

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
        /// <param name="argumentType">
        ///  The argument type
        /// </param>
        /// <param name="failureOptions">
        ///  The failure options specified to the parse operation
        /// </param>
        protected UnusedFlagOrOptionException(IArgument arg, string message, FailureOptions failureOptions, ArgumentType argumentType)
            : base(arg, MakeMessage_(arg, message, failureOptions, argumentType))
        {
            m_failureOptions = failureOptions;
        }
        #endregion

        #region implementation

        private static string MakeMessage_(IArgument arg, string message, FailureOptions failureOptions, ArgumentType argumentType)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                if (0 == (FailureOptions.ReportUnusedAsUnused & failureOptions))
                {
                    return String.Format("unrecognised {0} {1}", argumentType.ToString().ToLower(), arg.ResolvedName);
                }
                else
                {
                    return String.Format("given {0} {1} not used", argumentType.ToString().ToLower(), arg.ResolvedName);
                }
            }

            return message;
        }
        #endregion
    }
}
