
// Created: 13th July 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using global::System;

    /// <summary>
    ///  Thrown to indicate unused value(s)
    /// </summary>
    public class UnusedValueException
        : UnusedArgumentException
    {
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
        public UnusedValueException(IArgument arg, string message)
            : base(arg, MakeMessage_(arg, message))
        {
        }
        #endregion

        #region implementation

        private static string MakeMessage_(IArgument arg, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                return String.Format("unused value '{0}'", arg.Value);
            }

            return message;
        }
        #endregion
    }
}
