#region

using AutoMapper;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Interfaces;
using FluentValidation;
using MediatR;

#endregion

namespace BloodRush.DonationFacility.API.Handlers.Donors;

public class GetDonorByIdQueryHandler : IRequestHandler<GetDonorByIdQuery, DonorDto>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;

    public GetDonorByIdQueryHandler(
        IDonorRepository donorRepository,
        IMapper mapper
    )
    {
        _donorRepository = donorRepository;
        _mapper = mapper;
    }

    public async Task<DonorDto> Handle(GetDonorByIdQuery request, CancellationToken cancellationToken)
    {
        var donor = await _donorRepository.GetDonorByIdAsync(request.Id);
        var donorDto = _mapper.Map<DonorDto>(donor);
        return donorDto;
    }
}

public record GetDonorByIdQuery : IRequest<DonorDto>
{
    public Guid Id { get; set; }
}

public class GetDonorByIdQueryValidator : AbstractValidator<GetDonorByIdQuery>
{
    public GetDonorByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}