
// Created: 19th June 2017
// Updated: 14th August 2019

namespace Clasp.Util
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    ///  Options for controlling reflection lookups
    /// </summary>
    public enum ReflectionLookup
    {
        /// <summary>
        ///  Only the given type is used for lookup
        /// </summary>
        FromQueriedTypeOnly             =   0,
        /// <summary>
        ///  The type and its ancestors are used for lookup
        /// </summary>
        FromQueriedTypeAndAncestors     =   1,
    }

    /// <summary>
    ///  Utility class for reflection operations
    /// </summary>
    public static class ReflectionUtil
    {
        #region constants

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        internal static class Constants
        {
        }
        #endregion

        #region conversion operations

        /// <summary>
        ///  Visible (greppable) cast operator, used to improve code
        ///  transparency
        /// </summary>
        /// <typeparam name="T">
        ///  The type to which the object <c>o</c> will be cast
        /// </typeparam>
        /// <param name="o">
        ///  The object instance to be cast to type <c>T</c>
        /// </param>
        /// <returns>
        ///  The value of <c>o</c> cast to type <c>T</c>
        /// </returns>
        /// <remarks>
        ///  This method taken from the Synesis Software component
        ///  <c>SynSoft.Conversion.CastUtil</c> type, by kind permission of
        ///  Synesis Software Pty Ltd
        /// </remarks>
        /// <exception cref="System.InvalidCastException">
        ///  Since this method is implemented in terms of the cast operator
        ///  - <c>(T)o</c> - it is subject to the same requirements, so an
        ///  instance of <c>System.InvalidCastException</c> (or one of its
        ///  derived types) is thrown if the required cast cannot be
        ///  performed
        /// </exception>
        public static T CastTo<T>(object o)
        {
            return (T)o;
        }
        #endregion

        #region attribute operations

        /// <summary>
        ///  Obtains a typed array of attributes from a reflection member
        ///  information object
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the attribute. Must be derived from
        ///  <see cref="System.Attribute"/>
        /// </typeparam>
        /// <param name="mi">
        ///  The reflection member information object. May not be
        ///  <c>null</c>
        /// </param>
        /// <param name="reflectionLookup">
        ///  Flags that control the lookup
        /// </param>
        /// <returns>
        ///  An array with 0 or more <typeparamref name="T"/> instances
        ///  representing the attributes attached to the given reflection
        ///  member information
        /// </returns>
        public static T[] GetAttributes<T>(MemberInfo mi, ReflectionLookup reflectionLookup) where T : System.Attribute
        {
            Debug.Assert(null != mi);

            object[] attributes = mi.GetCustomAttributes(typeof(T), ReflectionLookup.FromQueriedTypeAndAncestors == reflectionLookup);

            T[] r = new T[attributes.Length];

            for (int i = 0; attributes.Length != i; ++i)
            {
                r[i] = CastTo<T>(attributes[i]);
            }

            return r;
        }

        /// <summary>
        ///  Obtains the first, if any, attribute of type
        ///  <typeparamref name="T"/> attached to the element referenced by
        ///  the reflection member information object
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the attribute. Must be derived from
        ///  <see cref="System.Attribute"/>
        /// </typeparam>
        /// <param name="mi">
        ///  The reflection member information object. May not be
        ///  <c>null</c>
        /// </param>
        /// <param name="reflectionLookup">
        ///  Flags that control the lookup
        /// </param>
        /// <returns>
        ///  The first attribute, or <c>null</c> if none found
        /// </returns>
        public static T GetFirstAttributeOrNull<T>(MemberInfo mi, ReflectionLookup reflectionLookup) where T : System.Attribute
        {
            T[] ar = GetAttributes<T>(mi, reflectionLookup);

            return (0 == ar.Length) ? null : ar[0];
        }

        /// <summary>
        ///  Obtains the only attribute of type <typeparamref name="T"/>
        ///  attached to the element referenced by the reflection member
        ///  information object
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the attribute. Must be derived from
        ///  <see cref="System.Attribute"/>
        /// </typeparam>
        /// <param name="mi">
        ///  The reflection member information object. May not be
        ///  <c>null</c>
        /// </param>
        /// <param name="reflectionLookup">
        ///  Flags that control the lookup
        /// </param>
        /// <returns>
        ///  The only attribute, or <c>null</c> if 0 or 2+ found
        /// </returns>
        public static T GetOnlyAttributeOrNull<T>(MemberInfo mi, ReflectionLookup reflectionLookup) where T : System.Attribute
        {
            T[] ar = GetAttributes<T>(mi, reflectionLookup);

            return (1 == ar.Length) ? ar[0] : null;
        }

        /// <summary>
        ///  [INTERNAL]
        /// </summary>
        internal static FieldInfo[] GetTypeFields(Type type)
        {
            return type.GetFields();
        }
        #endregion

        #region field operations

        /// <summary>
        ///  [INTERNAL] Obtains the public constant fields - constants and
        ///  readonly static fields - of the given type
        /// </summary>
        /// <param name="type">
        ///  The type to be examined
        /// </param>
        /// <param name="reflectionLookup">
        ///  Flags that control the lookup
        /// </param>
        /// <returns>
        ///  An array, which may be empty, of fields
        /// </returns>
        internal static FieldInfo[] GetPublicConstantFields(Type type, ReflectionLookup reflectionLookup)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Static;

            if (ReflectionLookup.FromQueriedTypeAndAncestors == reflectionLookup)
            {
                flags |= BindingFlags.FlattenHierarchy;
            }

            FieldInfo[] fields = type.GetFields(flags);

            return fields.Where((fi) => fi.IsLiteral || fi.IsStatic).ToArray();
        }
        #endregion
    }
}
