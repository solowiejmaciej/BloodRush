using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using AutoMapper;
using BloodRush.DonationFacility.API.Handlers.Donations;
using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Dtos;
using BloodRush.DonationFacility.API.Entities;

public class GetDonationsQueryHandlerTests
{
    private readonly Mock<IDonationRepository> _mockDonationRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetDonationsQueryHandler _handler;

    public GetDonationsQueryHandlerTests()
    {
        _mockDonationRepository = new Mock<IDonationRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetDonationsQueryHandler(_mockDonationRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ReturnsMappedDonations_WhenDonationsExistForDonor()
    {
        var donorId = Guid.NewGuid();
        var donations = new List<Donation> { new Donation { DonorId = donorId }, new Donation { DonorId = donorId } };
        var donationDtos = new List<DonationDto> { new DonationDto { DonorId = donorId }, new DonationDto { DonorId = donorId } };
        _mockDonationRepository.Setup(repo => repo.GetDonationsByDonorIdAsync(donorId)).ReturnsAsync(donations);
        _mockMapper.Setup(mapper => mapper.Map<List<DonationDto>>(donations)).Returns(donationDtos);

        var result = await _handler.Handle(new GetDonationsQuery { DonorId = donorId }, CancellationToken.None);

        Assert.Equal(donationDtos, result);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoDonationsExistForDonor()
    {
        var donorId = Guid.NewGuid();
        var donations = new List<Donation>();
        var donationDtos = new List<DonationDto>();
        _mockDonationRepository.Setup(repo => repo.GetDonationsByDonorIdAsync(donorId)).ReturnsAsync(donations);
        _mockMapper.Setup(mapper => mapper.Map<List<DonationDto>>(donations)).Returns(donationDtos);

        var result = await _handler.Handle(new GetDonationsQuery { DonorId = donorId }, CancellationToken.None);

        Assert.Empty(result);
    }
}
