
// Created: 3rd May 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  An option is invalid, due to its value not falling within a
    ///  required range
    /// </summary>
    public class OptionValueOutOfRangeException
        : InvalidOptionValueException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="qualifier">
        ///  Qualifier of the out-of-range condition
        /// </param>
        /// <param name="option">
        ///  The option argument
        /// </param>
        /// <param name="expectedType">
        ///  The expected type
        /// </param>
        public OptionValueOutOfRangeException(string qualifier, IArgument option, Type expectedType)
            : base(option, expectedType, null, qualifier)
        {}
        #endregion
    }
}
