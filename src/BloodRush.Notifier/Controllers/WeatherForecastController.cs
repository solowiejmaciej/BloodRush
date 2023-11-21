#region

using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.Notifier.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}