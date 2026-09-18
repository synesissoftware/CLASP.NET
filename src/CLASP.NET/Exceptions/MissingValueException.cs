
// Created: 19th June 2017
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::System;

    /// <summary>
    ///  Exception thrown to indicate a missing option.
    /// </summary>
    public class MissingValueException
        : MissingArgumentException
    {
        #region fields

        private readonly int    m_indexNotSatisfied;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="indexNotSatisfied">
        ///  The value index that was required but not satisfied
        /// </param>
        public MissingValueException(int indexNotSatisfied)
            : this(indexNotSatisfied, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="indexNotSatisfied">
        ///  The value index that was required but not satisfied
        /// </param>
        /// <param name="message">
        ///  The message associated with the exception
        /// </param>
        public MissingValueException(int indexNotSatisfied, string message)
            : base(MakeMessage_(indexNotSatisfied, message))
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

        private static string MakeMessage_(int indexNotSatisfied, string message)
        {
            if (String.IsNullOrWhiteSpace(message))
            {
                return String.Format("required value at index {0} not specified", indexNotSatisfied);
            }

            return message;
        }
        #endregion
    }
}

