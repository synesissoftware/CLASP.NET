
// Created: 23rd June 2010
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    using global::Clasp.Interfaces;

    using global::System;

    /// <summary>
    ///  Exception thrown to indicate that an option is missing a
    ///  value.
    /// </summary>
    public class MissingOptionValueException
        : InvalidOptionException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="option">
        ///  The argument
        /// </param>
        public MissingOptionValueException(IArgument option)
            : base(option, MakeMessage_(option), null)
        {
        }
        #endregion

        #region implementation

        private static string MakeMessage_(IArgument option)
        {
            return String.Format("missing value for option {0}", option.ResolvedName);
        }
        #endregion
    }
}
