using Microsoft.AspNetCore.Mvc;
using CodeRunner.Models;

namespace CodeRunner.Controllers;

[ApiController]
[Route("[controller]")]
public class CodeRunnerController : ControllerBase
{
    private readonly ILogger<CodeRunnerController> _logger;

    public CodeRunnerController(ILogger<CodeRunnerController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "GetWeatherForecast")]
    public ActionResult<string> Post()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

