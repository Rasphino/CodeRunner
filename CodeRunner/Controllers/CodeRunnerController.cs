using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using CodeRunner.Models;
using CodeRunner.Services;
using System.Text;

namespace CodeRunner.Controllers;

[ApiController]
[Route("[controller]")]
public class CodeRunnerController : ControllerBase
{
    private readonly ILogger<CodeRunnerController> _logger;
    private readonly ICodeRunnerService _service;
    private readonly string _scriptFolderPath;

    public CodeRunnerController(ILogger<CodeRunnerController> logger, ICodeRunnerService service)
    {
        _logger = logger;
        _service = service;

        _scriptFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
        // Create folder to store script files
        Directory.CreateDirectory(_scriptFolderPath);
    }

    [HttpPost(Name = "Python")]
    public ActionResult<string> Post(RunCodeRequest req)
    {
        if (req.Code is null || req.Lang is null) return BadRequest("Missing code or lang");

        _logger.LogInformation($"Received request: {JsonSerializer.Serialize(req)}");
        var code = new UTF8Encoding(true).GetBytes(req.Code);

        string scriptPath = Path.Combine(_scriptFolderPath, Guid.NewGuid().ToString());
        using (FileStream fsDst = new(scriptPath, FileMode.CreateNew, FileAccess.Write))
        {
            fsDst.Write(code, 0, code.Length);
        }

        var outputs = _service.RunPython(scriptPath);
        return Ok(outputs.Item1);
    }

}

