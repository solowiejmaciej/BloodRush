using BloodRush.API.Handlers.Account;

namespace BloodRush.API.Tests.Account;

using Xunit;
using Moq;
using System.Threading;
using Microsoft.AspNetCore.Http;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;

public class UploadPhotoCommandHandlerTests
{
    [Fact]
    public async Task Handle_UploadPhotoCommand_PhotoUploadedSuccessfully()
    {
        // Arrange
        var mockProfilePictureRepository = new Mock<IProfilePictureRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var handler = new UploadPhotoCommandHandler(mockProfilePictureRepository.Object, mockUserContextAccessor.Object);
        var command = new UploadPhotoCommand { Photo = new FormFile(Stream.Null, 0, 0, "name", "fileName") };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockProfilePictureRepository.Verify(x => x.AddProfilePictureAsync(It.IsAny<Guid>(), command.Photo), Times.Once);
    }
}