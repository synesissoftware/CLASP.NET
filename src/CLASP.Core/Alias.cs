﻿
// Created: 23rd July 2009
// Updated: 4th April 2019

namespace SynesisSoftware.SystemTools.Clasp
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///  Represents an alias.
    /// </summary>
    public abstract class Alias
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="givenName"></param>
        /// <param name="resolvedName"></param>
        /// <param name="description"></param>
        /// <param name="validValues"></param>
        protected internal Alias(ArgumentType type, string givenName, string resolvedName, string description, params string[] validValues)
        {
            Debug.Assert(null != givenName || null != resolvedName);

            if(null == validValues)
            {
                validValues =   new string[0];
            }

            Type            =   type;
            GivenName       =   givenName;
            ResolvedName    =   resolvedName;
            Description     =   description;
            ValidValues     =   validValues;
        }

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="shortName"></param>
        /// <param name="longName"></param>
        protected internal Alias(ArgumentType type, string shortName, string longName)
            : this(type, shortName, longName, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the class.
        /// </summary>
        /// <param name="description"></param>
        protected internal Alias(string description)
        {
            Debug.Assert(!String.IsNullOrEmpty(description));

            Type            =   ArgumentType.None;
            GivenName       =   null;
            ResolvedName    =   null;
            Description     =   description;

            ValidValues     =   new string[0];
        }
        #endregion

        #region creator methods

        /// <summary>
        ///  Creates a flag alias.
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="resolvedName"></param>
        /// <param name="description"></param>
        /// <returns>
        ///  An alias.
        /// </returns>
        public static Flag Flag(string givenName, string resolvedName, string description)
        {
            return new Flag(givenName, resolvedName, description);
        }

        /// <summary>
        ///  Creates a flag alias.
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="resolvedName"></param>
        /// <returns>
        ///  An alias.
        /// </returns>
        public static Flag Flag(string givenName, string resolvedName)
        {
            return new Flag(givenName, resolvedName, null);
        }

        /// <summary>
        ///  Creates an option alias.
        /// </summary>
        /// <param name="shortName"></param>
        /// <param name="longName"></param>
        /// <returns>
        ///  An alias.
        /// </returns>
        public static Option Option(string shortName, string longName)
        {
            return new Option(shortName, longName);
        }

        /// <summary>
        ///  Creates an option alias.
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="resolvedName"></param>
        /// <param name="description"></param>
        /// <param name="validValues"></param>
        /// <returns>
        ///  An alias.
        /// </returns>
        public static Option Option(string givenName, string resolvedName, string description, params string[] validValues)
        {
            return new Option(givenName, resolvedName, description, validValues);
        }

        /// <summary>
        ///  Creates a section separator alias.
        /// </summary>
        /// <param name="sectionName">
        ///  The name of the section.
        /// </param>
        /// <returns>
        ///  An alias.
        /// </returns>
        public static SectionSeparator SectionSeparator(string sectionName)
        {
            return new SectionSeparator(sectionName);
        }

        #endregion

        #region overrides

        /// <summary>
        ///  A string representation of the alias.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{{{0}, {1}, {2}, {3}}}", Type, GivenName, ResolvedName, Description);
        }
        #endregion

        #region properties

        /// <summary>
        ///  The alias type.
        /// </summary>
        public ArgumentType Type
        {
            get;
            private set;
        }
        /// <summary>
        ///  The given name of the alias.
        /// </summary>
        public string GivenName
        {
            get;
            private set;
        }
        /// <summary>
        ///  The resolved name of the alias.
        /// </summary>
        public string ResolvedName
        {
            get;
            private set;
        }
        /// <summary>
        ///  The description of the alias.
        /// </summary>
        public string Description
        {
            get;
            private set;
        }

        /// <summary>
        ///  The valid values for an option.
        /// </summary>
        public string[] ValidValues
        {
            get;
            private set;
        }
        #endregion
    }
}
