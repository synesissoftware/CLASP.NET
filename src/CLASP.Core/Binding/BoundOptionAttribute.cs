
// Created: 18th June 2017
// Updated: 3rd May 2019

namespace SynesisSoftware.SystemTools.Clasp.Binding
{
    using System;

    /// <summary>
    ///  Attribute applied to fields to receive a value from a command-line
    ///  option
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BoundOptionAttribute
        : BoundFieldAttribute
    {
        #region fields

        private bool                m_allowEmpty;
        private bool                m_requirePositive;
        private bool                m_requireWhole;
        private object              m_defaultValue;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the attribute that matches the given
        ///  option name
        /// </summary>
        /// <param name="optionName">
        ///  The option name
        /// </param>
        public BoundOptionAttribute(string optionName)
            : base(optionName)
        {
        }
        #endregion

        #region properties

        /// <summary>
        ///  Determines whether empty values are allowed
        /// </summary>
        public bool AllowEmpty
        {
            get
            {
                return m_allowEmpty;
            }
            set
            {
                m_allowEmpty = value;
            }
        }

        /// <summary>
        ///  Determines whether negative values are allowed, when parsing
        ///  numeric values; ignored otherwise
        /// </summary>
        public bool AllowNegative
        {
            get
            {
                return !m_requirePositive;
            }
            set
            {
                m_requirePositive = !value;
            }
        }

        /// <summary>
        ///  Determines whether fractional values are allowed, when parsing
        ///  numeric values; ignored otherwise
        /// </summary>
        public bool AllowFraction
        {
            get
            {
                return !m_requireWhole;
            }
            set
            {
                m_requireWhole = !value;
            }
        }

        /// <summary>
        ///  The default value to be used
        /// </summary>
        public object DefaultValue
        {
            get
            {
                return m_defaultValue;
            }
            set
            {
                m_defaultValue = value;
            }
        }
        #endregion
    }
}
