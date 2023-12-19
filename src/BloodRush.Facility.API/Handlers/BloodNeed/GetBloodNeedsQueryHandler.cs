using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using MediatR;

namespace BloodRush.DonationFacility.API.Handlers.BloodNeed;

public class GetBloodNeedsQueryHandler : IRequestHandler<GetBloodNeedsQuery, List<BloodNeedDto>>
{
    private readonly IBloodNeedRepository _bloodNeedRepository;
    private readonly IMapper _mapper;

    public GetBloodNeedsQueryHandler(IBloodNeedRepository bloodNeedRepository, IMapper mapper)
    {
        _bloodNeedRepository = bloodNeedRepository;
        _mapper = mapper;
    }
    public async Task<List<BloodNeedDto>> Handle(GetBloodNeedsQuery request, CancellationToken cancellationToken)
    {
        var bloodNeeds =  await _bloodNeedRepository.GetBloodNeedsAsync(request.CollectionFacilityId, cancellationToken);
        return _mapper.Map<List<BloodNeedDto>>(bloodNeeds);
    }
}


public record GetBloodNeedsQuery : IRequest<List<BloodNeedDto>>
{
    public required int CollectionFacilityId { get; init; }
}