#region

using BloodRush.API.Dtos;
using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;
[ApiController]
[Authorize]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public NotificationsController(
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
    public async Task<ActionResult<List<NotificationDto>>> GetNotifications()
    {
        var query = new GetNotificationsQuery();
        var notifications = await _mediator.Send(query);
        return Ok(notifications);
    }
    
    [HttpPatch("push-token")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePushToken(
        [FromBody] UpdatePushTokenCommand command
    )
    { 
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPatch("notifications-channel")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateNotificationsChannel(
        [FromBody] UpdateNotificationsChannelCommand command
    )
    {
        await _mediator.Send(command);
        return Ok();
    }
    
}