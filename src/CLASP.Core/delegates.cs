
// Created: 22nd June 2010
// Updated: 9th June 2015

namespace SynesisSoftware.SystemTools.Clasp
{
    /// <summary>
    ///  Delegate defining the main processing function to be invoked
    ///  by <see cref="Arguments.InvokeMain(string[], Alias[], ToolMain)"/>
    /// </summary>
    /// <param name="args"></param>
    public delegate int ToolMain(Arguments args);
}
