#region

using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Handlers.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.DonationFacility.API.Controllers;

[ApiController]
[Route("api/donor/{donorId:int}/notifications")]
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
    
    [HttpPost]
    public async Task<ActionResult> Add(
        [FromBody] AddNotificationCommand command
        )
    { 
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationDto>>> GetNotifications()
    {
        var query = new GetNotificationsQuery();
        var notifications = await _mediator.Send(query);
        return Ok(notifications);
    }
    
}