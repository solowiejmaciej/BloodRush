using BloodRush.DonationFacility.API.Handlers.Donations;
using BloodRush.DonationFacility.API.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.DonationFacility.API.Controllers;

[ApiController]
[Route("api/donors/{donorId}/donations/")]
public class DonationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonationsController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }
    
    [Route("collection-facility/{collectionFacilityId:int}")]
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
    
    [HttpGet]
    [Route("{donationId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(
        [FromRoute] int donationId,
        [FromRoute] Guid donorId
    )
    { 
        var query = new GetDonationByIdQuery
        {
            DonorId = donorId,
            DonationId = donationId
        };
        var donation = await _mediator.Send(query);
        return Ok(donation);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromRoute] Guid donorId
    )
    { 
        var query = new GetDonationsQuery
        {
            DonorId = donorId,
        };
        var donations = await _mediator.Send(query);
        return Ok(donations);
    }
    
}