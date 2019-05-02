
// Created: 9th August 2009
// Updated: 2nd May 2019

namespace SynesisSoftware.SystemTools.Clasp
{
    using System;

    /// <summary>
    ///  Flags that control the parsing behaviour of the
    ///  <see cref="Clasp.Arguments"/> constructor(s)
    /// </summary>
    [Flags]
    public enum ParseOptions
    {
        /// <summary>
        ///  Normal parsing behaviour. This includes, on non-UNIX platforms,
        ///  the automatic expansion of wildcard values (but not currently
        ///  option' values)
        /// </summary>
        None                                        =   0x00000000,

        /// <summary>
        /// Prevents the double-hyphen argument <c>"--"</c> from being
        /// recognised as a special modifier.
        ///
        /// Normally the <c>"--"</c> argument is interpreted to mean that
        /// all subsequent arguments are treated as values, regardless of
        /// whether they're prefixed with hyphens or not, in order to allow
        /// filenames (or other values) that contain leading hyphens to be
        /// interpreted correctly.
        /// </summary>
        DontRecogniseDoublehyphenToStartValues      =   0x00000001,

        /// <summary>
        /// Cause the single-hyphen argument <c>"-"</c> to be recognised as
        /// a value.
        ///
        /// Normally the <c>"-"</c> argument is interpreted as an option,
        /// requiring that client code examine the <b>Options</b> in
        /// addition to the <b>Values</b>, in order to process a sequence of
        /// files including <see cref="System.Console.In">standard input</see>
        /// (as represented by <c>"-"</c>). Specifying this flag causes it
        /// to appear in the <b>Values</b>, so all values can be processed
        /// together.
        ///
        /// To avoid ambiguity, a <c>"-"</c> transformed in this way will
        /// have non-empty <c>ResolvedName</c> and <c>GivenName</c>
        /// properties.
        /// </summary>
        TreatSinglehyphenAsValue                    =   0x00000002,

        /// <summary>
        /// [Windows-only] clasp_parseArguments() flag that prevents the expansion of
        /// wildcards on Windows.
        ///
        /// Because the Windows shell does not automatically expand wildcards,
        /// clasp_parseArguments() performs this function on any values containing '?'
        /// or '*' before returning the arguments. If that is not required, it can
        /// be suppressed.
        ///
        /// \note Specifying this flag does not remove the requirement to link the
        ///   <a href="http://recls.org/">recls</a> library along with clasp to
        ///   Windows executables. To do that, define the pre-processor symbol
        ///   ClaspCmdlineArgsNoReclsOnWindows during compilation clasp.
        /// </summary>
        DontExpandWildcardsOnWindows                =   0x00000100,

        /// <summary>
        /// [Windows-only] clasp_parseArguments() flag that causes the expansion of
        /// wildcards in apostrophe-quoted values on Windows.
        ///
        /// Ordinarily, wildcards that are quoted by apostrophes (single quotes) are
        /// not expanded. Specifying this flag causes such values to be expanded on
        /// Windows.
        /// </summary>
        DoExpandWildcardsInAposquotesOnWindows      =   0x00000200,

        /// <summary>
        ///  Default flags used in
        ///  <see cref="Clasp.Invoker"/>
        /// </summary>
        Default                                     =   None,
    }
}
