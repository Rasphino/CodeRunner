namespace CodeRunner.Services
{
    public interface ICodeRunnerService
    {
        /// <summary>
        /// Given python script path and arguments, execute the script and returning the stdout and stderr results.
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <param name="scriptArgs"></param>
        /// <returns>stdout and stderr outputs</returns>
        public (string?, string?) RunPython(string scriptPath, params string[] scriptArgs);
    }
}
