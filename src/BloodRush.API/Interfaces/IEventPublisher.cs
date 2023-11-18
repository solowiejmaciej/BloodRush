#region

#endregion

namespace BloodRush.API.Interfaces;

public interface IEventPublisher
{
    Task PublishDonorCreatedEventAsync(Guid donorId, CancellationToken cancellationToken = default);
}