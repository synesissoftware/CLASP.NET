
// Created: 
// Updated: 2nd June 2014

namespace SynesisSoftware.SystemTools.Clasp
{
    using System;
    using System.Diagnostics;

    public sealed class Alias
    {
        #region Construction

        public Alias(ArgumentType type, string givenName, string resolvedName, string description, params string[] validOptions)
        {
            Debug.Assert(null != givenName || null != resolvedName);

            Type = type;
            GivenName = givenName;
            ResolvedName = resolvedName;
            Description = description;
        }
        public Alias(ArgumentType type, string shortName, string longName)
            : this(type, shortName, longName, null)
        {
        }

        private Alias(string description)
        {
            Debug.Assert(!String.IsNullOrEmpty(description));

            Type = ArgumentType.None;
            GivenName = null;
            ResolvedName = null;
            Description = description;
        }

        #endregion

        #region Creator methods

        public static Alias Flag(string givenName, string resolvedName, string description)
        {
            return new Alias(ArgumentType.Flag, givenName, resolvedName, description);
        }

        public static Alias SectionSeparator(string description)
        {
            return new Alias(description);
        }

        #endregion

        #region Operations

        public override string ToString()
        {
            return String.Format("{{{0}, {1}, {2}, {3}}}", Type, GivenName, ResolvedName, Description);
        }

        #endregion

        #region Properties

        /// <summary>
        ///  The alias type.
        /// </summary>
        public ArgumentType Type
        {
            get;
            set;
        }
        /// <summary>
        ///  The given name of the alias.
        /// </summary>
        public string GivenName
        {
            get;
            set;
        }
        /// <summary>
        ///  The resolved name of the alias.
        /// </summary>
        public string ResolvedName
        {
            get;
            set;
        }
        /// <summary>
        ///  The description of the alias.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        #endregion
    }
}
