using BloodRush.API.Interfaces;
using Moq;

namespace BloodRush.API.Tests.Mocks;

public abstract class UserContextAccessorMock
{
    public static Mock<IUserContextAccessor> GetUserContextAccessorMock(Guid donorId)
    {
        var userContextAccessorMock = new Mock<IUserContextAccessor>();
        userContextAccessorMock.Setup(x => x.GetDonorId())
            .Returns(donorId);
        return userContextAccessorMock;
    }
}