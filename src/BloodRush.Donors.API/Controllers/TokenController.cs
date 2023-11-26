#region

using BloodRush.API.Handlers.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public TokenController(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("Donor/PhoneNumber")]
    public async Task<IActionResult> GenerateDonorToken(
        [FromBody] LoginWithPhoneNumberCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("Donor/Email")]
    public async Task<IActionResult> GenerateDonorToken(
        [FromBody] LoginWithEmailCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("Donor/Refresh")]
    public async Task<IActionResult> RefreshDonorToken(
        [FromBody] RefreshTokenCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}