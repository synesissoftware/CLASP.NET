
// Created: 18th June 2017
// Updated: 19th June 2017

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

        private bool m_allowEmpty;
        private bool m_allowNegative;
        private bool m_allowFraction;
        private object m_defaultValue;
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
                return m_allowNegative;
            }
            set
            {
                m_allowNegative = value;
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
                return m_allowFraction;
            }
            set
            {
                m_allowFraction = value;
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
