
// Created: 18th June 2017
// Updated: 14th August 2019

namespace Clasp.Binding
{
    using global::Clasp.Util;

    using global::System;
    using global::System.Reflection;

    /// <summary>
    ///  Abstract class for all bound fields
    /// </summary>
    public abstract class BoundFieldAttribute
        : Attribute
    {
        #region fields

        private readonly string m_resolvedName;
        #endregion

        #region construction

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        protected internal BoundFieldAttribute(string resolvedName)
        {
            m_resolvedName = resolvedName;
        }

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        protected internal BoundFieldAttribute(Type type)
        {
            m_resolvedName = GetRequiredPublicConstantField<string>(type, Constants.Arguments.ResolvedName);

            string s;

            if (LookupPublicConstantField(type, Constants.Arguments.Alias, out s))
            {
                this.Alias = s;
            }

            if (LookupPublicConstantField(type, Constants.Arguments.HelpDescription, out s))
            {
                this.HelpDescription = s;
            }

            if (LookupPublicConstantField(type, Constants.Arguments.HelpSection, out s))
            {
                this.HelpSection = s;
            }
        }
        #endregion

        #region properties

        /// <summary>
        ///  The resolved name
        /// </summary>
        public string ResolvedName
        {
            get
            {
                return m_resolvedName;
            }
        }

        /// <summary>
        ///  The alias
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///  Help description string
        /// </summary>
        public string HelpDescription { get; set; }

        /// <summary>
        ///  Help description section
        /// </summary>
        public string HelpSection { get; set; }
        #endregion

        #region operations

        /// <summary>
        ///  Looks up a required public constant field
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the field to be retrieved
        /// </typeparam>
        /// <param name="type">
        ///  The type in which to search for the public constant field
        /// </param>
        /// <param name="name">
        ///  The name of the public constant field
        /// </param>
        /// <returns>
        ///  The value of the public constant field in type
        ///  <paramref name="type"/>
        /// </returns>
        protected internal static T GetRequiredPublicConstantField<T>(Type type, string name)
        {
            T result;

            if (HasConstant_<T>(type, name, out result))
            {
                return result;
            }

            throw new MissingFieldException(type.FullName, name);
        }

        /// <summary>
        ///  Looks up an optional public constant field
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the field to be retrieved
        /// </typeparam>
        /// <param name="type">
        ///  The type in which to search for the public constant field
        /// </param>
        /// <param name="name">
        ///  The name of the public constant field
        /// </param>
        /// <param name="result">
        ///  The result
        /// </param>
        /// <returns>
        ///  <b>true</b> if the named field exists in type
        ///  <paramref name="type"/>; <b>false</b> otherwise
        /// </returns>
        protected internal static bool LookupPublicConstantField<T>(Type type, string name, out T result)
        {
            return HasConstant_<T>(type, name, out result);
        }
        #endregion

        #region implementation

        private static bool HasConstant_<T>(Type type, string name, out T result)
        {
            FieldInfo[] constants = ReflectionUtil.GetPublicConstantFields(type, ReflectionLookup.FromQueriedTypeAndAncestors);

            foreach (FieldInfo constant in constants)
            {
                if (name == constant.Name)
                {
                    object o = constant.GetValue(null);

                    result = (T)o;

                    return true;
                }
            }

            result = default(T);

            return false;
        }
        #endregion
    }
}
