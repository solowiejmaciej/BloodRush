using BloodRush.Notifier.Handlers;
using BloodRush.Notifier.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.Notifier.Controllers;


[ApiController]
[Route("api/notifications/{donorId:guid}")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromRoute] Guid donorId
        )
    { 
        var query = new GetNotificationsQuery
        {
            DonorId = donorId,
        };
        var notifications = await _mediator.Send(query);
        
        return Ok(notifications);
    }
 
}

