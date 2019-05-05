
// Created: 19th June 2017
// Updated: 5th May 2019

namespace Clasp.Exceptions
{
    using global::System;

    /// <summary>
    ///  Exception thrown to indicate a missing option.
    /// </summary>
    public class MissingValueException
        : ArgumentException
    {
        #region constants

        /// <summary>
        ///  Constants class
        /// </summary>
        public static class Constants
        {
            /// <summary>
            ///  The default message used by
            ///  <see cref="Clasp.Exceptions.MissingValueException"/>
            /// </summary>
            public const string     DefaultMessage  =   @"required value not specified";
        }
        #endregion

        #region fields

        private readonly int    m_indexNotSatisfied;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the exception type from the given
        ///  index.
        /// </summary>
        /// <param name="indexNotSatisfied">
        ///  The value index that was required but not satisfied
        /// </param>
        public MissingValueException(int indexNotSatisfied)
            : this(indexNotSatisfied, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the exception type from the given
        ///  index and message.
        /// </summary>
        /// <param name="indexNotSatisfied">
        ///  The value index that was required but not satisfied
        /// </param>
        /// <param name="message">
        ///  The message associated with the exception
        /// </param>
        public MissingValueException(int indexNotSatisfied, string message)
            : base(null, String.IsNullOrWhiteSpace(message) ? Constants.DefaultMessage : message, null)
        {
            m_indexNotSatisfied = indexNotSatisfied;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The value index that was required but not satisfied
        /// </summary>
        public int IndexNotSatisfied
        {
            get
            {
                return m_indexNotSatisfied;
            }
        }
        #endregion

        #region implementation

        #endregion
    }
}

