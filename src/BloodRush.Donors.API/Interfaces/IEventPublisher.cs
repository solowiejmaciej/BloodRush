#region

#endregion

#region

using BloodRush.API.Entities.Enums;
using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.API.Interfaces;

public interface IEventPublisher
{
    Task PublishDonorCreatedEventAsync(Guid donorId, string donorPhoneNumber,
        CancellationToken cancellationToken = default);

    Task PublishDonorDeletedEventAsync(Guid requestDonorId, CancellationToken cancellationToken = default);
}