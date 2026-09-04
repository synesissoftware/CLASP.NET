
// Created: 1st May 2019
// Updated: 5th May 2019

namespace Clasp.Binding
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///  Attribute applied to enumeration fields
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BoundEnumerationAttribute
        : Attribute
    {
        #region fields

        private readonly Type   m_type;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the attribute for the given type
        /// </summary>
        /// <param name="type">
        ///  A non-<c>null</c> reference to a type, which must refer to an
        ///  enumeration
        /// </param>
        public BoundEnumerationAttribute(Type type)
        {
            Debug.Assert(null != type);
            Debug.Assert(type.IsEnum);

            m_type = type;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The type of the enumeration
        /// </summary>
        public Type Type
        {
            get
            {
                return m_type;
            }
        }
        #endregion
    }

    /// <summary>
    ///  Attribute applied to enumeration fields (that have already been
    ///  marked <see cref="BoundEnumerationAttribute"/>) to specify an
    ///  association between a flag and an enumerator value (or combination
    ///  of enumerator values)
    /// </summary>
    /// <remarks>
    ///  For implementation reasons, the enumerator value has to be specified
    ///  as an <see cref="int"/>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class BoundEnumeratorAttribute
        : BoundFieldAttribute
    {
        #region fields

        private readonly int    m_enumeratorValue;
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the attribute for the given flag's
        ///  resolved name and corresponding enumerator value
        /// </summary>
        /// <param name="flagResolvedName">
        ///  The flag's name. May not be <c>null</c>.
        /// </param>
        /// <param name="enumeratorValue">
        ///  The enumerator value (or combination of enumerator values)
        /// </param>
        /// <remarks>
        ///  For implementation reasons, the enumerator value has to be specified
        ///  as an <see cref="int"/>
        /// </remarks>
        public BoundEnumeratorAttribute(string flagResolvedName, int enumeratorValue)
            : base(flagResolvedName)
        {
            m_enumeratorValue = enumeratorValue;
        }
        #endregion

        #region properties

        /// <summary>
        ///  The enumerator value
        /// </summary>
        /// <remarks>
        ///  For implementation reasons, the enumerator value has to be specified
        ///  as an <see cref="int"/>
        /// </remarks>
        public int EnumeratorValue
        {
            get
            {
                return m_enumeratorValue;
            }
        }
        #endregion
    }
}
