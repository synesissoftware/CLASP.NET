
// Created: 3rd May 2019
// Updated: 5th May 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using System;

    /// <summary>
    ///  .
    /// </summary>
    public class OptionValueOutOfRangeException
        : InvalidOptionValueException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance
        /// </summary>
        /// <param name="qualifier"></param>
        /// <param name="option"></param>
        /// <param name="expectedType"></param>
        public OptionValueOutOfRangeException(string qualifier, IArgument option, Type expectedType)
            : base(option, expectedType, null, qualifier)
        {}
        #endregion

        #region properties

        #endregion
    }
}
