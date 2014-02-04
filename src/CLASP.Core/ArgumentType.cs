
// Created: 
// Updated: 3rd February 2014

namespace SynesisSoftware.SystemTools.Clasp
{
	/// <summary>
	///  Denotes the type of the argument.
	/// </summary>
	public enum ArgumentType
	{
		None	=	0,
		/// <summary>
		///  The argument is a <b>flag</b>.
		/// </summary>
		Flag,
		/// <summary>
		///  The argument is an <b>option</b>.
		/// </summary>
		Option,
		/// <summary>
		///  The argument is a <b>value</b>.
		/// </summary>
		Value
	}
}
