#region

using BloodRush.API.Dtos;
using BloodRush.API.Handlers.Donors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return Ok(await _mediator.Send(new GetAllDonorsQuery()));
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<DonorDto>> Get([FromRoute] Guid id)
    {
        var donorDto = await _mediator.Send(new GetDonorByIdQuery { Id = id });
        return Ok(donorDto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] AddNewDonorCommand request)
    {
        var id = await _mediator.Send(request);
        return Created($"/api/Donors/{id}", request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] string value)
    {
        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _mediator.Send(new DeleteDonorCommand { DonorId = id });
        return NoContent();
    }
}