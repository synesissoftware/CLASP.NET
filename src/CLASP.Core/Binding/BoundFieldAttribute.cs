
// Created: 18th June 2017
// Updated: 7th June 2019

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
        ///  The resolved name
        /// </summary>
        public string ResolvedName
        {
            get
            {
                return m_resolvedName;
            }
        }

        /// <summary>
        ///  The alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///  Help description string
        /// </summary>
        public string HelpDescription { get; set; }

        /// <summary>
        ///  Help description section
        /// </summary>
        public string HelpSection { get; set; }
        #endregion
    }
}
