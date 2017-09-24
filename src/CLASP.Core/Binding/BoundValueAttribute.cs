
// Created: 19th June 2017
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Binding
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///  Attribute applied to a field that is loaded from a single value
    /// </summary>
    public class BoundValueAttribute
        : Attribute
    {
        #region fields

        private readonly int    m_valueIndex;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance
        /// </summary>
        /// <param name="valueIndex">
        ///  The index of the value. Must be >= 0
        /// </param>
        public BoundValueAttribute(int valueIndex)
        {
            Debug.Assert(valueIndex >= 0);

            m_valueIndex = valueIndex;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The index of the value that will be bound to the field
        /// </summary>
        public int ValueIndex
        {
            get
            {
                return m_valueIndex;
            }
        }

        /// <summary>
        ///  The default value to be used. If <c>null</c>, the value is not
        ///  optional
        /// </summary>
        public string DefaultValue { get; set; }
        #endregion
    }
}
