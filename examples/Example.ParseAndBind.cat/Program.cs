
namespace Example.ParseAndBind
{
    using CatArguments = global::Clasp.Examples.Common.Programs.Cat.Arguments;
    using CatLogic = global::Clasp.Examples.Common.Programs.Cat.Logic;

    using System;

    class Program
    {
        static int Main(string[] argv)
        {
            try
            {
                var specifications = CatArguments.Constants.Specifications;

                return Clasp.Invoker.ParseAndInvokeMainWithBoundArgumentOfType<CatArguments>(argv, specifications, (CatArguments prargs, Clasp.Arguments clargs) => {

                    if (clargs.HasFlag(Clasp.Util.UsageUtil.Help))
                    {
                        return Clasp.Util.UsageUtil.ShowBoundUsage<CatArguments>(clargs, null);
                    }

                    if (clargs.HasFlag(Clasp.Util.UsageUtil.Version))
                    {
                        return Clasp.Util.UsageUtil.ShowVersion(clargs, null);
                    }

                    return CatLogic.Run(prargs, Console.Out, Console.Error);
                });
            }
            catch (System.OutOfMemoryException)
            {
                throw;
            }
            catch (System.Exception x)
            {
                System.Console.Error.WriteLine("{0}: exception({1}): {2}", Clasp.Arguments.ProgramName, x.GetType(), x);

                return 1; // Non-normative exit code
            }
        }
    }
}
