using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Handlers.Donors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.DonationFacility.API.Controllers;

[ApiController]
[Route("api/donors")]
public class DonorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonorsController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    //[Authorize]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<List<DonorDto>>> Get()
    {
        var donors = await _mediator.Send(new GetAllDonorsQuery());
        return Ok(donors);
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<DonorDto>> Get([FromRoute] Guid id)
    {
        var donorDto = await _mediator.Send(new GetDonorByIdQuery { Id = id });
        return Ok(donorDto);
    }
}