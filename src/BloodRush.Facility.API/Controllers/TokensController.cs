#region

using BloodRush.DonationFacility.API.Handlers.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.DonationFacility.API.Controllers;

[ApiController]
[Route("api/tokens/{donationFacilityId:int}")]
public class TokensController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public TokensController(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }
    

    [HttpPost("user")]
    public async Task<IActionResult> GenerateUserToken(
        [FromBody] GenerateUserTokenCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshDoctorToken(
        [FromBody] RefreshTokenCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    
}