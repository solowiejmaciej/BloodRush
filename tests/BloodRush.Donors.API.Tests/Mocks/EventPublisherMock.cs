#region

using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.API.Exceptions;
using BloodRush.API.Interfaces;
using Moq;

#endregion

namespace BloodRush.API.Tests.Mocks;

public static class EventPublisherMock
{
    public static Mock<IEventPublisher> GetEventPublisherMock()
    {

        var eventPublisherMock = new Mock<IEventPublisher>();
        eventPublisherMock.Setup(x => x.PublishDonorDeletedEventAsync(It.IsAny<Guid>(), CancellationToken.None));
        eventPublisherMock.Setup(x => x.PublishDonorCreatedEventAsync(It.IsAny<Guid>(), CancellationToken.None));
        return eventPublisherMock;
    }
}