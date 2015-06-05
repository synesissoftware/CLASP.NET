
// Created: 23rd June 2010
// Updated: 5th June 2015

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
