using BloodRush.API.Dtos;
using BloodRush.API.Handlers.RestingPeriod;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using Moq;

namespace BloodRush.API.Tests.RestingPeriod;

public class GetRestingInfoQueryHandlerTests
{
    private readonly Mock<IRestingPeriodRepository> _mockRestingPeriodRepository;
    private readonly Mock<IUserContextAccessor> _mockUserContextAccessor;
    private readonly GetRestingInfoQueryHandler _handler;

    public GetRestingInfoQueryHandlerTests()
    {
        _mockRestingPeriodRepository = new Mock<IRestingPeriodRepository>();
        _mockUserContextAccessor = new Mock<IUserContextAccessor>();
        _handler = new GetRestingInfoQueryHandler(_mockRestingPeriodRepository.Object, _mockUserContextAccessor.Object);
    }

    [Fact]
    public async Task Handle_ReturnsCorrectRestingPeriod_WhenDonorIdExists()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        var restingPeriod = new RestingPeriodDto { DonorId = donorId};
        _mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(donorId);
        _mockRestingPeriodRepository.Setup(x => x.GetRestingPeriodByDonorIdAsync(donorId)).ReturnsAsync(restingPeriod);

        // Act
        var result = await _handler.Handle(new GetRestingPeriodQuery(), CancellationToken.None);

        // Assert
        Assert.Equal(restingPeriod, result);
    }

    [Fact]
    public async Task Handle_ReturnsNull_WhenDonorIdDoesNotExist()
    {
        // Arrange
        var donorId = Guid.NewGuid();
        _mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(donorId);
        _mockRestingPeriodRepository.Setup(x => x.GetRestingPeriodByDonorIdAsync(donorId)).ReturnsAsync((RestingPeriodDto)null);

        // Act
        var result = await _handler.Handle(new GetRestingPeriodQuery(), CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}