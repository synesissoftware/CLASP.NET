
// Created: 19th June 2017
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for argument-related exceptions.
    /// </summary>
    public abstract class FlagOrOptionArgumentException
        : ArgumentException
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
        ///  exception to be thrown. May be <code>null</code>.
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception.
        /// </param>
        /// <param name="optionName">
        ///  The name of the flag/option.
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <code>null</code>.
        /// </param>
        protected FlagOrOptionArgumentException(IArgument argument, string message, string optionName, Exception innerException)
            : base(argument, MakeMessage_(argument, message, optionName), innerException)
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

        private static string MakeMessage_(IArgument argument, string message, string optionName)
        {
            if(null != argument)
            {
                if(String.IsNullOrEmpty(optionName))
                {
                    optionName = argument.GivenName;
                }

                if(String.IsNullOrEmpty(optionName))
                {
                    optionName = argument.ResolvedName;
                }

                if(String.IsNullOrEmpty(optionName))
                {
                    optionName = argument.Value;
                }
            }

            return String.Format("{0}: {1}", message, optionName);
        }
        #endregion
    }
}
