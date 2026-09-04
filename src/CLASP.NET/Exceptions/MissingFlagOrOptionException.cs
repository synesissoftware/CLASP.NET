
// Created: 13th July 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::System;

    /// <summary>
    ///  Root exception for missing flag / option arguments
    /// </summary>
    public abstract class MissingFlagOrOptionException
        : MissingArgumentException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="name">
        ///  The name of the missing flag/option
        /// </param>
        /// <param name="argumentType">
        ///  The type of the argument
        /// </param>
        /// <param name="message">
        ///  A message. If <c>null</c>, a message is derived from
        ///  <paramref name="name"/> and
        ///  <paramref name="argumentType" />
        /// </param>
        protected MissingFlagOrOptionException(string name, ArgumentType argumentType, string message)
            : base(MakeMessage_(name, argumentType, message))
        {
        }
        #endregion

        #region implementation

        private static string MakeMessage_(string name, ArgumentType argumentType, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                return String.Format("required {0} {1} not specified", argumentType.ToString().ToLower(), name);
            }

            return message;
        }
        #endregion
    }
}
