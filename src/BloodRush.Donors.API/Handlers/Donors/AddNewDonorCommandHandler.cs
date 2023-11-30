#region

using AutoMapper;
using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using FluentValidation;
using MediatR;

#endregion

namespace BloodRush.API.Handlers.Donors;

public class AddNewDonorCommandHandler : IRequestHandler<AddNewDonorCommand, Guid>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILoginManager _loginManager;

    public AddNewDonorCommandHandler(
        IDonorRepository donorRepository,
        IMapper mapper,
        IEventPublisher eventPublisher,
        ILoginManager loginManager
    )
    {
        _donorRepository = donorRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
        _loginManager = loginManager;
    }

    public async Task<Guid> Handle(AddNewDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = _mapper.Map<Donor>(request);
        var donorWithHashedPassword = _loginManager.HashPassword(donor, donor.Password);
        var id = await _donorRepository.AddDonorAsync(donorWithHashedPassword);
        await _eventPublisher.PublishDonorCreatedEventAsync(id, donor.PhoneNumber, cancellationToken);
        return id;
    }
}

public record AddNewDonorCommand : IRequest<Guid>
{
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required string Password { get; set; }
    public required ESex Sex { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required EBloodType BloodType { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string HomeAddress { get; set; }
    public required string Pesel { get; set; }
}

public class AddNewDonorCommandValidator : AbstractValidator<AddNewDonorCommand>
{
    private readonly IDonorRepository _donorRepository; // Inject the IDonorRepository

    public AddNewDonorCommandValidator(IDonorRepository donorRepository)
    {
        _donorRepository = donorRepository;

        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        RuleFor(x => x.Surname)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(3);
        RuleFor(x => x.Pesel)
            .NotEmpty();
        RuleFor(x => x.HomeAddress)
            .NotEmpty();
        RuleFor(x => x.PhoneNumber)
            .Must(UniquePhoneNumber).WithMessage("Phone number already exists")
            .MinimumLength(9)
            .MaximumLength(11)
            .NotEmpty();
        RuleFor(x => x.BloodType)
            .IsInEnum();
    }

    private bool UniquePhoneNumber(string phoneNumber)
    {
        var donorInDb = _donorRepository.GetDonorByPhoneNumberAsync(phoneNumber).Result;
        return donorInDb == null;
    }
}