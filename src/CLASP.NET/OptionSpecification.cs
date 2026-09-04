
// Created: 8th June 2015
// Updated: 15th July 2019

namespace Clasp
{
    /// <summary>
    ///  An option specification.
    /// </summary>
    public class OptionSpecification
        : Specification
    {
        #region fields

        private readonly string[]                       m_validValues;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="givenName">
        ///  The given name of the flag. May be <c>null</c> except
        ///  when <c>resolvedName</c> is <c>null</c>.
        /// </param>
        /// <param name="resolvedName">
        ///  The resolved name of the flag. May not be <c>null</c>
        ///  unless <c>givenName</c> is non-<c>null</c>.
        /// </param>
        /// <param name="description">
        ///  The description of the flag, to be displayed in usage.
        /// </param>
        /// <param name="validValues">
        ///  Zero or more valid option values.
        /// </param>
        public OptionSpecification(string givenName, string resolvedName, string description, params string[] validValues)
            : base(ArgumentType.Option, givenName, resolvedName, description)
        {
            if (null == validValues)
            {
                validValues =   new string[0];
            }

            m_validValues   =   validValues;
        }

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="shortName">
        ///  The given name of the flag. May be <c>null</c> except
        ///  when <c>longName</c> is <c>null</c>.
        /// </param>
        /// <param name="longName">
        ///  The resolved name of the flag. May not be <c>null</c>
        ///  unless <c>shortName</c> is non-<c>null</c>.
        /// </param>
        public OptionSpecification(string shortName, string longName)
            : this(shortName, longName, null)
        {
        }
        #endregion

        #region properties

        /// <summary>
        ///  The valid values for an option.
        /// </summary>
        /// <remarks>
        ///  Will <b>never</b> be <b>null</b>
        /// </remarks>
        public string[] ValidValues
        {
            get
            {
                return m_validValues;
            }
        }

        /// <summary>
        ///  Default value for the option
        /// </summary>
        public string DefaultValue
        {
            get;
            set;
        }
        #endregion

        #region operations

        /// <summary>
        ///  Builder method that sets the <see cref="Clasp.OptionSpecification.DefaultValue"/>
        /// </summary>
        /// <param name="value">
        ///  The default value
        /// </param>
        /// <returns>
        ///  The instance
        /// </returns>
        public OptionSpecification WithDefaultValue(string value)
        {
            this.DefaultValue = value;

            return this;
        }
        #endregion
    }
}
