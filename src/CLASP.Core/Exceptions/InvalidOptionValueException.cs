
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    public class InvalidOptionValueException
        : ArgumentException
    {
        public InvalidOptionValueException(string message, string optionName)
            : base(message, optionName)
        {}
    }
}
