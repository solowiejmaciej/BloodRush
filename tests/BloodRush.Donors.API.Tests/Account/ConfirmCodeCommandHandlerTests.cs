using BloodRush.API.Handlers.Account;

namespace BloodRush.API.Tests.Account;

using Xunit;
using Moq;
using System.Threading;
using BloodRush.API.Exceptions.ConfirmationCodes;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.Contracts.Enums;

public class ConfirmCodeCommandHandlerTests
{
    [Fact]
    public async Task Handle_ConfirmCodeCommand_EmailCode_ValidCode_EmailConfirmed()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockConfirmationCodesRepository = new Mock<IConfirmationCodesRepository>();
        var handler = new ConfirmCodeCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object, mockConfirmationCodesRepository.Object);
        var command = new ConfirmCodeCommand { CodeType = ECodeType.Email, Code = "123456" };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockConfirmationCodesRepository.Setup(x => x.IsValidCode(It.IsAny<Guid>(), command.Code, command.CodeType)).ReturnsAsync(true);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockDonorRepository.Verify(x => x.ConfirmEmailAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ConfirmCodeCommand_SmsCode_ValidCode_PhoneNumberConfirmed()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockConfirmationCodesRepository = new Mock<IConfirmationCodesRepository>();
        var handler = new ConfirmCodeCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object, mockConfirmationCodesRepository.Object);
        var command = new ConfirmCodeCommand { CodeType = ECodeType.Sms, Code = "123456" };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockConfirmationCodesRepository.Setup(x => x.IsValidCode(It.IsAny<Guid>(), command.Code, command.CodeType)).ReturnsAsync(true);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockDonorRepository.Verify(x => x.ConfirmPhoneNumberAsync(It.IsAny<Guid>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ConfirmCodeCommand_InvalidCode_ThrowsInvalidCodeException()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockConfirmationCodesRepository = new Mock<IConfirmationCodesRepository>();
        var handler = new ConfirmCodeCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object, mockConfirmationCodesRepository.Object);
        var command = new ConfirmCodeCommand { CodeType = ECodeType.Email, Code = "123456" };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockConfirmationCodesRepository.Setup(x => x.IsValidCode(It.IsAny<Guid>(), command.Code, command.CodeType)).ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidCodeException>(() => handler.Handle(command, CancellationToken.None));
    }
}