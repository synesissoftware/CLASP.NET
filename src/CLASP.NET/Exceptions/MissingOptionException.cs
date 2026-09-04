
// Created: 23rd June 2010
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    /// <summary>
    ///  Exception thrown to indicate a missing option
    /// </summary>
    public class MissingOptionException
        : MissingFlagOrOptionException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="name">
        ///  The name of the option. May <b>not</b> be <c>null</c>
        /// </param>
        public MissingOptionException(string name)
            : base(name, ArgumentType.Option, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="name">
        ///  The name of the option. May <b>not</b> be <c>null</c>
        /// </param>
        /// <param name="message">
        ///  The message to be associated with the exception
        /// </param>
        public MissingOptionException(string name, string message)
            : base(name, ArgumentType.Option, message)
        {
        }
        #endregion
    }
}
