using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using BloodRush.Contracts.QrCodes;
using MediatR;

namespace BloodRush.API.Handlers.QrCodes;

public class GenerateQrCodeCommandHandler : IRequestHandler<GenerateQrCodeCommand, QrCodeResponse>
{
    private readonly IUserContextAccessor _userContextAccessor;

    public GenerateQrCodeCommandHandler(
        IUserContextAccessor userContextAccessor
        )
    {
        _userContextAccessor = userContextAccessor;
    }
    public Task<QrCodeResponse> Handle(GenerateQrCodeCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        var qrCode = new QrCode(donorId);
        var result = new QrCodeResponse(qrCode.ToString(), donorId, qrCode.CreatedAt);
        return Task.FromResult(result);
    }
}

public record GenerateQrCodeCommand : IRequest<QrCodeResponse>;