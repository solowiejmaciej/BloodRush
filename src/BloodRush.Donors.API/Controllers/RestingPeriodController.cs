#region

using BloodRush.API.Handlers.RestingPeriod;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[Route("api/resting-periods")]
public class RestingPeriodController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public RestingPeriodController(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRestingPeriod(
    )
    {
        var query = new GetRestingPeriodQuery();
        var restingPeriod = await _mediator.Send(query);
        return Ok(restingPeriod);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateRestingPeriod(
    )
    {
        var query = new UpdateRestingPeriodQuery(); 
        await _mediator.Send(query);
        return NoContent();
    }
    
}