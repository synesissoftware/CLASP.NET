
// Created: 23rd July 2009
// Updated: 8th June 2019

namespace Clasp
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    ///  Represents an argument specification.
    /// </summary>
    public abstract class Specification
    {
        #region fields

        private readonly ArgumentType                   m_argumentType;
        private readonly string                         m_givenName;
        private readonly string                         m_resolvedName;
        private readonly string                         m_description;
        private readonly string[]                       m_validValues;
        private readonly bool                           m_isSection;
        private readonly IDictionary<string, object>    m_extras;
        #endregion

        #region construction

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        protected internal Specification(ArgumentType type, string givenName, string resolvedName, string description, params string[] validValues)
        {
            Debug.Assert(null != givenName || null != resolvedName);

            if(null == validValues)
            {
                validValues =   new string[0];
            }

            m_argumentType  =   type;
            m_givenName     =   givenName;
            m_resolvedName  =   resolvedName;
            m_description   =   description;
            m_validValues   =   validValues;
            m_isSection     =   false;

            m_extras        =   new Dictionary<string, object>();
        }

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        protected internal Specification(ArgumentType type, string shortName, string longName)
            : this(type, shortName, longName, null)
        {
        }

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        protected internal Specification(string description)
        {
            Debug.Assert(!String.IsNullOrEmpty(description));

            m_argumentType  =   ArgumentType.None;
            m_givenName     =   null;
            m_resolvedName  =   null;
            m_description   =   description;
            m_validValues   =   new string[0];
            m_isSection     =   true;
        }
        #endregion

        #region creator methods

        /// <summary>
        ///  Creates an aliases for a flag or option
        /// </summary>
        /// <param name="resolvedName">The resolved name</param>
        /// <param name="aliasName">The alias name</param>
        /// <returns>
        ///  A specification representing the alias
        /// </returns>
        public static Clasp.Specification Alias(string resolvedName, string aliasName)
        {
            return new FlagSpecification(aliasName, resolvedName, null);
        }

        /// <summary>
        ///  Creates a flag specification.
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="resolvedName"></param>
        /// <param name="description"></param>
        /// <returns>
        ///  A flag specification.
        /// </returns>
        public static FlagSpecification Flag(string givenName, string resolvedName, string description)
        {
            return new FlagSpecification(givenName, resolvedName, description);
        }

        /// <summary>
        ///  Creates a flag specification.
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="resolvedName"></param>
        /// <returns>
        ///  A flag specification.
        /// </returns>
        public static FlagSpecification Flag(string givenName, string resolvedName)
        {
            return new FlagSpecification(givenName, resolvedName, null);
        }

        /// <summary>
        ///  Creates an option specification.
        /// </summary>
        /// <param name="shortName"></param>
        /// <param name="longName"></param>
        /// <returns>
        ///  An option specification.
        /// </returns>
        public static OptionSpecification Option(string shortName, string longName)
        {
            return new OptionSpecification(shortName, longName);
        }

        /// <summary>
        ///  Creates an option specification.
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="resolvedName"></param>
        /// <param name="description"></param>
        /// <param name="validValues"></param>
        /// <returns>
        ///  An option specification.
        /// </returns>
        public static OptionSpecification Option(string givenName, string resolvedName, string description, params string[] validValues)
        {
            return new OptionSpecification(givenName, resolvedName, description, validValues);
        }

        /// <summary>
        ///  Creates a section separator specification.
        /// </summary>
        /// <param name="sectionName">
        ///  The name of the section.
        /// </param>
        /// <returns>
        ///  A section separator specification.
        /// </returns>
        public static SectionSeparator Section(string sectionName)
        {
            return new SectionSeparator(sectionName);
        }

        /// <summary>
        ///  [DEPRECATED] Instead use <see cref="Clasp.Specification.Section(string)"/>.
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public static SectionSeparator SectionSeparator(string sectionName)
        {
            return new SectionSeparator(sectionName);
        }
        #endregion

        #region overrides

        /// <summary>
        ///  A string representation of the specification.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{{{0}, {1}, {2}, {3}}}", Type, GivenName, ResolvedName, Description);
        }
        #endregion

        #region properties

        /// <summary>
        ///  The specification type.
        /// </summary>
        public ArgumentType Type
        {
            get
            {
                return m_argumentType;
            }
        }
        /// <summary>
        ///  The given name of the specification.
        /// </summary>
        public string GivenName
        {
            get
            {
                return m_givenName;
            }
        }
        /// <summary>
        ///  The resolved name of the specification.
        /// </summary>
        public string ResolvedName
        {
            get
            {
                return m_resolvedName;
            }
        }
        /// <summary>
        ///  The description of the specification.
        /// </summary>
        public string Description
        {
            get
            {
                return m_description;
            }
        }

        /// <summary>
        ///  The valid values for an option.
        /// </summary>
        public string[] ValidValues
        {
            get
            {
                return m_validValues;
            }
        }

        /// <summary>
        ///  Extras
        /// </summary>
        public IDictionary<string, object> Extras
        {
            get
            {
                return m_extras;
            }
        }

        /// <summary>
        ///  Indicates whether it is a separator
        /// </summary>
        internal bool IsSection
        {
            get
            {
                if(m_isSection)
                {
                    Debug.Assert(String.IsNullOrWhiteSpace(ResolvedName));
                    Debug.Assert(String.IsNullOrWhiteSpace(GivenName));
                }

                return m_isSection;
            }
        }
        #endregion
    }
}
