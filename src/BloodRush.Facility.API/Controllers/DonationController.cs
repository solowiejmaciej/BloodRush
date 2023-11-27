using BloodRush.DonationFacility.API.Handlers.Donations;
using BloodRush.DonationFacility.API.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.DonationFacility.API.Controllers;

[ApiController]
[Route("api/Donors/{donorId}/[controller]/{collectionFacilityId:int}")]
public class DonationController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonationController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post(
        [FromRoute] Guid donorId,
        [FromRoute] int collectionFacilityId,
        [FromBody] AddNewDonationRequest request
    )
    { 
        var command = new AddNewDonationCommand
        {
            DonationDate = request.DonationDate,
            DonationFacilityId = collectionFacilityId,
            DonationResult = request.DonationResult,
            QuantityInMl = request.QuantityInMl,
            DonorId = donorId
        };
        await _mediator.Send(command);
        return Ok();
    }
    
}