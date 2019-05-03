
// Created: 18th June 2017
// Updated: 3rd May 2019

namespace SynesisSoftware.SystemTools.Clasp.Binding
{
    using System;

    /// <summary>
    ///  Abstract class for all bound fields
    /// </summary>
    public abstract class BoundFieldAttribute
        : Attribute
    {
        #region fields

        private readonly string m_resolvedName;
        #endregion

        #region construction

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        protected internal BoundFieldAttribute(string resolvedName)
        {
            m_resolvedName = resolvedName;
        }
        #endregion

        #region properties

        /// <summary>
        ///
        /// </summary>
        public string ResolvedName
        {
            get
            {
                return m_resolvedName;
            }
        }
        #endregion
    }
}
