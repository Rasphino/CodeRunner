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

        var service = new CodeRunnerService(logger, @"C:\Users\t-honghaoli\AppData\Local\Microsoft\WindowsApps\python.exe");
        var (outResult, errResult) = service.RunPython(@"D:\scripts\print_args.py", "1", "Hello world");
        Assert.Equal("cmd args: ['1', 'Hello world']\r\n", outResult);
        Assert.Equal(String.Empty, errResult);
    }
}
