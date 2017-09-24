
// Created: 18th June 2017
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Binding
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
        #endregion
    }
}
