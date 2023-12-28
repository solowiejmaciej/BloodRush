using Xunit;
using Moq;
using System;
using AutoMapper;
using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Handlers.Donations;
using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Entities.Enums;
using BloodRush.DonationFacility.API.Exceptions;
using MediatR;

public class AddNewDonationCommandHandlerTests
{
    private readonly Mock<IDonationRepository> _mockDonationRepository;
    private readonly Mock<IDonorInfoRepository> _mockDonorInfoRepository;
    private readonly Mock<IMediator> _mockMediator;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IDonationFacilityRepository> _mockDonationFacilityRepository;
    private readonly AddNewDonationCommandHandler _handler;

    public AddNewDonationCommandHandlerTests()
    {
        _mockMapper = new Mock<IMapper>();
        _mockDonationRepository = new Mock<IDonationRepository>();
        _mockDonorInfoRepository = new Mock<IDonorInfoRepository>();
        _mockMediator = new Mock<IMediator>();
        _mockDonationFacilityRepository = new Mock<IDonationFacilityRepository>();
        
        _handler = new AddNewDonationCommandHandler(null, _mockMapper.Object , _mockDonationRepository.Object, _mockDonorInfoRepository.Object, _mockMediator.Object, _mockDonationFacilityRepository.Object);
    }

    [Fact]
    public async Task Handle_ThrowsDonationFacilityNotFoundException_WhenDonationFacilityDoesNotExist()
    {
        var command = new AddNewDonationCommand { DonationFacilityId = 1 };
        _mockDonationFacilityRepository.Setup(repo => repo.GetDonationFacilityByIdAsync(command.DonationFacilityId)).ReturnsAsync((DonationFacility)null);

        await Assert.ThrowsAsync<DonationFacilityNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsDonorIsRestingException_WhenDonorIsInRestingPeriod()
    {
        var command = new AddNewDonationCommand { DonorId = Guid.NewGuid(), DonationFacilityId = 1 };
        _mockDonationFacilityRepository.Setup(repo => repo.GetDonationFacilityByIdAsync(command.DonationFacilityId)).ReturnsAsync(new DonationFacility());
        _mockDonorInfoRepository.Setup(repo => repo.GetRestingPeriodInfoByDonorIdAsync(command.DonorId)).ReturnsAsync(new DonorRestingPeriodInfo { IsRestingPeriodActive = true });

        await Assert.ThrowsAsync<DonorIsRestingException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_PublishesSuccessfulDonationEvent_WhenDonationIsSuccessful()
    {
        var command = new AddNewDonationCommand { DonorId = Guid.NewGuid(), DonationFacilityId = 1, DonationResult = EDonationResult.Success };
        _mockDonationFacilityRepository.Setup(repo => repo.GetDonationFacilityByIdAsync(command.DonationFacilityId)).ReturnsAsync(new DonationFacility());
        _mockDonorInfoRepository.Setup(repo => repo.GetRestingPeriodInfoByDonorIdAsync(command.DonorId)).ReturnsAsync(new DonorRestingPeriodInfo { IsRestingPeriodActive = false });
        _mockMapper.Setup(mapper => mapper.Map<Donation>(command)).Returns(new Donation());
        
        await _handler.Handle(command, CancellationToken.None);

        _mockMediator.Verify(mediator => mediator.Publish(It.IsAny<SuccessfulDonationEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}