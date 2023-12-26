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
        var notification = new Notification()
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            Message = notificationContent.Message,
            Title = notificationContent.Title,
            NotificationChannel = donorNotificationInfo.NotificationChannel,
            NotificationChannelLabel = Enum.GetName(donorNotificationInfo.NotificationChannel)!,
            NotificationType = notificationType,
            NotificationTypeLabel = Enum.GetName(notificationType)!
        };
        
        return notification;
    }

    public async Task<Notification> BuildCustomAsync(Guid donorId, int collectionFacilityId, string title, string message)
    { 
        var notificationInfo = await _notificationsRepository.GetNotificationInfoByDonorIdAsync(donorId);
        
        var notification = new Notification()
        {
            DonorId = donorId,
            CollectionFacilityId = collectionFacilityId,
            Message = message,
            Title = title,
            NotificationChannel = notificationInfo.NotificationChannel,
            NotificationChannelLabel = Enum.GetName(notificationInfo.NotificationChannel)!,
            NotificationType = ENotificationType.Custom,
            NotificationTypeLabel = Enum.GetName(ENotificationType.Custom)!
        };
        
        return notification;
    }
    
    public Task<Notification> BuildConfirmationCodeNotification(Guid donorId, ConfirmationCode confirmationCode)
    {
        var notificationChannel = confirmationCode.CodeType == ECodeType.Email
            ? ENotificationChannel.Email
            : ENotificationChannel.Sms;
        
        var notification = new Notification()
        {
            DonorId = donorId,
            CollectionFacilityId = -1,
            Message = string.Format(NotificationConstants.ConfirmationCodeTemplate, confirmationCode.Code),
            NotificationChannel = notificationChannel,
            NotificationChannelLabel = Enum.GetName(notificationChannel)!,
            NotificationType = ENotificationType.ConfirmationCode,
            NotificationTypeLabel = Enum.GetName(ENotificationType.ConfirmationCode)!
        };
        
        return Task.FromResult(notification);
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