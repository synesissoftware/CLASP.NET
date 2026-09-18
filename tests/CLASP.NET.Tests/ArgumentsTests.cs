using Clasp;
using Clasp.Interfaces;
using Clasp.Util;

using Xunit;

namespace Clasp.Tests;

public sealed class ArgumentsTests
{
    private static readonly string[] HelpArgv = new[] { "--help" };

    [Fact]
    public void Empty_argv_yields_no_flags_or_options()
    {
        var arguments = new Arguments(
            Array.Empty<string>(),
            new Specification[]
            {
                UsageUtil.Help,
                UsageUtil.Version,
            });

        Assert.Empty(arguments.Flags);
        Assert.Empty(arguments.Options);
        Assert.Empty(arguments.Values);
    }

    [Fact]
    public void Help_flag_is_recognised()
    {
        var arguments = new Arguments(
            HelpArgv,
            new Specification[]
            {
                UsageUtil.Help,
                UsageUtil.Version,
            });

        IArgument? help = arguments.Flags.FirstOrDefault(
            f => f.ResolvedName == UsageUtil.Constants.StandardSpecifications.Help_ResolvedName);

        Assert.NotNull(help);
    }

    [Fact]
    public void ProgramName_is_non_empty()
    {
        _ = new Arguments(Array.Empty<string>());

        Assert.False(string.IsNullOrWhiteSpace(Arguments.ProgramName));
    }
}
