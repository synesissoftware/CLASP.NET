
// Created: 23rd June 2010
// Updated: 9th June 2015

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    /// <summary>
    ///  Exception thrown to indicate a missing option.
    /// </summary>
    public class MissingOptionException
        : ArgumentException
    {
        /// <summary>
        ///  Constructs an instance of the exception type.
        /// </summary>
        /// <param name="optionName">
        /// </param>
        public MissingOptionException(string optionName)
            : base(null, "option not specified", optionName, null)
        {
        }
    }
}
