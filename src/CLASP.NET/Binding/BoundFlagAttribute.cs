
// Created: 18th June 2017
// Updated: 14th August 2019

namespace Clasp.Binding
{
    using System;

    /// <summary>
    ///  Attribute applied to Boolean fields to receive a true/false
    /// </summary>
    /// <remarks>
    ///  By definition, flags are optional on a command-line
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BoundFlagAttribute
        : BoundFieldAttribute
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the attribute that matches the given
        ///  flag name
        /// </summary>
        /// <param name="flagName">
        ///  The flag name
        /// </param>
        public BoundFlagAttribute(string flagName)
            : base(flagName)
        {
        }

        /// <summary>
        ///  Constructs an instance of the attribute from the given type,
        ///  whose public constant fields - <c>const</c>; <c>static</c> +
        ///  <c>readonly</c> - are used to
        /// </summary>
        /// <param name="type">
        /// </param>
        ///
        /// <seealso cref="Clasp.Util.Constants"/>
        public BoundFlagAttribute(Type type)
            : base(type)
        {
        }
        #endregion

        #region properties
        #endregion
    }
}
