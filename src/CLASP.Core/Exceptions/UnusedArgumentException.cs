
// Created: 23rd June 2010
// Updated: 5th June 2015

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    public class UnusedArgumentException
        : ArgumentException
    {
        public UnusedArgumentException(string message, string optionName)
            : base(message, optionName)
        { }
    }
}
