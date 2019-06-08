
// Created: 18th June 2017
// Updated: 5th May 2019

namespace Clasp.Binding
{
    using System;

    /// <summary>
    ///  Attribute applied to structures (and classes) to be subject to
    ///  bound parsing
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class BoundTypeAttribute
        : System.Attribute
    {
        #region fields

        private ArgumentBindingOptions?     m_bindingOptions;
        private ParseOptions?               m_parsingOptions;
        #endregion

        #region construction

        /// <summary>
        ///  Constructor
        /// </summary>
        public BoundTypeAttribute()
        {
        }
        #endregion

        #region properties

        /// <summary>
        ///  The binding options specified with the attribute
        /// </summary>
        public ArgumentBindingOptions BindingOptions
        {
            get
            {
                return m_bindingOptions.HasValue ? m_bindingOptions.Value : ArgumentBindingOptions.Default;
            }
            set
            {
                m_bindingOptions = value;
            }
        }
        internal ArgumentBindingOptions? GivenBindingOptions
        {
            get
            {
                return m_bindingOptions;
            }
        }

        /// <summary>
        ///  The parsing options specified with the attribute
        /// </summary>
        public ParseOptions ParsingOptions
        {
            get
            {
                return m_parsingOptions.HasValue ? m_parsingOptions.Value : ParseOptions.Default;
            }
            set
            {
                m_parsingOptions = value;
            }
        }
        internal ParseOptions? GivenParsingOptions
        {
            get
            {
                return m_parsingOptions;
            }
        }

        /// <summary>
        ///  If <c>true</c> the attribute's parse options take precedence
        ///  over any passed to InvokeMainWithBoundArgumentOfType()
        /// </summary>
        public bool AttributeOptionsHavePrecedence { get; set; }
        #endregion
    }
}
