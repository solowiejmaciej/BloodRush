using BloodRush.API.Handlers.Account;

namespace BloodRush.API.Tests.Account;

using Xunit;
using Moq;
using System.Threading;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;

public class DeletePhotoCommandHandlerTests
{
    [Fact]
    public async Task Handle_DeletePhotoCommand_PhotoDeletedSuccessfully()
    {
        // Arrange
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockProfilePictureRepository = new Mock<IProfilePictureRepository>();
        var handler = new DeletePhotoCommandHandler(mockUserContextAccessor.Object, mockProfilePictureRepository.Object);
        var command = new DeletePhotoCommand();

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockProfilePictureRepository.Verify(x => x.DeleteProfilePictureByDonorIdAsync(It.IsAny<Guid>()), Times.Once);
    }
}