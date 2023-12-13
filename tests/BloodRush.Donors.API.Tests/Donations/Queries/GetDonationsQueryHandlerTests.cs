using Xunit;
using Moq;
using MediatR;
using BloodRush.API.Handlers.Donations;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BloodRush.API.Dtos;

public class GetDonationsQueryHandlerTests
{
    private readonly Mock<IDonationRepository> _mockDonationRepository;
    private readonly Mock<IUserContextAccessor> _mockUserContextAccessor;
    private readonly GetDonationsQueryHandler _handler;

    public GetDonationsQueryHandlerTests()
    {
        _mockDonationRepository = new Mock<IDonationRepository>();
        _mockUserContextAccessor = new Mock<IUserContextAccessor>();
        _handler = new GetDonationsQueryHandler(_mockDonationRepository.Object, _mockUserContextAccessor.Object);
    }

    [Fact]
    public async Task Handle_ReturnsDonationsList_WhenUserHasDonations()
    {
        var donations = new List<DonationDto> { new DonationDto(), new DonationDto() };
        _mockDonationRepository.Setup(x => x.GetDonationsByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(donations);

        var result = await _handler.Handle(new GetDonationsQuery(), new CancellationToken());

        Assert.Equal(donations, result);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenUserHasNoDonations()
    {
        var donations = new List<DonationDto>();
        _mockDonationRepository.Setup(x => x.GetDonationsByUserIdAsync(It.IsAny<Guid>())).ReturnsAsync(donations);

        var result = await _handler.Handle(new GetDonationsQuery(), new CancellationToken());

        Assert.Empty(result);
    }
}
