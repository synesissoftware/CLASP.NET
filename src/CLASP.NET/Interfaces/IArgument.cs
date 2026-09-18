
// Created: 17th July 2009
// Updated: 5th May 2019

namespace Clasp.Interfaces
{
    /// <summary>
    ///  Argument interface.
    /// </summary>
    public interface IArgument
    {
        #region properties

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

        /// <summary>
        ///  The specification associated with the argument; <c>null</c> if
        ///  none is associated
        /// </summary>
        Specification Specification { get; }

        /// <summary>
        ///  Indicates whether the argument has been used, such as having
        ///  been found in a call to <see cref="Clasp.Arguments.HasFlag(string)"/>
        ///  or <see cref="Clasp.Arguments.CheckOption(string, out string)"/>
        /// </summary>
        bool Used { get; }
        #endregion

        #region Operations

        /// <summary>
        ///  Causes an argument to be marked as used
        /// </summary>
        /// <seealso cref="Clasp.Interfaces.IArgument.Used"/>
        void Use();
        #endregion
    }
}
