#region

using BloodRush.API.Dtos;
using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.Donations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[ApiController]
[Authorize]
[Route("api/donations")]
public class DonationsController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public DonationsController(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DonationDto>>> GetDonations()
    {
        var query = new GetDonationsQuery();
        var donations = await _mediator.Send(query);
        return Ok(donations);
    }
    
}