
// Created: 2nd May 2019
// Updated: 3rd May 2019

namespace SynesisSoftware.SystemTools.Clasp.Binding
{
    using System;

    /// <summary>
    ///  Used on option fields to specify numeric constraints
    /// </summary>
    [Flags]
    public enum BoundNumberConstraints
    {
        /// <summary>
        ///  No constraint
        /// </summary>
        None                                =   0x00000000,

        /// <summary>
        ///  Requires that the value must be integral
        /// </summary>
        /// <remarks>
        ///  This is assumed if the field is an integral type; ignored except
        ///  when applied to floating-point fields
        /// </remarks>
        MustBeIntegral                      =   0x00000001,

        /// <summary>
        ///  Requires that the value must be be greater than 0
        /// </summary>
        MustBePositive                      =   0x00000010,

        /// <summary>
        ///  Requires that the value must be be less than 0
        /// </summary>
        MustBeNegative                      =   0x00000020,

        /// <summary>
        ///  Requires that the value must be be greater than or equal to 0
        /// </summary>
        MustBeNonNegative                   =   0x00000040,

        /// <summary>
        ///  Requires that the value must be be less than or equal to 0
        /// </summary>
        MustBeNonPositive                   =   0x00000080,

        /// <summary>
        ///  .
        /// </summary>
        RangeMask                           =   MustBePositive | MustBeNegative | MustBeNonNegative | MustBeNonPositive,
    }
}
