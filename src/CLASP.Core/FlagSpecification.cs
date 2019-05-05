
// Created: 8th June 2015
// Updated: 5th May 2019

namespace Clasp
{
    /// <summary>
    ///  A flag specification.
    /// </summary>
    public class FlagSpecification
        : Specification
    {
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
        public FlagSpecification(string givenName, string resolvedName, string description)
            : base(ArgumentType.Flag, givenName, resolvedName, description)
        {
        }
    }
}
