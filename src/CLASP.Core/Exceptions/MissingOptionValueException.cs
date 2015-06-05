
// Created: 23rd June 2010
// Updated: 5th June 2015

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using System;

    public class MissingOptionValueException
        : ArgumentException
    {
        public MissingOptionValueException(string message, string optionName)
            : base(message, optionName)
        { }
    }
}
