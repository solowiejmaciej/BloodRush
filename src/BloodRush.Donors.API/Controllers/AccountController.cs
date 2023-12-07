#region

using BloodRush.API.Entities;
using BloodRush.API.Extensions;
using BloodRush.API.Handlers.Account;
using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.QrCodes;
using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using BloodRush.Contracts.Enums;
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
    public async Task<ActionResult> ChangePassword(
        [FromBody] ChangePasswordCommand command
        )
    {
        
        await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPatch("change-email")]
    public async Task<ActionResult> ChangeEmail(
        [FromBody] ChangeEmailCommand command
        )
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPatch("change-phone-number")]
    public async Task<ActionResult> ChangePhoneNumber(
        [FromBody] ChangePhoneNumberCommand command
        )
    {
        await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("send-email-confirmation-code")]
    public async Task<ActionResult> SendEmailConfirmation()
    {
        var command = new SendConfirmationCommand
        {
            CodeType = ECodeType.Email
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("send-phone-number-confirmation-code")]
    public async Task<ActionResult> SendPhoneNumberConfirmation()
    {
        var command = new SendConfirmationCommand
        {
            CodeType = ECodeType.Sms
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("confirm-email")]
    public async Task<ActionResult> ConfirmEmail([FromQuery] string code)
    {
        var command = new ConfirmCodeCommand
        {
            CodeType = ECodeType.Email,
            Code = code
        };
        await _mediator.Send(command);
        return Ok();
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("confirm-phone-number")]
    public async Task<ActionResult> ConfirmPhoneNumber([FromQuery] string code)
    {
        var command = new ConfirmCodeCommand
        {
            CodeType = ECodeType.Sms,
            Code = code
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("upload-photo")]
    public async Task<ActionResult> UploadPhoto(
        [FromForm] UploadPhotoCommand command
        )
    {
        await _mediator.Send(command);
        return Ok();
    }

    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpDelete("delete-photo")]
    public async Task<ActionResult> DeletePhoto()
    {
        var command = new DeletePhotoCommand();
        await _mediator.Send(command);
        return Ok();
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpGet("photo")]
    public async Task<ActionResult> GetPhoto()
    {
        var command = new GetPhotoQuery();
        return Ok(await _mediator.Send(command));
    }

    
    
}