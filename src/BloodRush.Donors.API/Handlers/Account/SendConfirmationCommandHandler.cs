using BloodRush.API.Entities;
using BloodRush.API.Exceptions;
using BloodRush.API.Exceptions.ConfirmationCodes;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.Contracts.Enums;
using MediatR;

namespace BloodRush.API.Handlers.Account;

public class SendConfirmationCommandHandler : IRequestHandler<SendConfirmationCommand>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IConfirmationCodesRepository _confirmationCodesRepository;
    private readonly IEventPublisher _eventPublisher;

    public SendConfirmationCommandHandler(
        IDonorRepository donorRepository,
        IUserContextAccessor userContextAccessor,
        IConfirmationCodesRepository confirmationCodesRepository,
        IEventPublisher eventPublisher
        )
    {
        _donorRepository = donorRepository;
        _userContextAccessor = userContextAccessor;
        _confirmationCodesRepository = confirmationCodesRepository;
        _eventPublisher = eventPublisher;
    }
    public async Task Handle(SendConfirmationCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContextAccessor.GetDonorId();
        
        var donor = await _donorRepository.GetDonorByIdAsync(currentUser);

        if (donor.IsEmailConfirmed && request.CodeType == ECodeType.Email) throw new EmailAlreadyConfirmedException();
        if (donor.IsPhoneNumberConfirmed && request.CodeType == ECodeType.Sms) throw new PhoneNumberAlreadyConfirmedException();
        
        var code = await _confirmationCodesRepository.GenerateCodeAsync(request.CodeType, currentUser);
        
        await _eventPublisher.PublishSendConfirmationCodeEventAsync(code, currentUser, cancellationToken);
    }
}

public record SendConfirmationCommand : IRequest
{ 
    public required ECodeType CodeType { get; set; }
}