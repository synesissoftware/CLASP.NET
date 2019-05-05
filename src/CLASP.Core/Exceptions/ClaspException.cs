
// Created: 23rd June 2010
// Updated: 5th May 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for CLASP.NET.
    /// </summary>
    public abstract class ClaspException
        : Exception
    {
        #region fields

        private readonly IArgument  m_argument;
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
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        protected ClaspException(IArgument argument, string message, Exception innerException)
            : base(message, innerException)
        {
            m_argument = argument;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The argument associated with the exception. May be <c>nul</c>
        /// </summary>
        public IArgument Argument
        {
            get
            {
                return m_argument;
            }
        }
        #endregion
    }
}
