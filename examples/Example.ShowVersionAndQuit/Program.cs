
// Purpose: Illustrates use of ShowVersionAndQuit()

namespace Example.ShowVersionAndQuit
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Util;

    class Program
    {
        static void Main(string[] argv)
        {
            Invoker.ParseAndInvokeMainVA(argv, null, (Arguments clargs) =>
                {
                    UsageUtil.ShowVersionAndQuit(clargs, Invoker.Constants.ExitCode_Success);
                });
        }
    }
}
