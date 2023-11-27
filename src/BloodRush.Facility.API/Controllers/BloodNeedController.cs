using BloodRush.DonationFacility.API.Handlers.BloodNeed;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.DonationFacility.API.Controllers;


[ApiController]
[Route("api/{collectionFacilityId:int}/[controller]")]
public class BloodNeedController : ControllerBase
{
    private readonly IMediator _mediator;

    public BloodNeedController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(
        [FromRoute] int collectionFacilityId, 
        [FromQuery] bool isUrgent
        )
    { 
        var command = new AddNewBloodNeedCommand
        {
            CollectionFacilityId = collectionFacilityId,
            IsUrget = isUrgent
        };
        await _mediator.Send(command);
        
        return Ok();
    }
}