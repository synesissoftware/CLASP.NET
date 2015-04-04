
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp.Exceptions
{
    using System;

    public abstract class ArgumentException
        : ClaspException
    {
        public ArgumentException(string message, string optionName)
            : base(MakeMessage_(message, optionName))
        {
            OptionName = optionName;
        }

        public string OptionName
        {
            get;
            private set;
        }

        private static string MakeMessage_(string message, string optionName)
        {
            return String.Format("{0}: {1}", message, optionName);
        }
    }
}
