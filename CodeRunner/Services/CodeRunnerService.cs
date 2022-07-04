using System;
using System.Diagnostics;
using System.Linq;

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
            scriptArgs = scriptArgs.Select(x => $"\"{x}\"").ToArray();
            ProcessStartInfo start = new()
            {
                FileName = _pythonPath,
                Arguments = $"{scriptPath} {String.Join(" ", scriptArgs)}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            _logger.LogInformation("Process start info: ", start.Arguments);

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

