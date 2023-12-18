using BloodRush.API.Handlers.Account;

namespace BloodRush.API.Tests.Account;

using Xunit;
using Moq;
using System.Threading;
using System.IO;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;

public class GetPhotoQueryHandlerTests
{
    [Fact]
    public async Task Handle_GetPhotoQuery_ReturnsPhotoStream()
    {
        // Arrange
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockProfilePictureRepository = new Mock<IProfilePictureRepository>();
        var handler = new GetPhotoQueryHandler(mockUserContextAccessor.Object, mockProfilePictureRepository.Object);
        var query = new GetPhotoQuery();

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockProfilePictureRepository.Setup(x => x.GetProfilePictureByDonorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new MemoryStream());

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsType<MemoryStream>(result);
    }

    [Fact]
    public async Task Handle_GetPhotoQuery_NoPhotoExists_ReturnsNull()
    {
        // Arrange
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockProfilePictureRepository = new Mock<IProfilePictureRepository>();
        var handler = new GetPhotoQueryHandler(mockUserContextAccessor.Object, mockProfilePictureRepository.Object);
        var query = new GetPhotoQuery();

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockProfilePictureRepository.Setup(x => x.GetProfilePictureByDonorIdAsync(It.IsAny<Guid>()))!.ReturnsAsync((Stream)null);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}