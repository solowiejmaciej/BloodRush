#region

using AutoMapper;
using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Interfaces;
using FluentValidation;
using MediatR;

#endregion

namespace BloodRush.API.Handlers;

public class AddNewDonorCommandHandler : IRequestHandler<AddNewDonorCommand, Guid>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public AddNewDonorCommandHandler(
        IDonorRepository donorRepository,
        IMapper mapper,
        IEventPublisher eventPublisher
    )
    {
        _donorRepository = donorRepository;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<Guid> Handle(AddNewDonorCommand request, CancellationToken cancellationToken)
    {
        var donor = _mapper.Map<Donor>(request);
        await _eventPublisher.PublishDonorCreatedEventAsync(donor.Id, cancellationToken);
        return await _donorRepository.AddDonorAsync(donor);
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
    public required int PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string HomeAddress { get; set; }
    public required string Pesel { get; set; }
}

public class AddNewDonorCommandValidator : AbstractValidator<AddNewDonorCommand>
{
    public AddNewDonorCommandValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
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
            .NotEmpty();
        RuleFor(x => x.BloodType)
            .IsInEnum();
    }
}