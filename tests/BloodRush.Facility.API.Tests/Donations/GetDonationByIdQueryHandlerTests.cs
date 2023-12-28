using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BloodRush.DonationFacility.API.Handlers.Donations;
using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Dtos;

public class GetDonationByIdQueryHandlerTests
{
    private readonly Mock<IDonationRepository> _mockDonationRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetDonationByIdQueryHandler _handler;

    public GetDonationByIdQueryHandlerTests()
    {
        _mockDonationRepository = new Mock<IDonationRepository>();
        _mockMapper = new Mock<IMapper>();
        _handler = new GetDonationByIdQueryHandler(_mockDonationRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ReturnsMappedDonation_WhenDonationExistsForDonor()
    {
        var donorId = Guid.NewGuid();
        var donationId = 1;
        var donations = new List<Donation> { new Donation { Id = donationId, DonorId = donorId } };
        var donationDto = new DonationDto { Id = donationId };
        _mockDonationRepository.Setup(repo => repo.GetDonationsByDonorIdAsync(donorId)).ReturnsAsync(donations);
        _mockMapper.Setup(mapper => mapper.Map<DonationDto>(donations.First())).Returns(donationDto);

        var result = await _handler.Handle(new GetDonationByIdQuery { DonorId = donorId, DonationId = donationId }, CancellationToken.None);

        Assert.Equal(donationDto, result);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenDonationDoesNotExistForDonor()
    {
        var donorId = Guid.NewGuid();
        var donationId = 1;
        var donations = new List<Donation>();
        _mockDonationRepository.Setup(repo => repo.GetDonationsByDonorIdAsync(donorId)).ReturnsAsync(donations);

        var result = await _handler.Handle(new GetDonationByIdQuery { DonorId = donorId, DonationId = donationId }, CancellationToken.None);

        Assert.Null(result);
    }
}