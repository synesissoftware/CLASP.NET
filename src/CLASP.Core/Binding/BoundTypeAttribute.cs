
// Created: 18th June 2017
// Updated: 21st February 2018

namespace SynesisSoftware.SystemTools.Clasp.Binding
{
    using System;

    /// <summary>
    ///  Attribute applied to structures (and classes) to be subject to
    ///  bound parsing
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class BoundTypeAttribute
        : System.Attribute
    {
        #region fields

        #endregion

        #region construction

        /// <summary>
        ///  Constructor
        /// </summary>
        public BoundTypeAttribute()
        {
        }
        #endregion

        #region properties

        /// <summary>
        ///  The binding options specified with the attribute
        /// </summary>
        public ArgumentBindingOptions BindingOptions { get; set; }

        /// <summary>
        ///  If <c>true</c> the attribute's parse options take precedence
        ///  over any passed to InvokeMainWithBoundArgumentOfType()
        /// </summary>
        public bool AttributeOptionsHavePrecedence { get; set; }
        #endregion
    }
}
