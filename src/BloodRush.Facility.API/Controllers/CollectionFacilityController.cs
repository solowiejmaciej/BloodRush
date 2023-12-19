#region

using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Handlers.DonationsFacility;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.DonationFacility.API.Controllers;

[ApiController]
[Route("api/donation-facility")]
public class DonationsFacility : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public DonationsFacility(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<DonationFacilityDto>>> GetDonationFacility(
    )
    {
        var query = new GetDonationFacilityQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<List<DonationFacilityDto>>> AddDonationFacility(
        [FromBody] AddDonationFacilityCommand commandHandler
    )
    {
        await _mediator.Send(commandHandler);
        return Ok();
    }
    
}