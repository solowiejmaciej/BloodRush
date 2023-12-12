using BloodRush.API.Handlers.Auth;
using BloodRush.API.Interfaces;
using BloodRush.API.Models.Responses;
using Moq;

namespace BloodRush.API.Tests.Auth.Commands
{
    public class RefreshTokenCommandHandlerTests
    {
        private readonly Mock<ILoginManager> _loginManagerMock;
        private readonly RefreshTokenCommandHandler _handler;

        public RefreshTokenCommandHandlerTests()
        {
            _loginManagerMock = new Mock<ILoginManager>();
            _handler = new RefreshTokenCommandHandler(_loginManagerMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsLoginResult()
        {
            // Arrange
            var command = new RefreshTokenCommand { JwtToken = "jwtToken", RefreshToken = "refreshToken" };
            var expectedLoginResult = new LoginResult
            {
                IsSuccess = true
            };
            _loginManagerMock.Setup(lm => lm.RefreshTokenAsync(command.JwtToken, command.RefreshToken))
                .ReturnsAsync(expectedLoginResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(expectedLoginResult, result);
        }
        
    }
}