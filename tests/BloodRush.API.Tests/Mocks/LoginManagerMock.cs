using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Interfaces;
using Moq;

namespace BloodRush.API.Tests.Mocks;

public static class LoginManagerMock
{
    public static Mock<ILoginManager> GetLoginManagerMock(Donor donor)
    {
        var loginManagerMock = new Mock<ILoginManager>();
        //loginManagerMock.Setup(x => x.LoginWithPhoneNumberAsync(It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync()
        loginManagerMock.Setup(x => x.HashPassword(It.IsAny<Donor>(), It.IsAny<string>())).Returns(donor);
        return loginManagerMock;
    }
}