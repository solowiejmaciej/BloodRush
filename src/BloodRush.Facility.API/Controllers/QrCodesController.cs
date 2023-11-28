using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Handlers.QrCodes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BloodRush.DonationFacility.API.Controllers;


[ApiController]
[Route("api/qr-codes")]
public class QrCodesController : ControllerBase
{
    private readonly IMediator _mediator;

    public QrCodesController(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<ActionResult<DonorDto?>> Get(
        [FromBody] string qrCode
        )
    {
        var query = new GetDonorByQrCodeQuery
        {
            QrCode = qrCode
        };
        var donorDto = await _mediator.Send(query);
        return Ok(donorDto);
    }
    
}