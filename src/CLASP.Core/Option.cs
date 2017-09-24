
// Created: 8th June 2015
// Updated: 8th June 2015

namespace SynesisSoftware.SystemTools.Clasp
{
    /// <summary>
    ///  An option alias.
    /// </summary>
    public class Option
        : Alias
    {
        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="givenName">
        ///  The given name of the flag. May be <code>null</code> except
        ///  when <code>resolvedName</code> is <code>null</code>.
        /// </param>
        /// <param name="resolvedName">
        ///  The resolved name of the flag. May not be <code>null</code>
        ///  unless <code>givenName</code> is non-<code>null</code>.
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
        ///  The given name of the flag. May be <code>null</code> except
        ///  when <code>longName</code> is <code>null</code>.
        /// </param>
        /// <param name="longName">
        ///  The resolved name of the flag. May not be <code>null</code>
        ///  unless <code>shortName</code> is non-<code>null</code>.
        /// </param>
        public Option(string shortName, string longName)
            : base(ArgumentType.Option, shortName, longName)
        {
        }
    }
}
