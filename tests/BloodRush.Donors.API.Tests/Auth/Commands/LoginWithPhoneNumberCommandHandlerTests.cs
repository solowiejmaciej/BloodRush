using BloodRush.API.Handlers.Auth;
using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using Moq;

namespace BloodRush.API.Tests.Auth.Commands
{
    public class LoginWithPhoneNumberCommandHandlerTests
    {
        private readonly Mock<ILoginManager> _loginManagerMock;
        private readonly LoginWithPhoneNumberCommandHandler _handler;

        public LoginWithPhoneNumberCommandHandlerTests()
        {
            _loginManagerMock = new Mock<ILoginManager>();
            _handler = new LoginWithPhoneNumberCommandHandler(_loginManagerMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsLoginResult()
        {
            // Arrange
            var command = new LoginWithPhoneNumberCommand { PhoneNumber = "1234567890", Password = "password" };
            var expectedLoginResult = new LoginResult
            {
                IsSuccess = true
            };
            _loginManagerMock.Setup(lm => lm.LoginWithPhoneNumberAsync(command.PhoneNumber, command.Password))
                .ReturnsAsync(expectedLoginResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedLoginResult, result);
        }
        
    }
}
