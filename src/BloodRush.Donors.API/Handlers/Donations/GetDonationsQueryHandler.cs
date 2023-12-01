using BloodRush.API.Dtos;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using MediatR;

namespace BloodRush.API.Handlers.Donations;
 
public class GetDonationsQueryHandler : IRequestHandler<GetDonationsQuery, List<DonationDto>>
{
    private readonly IDonationRepository _donationRepository;
    private readonly IUserContextAccessor _userContextAccessor;

    public GetDonationsQueryHandler(
        IDonationRepository donationRepository,
        IUserContextAccessor userContextAccessor
        )
    {
        _donationRepository = donationRepository;
        _userContextAccessor = userContextAccessor;
    }
    public Task<List<DonationDto>> Handle(GetDonationsQuery request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        return _donationRepository.GetDonationsByUserIdAsync(donorId);
    }
}

public record GetDonationsQuery : IRequest<List<DonationDto>>;