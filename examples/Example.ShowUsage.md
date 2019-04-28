# CLASP.NET Example - **Example.ShowUsage**

## Summary

T.B.C.

## Source

```C#
// Purpose: Illustrates use of ShowUsage()

namespace Example.ShowUsage
{
    using global::SynesisSoftware.SystemTools.Clasp;
    using global::SynesisSoftware.SystemTools.Clasp.Util;

    // NOTE: this alias is temporary
    using Specification = global::SynesisSoftware.SystemTools.Clasp.Alias;

    class Program
    {
        static Flag Flag_Verbose    =   new Flag(@"-v", @"--verbose", @"runs with verbose output");

        static Specification[] specifications =
        {
            Specification.Section("Verbosity:"),
            Flag_Verbose,

            Specification.Section("Standard:"),
            UsageUtil.Help,
            UsageUtil.Version,
        };

        static string[] info_lines =
        {
            "CLASP.NET examples",
            "",
            ":version:",
            null,
        };

        static int Main(string[] argv)
        {
            return Invoker.ParseAndInvokeMain(argv, specifications, (Arguments args) =>
                {
                    return UsageUtil.ShowUsage(args, new UsageUtil.UsageParams{ InfoLines = info_lines });
                });
        }
    }
}
```

## Usage

T.B.C.

