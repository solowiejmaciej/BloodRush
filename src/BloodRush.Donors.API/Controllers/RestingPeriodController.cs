#region

using BloodRush.API.Dtos;
using BloodRush.API.Handlers.RestingPeriod;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[Authorize]
[ApiController]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public async Task<ActionResult<RestingPeriodDto>> GetRestingPeriod(
    )
    {
        var query = new GetRestingPeriodQuery();
        var restingPeriod = await _mediator.Send(query);
        return Ok(restingPeriod);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateRestingPeriodMonths(
        [FromBody] UpdateRestingPeriodMonthsCommand command
    )
    {
        await _mediator.Send(command);
        return NoContent();
    }
    
}