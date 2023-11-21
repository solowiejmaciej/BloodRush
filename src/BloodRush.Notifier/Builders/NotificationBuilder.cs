using BloodRush.Contracts.Enums;
using BloodRush.Contracts.Events;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Interfaces;

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
    public async Task<Notification> BuildAsync(SendNotificationEvent notificationEvent)
    {
        var donorNotificationInfo = await _notificationsRepository.GetNotificationInfoByDonorIdAsync(notificationEvent.DonorId);
        var notificationContent = await BuildNotificationContentAsync(notificationEvent.CollectionFacilityId, notificationEvent.NotificationType);
        var notification = new Notification
        {
            DonorId = notificationEvent.DonorId,
            CollectionFacilityId = notificationEvent.CollectionFacilityId,
            NotificationChannel = donorNotificationInfo.NotificationChannel,
            PushNotificationToken = donorNotificationInfo.PushNotificationToken,
            PhoneNumber = donorNotificationInfo.PhoneNumber,
            Message = notificationContent.Message,
            Title = notificationContent.Title,
        };
        
        _logger.LogInformation($"Notification for donor {notification.DonorId} was successfully built.");
        return notification;
    }
    
    private async Task<NotificationContent> BuildNotificationContentAsync(int collectionFacilityId, ENotificationType notificationType)
    {
        var notificationContent = new NotificationContent
        {
            Title = "Test title",
            Message = "Test message"
        };
        return notificationContent;
    }
    
    private class NotificationContent
    {
        public string? Title { get; set; }
        public string Message { get; set; }
    }

}