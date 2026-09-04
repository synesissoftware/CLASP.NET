
// Created: 13th July 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Root exception for missing arguments
    /// </summary>
    public abstract class MissingArgumentException
        : ArgumentException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="message">
        ///  The human-readable message to be associated with the exception
        /// </param>
        protected MissingArgumentException(string message)
            : base(null, message, null)
        {
        }
        #endregion
    }
}
