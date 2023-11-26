using BloodRush.API.Entities.Enums;
using BloodRush.API.Handlers.Donations;
using BloodRush.API.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.API.Controllers;

[ApiController]
[Route("api/Donors/{donorId}/[controller]")]
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
        [FromBody] AddNewDonationRequest request
    )
    { 
        var command = new AddNewDonationCommand
        {
            DonationDate = request.DonationDate,
            DonationResult = request.DonationResult,
            QuantityInMl = request.QuantityInMl,
            DonorId = donorId
        };
        await _mediator.Send(command);
        return Ok();
    }
    
}