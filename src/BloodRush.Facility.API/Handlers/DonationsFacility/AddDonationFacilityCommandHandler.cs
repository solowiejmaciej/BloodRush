using BloodRush.DonationFacility.API.Interfaces;
using FluentValidation;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.DonationsFacility;

public class AddDonationFacilityCommandHandler : IRequestHandler<AddDonationFacilityCommand>
{
    private readonly IDonationFacilityRepository _donationFacilityRepository;

    public AddDonationFacilityCommandHandler(
        IDonationFacilityRepository donationFacilityRepository
        )
    {
        _donationFacilityRepository = donationFacilityRepository;
    }
    public async Task Handle(AddDonationFacilityCommand request, CancellationToken cancellationToken)
    {
        var donationFacility = new Entities.DonationFacility()
        {
            Name = request.Name,
            Address = request.Address
        };

        await _donationFacilityRepository.AddDonationFacilityAsync(donationFacility);
    }
}

public record AddDonationFacilityCommand : IRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
}

public class AddDonationFacilityCommandValidator : AbstractValidator<AddDonationFacilityCommand>
{
    public AddDonationFacilityCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Address).NotEmpty();
    }
}