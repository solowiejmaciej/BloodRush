using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Handlers.Account;

namespace BloodRush.API.Tests.Account;

using Xunit;
using Moq;
using System.Threading;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;

public class ChangeEmailCommandHandlerTests
{
    [Fact]
    public async Task Handle_ChangeEmailCommand_EmailChangedSuccessfully()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var handler = new ChangeEmailCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object);
        var command = new ChangeEmailCommand { NewEmail = "newemail@example.com" };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Donor 
        { 
            Email = "oldemail@example.com",
            FirstName = "John",
            Surname = "Doe",
            Password = "password",
            Sex = ESex.Male,
            DateOfBirth = DateTime.Now.AddYears(-30),
            BloodType = EBloodType.APositive,
            PhoneNumber = "123456789",
            HomeAddress = "Home Address",
            Pesel = "12345678901",
            MaxDonationRangeInKm = 10
        });
        mockDonorRepository.Setup(x => x.GetDonorByEmailAsync(command.NewEmail)).ReturnsAsync((Donor)null);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        mockDonorRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ChangeEmailCommand_EmailAlreadyExists_ThrowsException()
    {
        // Arrange
        var mockDonorRepository = new Mock<IDonorRepository>();
        var mockUserContextAccessor = new Mock<IUserContextAccessor>();
        var handler = new ChangeEmailCommandHandler(mockDonorRepository.Object, mockUserContextAccessor.Object);
        var command = new ChangeEmailCommand { NewEmail = "newemail@example.com" };

        mockUserContextAccessor.Setup(x => x.GetDonorId()).Returns(Guid.NewGuid());
        mockDonorRepository.Setup(x => x.GetDonorByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: new Donor
        {
            Email = "oldemail@example.com",
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        });
        mockDonorRepository.Setup(x => x.GetDonorByEmailAsync(command.NewEmail)).ReturnsAsync(new Donor
        {
            Email = command.NewEmail,
            FirstName = null,
            Surname = null,
            Password = null,
            Sex = ESex.Male,
            DateOfBirth = default,
            BloodType = EBloodType.APositive,
            PhoneNumber = null,
            HomeAddress = null,
            Pesel = null,
            MaxDonationRangeInKm = 0
        });

        // Act & Assert
        await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => handler.Handle(command, CancellationToken.None));
    }
}