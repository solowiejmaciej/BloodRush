using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Handlers.Account;
using BloodRush.Contracts.ConfirmationCodes;

namespace BloodRush.API.Tests.Account;

using Xunit;
using Moq;
using System.Threading;
using BloodRush.API.Exceptions;
using BloodRush.API.Exceptions.ConfirmationCodes;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.Contracts.Enums;

public class SendConfirmationCommandHandlerTests
{
    [Fact]
    public async Task Handle_SendConfirmationCommand_EmailNotConfirmed_EmailConfirmationCodeSent()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockConfirmationCodesRepository = new Mock<IConfirmationCodesRepository>();
        var mockEventPublisher = new Mock<IEventPublisher>();
        var handler = new SendConfirmationCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object, mockConfirmationCodesRepository.Object, mockEventPublisher.Object);
        var command = new SendConfirmationCommand { CodeType = ECodeType.Email };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Donor
        {
            IsEmailConfirmed = false,
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
        });
        mockConfirmationCodesRepository.Setup(x => x.GenerateCodeAsync(command.CodeType, It.IsAny<Guid>())).ReturnsAsync(new ConfirmationCode { Code = 123456, CodeType = command.CodeType});

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockEventPublisher.Verify(
            x => x.PublishSendConfirmationCodeEventAsync(It.Is<ConfirmationCode>(c => c.Code == 123456),
                It.IsAny<Guid>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task Handle_SendConfirmationCommand_EmailAlreadyConfirmed_ThrowsEmailAlreadyConfirmedException()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockConfirmationCodesRepository = new Mock<IConfirmationCodesRepository>();
        var mockEventPublisher = new Mock<IEventPublisher>();
        var handler = new SendConfirmationCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object, mockConfirmationCodesRepository.Object, mockEventPublisher.Object);
        var command = new SendConfirmationCommand { CodeType = ECodeType.Email };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: new Donor
        {
            IsEmailConfirmed = true,
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
        });

        // Act & Assert
        await Assert.ThrowsAsync<EmailAlreadyConfirmedException>(() => handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_SendConfirmationCommand_PhoneNumberNotConfirmed_PhoneNumberConfirmationCodeSent()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockConfirmationCodesRepository = new Mock<IConfirmationCodesRepository>();
        var mockEventPublisher = new Mock<IEventPublisher>();
        var handler = new SendConfirmationCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object, mockConfirmationCodesRepository.Object, mockEventPublisher.Object);
        var command = new SendConfirmationCommand { CodeType = ECodeType.Sms };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Donor
        {
            IsPhoneNumberConfirmed = false,
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
        });
        mockConfirmationCodesRepository.Setup(x => x.GenerateCodeAsync(command.CodeType, It.IsAny<Guid>())).ReturnsAsync(new ConfirmationCode { Code = 123456, CodeType = command.CodeType});

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockEventPublisher.Verify(x => x.PublishSendConfirmationCodeEventAsync(It.Is<ConfirmationCode>(c => c.Code == 123456),It.IsAny<Guid>(), It.IsAny<CancellationToken>()),Times.Once);
    }

    [Fact]
    public async Task Handle_SendConfirmationCommand_PhoneNumberAlreadyConfirmed_ThrowsPhoneNumberAlreadyConfirmedException()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var mockConfirmationCodesRepository = new Mock<IConfirmationCodesRepository>();
        var mockEventPublisher = new Mock<IEventPublisher>();
        var handler = new SendConfirmationCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object, mockConfirmationCodesRepository.Object, mockEventPublisher.Object);
        var command = new SendConfirmationCommand { CodeType = ECodeType.Sms };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Donor
        {
            IsPhoneNumberConfirmed = true,
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
        });

        // Act & Assert
        await Assert.ThrowsAsync<PhoneNumberAlreadyConfirmedException>(() => handler.Handle(command, CancellationToken.None));
    }
}