
// Created: 23rd June 2010
// Updated: 4th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for CLASP.NET.
    /// </summary>
    public abstract class ClaspException
        : Exception
    {
        #region Fields
        private readonly IArgument  _argument;
        #endregion

        #region Construction
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
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        protected ClaspException(IArgument argument, string message, Exception innerException)
            : base(message, innerException)
        {
            _argument = argument;
        }
        #endregion

        #region Properties
        /// <summary>
        ///  The argument associated with the exception.
        /// </summary>
        public IArgument Argument
        {
            get
            {
                return _argument;
            }
        }
        #endregion
    }
}
