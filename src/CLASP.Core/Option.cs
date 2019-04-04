﻿
// Created: 8th June 2015
// Updated: 4th April 2019

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
    }
}
