#region

using AutoMapper;
using BloodRush.API.Dtos;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using FluentValidation;
using MediatR;

#endregion

namespace BloodRush.API.Handlers.Donors;

public class GetDonorByIdQueryHandler : IRequestHandler<GetDonorByIdQuery, DonorDto>
{
    private readonly IDonorRepository _donorRepository;
    private readonly IMapper _mapper;
    private readonly IUserContextAccessor _userContextAccessor;

    public GetDonorByIdQueryHandler(
        IDonorRepository donorRepository,
        IMapper mapper,
        IUserContextAccessor userContextAccessor
    )
    {
        _donorRepository = donorRepository;
        _mapper = mapper;
        _userContextAccessor = userContextAccessor;
    }

    public async Task<DonorDto> Handle(GetDonorByIdQuery request, CancellationToken cancellationToken)
    {
        var donor = await _donorRepository.GetDonorByIdAsync(request.Id);
        var currentUserId = _userContextAccessor.GetDonorId();
        if (donor.Id != currentUserId )    
        {
            throw new DonorNotFoundException();
        }
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