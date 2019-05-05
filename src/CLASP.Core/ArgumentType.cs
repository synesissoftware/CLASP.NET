
// Created: 17th July 2009
// Updated: 5th May 2019

namespace Clasp
{
    /// <summary>
    ///  Denotes the type of the argument.
    /// </summary>
    public enum ArgumentType
    {
        /// <summary>
        ///  The argument type is unspecified.
        /// </summary>
        None    =   0,
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
