
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using System;

    public class MissingOptionException
        : ArgumentException
    {
        public MissingOptionException(string message, string optionName)
            : base(message, optionName)
        {}
    }
}
