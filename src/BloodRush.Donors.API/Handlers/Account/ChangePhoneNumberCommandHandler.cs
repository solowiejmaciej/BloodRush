using Azure.Core;
using BloodRush.API.Exceptions;
using BloodRush.API.Exceptions.ConfirmationCodes;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace BloodRush.API.Handlers.Account;

public class ChangePhoneNumberCommandHandler : IRequestHandler<ChangePhoneNumberCommand>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public ChangePhoneNumberCommandHandler(
        IDonorRepository donorRepository,
        IUserContextAccessor userContextAccessor
    )
    {
        _donorRepository = donorRepository;
        _userContextAccessor = userContextAccessor;
    }

    public async Task Handle(ChangePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var currentDonorId = _userContextAccessor.GetDonorId();
        var donor = await _donorRepository.GetDonorByIdAsync(currentDonorId);
        
        var donorWithNewPhoneNumber = await _donorRepository.GetDonorByPhoneNumberAsync(request.NewPhoneNumber);
        if (donorWithNewPhoneNumber != null) throw new PhoneNumberAlreadyConfirmedException();
        
        donor.PhoneNumber = request.NewPhoneNumber;
        donor.IsPhoneNumberConfirmed = false;
        await _donorRepository.SaveChangesAsync();
    }
}

public record ChangePhoneNumberCommand : IRequest
{
    public required string NewPhoneNumber { get; init; }
}

public class ChangePhoneNumberCommandValidator : AbstractValidator<ChangePhoneNumberCommand>
{
    private readonly IDonorRepository _donorRepository; // Inject the IDonorRepository

    
    public ChangePhoneNumberCommandValidator(IDonorRepository donorRepository)
    {
        _donorRepository = donorRepository;
        RuleFor(x => x.NewPhoneNumber)
            .Must(UniquePhoneNumber).WithMessage("Phone number already exists")
            .MinimumLength(9)
            .MaximumLength(11)
            .NotEmpty();
    }
    
    private bool UniquePhoneNumber(string phoneNumber)
    {
        var donorInDb = _donorRepository.GetDonorByPhoneNumberAsync(phoneNumber).Result;
        return donorInDb == null;
    }
}
