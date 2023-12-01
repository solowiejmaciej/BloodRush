#region

using BloodRush.API.Handlers.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[Route("api/tokens")]
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

    [HttpPost("donor/generate-via-phone-number")]
    public async Task<IActionResult> GenerateDonorToken(
        [FromBody] LoginWithPhoneNumberCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("donor/generate-via-email")]
    public async Task<IActionResult> GenerateDonorToken(
        [FromBody] LoginWithEmailCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("donor/refresh")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    
    public async Task<IActionResult> RefreshDonorToken(
        [FromBody] RefreshTokenCommand command
    )
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}