
// Created: 23rd June 2010
// Updated: 4th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for argument-related exceptions.
    /// </summary>
    public abstract class ArgumentException
        : ClaspException
    {
        #region fields

        private readonly string _optionName;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="argument">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May be <c>null</c>.
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception.
        /// </param>
        /// <param name="optionName">
        ///  The name of the flag/option.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        protected ArgumentException(IArgument argument, string message, string optionName, Exception innerException)
            : base(argument, MakeMessage_(message, optionName), innerException)
        {
            _optionName = optionName;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The option name associated with
        /// </summary>
        public string OptionName
        {
            get
            {
                return _optionName;
            }
        }
        #endregion

        #region implementation

        private static string MakeMessage_(string message, string optionName)
        {
            return String.Format("{0}: {1}", message, optionName);
        }
        #endregion
    }
}
