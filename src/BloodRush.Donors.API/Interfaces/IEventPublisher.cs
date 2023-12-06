#region

#endregion

#region

using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.Contracts.ConfirmationCodes;
using BloodRush.Contracts.Enums;

#endregion

namespace BloodRush.API.Interfaces;

public interface IEventPublisher
{
    Task PublishDonorCreatedEventAsync(Guid donorId, CancellationToken cancellationToken = default);

    Task PublishDonorDeletedEventAsync(Guid requestDonorId, CancellationToken cancellationToken = default);
    Task PublishSendConfirmationCodeEventAsync(ConfirmationCode code, Guid currentUser, CancellationToken cancellationToken = default);
}