using BloodRush.API.Entities.Enums;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using static BloodRush.Contracts.Constants.DonorConstants;

namespace BloodRush.API.Handlers.RestingPeriod;

public class UpdateRestingPeriodQueryHandler : IRequestHandler<UpdateRestingPeriodMonthsCommand>
{
    private readonly IRestingPeriodRepository _restingPeriodRepository;
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IDonorRepository _donorRepository;

    public UpdateRestingPeriodQueryHandler(
        IRestingPeriodRepository restingPeriodRepository,
        IUserContextAccessor userContextAccessor,
        IDonorRepository donorRepository
        )
    {
        _restingPeriodRepository = restingPeriodRepository;
        _userContextAccessor = userContextAccessor;
        _donorRepository = donorRepository;
    }
    public async Task Handle(UpdateRestingPeriodMonthsCommand request, CancellationToken cancellationToken)
    {
        var donorId = _userContextAccessor.GetDonorId();
        var donor = await _donorRepository.GetDonorByIdAsync(donorId);

        if (donor.Sex == ESex.Female)
        {
            if (request.RestingPeriodInMonths < FemaleMinRestingPeriod)
            {
                throw new ValidationException($"Minimal resting period for female is {FemaleMinRestingPeriod}");
            }
        }
        
        await _restingPeriodRepository.UpdateRestingPeriodMonthsAsync(donorId, request.RestingPeriodInMonths);
    }
}

public record UpdateRestingPeriodMonthsCommand : IRequest
{
    public required int RestingPeriodInMonths { get; init; }
}

public class UpdateRestingPeriodMonthsCommandValidator : AbstractValidator<UpdateRestingPeriodMonthsCommand>
{
    public UpdateRestingPeriodMonthsCommandValidator()
    {
        RuleFor(x => x.RestingPeriodInMonths)
            .NotEmpty()
            .ExclusiveBetween(1, 12)
            .WithMessage("Invalid resting period");
    }
}