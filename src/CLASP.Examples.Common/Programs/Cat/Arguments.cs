
namespace Clasp.Examples.Common.Programs.Cat
{
    /// <summary>
    ///  Structure representing the total arguments specified to the program
    /// </summary>
    //[Clasp.Binding.BoundType(ParsingOptions=Clasp.ParseOptions.TreatSinglehyphenAsValue, BindingOptions=Clasp.ArgumentBindingOptions.IgnoreOtherFlags, AttributeOptionsHavePrecedence=true)]
    [Clasp.Binding.BoundType(ParsingOptions=Clasp.ParseOptions.TreatSinglehyphenAsValue, AttributeOptionsHavePrecedence=true)]
    public struct Arguments
    {
        #region constants

        public static class Constants
        {
            public static class Sections
            {
                public const string Behaviour   =   "behaviour:";

                public const string Display     =   "display:";

                public const string Standard    =   "standard:";
            }

            public static readonly Clasp.Specification[] Specifications = {

                Clasp.Specification.Section(Constants.Sections.Display),

                Clasp.Specification.Section(Constants.Sections.Behaviour),

                Clasp.Specification.Section(Constants.Sections.Standard),

                Clasp.Util.UsageUtil.Help,
                Clasp.Util.UsageUtil.Version,
            };
        }
        #endregion

        /// <summary>
        ///  Array of input paths, which will be non-<c>null</c> and will
        ///  have at least one element
        /// </summary>
        [Clasp.Binding.BoundValues(Minimum=1)]
        public string[] InputPaths;

        /// <summary>
        ///  Combination of <see cref="CatOptions"/> from CLI flags
        /// </summary>
        [Clasp.Binding.BoundEnumeration(typeof(CatOptions))]
        [Clasp.Binding.BoundEnumerator("--show-lines", (int)CatOptions.NumberLines, Alias="-n", HelpSection=Constants.Sections.Display, HelpDescription="shows the number of lines")]
        [Clasp.Binding.BoundEnumerator("--show-blanks", (int)CatOptions.NumberNonBlankLines, Alias="-b", HelpSection=Constants.Sections.Display, HelpDescription="shows the number of non-blank lines")]
        [Clasp.Binding.BoundEnumerator("--squeeze-empty", (int)CatOptions.SqueezeEmptyAdjacentLines, Alias="-s", HelpSection=Constants.Sections.Display, HelpDescription="squeeze adjacent empty lines into one")]
        public CatOptions Options;

        /// <summary>
        ///  The end-of-line sequence
        /// </summary>
        [Clasp.Binding.BoundOption("--eol-sequence", Alias="-E", HelpSection=Constants.Sections.Behaviour, HelpDescription="specifies the end-of-line-sequence", DefaultValue="")]
        public string EolSequence;

        #region implementation
        internal void _suppress_warning_CS0469()
        {
            this.InputPaths = null;
            this.Options = CatOptions.None;
            this.EolSequence = null;
        }
        #endregion
    }

}
