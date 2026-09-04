using Clasp;
using Clasp.Util;

Console.WriteLine($"CLASP.NET {LibraryVersion.VersionString}");

var arguments = new Arguments(
    Array.Empty<string>(),
    new Specification[]
    {
        UsageUtil.Help,
        UsageUtil.Version,
    });

Console.WriteLine($"ProgramName={Arguments.ProgramName}");
UsageUtil.ShowVersion(arguments, new Dictionary<string, object>());
