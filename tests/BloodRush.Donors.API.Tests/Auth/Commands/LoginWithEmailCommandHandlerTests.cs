using BloodRush.API.Handlers.Auth;
using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using Moq;

namespace BloodRush.API.Tests.Auth.Commands
{
    public class LoginWithEmailCommandHandlerTests
    {
        private readonly Mock<ILoginManager> _loginManagerMock;
        private readonly LoginWithEmailCommandHandler _handler;

        public LoginWithEmailCommandHandlerTests()
        {
            _loginManagerMock = new Mock<ILoginManager>();
            _handler = new LoginWithEmailCommandHandler(_loginManagerMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsLoginResult()
        {
            // Arrange
            var command = new LoginWithEmailCommand { Email = "test@test.com", Password = "password" };
            var expectedLoginResult = new LoginResult
            {
                IsSuccess = true
            };
            _loginManagerMock.Setup(lm => lm.LoginEmailAsync(command.Email, command.Password))
                .ReturnsAsync(expectedLoginResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedLoginResult, result);
        }
        
    }
}