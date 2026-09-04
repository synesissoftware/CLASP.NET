
// Created: 23rd June 2010
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for CLASP.NET
    /// </summary>
    public abstract class ClaspException
        : Exception
    {
        #region fields

        private readonly IArgument  m_argument;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="arg">
        ///  The argument associated with the condition that caused the
        ///  exception to be thrown. May be <c>null</c>
        /// </param>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception
        /// </param>
        /// <param name="innerException">
        ///  Inner exception, or <c>null</c>.
        /// </param>
        protected ClaspException(IArgument arg, string message, Exception innerException)
            : base(message, innerException)
        {
            m_argument = arg;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The argument associated with the exception. May be <c>null</c>
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
