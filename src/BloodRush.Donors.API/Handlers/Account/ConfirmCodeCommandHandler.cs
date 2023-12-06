using BloodRush.API.Exceptions.ConfirmationCodes;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.Contracts.Enums;
using FluentValidation;
using MediatR;

namespace BloodRush.API.Handlers.Account;

public class ConfirmCodeCommandHandler : IRequestHandler<ConfirmCodeCommand>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IConfirmationCodesRepository _confirmationCodesRepository;
    

    public ConfirmCodeCommandHandler(
        IDonorRepository donorRepository,
        IUserContextAccessor userContextAccessor,
        IConfirmationCodesRepository confirmationCodesRepository
    )
    {
        _donorRepository = donorRepository;
        _userContextAccessor = userContextAccessor;
        _confirmationCodesRepository = confirmationCodesRepository;
    }
    public async Task Handle(ConfirmCodeCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContextAccessor.GetDonorId();
        var isValid = await _confirmationCodesRepository.IsValidCode(currentUser, request.Code, request.CodeType);
        if (!isValid) throw new InvalidCodeException();
        switch (request.CodeType)
        {
            case ECodeType.Email:
                await _donorRepository.ConfirmEmailAsync(currentUser);
                break;
            case ECodeType.Sms:
                await _donorRepository.ConfirmPhoneNumberAsync(currentUser);
                break;
            default:
                throw new InvalidOperationException("Invalid code type");
        }
    }
}

public record ConfirmCodeCommand : IRequest
{
   public ECodeType CodeType { get; set; }
   public string Code { get; set; }
}

public class ConfirmCodeCommandValidator : AbstractValidator<ConfirmCodeCommand>
{
    public ConfirmCodeCommandValidator()
    {
        RuleFor(x => x.CodeType).IsInEnum();
        RuleFor(x => x.Code).NotEmpty();
    }
}