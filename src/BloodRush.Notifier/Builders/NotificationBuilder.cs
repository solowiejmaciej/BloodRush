#region

using BloodRush.Contracts.ConfirmationCodes;
using BloodRush.Contracts.Enums;
using BloodRush.Notifier.Constants;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Interfaces;

#endregion

namespace BloodRush.Notifier.Builders;

public class NotificationBuilder : INotificationBuilder
{
    private readonly INotificationsRepository _notificationsRepository;
    
    public NotificationBuilder(
        INotificationsRepository notificationsRepository
        )
    {
        _notificationsRepository = notificationsRepository;
    }

    public async Task<Notification> BuildAsync(Guid donorId,int collectionFacilityId, ENotificationType notificationType)
    {
        var donorNotificationInfo = await _notificationsRepository.GetNotificationInfoByDonorIdAsync(donorId);
        
        var notificationContent = await BuildNotificationContentAsync(collectionFacilityId,
            notificationType);
        var notification = new Notification
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            NotificationChannel = donorNotificationInfo.NotificationChannel,
            Message = notificationContent.Message,
            Title = notificationContent.Title
        };
        
        return notification;
    }

    public async Task<Notification> BuildCustomAsync(Guid donorId, int collectionFacilityId, string title, string message)
    { 
        var notificationInfo = await _notificationsRepository.GetNotificationInfoByDonorIdAsync(donorId);
        
        var notification = new Notification
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            NotificationChannel = notificationInfo.NotificationChannel,
            Message = message,
            Title = title
        };
        
        return notification;
    }
    
    public async Task<Notification> BuildConfirmationCodeNotification(Guid donorId, ConfirmationCode confirmationCode)
    {
        var notificationChannel = confirmationCode.CodeType == ECodeType.Email
            ? ENotificationChannel.Email
            : ENotificationChannel.Sms;
        
        var notification = new Notification
        {
            DonorId = donorId,
            CollectionFacilityId = -1,
            Message = string.Format(NotificationConstants.ConfirmationCodeTemplate, confirmationCode.Code),
            NotificationChannel = notificationChannel
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