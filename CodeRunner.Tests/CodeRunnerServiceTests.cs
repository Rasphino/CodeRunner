using Microsoft.Extensions.Logging;
using CodeRunner.Services;

namespace CodeRunner.Tests;

public class CodeRunnerServiceTests
{
    [Fact]
    public void Test_RunningScriptWithArgs()
    {
        using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        var logger = loggerFactory.CreateLogger<CodeRunnerService>();

        var service = new CodeRunnerService(logger, "/opt/homebrew/bin/python3");
        var (outResult, errResult) = service.RunPython("/Users/rasp/repo/python/print_args.py", "1", "Hello world");
        Assert.Equal("cmd entry: ['/Users/rasp/repo/python/print_args.py', '1', 'Hello world']\n", outResult);
        Assert.Equal(String.Empty, errResult);
    }
}
