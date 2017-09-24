
// Created: 18th June 2017
// Updated: 19th June 2017

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
        ///  Constructs an instance of the type from the given resolved name
        /// </summary>
        /// <param name="resolvedName"></param>
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
