
// Created: 17th July 2009
// Updated: 20th April 2019

namespace SynesisSoftware.SystemTools.Clasp.Interfaces
{
    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

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
        #endregion
    }
}
