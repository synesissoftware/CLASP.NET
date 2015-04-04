
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using System;

    public abstract class ClaspException
        : Exception
    {
        public ClaspException(string message)
            : base(message)
        {}
    }
}
