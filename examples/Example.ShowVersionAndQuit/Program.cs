
// Purpose: Illustrates use of ShowVersionAndQuit()

// Created: 14th October 2017
// Updated: 14th October 2017

namespace Example.ShowVersionAndQuit
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Util;

    class Program
    {
        static void Main(string[] args)
        {
            Invoker.InvokeMainVA(args, null, (Arguments clargs) =>
                {
                    UsageUtil.ShowVersionAndQuit(clargs, Invoker.Constants.ExitCode_Success);
                });
        }
    }
}
