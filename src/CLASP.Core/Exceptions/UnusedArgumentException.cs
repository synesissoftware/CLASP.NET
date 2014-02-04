
// Created: 
// Updated: 3rd February 2014

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
