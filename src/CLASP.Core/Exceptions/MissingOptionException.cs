
// Created: 23rd June 2010
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    /// <summary>
    ///  Exception thrown to indicate a missing option.
    /// </summary>
    public class MissingOptionException
        : FlagOrOptionArgumentException
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
