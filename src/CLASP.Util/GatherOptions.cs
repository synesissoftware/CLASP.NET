
// Created: 22nd August 2009
// Updated: 18th May 2019

namespace Clasp.Util
{
    using System;

    /// <summary>
    ///  Options that moderate the behaviour of
    ///  <see cref="Clasp.Util.SearchSpec.Gather"/>
    /// </summary>
    [Flags]
    public enum GatherOptions : int
    {
        /// <summary>
        ///  Normal processing
        /// </summary>
        None                            =   0x00000000,

        /// <summary>
        ///  Causes an default <see cref="SearchSpec"/> instance, consisting
        ///  of the current directory (<c>"."</c>) and Recls'
        ///  <b>FileSearcher.WildcardsAll</b> property, to be created and
        ///  returned from
        ///  <see cref="Clasp.Util.SearchSpec.Gather">Gather()</see>
        ///  if no specs are inferred from the <b>Values</b>
        /// </summary>
        AddSearchAllSpecToEmptyList     =   0x00000001,
    }
}
