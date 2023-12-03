#region
using BloodRush.Contracts.Enums;
using BloodRush.Notifier.Constants;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Interfaces;

#endregion

namespace BloodRush.Notifier.Builders;

public class NotificationBuilder : INotificationBuilder
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly ILogger<NotificationBuilder> _logger;
    private readonly IDonorRepository _donorRepository;

    public NotificationBuilder(
        INotificationsRepository notificationsRepository,
        ILogger<NotificationBuilder> logger,
        IDonorRepository donorRepository 
        )
    {
        _notificationsRepository = notificationsRepository;
        _logger = logger;
        _donorRepository = donorRepository;
    }

    public async Task<Notification> BuildAsync(Guid donorId,int collectionFacilityId, ENotificationType notificationType)
    {
        var donorNotificationInfo = await _notificationsRepository.GetNotificationInfoByDonorIdAsync(donorId);
        var phoneNumber = await _donorRepository.GetPhoneNumberAsync(donorId);
        
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

    public async Task<Notification> BuildCustomAsync(Guid donorId, int collectionFacilityId, string title, string message)
    { 
        var notificationInfo = await _notificationsRepository.GetNotificationInfoByDonorIdAsync(donorId);
        var phoneNumber = await _donorRepository.GetPhoneNumberAsync(donorId);
        
        var notification = new Notification
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            NotificationChannel = notificationInfo.NotificationChannel,
            PushNotificationToken = notificationInfo.PushNotificationToken,
            PhoneNumber = phoneNumber,
            Message = message,
            Title = title
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