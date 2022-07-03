using System;
using System.Diagnostics;

namespace CodeRunner.Services
{
    public class CodeRunnerService
    {
        private readonly ILogger<CodeRunnerService> _logger;
        private readonly string _pythonPath;

        public CodeRunnerService(ILogger<CodeRunnerService> logger, string pythonPath)
        {
            _logger = logger;
            _pythonPath = pythonPath;
        }

        public (string?, string?) RunPython(string scriptPath, params string[] scriptArgs)
        {
            ProcessStartInfo start = new()
            {
                FileName = "my/full/path/to/python.exe",
                Arguments = $"{scriptPath} {String.Join(" ", scriptArgs)}",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using Process? process = Process.Start(start);
            if (process is null) return (null, null);

            using StreamReader stdoutReader = process.StandardOutput;
            string stdoutResult = stdoutReader.ReadToEnd();

            using StreamReader stderrReader = process.StandardError;
            string stderrResult = stderrReader.ReadToEnd();

            return (stdoutResult, stderrResult);
        }

    }
}

