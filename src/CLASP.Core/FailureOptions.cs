
// Created: 19th June 2017
// Updated: 1st May 2019

namespace SynesisSoftware.SystemTools.Clasp
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
        ///  Invokes exit for VV overloads
        /// </summary>
        InvokeExitForVV                             =   0x00010000,

        /// <summary>
        ///  Causes exit code to be set for VV overloads
        /// </summary>
        SetExitCodeForVV                            =   0x00020000,

        /// <summary>
        ///  Appends the string
        ///  <c>"; use --help for usage"</c>
        ///  to contingent reports
        /// </summary>
        AppendStandardUsagePromptToContingentReport =   0x00000100,
    }
}
