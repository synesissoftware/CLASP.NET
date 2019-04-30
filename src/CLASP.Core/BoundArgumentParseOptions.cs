
// Created: 18th June 2017
// Updated: 30th April 2019

namespace SynesisSoftware.SystemTools.Clasp
{
    using System;

    /// <summary>
    ///  Flags that control the parsing of bound arguments
    /// </summary>
    [Flags]
    public enum ArgumentBindingOptions
    {
        /// <summary>
        ///  No options specified
        /// </summary>
        None                       =   0x00000000,

        /// <summary>
        ///  Unless specified, any additional flags will result in the
        ///  throwing of <see cref="Clasp.Exceptions.UnusedArgumentException"/>
        /// </summary>
        IgnoreOtherFlags            =   0x00000010,

        /// <summary>
        ///  Unless specified, any additional options will result in the
        ///  throwing of <see cref="Clasp.Exceptions.UnusedArgumentException"/>
        /// </summary>
        IgnoreOtherOptions          =   0x00000020,

        /// <summary>
        ///  Unless specified, any additional values will result in the
        ///  throwing of <see cref="Clasp.Exceptions.UnusedArgumentException"/>
        /// </summary>
        IgnoreExtraValues           =   0x00000040,

        /// <summary>
        ///  [DEPRECATED] Use IgnoreExtraValues
        /// </summary>
        [Obsolete("Use IgnoreExtraValues")]
        IgnoreOtherValues           =   IgnoreExtraValues,

        /// <summary>
        ///  Unless specified, too-few values will result in the throwing
        ///  throwing of <see cref="Clasp.Exceptions.MissingValueException"/>
        /// </summary>
        IgnoreMissingValues         =   0x00000080,

        /// <summary>
        ///  Unless specified, any additional arguments will result in the
        ///  throwing of <c>UnusedArgumentException</c>
        /// </summary>
        IgnoreOtherArguments        =   IgnoreOtherFlags | IgnoreOtherOptions | IgnoreExtraValues,
    }
}
