#region

using BloodRush.API.Handlers.Auth;
using BloodRush.API.Handlers.QrCodes;
using BloodRush.API.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BloodRush.API.Controllers;

[Authorize]
[Route("api/qr-codes")]
public class QrCodesController : ControllerBase
{
    private readonly ILogger<DonorsController> _logger;
    private readonly IMediator _mediator;

    public QrCodesController(
        ILogger<DonorsController> logger,
        IMediator mediator
    )
    {
        _logger = logger;
        _mediator = mediator;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("generate")]
    public async Task<ActionResult<QrCodeResponse>> GenerateQrCode()
    {
        var command = new GenerateQrCodeCommand();
        var qrCode = await _mediator.Send(command);
        return Ok(qrCode);
    }
    
}