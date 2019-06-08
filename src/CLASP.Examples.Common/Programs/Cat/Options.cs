
namespace Clasp.Examples.Common.Programs.Cat
{
    using global::System;

    /// <summary>
    ///  Options that moderate the program behaviour
    /// </summary>
    [Flags]
    public enum CatOptions
    {
        /// <summary>
        ///  No arguments
        /// </summary>
        None                        =   0x00000000,

        /// <summary>
        ///  Number of lines will be shown
        /// </summary>
        NumberLines                 =   0x00000001,

        /// <summary>
        ///  Number of non-blank lines will be shown
        /// </summary>
        NumberNonBlankLines         =   0x00000002,

        /// <summary>
        ///  Squeezes consecutive blank lines into one
        /// </summary>
        SqueezeEmptyAdjacentLines   =   0x00000010,
    }
}
