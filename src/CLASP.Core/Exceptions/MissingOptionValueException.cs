
// Created: 23rd June 2010
// Updated: 9th June 2015

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    /// <summary>
    ///  Exception thrown to indicate that an option is missing a
    ///  value.
    /// </summary>
    public class MissingOptionValueException
        : ArgumentException
    {
        /// <summary>
        ///  Constructs an instance of the exception type.
        /// </summary>
        /// <param name="argument">
        /// </param>
        /// <param name="optionName">
        /// </param>
        public MissingOptionValueException(IArgument argument, string optionName)
            : base(argument, "missing option value", optionName, null)
        {
        }
    }
}
