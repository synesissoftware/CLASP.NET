
// Created: 23rd June 2010
// Updated: 5th May 2019

namespace Clasp.Exceptions
{
    /// <summary>
    ///  Exception thrown to indicate a missing option.
    /// </summary>
    public class MissingOptionException
        : FlagOrOptionArgumentException
    {
        #region constants

        /// <summary>
        ///  Constants class
        /// </summary>
        public static class Constants
        {
            /// <summary>
            ///  The default message used by
            ///  <see cref="Clasp.Exceptions.MissingOptionException"/>
            /// </summary>
            public const string     DefaultMessage  =   @"option not specified";
        }
        #endregion

        #region construction

        /// <summary>
        ///  Constructs an instance of the exception type.
        /// </summary>
        /// <param name="optionName">
        /// </param>
        public MissingOptionException(string optionName)
            : base(null, Constants.DefaultMessage, optionName, null)
        {
        }
        #endregion
    }
}
