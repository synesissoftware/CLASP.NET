
// Created: 23rd June 2010
// Updated: 9th June 2015

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    using System;

    /// <summary>
    ///  Exception thrown to indicate an unused flag/option.
    /// </summary>
    public class UnusedArgumentException
        : ArgumentException
    {
        /// <summary>
        ///  Constructs an instance of the exception.
        /// </summary>
        /// <param name="optionName">
        ///  The name of the unused flag/option.
        /// </param>
        public UnusedArgumentException(string optionName)
            : base(null, "unused argument", optionName, null)
        {
        }
    }
}
