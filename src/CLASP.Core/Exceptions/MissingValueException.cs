
// Created: 19th June 2017
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    /// <summary>
    ///  Exception thrown to indicate a missing option.
    /// </summary>
    public class MissingValueException
        : ArgumentException
    {
        #region fields

        private readonly int    m_indexNotSatisfied;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the exception type.
        /// </summary>
        /// <param name="indexNotSatisfied">
        ///  The value index that was required but not satisfied
        /// </param>
        public MissingValueException(int indexNotSatisfied)
            : base(null, "value not specified at the required index", null)
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

