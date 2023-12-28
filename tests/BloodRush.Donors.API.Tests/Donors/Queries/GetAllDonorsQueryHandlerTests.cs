using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading;
using AutoMapper;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.Dtos;
using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;

public partial class GetAllDonorsQueryHandlerTests
{
    private readonly Mock<IDonorRepository> _mockDonorRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAllDonorsQueryHandler _handler;

    public GetAllDonorsQueryHandlerTests()
    {
        _mockDonorRepository = new Mock<IDonorRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetAllDonorsQueryHandler(_mockDonorRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ReturnsMappedDonors_WhenRepositoryReturnsDonors()
    {
        // Arrange
        var donors = new List<Donor> { new Donor
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
        }, new Donor
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
            }
        };
        var donorDtos = new List<DonorDto> {
            new DonorDto
            {
                FirstName = null,
                Surname = null,
                Sex = ESex.Male,
                DateOfBirth = default,
                BloodType = EBloodType.APositive,
                PhoneNumber = 0,
                Email = null,
                HomeAddress = null,
                Pesel = null,
                MaxDonationRangeInKm = 0,
                Id = default
            }
        };
        donorDtos.Add(new DonorDto
        {
            FirstName = null,
            Surname = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = 0,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0,
            Id = default
        });
        _mockDonorRepository.Setup(repo => repo.GetAllDonorsAsync()).ReturnsAsync(donors);
        _mockMapper.Setup(mapper => mapper.Map<List<DonorDto>>(donors)).Returns(donorDtos);

        // Act
        var result = await _handler.Handle(new GetAllDonorsQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(donorDtos, result);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenRepositoryReturnsNoDonors()
    {
        // Arrange
        var donors = new List<Donor>();
        var donorDtos = new List<DonorDto>();
        _mockDonorRepository.Setup(repo => repo.GetAllDonorsAsync()).ReturnsAsync(donors);
        _mockMapper.Setup(mapper => mapper.Map<List<DonorDto>>(donors)).Returns(donorDtos);

        // Act
        var result = await _handler.Handle(new GetAllDonorsQuery(), CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }
}