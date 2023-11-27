#region
using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.Notifier.Constants;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Interfaces;

#endregion

namespace BloodRush.Notifier.Builders;

public class NotificationBuilder : INotificationBuilder
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly ILogger<NotificationBuilder> _logger;

    public NotificationBuilder(
        INotificationsRepository notificationsRepository,
        ILogger<NotificationBuilder> logger
    )
    {
        _notificationsRepository = notificationsRepository;
        _logger = logger;
    }

    public async Task<Notification> BuildAsync(Guid donorId, string phoneNumber ,int collectionFacilityId, ENotificationType notificationType)
    {
        var donorNotificationInfo = await _notificationsRepository.GetNotificationInfoByDonorIdAsync(donorId);
        
        var notificationContent = await BuildNotificationContentAsync(collectionFacilityId,
            notificationType);
        var notification = new Notification
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            NotificationChannel = donorNotificationInfo.NotificationChannel,
            PushNotificationToken = donorNotificationInfo.PushNotificationToken,
            PhoneNumber = phoneNumber,
            Message = notificationContent.Message,
            Title = notificationContent.Title
        };
        return notification;
    }

    private async Task<NotificationContent> BuildNotificationContentAsync(int collectionFacilityId,
        ENotificationType notificationType)
    {
        if (notificationType == ENotificationType.Welcome)
        {
            //TODO: Temporary solution, need to be refactored to consider BloodRushNotifications
            return new NotificationContent()
            {
                Message = CollectionFacilityConstants.DefaultWelcomeMessage,
                Title = CollectionFacilityConstants.DefaultWelcomeTitle
            };
        }
        var notificationContent = await _notificationsRepository.GetNotificationContentForFacilityAsync(collectionFacilityId,
            notificationType);
        return notificationContent;
    }
    
}