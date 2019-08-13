
// Created: 22nd June 2010
// Updated: 13th July 2019

namespace Clasp.Util
{
    using System;
    using System.Diagnostics;

    /// <summary>
    ///  Utility class for additional CLASP-related functionality.
    /// </summary>
    public static class ParseUtil
    {
        #region constants

        /// <summary>
        ///  The (lower-case) default false-forms used for the comparisons
        /// </summary>
        public static readonly string[] FalseForms =
        {
            "false",
            "no"
        };
        /// <summary>
        ///  The (lower-case) default true-forms used for the comparisons
        /// </summary>
        public static readonly string[] TrueForms =
        {
            "true",
            "yes"
        };
        #endregion

        #region Boolean parsing methods

        /// <summary>
        ///  Parses the given string for a <c>bool</c> value.
        /// </summary>
        /// <param name="s">
        ///  The string to be parsed. May not be <c>null</c>.
        /// </param>
        /// <returns>
        ///  The boolean value.
        /// </returns>
        /// <exception cref="System.FormatException">
        ///  Thrown if the string cannot be parsed as boolean.
        /// </exception>
        public static bool ParseBool(string s)
        {
            Debug.Assert(null != s);

            bool v;

            if (!TryParseBool(s, out v))
            {
                throw new System.FormatException("String was not recognised as a valid Boolean");
            }

            return v;
        }

        /// <summary>
        ///  Attemps to parse the given string for a <c>bool</c>
        ///  value.
        /// </summary>
        /// <param name="s">
        ///  The string to be parsed. May not be <c>null</c>.
        /// </param>
        /// <param name="value">
        ///  The boolean value.
        /// </param>
        /// <returns>
        ///  <b>true</b> if the string can be parsed; <b>false</b>
        ///  otherwise.
        /// </returns>
        public static bool TryParseBool(string s, out bool value)
        {
            Debug.Assert(null != s);

            return TryParseBool(TrueForms, FalseForms, s, out value);
        }

        /// <summary>
        ///  Attempts to parse the given string for a <c>bool</c>
        /// </summary>
        /// <param name="trueForms"></param>
        /// <param name="falseForms"></param>
        /// <param name="s">
        ///  The string to be parsed. May not be <c>null</c>.
        /// </param>
        /// <param name="comparisonType"></param>
        /// <param name="value">
        ///  The boolean value.
        /// </param>
        /// <returns></returns>
        public static bool TryParseBool(string[] trueForms, string[] falseForms, string s, StringComparison comparisonType, out bool value)
        {
            Debug.Assert(null != trueForms);
            Debug.Assert(null != falseForms);
            Debug.Assert(null != s);

            foreach (string trueForm in trueForms)
            {
                if (0 == String.Compare(s, trueForm, comparisonType))
                {
                    value = true;

                    return true;
                }
            }

            foreach (string falseForm in falseForms)
            {
                if (0 == String.Compare(s, falseForm, comparisonType))
                {
                    value = false;

                    return true;
                }
            }

            value = false;

            return false;
        }

        /// <summary>
        ///  Attempts to parse the given string for a <c>bool</c>
        /// </summary>
        /// <param name="trueForms"></param>
        /// <param name="falseForms"></param>
        /// <param name="s">
        ///  The string to be parsed. May not be <c>null</c>.
        /// </param>
        /// <param name="value">
        ///  The boolean value.
        /// </param>
        /// <returns></returns>
        public static bool TryParseBool(string[] trueForms, string[] falseForms, string s, out bool value)
        {
            Debug.Assert(null != trueForms);
            Debug.Assert(null != falseForms);
            Debug.Assert(null != s);

            return TryParseBool(trueForms, falseForms, s, StringComparison.CurrentCultureIgnoreCase, out value);
        }
        #endregion
    }
}
