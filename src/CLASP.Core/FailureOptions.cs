
// Created: 19th June 2017
// Updated: 14th August 2019

namespace Clasp
{
    using System;

    /// <summary>
    ///  Flags that control the failure behaviour of the
    ///  invoke-around-method facilities
    /// </summary>
    [Flags]
    public enum FailureOptions
    {
        /// <summary>
        ///  None
        /// </summary>
        None                                        =   0x00000000,

        /// <summary>
        ///  Handles CLASP exceptions and issues contingent report
        /// </summary>
        HandleClaspExceptions                       =   0x00000001,

        /// <summary>
        ///  Handles memory exception(s) and issues contingent report
        /// </summary>
        HandleMemoryExceptions                      =   0x00000002,

        /// <summary>
        ///  Handles system exceptions and issues contingent report
        /// </summary>
        HandleSystemExceptions                      =   0x00000004,

        /// <summary>
        ///  Invokes exit for VA overloads
        /// </summary>
        /// <remarks>
        ///  When using VA overloads of the
        ///  <see cref="Clasp.Invoker"/> class with a
        ///  <c>void Main(string[] argv)</c>, this flag causes
        ///  <see cref="System.Environment.Exit()"/> to be called
        ///  when the delegate is complete.
        /// </remarks>
        InvokeExitForVA                             =   0x00010000,

        /// <summary>
        ///  Causes exit code to be set for VA overloads
        /// </summary>
        /// <remarks>
        ///  When using VA overloads of the
        ///  <see cref="Clasp.Invoker"/> class with a
        ///  <c>void Main(string[] argv)</c>, this flag causes
        ///  <see cref="System.Environment.ExitCode"/> to be set
        ///  when the delegate is complete.
        /// </remarks>
        SetExitCodeForVA = 0x00020000,

        /// <summary>
        ///  Appends the string
        ///  <c>"; use --help for usage"</c>
        ///  to contingent reports
        /// </summary>
        AppendStandardUsagePromptToContingentReport =   0x00000100,

        /// <summary>
        ///  If specified, causes unused messages to use the term
        ///  "unused", rather than "unrecognised", for flags and
        ///  options
        /// </summary>
        ReportUnusedAsUnused                        =   0x00000200,

        /// <summary>
        ///  Default flags used in
        ///  <see cref="Clasp.Invoker"/>
        /// </summary>
        Default                                     =   HandleClaspExceptions | SetExitCodeForVA | AppendStandardUsagePromptToContingentReport,

        /// <summary>
        ///  [OBSOLETE] Instead, use InvokeExitForVA
        /// </summary>
        [Obsolete]
        InvokeExitForVV                             =   InvokeExitForVA,

        /// <summary>
        ///  [OBSOLETE] Instead, use SetExitCodeForVA
        /// </summary>
        [Obsolete]
        SetExitCodeForVV                            =   SetExitCodeForVA,
    }
}
