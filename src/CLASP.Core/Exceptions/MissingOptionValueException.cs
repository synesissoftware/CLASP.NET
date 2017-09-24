
// Created: 23rd June 2010
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using SynesisSoftware.SystemTools.Clasp.Interfaces;

    /// <summary>
    ///  Exception thrown to indicate that an option is missing a
    ///  value.
    /// </summary>
    public class MissingOptionValueException
        : FlagOrOptionArgumentException
    {
        /// <summary>
        ///  Constructs an instance of the exception type.
        /// </summary>
        /// <param name="option">
        /// </param>
        public MissingOptionValueException(IArgument option)
            : base(option, "missing option value", null, null)
        {
        }
    }
}
