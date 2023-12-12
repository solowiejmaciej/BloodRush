using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using BloodRush.Contracts.QrCodes;
using BloodRush.Contracts.QrCodes.Interfaces;
using MediatR;

namespace BloodRush.API.Handlers.QrCodes;

public class GenerateQrCodeCommandHandler : IRequestHandler<GenerateQrCodeCommand, QrCodeResponse>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IQrCodeGenerator _qrCodeGenerator;

    public GenerateQrCodeCommandHandler(
        IUserContextAccessor userContextAccessor,
        IQrCodeGenerator qrCodeGenerator
        )
    {
        _userContextAccessor = userContextAccessor;
        _qrCodeGenerator = qrCodeGenerator;
    }
    public Task<QrCodeResponse> Handle(GenerateQrCodeCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        var qrCode = _qrCodeGenerator.GenerateQrCode(donorId);
        var result = new QrCodeResponse(qrCode.ToString(), donorId, qrCode.CreatedAt);
        return Task.FromResult(result);
    }
}

public record GenerateQrCodeCommand : IRequest<QrCodeResponse>;