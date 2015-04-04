﻿
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp.Internal
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    internal sealed class Argument
        : IArgument
    {
        #region Construction
        private Argument(ArgumentType type, string givenName, string resolvedName, string value, int numHyphens, int index)
        {
            Type = type;
            GivenName = givenName;
            ResolvedName = resolvedName;
            Value = value;
            Index = index;
        }

        internal static Argument NewValue(string value, int index)
        {
            return new Argument(ArgumentType.Value, null, null, value, 0, index);
        }

        internal static Argument NewFlag(string arg, int i)
        {
            return new Argument(ArgumentType.Flag, arg, arg, null, CountHyphens(arg), i);
        }

        internal static Argument NewFlag(string givenName, string name, int i)
        {
            return new Argument(ArgumentType.Flag, givenName, name, null, CountHyphens(givenName), i);
        }

        internal static Argument NewOption(string arg, int i)
        {
            return new Argument(ArgumentType.Option, arg, arg, null, CountHyphens(arg), i);
        }

        internal static Argument NewOption(string givenName, string name, string value, int i)
        {
            return new Argument(ArgumentType.Option, givenName, name, value, CountHyphens(givenName), i);
        }
        #endregion

        #region IArgument Members
        public ArgumentType Type
        {
            get;
            private set;
        }
        public string ResolvedName
        {
            get;
            private set;
        }
        public string GivenName
        {
            get;
            private set;
        }
        public string Value
        {
            get;
            internal set;
        }
        public int Index
        {
            get;
            private set;
        }
        #endregion

        #region Properties
        internal bool Used
        {
            get;
            set;
        }
        #endregion

        #region Operations
        public override string ToString()
        {
            return String.Format("{{{0}, {1}, {2}, {3}, {4}}}", Type, GivenName, ResolvedName, Value, Index);
        }
        #endregion

        #region Implementation
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