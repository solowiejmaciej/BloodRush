#region

using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.QrCodes;
using BloodRush.API.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[Authorize]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public AccountController(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPatch("change-password")]
    public async Task<ActionResult> ChangePassword()
    {
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPatch("change-email")]
    public async Task<ActionResult> ChangeEmail()
    {
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPatch("change-phone-number")]
    public async Task<ActionResult> ChangePhoneNumber()
    {
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("send-email-confirmation-code")]
    public async Task<ActionResult> ConfirmEmail()
    {
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("send-phone-number-confirmation-code")]
    public async Task<ActionResult> ConfirmPhoneNumber()
    {
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmEmail([FromQuery] string code)
    {
        return Ok();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("confirm-phone-number")]
    public async Task<ActionResult> ConfirmPhoneNumber([FromQuery] string code)
    {
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("upload-photo")]
    public async Task<ActionResult> UploadPhoto()
    {
        return Ok();
    }

    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("delete-photo")]
    public async Task<ActionResult> DeletePhoto()
    {
        return Ok();
    }
    
    
}