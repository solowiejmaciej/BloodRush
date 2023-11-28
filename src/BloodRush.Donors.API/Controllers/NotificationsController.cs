#region

using BloodRush.API.Dtos;
using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

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
    public async Task<ActionResult<List<NotificationDto>>> GetNotifications()
    {
        var query = new GetNotificationsQuery();
        var notifications = await _mediator.Send(query);
        return Ok(notifications);
    }
    
    [HttpPut("push-token")]
    public async Task<IActionResult> UpdatePushToken(
        [FromBody] UpdatePushTokenCommand command
    )
    { 
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpPut("notifications-channel")]
    public async Task<IActionResult> UpdateNotificationsChannel(
        [FromBody] UpdateNotificationsChannelCommand command
    )
    {
        await _mediator.Send(command);
        return Ok();
    }
    
}