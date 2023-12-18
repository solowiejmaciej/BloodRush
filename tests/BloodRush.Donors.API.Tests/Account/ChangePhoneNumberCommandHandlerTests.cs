using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Exceptions.ConfirmationCodes;
using BloodRush.API.Handlers.Account;

namespace BloodRush.API.Tests.Account;

using Xunit;
using Moq;
using System.Threading;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;

public class ChangePhoneNumberCommandHandlerTests
{
    [Fact]
    public async Task Handle_ChangePhoneNumberCommand_PhoneNumberChangedSuccessfully()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var handler = new ChangePhoneNumberCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object);
        var command = new ChangePhoneNumberCommand { NewPhoneNumber = "123456789" };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Donor
        {
            PhoneNumber = "987654321",
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        });
        mockDonorRepository.Setup(x => x.GetDonorByPhoneNumberAsync(command.NewPhoneNumber)).ReturnsAsync((Donor)null!);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockDonorRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ChangePhoneNumberCommand_PhoneNumberAlreadyExists_ThrowsException()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var handler = new ChangePhoneNumberCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object);
        var command = new ChangePhoneNumberCommand { NewPhoneNumber = "123456789" };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Donor
        {
            PhoneNumber = "987654321",
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        });
        mockDonorRepository.Setup(x => x.GetDonorByPhoneNumberAsync(command.NewPhoneNumber)).ReturnsAsync(new Donor
        {
            PhoneNumber = command.NewPhoneNumber,
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            Email = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        });

        // Act & Assert
        await Assert.ThrowsAsync<PhoneNumberAlreadyConfirmedException>(() => handler.Handle(command, CancellationToken.None));
    }
}