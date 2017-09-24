
// Created: 18th June 2017
// Updated: 19th June 2017

namespace SynesisSoftware.SystemTools.Clasp
{
    using System;

    /// <summary>
    ///  Flags that control the parsing of bound arguments
    /// </summary>
    [Flags]
    public enum BoundArgumentParseOptions
    {
        /// <summary>
        ///  No options specified
        /// </summary>
        None                       =   0x00000000,

        /// <summary>
        ///  Unless specified, any additional flags will result in the
        ///  throwing of <c>UnusedArgumentException</c>
        /// </summary>
        IgnoreOtherFlags            =   0x00000010,

        /// <summary>
        ///  Unless specified, any additional options will result in the
        ///  throwing of <c>UnusedArgumentException</c>
        /// </summary>
        IgnoreOtherOptions          =   0x00000020,

        /// <summary>
        ///  Unless specified, any additional values will result in the
        ///  throwing of <c>UnusedArgumentException</c>
        /// </summary>
        IgnoreOtherValues           =   0x00000040,

        /// <summary>
        ///  Unless specified, any additional arguments will result in the
        ///  throwing of <c>UnusedArgumentException</c>
        /// </summary>
        IgnoreOtherArguments        =   IgnoreOtherFlags | IgnoreOtherOptions | IgnoreOtherValues,
    }
}
