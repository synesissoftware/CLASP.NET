﻿
// Created: 19th June 2017
// Updated: 19th June 2017

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
        /// </summary>
        None                                        =   0x00000000,

        /// <summary>
        /// </summary>
        HandleClaspExceptions                       =   0x00000001,

        /// <summary>
        /// </summary>
        HandleMemoryExceptions                      =   0x00000002,

        /// <summary>
        /// </summary>
        HandleSystemExceptions                      =   0x00000004,

        /// <summary>
        /// </summary>
        InvokeExitForVV                             =   0x00010000,

        /// <summary>
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