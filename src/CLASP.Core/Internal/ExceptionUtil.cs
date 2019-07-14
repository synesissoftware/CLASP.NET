
// Created: 14th July 2019
// Updated: 14th July 2019

namespace Clasp.Internal
{
    using global::System;
    using global::System.Text.RegularExpressions;

    internal static class ExceptionUtil
    {
        #region constants

        /// <summary>
        ///  Constants
        /// </summary>
        internal static class Constants
        {
        }
        #endregion

        #region operations

        /// <summary>
        ///  Pretties up the exception message, including trailing periods
        ///  and whitespace
        /// </summary>
        /// <param name="x">
        ///  The exception. May <b>not</b> be <c>null</c>
        /// </param>
        /// <returns></returns>
        internal static string PrettifyExceptionMessage(Exception x)
        {
            var m = Regex.Match(x.Message, @"^\s*(.)(.+?)[. \t]*$");

            if (m.Success)
            {
                return m.Groups[1].Value.ToLower() + m.Groups[2].Value;
            }

            return null;
        }
        #endregion
    }
}
