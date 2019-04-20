
// Created: 17th July 2009
// Updated: 20th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Internal
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

    internal sealed class Argument
        : IArgument
    {
        #region fields

        private readonly Specification  m_specification;
        private readonly ArgumentType   m_argumentType;
        private readonly string         m_resolvedName;
        private readonly string         m_givenName;
        private readonly int            m_index;
        #endregion

        #region construction

        private Argument(Specification specification, ArgumentType type, string givenName, string resolvedName, string value, int numHyphens, int index)
        {
            m_specification =   specification;
            m_argumentType  =   type;
            m_resolvedName  =   resolvedName;
            m_givenName     =   givenName;
            Value           =   value;
            m_index         =   index;
        }

        internal static Argument NewValue(string value, int index)
        {
            return new Argument(null, ArgumentType.Value, null, null, value, 0, index);
        }

        internal static Argument NewFlag(Specification specification, string givenName, string resolvedName, int index)
        {
            return new Argument(specification, ArgumentType.Flag, givenName, resolvedName, null, CountHyphens(givenName), index);
        }

        internal static Argument NewOption(Specification specification, string givenName, string resolvedName, string value, int index)
        {
            return new Argument(specification, ArgumentType.Option, givenName, resolvedName, value, CountHyphens(givenName), index);
        }
        #endregion

        #region IArgument members
        public ArgumentType Type
        {
            get
            {
                return m_argumentType;
            }
        }
        public string ResolvedName
        {
            get
            {
                return m_resolvedName;
            }
        }
        public string GivenName
        {
            get
            {
                return m_givenName;
            }
        }
        public string Value
        {
            get;
            internal set;
        }
        public int Index
        {
            get
            {
                return m_index;
            }
        }

        public Specification Specification
        {
            get
            {
                return m_specification;
            }
        }

        public bool Used
        {
            get;
            internal set;
        }

        public void Use()
        {
            Used = true;
        }
        #endregion

        #region operations

        public override string ToString()
        {
            switch(Type)
            {
            case ArgumentType.Flag:

                return ResolvedName;
            case ArgumentType.Option:

                return String.Format("{0}={1}", ResolvedName, Value);
            case ArgumentType.Value:

                return Value;
            default:

                return String.Format("{{{0}, {1}, {2}, {3}, {4}}}", Type, GivenName, ResolvedName, Value, Index);
            }
        }
        #endregion

        #region implementation

        internal static int CountHyphens(string arg)
        {
            int n = 0;

            foreach(char ch in arg)
            {
                if('-' == ch)
                {
                    ++n;
                }
                else
                {
                    break;
                }
            }

            return n;
        }
        #endregion
    }
}
