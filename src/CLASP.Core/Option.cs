
// Created: 8th June 2015
// Updated: 27th April 2019

namespace SynesisSoftware.SystemTools.Clasp
{
    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

    /// <summary>
    ///  An option specification.
    /// </summary>
    public class Option
        : Specification
    {
        #region fields

        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the class.
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
        public Option(string givenName, string resolvedName, string description, params string[] validValues)
            : base(ArgumentType.Option, givenName, resolvedName, description, validValues)
        {
        }

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="shortName">
        ///  The given name of the flag. May be <c>null</c> except
        ///  when <c>longName</c> is <c>null</c>.
        /// </param>
        /// <param name="longName">
        ///  The resolved name of the flag. May not be <c>null</c>
        ///  unless <c>shortName</c> is non-<c>null</c>.
        /// </param>
        public Option(string shortName, string longName)
            : base(ArgumentType.Option, shortName, longName)
        {
        }
        #endregion

        #region properties

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
        ///  Builder method that sets the <see cref="DefaultValue"/>
        /// </summary>
        /// <param name="value">
        ///  The default value
        /// </param>
        /// <returns>
        ///  The instance
        /// </returns>
        public Option WithDefaultValue(string value)
        {
            this.DefaultValue = value;

            return this;
        }
        #endregion
    }
}
