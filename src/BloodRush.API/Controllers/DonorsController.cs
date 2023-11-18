#region

using BloodRush.API.Dtos;
using BloodRush.API.Handlers;
using BloodRush.API.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[ApiController]
[Route("[controller]")]
public class DonorsController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;
    private readonly IEventPublisher _eventPublisher;

    public DonorsController(
        ILogger<DonorsController> logger,
        IMediator mediator,
        IEventPublisher eventPublisher
    )
    {
        _logger = logger;
        _mediator = mediator;
        _eventPublisher = eventPublisher;
    }

    [HttpGet]
    public async Task<ActionResult<List<DonorDto>>> Get()
    {
        return Ok(await _mediator.Send(new GetAllDonorsQuery()));
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<DonorDto>> Get([FromRoute] Guid id)
    {
        var donorDto = await _mediator.Send(new GetDonorByIdQuery { Id = id });
        return Ok(donorDto);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddNewDonorCommand request)
    {
        var id = await _mediator.Send(request);
        return Created($"/api/Donors/{id}", request);
    }

    [HttpPut("{id}")]
    public async Task Put([FromRoute] int id, [FromBody] string value)
    {
        await _eventPublisher.PublishDonorCreatedEventAsync(Guid.NewGuid());
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] int id)
    {
        await Task.CompletedTask;
    }
}