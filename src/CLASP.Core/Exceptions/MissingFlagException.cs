
// Created: 13th July 2019
// Updated: 14th July 2019

namespace Clasp.Exceptions
{
    /// <summary>
    ///  Exception thrown to indicate a missing flag
    /// </summary>
    public class MissingFlagException
        : MissingFlagOrOptionException
    {
        #region construction

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="name">
        ///  The name of the flag. May <b>not</b> be <c>null</c>
        /// </param>
        public MissingFlagException(string name)
            : this(name, null)
        {
        }

        /// <summary>
        ///  Constructs an instance of the class
        /// </summary>
        /// <param name="name">
        ///  The name of the flag. May <b>not</b> be <c>null</c>
        /// </param>
        /// <param name="message">
        ///  The message to be associated with the exception
        /// </param>
        public MissingFlagException(string name, string message)
            : base(name, ArgumentType.Flag, message)
        {
        }
        #endregion
    }
}
