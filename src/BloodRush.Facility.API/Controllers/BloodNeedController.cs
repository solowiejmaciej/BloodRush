using BloodRush.DonationFacility.API.Handlers.BloodNeed;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.DonationFacility.API.Controllers;


[ApiController]
[Route("api/collection-facility/{collectionFacilityId:int}/blood-need")]
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
            IsUrgent = isUrgent
        };
        await _mediator.Send(command);
        
        return Ok();
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromRoute] int collectionFacilityId
        )
    { 
        var query = new GetBloodNeedsQuery
        {
            CollectionFacilityId = collectionFacilityId,
        };
        var bloodNeeds = await _mediator.Send(query);
        
        return Ok(bloodNeeds);
    }
    
    [Route("{bloodNeedId:int}/cancel")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CancelBloodNeed(
        [FromRoute] int collectionFacilityId, 
        [FromRoute] int bloodNeedId
        )
    { 
        var command = new CancelBloodNeedCommand
        {
            BloodNeedId = bloodNeedId,
            CollectionFacilityId = collectionFacilityId,
        };
        await _mediator.Send(command);
        
        return Ok();
    }
}