using Xunit;
using Moq;
using System;
using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.Interfaces;

public class DeleteDonorCommandHandlerTests
{
    private readonly Mock<IDonorRepository> _mockDonorRepository;
    private readonly Mock<IUserContextAccessor> _mockUserContextAccessor;
    private readonly Mock<IEventPublisher> _mockEventPublisher;
    private readonly DeleteDonorCommandHandler _handler;

    public DeleteDonorCommandHandlerTests()
    {
        _mockDonorRepository = new Mock<IDonorRepository>();
        _mockUserContextAccessor = new Mock<IUserContextAccessor>();
        _mockEventPublisher = new Mock<IEventPublisher>();
        _handler = new DeleteDonorCommandHandler(_mockUserContextAccessor.Object, _mockDonorRepository.Object, _mockEventPublisher.Object);
    }

    [Fact]
    public async Task Handle_ReturnsTrueAndPublishesEvent_WhenDonorIsDeletedSuccessfully()
    {
        var donorId = Guid.NewGuid();
        _mockUserContextAccessor.Setup(accessor => accessor.GetDonorId()).Returns(donorId);
        _mockDonorRepository.Setup(repo => repo.DeleteDonorAsync(donorId)).ReturnsAsync(true);

        var result = await _handler.Handle(new DeleteDonorCommand { DonorId = donorId }, CancellationToken.None);

        Assert.True(result);
        _mockEventPublisher.Verify(publisher => publisher.PublishDonorDeletedEventAsync(donorId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ReturnsFalseAndDoesNotPublishEvent_WhenDonorDeletionFails()
    {
        var donorId = Guid.NewGuid();
        _mockUserContextAccessor.Setup(accessor => accessor.GetDonorId()).Returns(donorId);
        _mockDonorRepository.Setup(repo => repo.DeleteDonorAsync(donorId)).ReturnsAsync(false);

        var result = await _handler.Handle(new DeleteDonorCommand { DonorId = donorId }, CancellationToken.None);

        Assert.False(result);
        _mockEventPublisher.Verify(publisher => publisher.PublishDonorDeletedEventAsync(donorId, It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ThrowsUnauthorizedAccessException_WhenDonorIdDoesNotMatchCurrentUser()
    {
        var donorId = Guid.NewGuid();
        var currentUserId = Guid.NewGuid();
        _mockUserContextAccessor.Setup(accessor => accessor.GetDonorId()).Returns(currentUserId);

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(new DeleteDonorCommand { DonorId = donorId }, CancellationToken.None));
    }
}