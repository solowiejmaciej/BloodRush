using BloodRush.API.Handlers.Donors;
using BloodRush.API.Interfaces;
using BloodRush.API.Interfaces.Repositories;
using BloodRush.API.Tests.Mocks;
using Moq;
using Should.Fluent;

namespace BloodRush.API.Tests.Donors.Commands;

public class DeleteDonorCommandHandlerTests
{
    private static readonly Guid LoggedInDonorId = Guid.Parse("9a1cbadd-7571-4d0c-bdc2-b5487149d276");
    private readonly Mock<IDonorRepository> _donorRepositoryMock = DonorRepositoryMock.GetDonorRepositoryMock();
    private readonly Mock<IUserContextAccessor> _userContextAccessorMock = UserContextAccessorMock.GetUserContextAccessorMock(LoggedInDonorId);
    private readonly Mock<IEventPublisher> _eventPublisherMock = EventPublisherMock.GetEventPublisherMock();


    [Fact]
    public void DeleteDonorCommandHandler_ShouldDeleteDonor_If_CurrentDonorId_IsDonorId()
    {
        var handler = new DeleteDonorCommandHandler(_userContextAccessorMock.Object, _donorRepositoryMock.Object,
            _eventPublisherMock.Object);
        var result = handler.Handle(new DeleteDonorCommand{DonorId = Guid.Parse("9a1cbadd-7571-4d0c-bdc2-b5487149d276")}, CancellationToken.None).Result;
        result.Should().Be.True();
    }
    
    [Fact]
    public void DeleteDonorCommandHandler_Should_Throw_If_CurrentDonorId_Is_Not_DonorId()
    {
        var handler = new DeleteDonorCommandHandler(_userContextAccessorMock.Object, _donorRepositoryMock.Object,
            _eventPublisherMock.Object);
        var exception = Assert.Throws<AggregateException>(() =>
            handler.Handle(new DeleteDonorCommand { DonorId = Guid.Parse("9a1cbadd-7571-4d0c-bdc2-b5487149d275") },
                CancellationToken.None).Result);
        
        Assert.IsType<UnauthorizedAccessException>(exception.InnerException);

    }
    
}