
// Created: 18th June 2017
// Updated: 5th May 2019

namespace Clasp.Binding
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
