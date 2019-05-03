
// Created: 3rd May 2019
// Updated: 3rd May 2019

namespace SynesisSoftware.SystemTools.Clasp.Binding
{
    /// <summary>
    ///  Specifies a truncation option, if any
    /// </summary>
    public enum NumberTruncate
    {
        /// <summary>
        ///  No truncation
        /// </summary>
        None                                =   0,

        /// <summary>
        ///  Causes numbers to be made equal to the lowest integral value
        ///  greater than or equal to the specified option's value
        /// </summary>
        /// <remarks>
        ///  This is observed for all numeric field types
        /// </remarks>
        ToCeiling,

        /// <summary>
        ///  Causes numbers to be made equal to the highest integral value
        ///  less than or equal to the specified option's value
        /// </summary>
        /// <remarks>
        ///  This is observed for all numeric field types
        /// </remarks>
        ToFloor,

        /// <summary>
        ///  Causes fractions to be rounded to the nearest integral value
        /// </summary>
        /// <remarks>
        ///  This is observed for all numeric field types
        /// </remarks>
        ToNearest,

        /// <summary>
        ///  Causes fractions to be truncated to the nearest integral value in
        ///  the direction of zero
        /// </summary>
        /// <remarks>
        ///  This is observed for all numeric field types
        /// </remarks>
        ToZero,
    }
}
