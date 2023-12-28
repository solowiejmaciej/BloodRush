using Xunit;
using Moq;
using System;
using AutoMapper;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.Interfaces;
using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;

public class AddNewDonorCommandHandlerTests
{
    private readonly Mock<IDonorRepository> _mockDonorRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IEventPublisher> _mockEventPublisher;
    private readonly Mock<ILoginManager> _mockLoginManager;
    private readonly AddNewDonorCommandHandler _handler;

    public AddNewDonorCommandHandlerTests()
    {
        _mockDonorRepository = new Mock<IDonorRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockEventPublisher = new Mock<IEventPublisher>();
        _mockLoginManager = new Mock<ILoginManager>();
        _handler = new AddNewDonorCommandHandler(_mockDonorRepository.Object, _mockMapper.Object, _mockEventPublisher.Object, _mockLoginManager.Object);
    }

    [Fact]
    public async Task Handle_ReturnsNewDonorId_WhenDonorIsAddedSuccessfully()
    {
        var donor = new Donor
        {
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        };
        var donorWithHashedPassword = new Donor
        {
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        };
        var command = new AddNewDonorCommand
        {
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        };
        var id = Guid.NewGuid();
        _mockMapper.Setup(mapper => mapper.Map<Donor>(command)).Returns(donor);
        _mockLoginManager.Setup(manager => manager.HashPassword(donor, donor.Password)).Returns(donorWithHashedPassword);
        _mockDonorRepository.Setup(repo => repo.AddDonorAsync(donorWithHashedPassword)).ReturnsAsync(id);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.Equal(id, result);
    }

    [Fact]
    public async Task Handle_PublishesDonorCreatedEvent_WhenDonorIsAddedSuccessfully()
    {
        var donor = new Donor
        {
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        };
        var donorWithHashedPassword = new Donor
        {
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        };
        var command = new AddNewDonorCommand
        {
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        };
        var id = Guid.NewGuid();
        _mockMapper.Setup(mapper => mapper.Map<Donor>(command)).Returns(donor);
        _mockLoginManager.Setup(manager => manager.HashPassword(donor, donor.Password)).Returns(donorWithHashedPassword);
        _mockDonorRepository.Setup(repo => repo.AddDonorAsync(donorWithHashedPassword)).ReturnsAsync(id);

        await _handler.Handle(command, CancellationToken.None);

        _mockEventPublisher.Verify(publisher => publisher.PublishDonorCreatedEventAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }
}