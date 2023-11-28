using BloodRush.DonationFacility.API.Dtos;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.Donations;

public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, DonationDto>
{
    public Task<DonationDto> Handle(GetDonationByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

}

public record GetDonationByIdQuery : IRequest<DonationDto>
{
    public int DonationId { get; set; }
    public Guid DonorId { get; set; }
}