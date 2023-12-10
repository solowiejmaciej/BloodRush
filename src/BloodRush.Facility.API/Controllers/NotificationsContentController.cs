#region

using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Handlers.Notifications;
using BloodRush.DonationFacility.API.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.DonationFacility.API.Controllers;

[ApiController]
[Route("api/notifications-content/{donationFacilityId:int}")]
public class NotificationsContentController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public NotificationsContentController(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    [HttpPatch]
    [Route("update-content")]
    public async Task<ActionResult> UpdateNotificationContent(
        [FromRoute] int donationFacilityId,
        [FromBody] UpdateNotificationContentRequest request
        )
    {
        var command = new UpdateNotificationContentTemplateCommand
        {

            DonationFacilityId = donationFacilityId,
            Content = request.Content,
            Title = request.Title,
            NotificationType = request.NotificationType
        };
        await _mediator.Send(command);
        return Ok();
    }
    
    [HttpGet]
    public async Task<ActionResult<NotificationContentDto>> GetNotificationContent(
        [FromRoute] int donationFacilityId
        )
    {
        var query = new GetNotificationContentQuery()
        {
            DonationFacilityId = donationFacilityId
        };
        var notificationContent = await _mediator.Send(query);
        return Ok(notificationContent);
    }
    
}