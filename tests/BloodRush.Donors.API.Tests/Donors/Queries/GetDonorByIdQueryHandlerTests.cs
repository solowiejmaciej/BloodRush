using Xunit;
using Moq;
using System;
using AutoMapper;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.Interfaces;
using BloodRush.API.Dtos;
using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Exceptions;

public class GetDonorByIdQueryHandlerTests
{
    private readonly Mock<IDonorRepository> _mockDonorRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IUserContextAccessor> _mockUserContextAccessor;
    private readonly GetDonorByIdQueryHandler _handler;

    public GetDonorByIdQueryHandlerTests()
    {
        _mockDonorRepository = new Mock<IDonorRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockUserContextAccessor = new Mock<IUserContextAccessor>();
        _handler = new GetDonorByIdQueryHandler(_mockDonorRepository.Object, _mockMapper.Object, _mockUserContextAccessor.Object);
    }

    [Fact]
    public async Task Handle_ReturnsMappedDonor_WhenDonorExistsAndMatchesCurrentUser()
    {
        var donorId = Guid.NewGuid();
        var donor = new Donor
        {
            Id = donorId,
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
        var donorDto = new DonorDto
        {
            Id = donorId,
            FirstName = null,
            Surname = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = 0,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        };
        _mockDonorRepository.Setup(repo => repo.GetDonorByIdAsync(donorId)).ReturnsAsync(donor);
        _mockMapper.Setup(mapper => mapper.Map<DonorDto>(donor)).Returns(donorDto);
        _mockUserContextAccessor.Setup(accessor => accessor.GetDonorId()).Returns(donorId);

        var result = await _handler.Handle(new GetDonorByIdQuery { Id = donorId }, CancellationToken.None);

        Assert.Equal(donorDto, result);
    }

    [Fact]
    public async Task Handle_ThrowsDonorNotFoundException_WhenDonorExistsButDoesNotMatchCurrentUser()
    {
        var donorId = Guid.NewGuid();
        var currentUserId = Guid.NewGuid();
        var donor = new Donor
        {
            Id = donorId,
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
        _mockDonorRepository.Setup(repo => repo.GetDonorByIdAsync(donorId)).ReturnsAsync(donor);
        _mockUserContextAccessor.Setup(accessor => accessor.GetDonorId()).Returns(currentUserId);

        await Assert.ThrowsAsync<DonorNotFoundException>(() => _handler.Handle(new GetDonorByIdQuery { Id = donorId }, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ThrowsDonorNotFoundException_WhenDonorDoesNotExist()
    {
        var donorId = Guid.NewGuid();
        _mockDonorRepository.Setup(repo => repo.GetDonorByIdAsync(donorId)).ReturnsAsync((Donor)null);

        await Assert.ThrowsAsync<DonorNotFoundException>(() => _handler.Handle(new GetDonorByIdQuery { Id = donorId }, CancellationToken.None));
    }
}