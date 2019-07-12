
// Created: 17th June 2017
// Updated: 13th July 2019

namespace Clasp.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    ///  Defines extension methods for working with options hashes
    ///  (<c><see cref="System.Collections.Generic"/>&lt;string, object&gt;</c>)
    /// </summary>
    public static class X_E234A551_935B_4ef1_A977_FDDDEE8AA1E9
    {
        /// <summary>
        ///  Extension method to lookup the value corresponding to a
        ///  <paramref name="key"/>
        ///  in an
        ///  <paramref name="options"/>
        ///  dictionary, or
        ///  use a <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="options">
        ///  The options dictionary
        /// </param>
        /// <param name="key">
        ///  The key to look up
        /// </param>
        /// <param name="defaultValue">
        ///  The default value to use if
        ///  <paramref name="key"/> is not found in
        ///  <paramref name="options"/>
        /// </param>
        /// <returns>
        ///  The option value corresponding to the given
        ///  <paramref name="key"/> if it exists in
        ///  <paramref name="options"/>;
        ///  <paramref name="defaultValue"/> otherwise
        /// </returns>
        public static object Lookup(this IDictionary<string, object> options, string key, object defaultValue)
        {
            object v;

            if (options.TryGetValue(key, out v))
            {
                return v;
            }

            return defaultValue;
        }

        /// <summary>
        ///  Extension method to lookup the Boolean value corresponding to a
        ///  <paramref name="key"/>
        ///  in an
        ///  <paramref name="options"/>
        ///  dictionary, or
        ///  use a <paramref name="defaultValue"/>
        /// </summary>
        /// <param name="options">
        ///  The options dictionary
        /// </param>
        /// <param name="key">
        ///  The key to look up
        /// </param>
        /// <param name="defaultValue">
        ///  The default value to use if
        ///  <paramref name="key"/> is not found in
        ///  <paramref name="options"/>
        /// </param>
        /// <returns>
        ///  The option value corresponding to the given
        ///  <paramref name="key"/> if it exists in
        ///  <paramref name="options"/>;
        ///  <paramref name="defaultValue"/> otherwise
        /// </returns>
        public static bool Lookup(this IDictionary<string, object> options, string key, bool defaultValue)
        {
            object v;

            if (options.TryGetValue(key, out v))
            {
                return Util.ParseUtil.ParseBool(v.ToString());
            }

            return defaultValue;
        }

        /// <summary>
        ///  Extension method to lookup a typed value corresponding to a
        ///  <paramref name="key"/>
        ///  in an
        ///  <paramref name="options"/>
        ///  dictionary, or
        ///  use a <paramref name="defaultValue"/>
        /// </summary>
        /// <typeparam name="T">
        ///  The type of the value to be returned
        /// </typeparam>
        /// <param name="options">
        ///  The options dictionary
        /// </param>
        /// <param name="key">
        ///  The key to look up
        /// </param>
        /// <param name="defaultValue">
        ///  The default value to use if
        ///  <paramref name="key"/> is not found in
        ///  <paramref name="options"/>
        /// </param>
        /// <returns>
        ///  The option value corresponding to the given
        ///  <paramref name="key"/> if it exists in
        ///  <paramref name="options"/>;
        ///  <paramref name="defaultValue"/> otherwise
        /// </returns>
        public static T Lookup<T>(this IDictionary<string, object> options, string key, T defaultValue) where T : class
        {
            object v;

            if (options.TryGetValue(key, out v))
            {
                return (T)v;
            }

            return defaultValue;
        }

        /// <summary>
        ///  Extension method that determines whether a Boolean value exists within an
        ///  <paramref name="options"/> dictionary
        /// </summary>
        /// <param name="options">
        ///  The options dictionary
        /// </param>
        /// <param name="key">
        ///  The key to look up
        /// </param>
        /// <returns>
        ///  <c>true</c> if the
        ///  <paramref name="key"/>
        ///  exists in the
        ///  <paramref name="options"/>
        ///  dictionary and is <c>true</c>; <c>false</c> otherwise
        /// </returns>
        public static bool HasOption(this IDictionary<string, object> options, string key)
        {
            return Lookup(options, key, false);
        }
    }
}
