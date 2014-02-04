
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp.Interfaces
{
	public interface IArgument
	{
		/// <summary>
		///  The type of the argument
		/// </summary>
		ArgumentType Type { get; }

		/// <summary>
		///  The resolved name of the argument
		/// </summary>
		string ResolvedName { get; }

		/// <summary>
		///  The given name of the argument
		/// </summary>
		string GivenName { get; }

		/// <summary>
		///  The value of the argument
		/// </summary>
		string Value { get; }

		/// <summary>
		///  The original command-line index
		/// </summary>
		int Index { get; }
	}
}
