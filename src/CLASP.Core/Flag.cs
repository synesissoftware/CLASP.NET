
// Created: 8th June 2015
// Updated: 8th June 2015

namespace SynesisSoftware.SystemTools.Clasp
{
    /// <summary>
    ///  A flag alias.
    /// </summary>
    public class Flag
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
        public Flag(string givenName, string resolvedName, string description)
            : base(ArgumentType.Flag, givenName, resolvedName, description)
        {
        }
    }
}
