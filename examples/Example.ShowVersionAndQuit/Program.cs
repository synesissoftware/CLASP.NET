
// Purpose: Illustrates use of ShowVersionAndQuit()

namespace Example.ShowVersionAndQuit
{
    using global::Clasp;
    using UsageUtil = global::Clasp.Util.UsageUtil;

    class Program
    {
        static void Main(string[] argv)
        {
            Invoker.ParseAndInvokeMainVA(argv, null, (Arguments clargs) => {

                UsageUtil.ShowVersionAndQuit(clargs, Invoker.Constants.ExitCode_Success);
            });
        }
    }
}
