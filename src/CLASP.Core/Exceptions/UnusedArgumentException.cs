
// Created: 23rd June 2010
// Updated: 30th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using global::SynesisSoftware.SystemTools.Clasp.Interfaces;

    using global::System;

    /// <summary>
    ///  Exception thrown to indicate an unused flag/option.
    /// </summary>
    public class UnusedArgumentException
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
            ///  <see cref="Clasp.Exceptions.UnusedArgumentException"/>
            /// </summary>
            public const string     DefaultMessage  =   @"unused argument";
        }
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the exception from the given
        ///  argument and message
        /// </summary>
        /// <param name="arg">
        ///  The argument that is not used.
        /// </param>
        public UnusedArgumentException(IArgument arg)
            : this(arg, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the exception from the given
        ///  argument and message
        /// </summary>
        /// <param name="arg">
        ///  The argument that is not used.
        /// </param>
        /// <param name="message">
        ///  The message to be associated with the exception
        /// </param>
        public UnusedArgumentException(IArgument arg, string message)
            : base(arg, String.IsNullOrWhiteSpace(message) ? Constants.DefaultMessage : message, null, null)
        {
        }
        #endregion
    }
}
