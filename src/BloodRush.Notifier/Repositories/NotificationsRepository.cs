#region

using BloodRush.Contracts.Enums;
using BloodRush.Notifier.Constants;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Entities.DbContext;
using BloodRush.Notifier.Exceptions;
using BloodRush.Notifier.Interfaces;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BloodRush.Notifier.Repositories;

public class NotificationsRepository : INotificationsRepository
{
    private readonly BloodRushNotificationsDbContext _context;

    public NotificationsRepository(
        BloodRushNotificationsDbContext bloodRushNotificationsDbContext
    )
    {
        _context = bloodRushNotificationsDbContext;
    }

    public async Task AddDefaultNotificationInfoAsync(Guid donorId)
    {
        var donorNotificationInfo = new DonorNotificationInfo
        {
            DonorId = donorId,
            NotificationChannel = DonorConstants.DefaultNotificationChannel
        };
        await _context.DonorsNotificationInfo.AddAsync(donorNotificationInfo);
        await _context.SaveChangesAsync();
    }

    public async Task<DonorNotificationInfo> GetNotificationInfoByDonorIdAsync(Guid id)
    {
        var notificationInfo = await _context.DonorsNotificationInfo.FirstOrDefaultAsync(d => d.DonorId == id);
        if (notificationInfo is null) throw new NotificationProfileNotFoundException();

        return notificationInfo;
    }

    public async Task DeleteNotificationInfoAsync(Guid donorId)
    {
        var notificationInfo = await GetNotificationInfoByDonorIdAsync(donorId);
        _context.Remove(notificationInfo);
        await _context.SaveChangesAsync();
    }

    public async Task<NotificationContent> GetNotificationContentForFacilityAsync(int collectionFacilityId, ENotificationType notificationType)
    {
        var result = await _context.NotificationsContent.SingleOrDefaultAsync(c => c.CollectionFacilityId == collectionFacilityId && c.NotificationType == notificationType);
        if (result is null)
        {
            return CreateDefaultNotificationContent(collectionFacilityId, notificationType);
        }
        return result;
    }

    public Task AddNotificationAsync(Notification notification)
    {
        _context.Notifications.Add(notification);
        return _context.SaveChangesAsync();
    }

    public Task<List<Notification>> GetNotificationsByDonorIdAsync(Guid donorId)
    {
        return _context.Notifications.Where(n => n.DonorId == donorId).ToListAsync();
    }

    private NotificationContent CreateDefaultNotificationContent(int collectionFacilityId, ENotificationType notificationType)
    {
        var title = notificationType switch
        {
            ENotificationType.Welcome => CollectionFacilityConstants.DefaultWelcomeTitle,
            ENotificationType.BloodNeed => CollectionFacilityConstants.DefaultBloodNeedTitle,
            ENotificationType.ThankYou => CollectionFacilityConstants.DefaultThankYouTitle,
            ENotificationType.RestingPeriodOver => CollectionFacilityConstants.DefaultRestingPeriodOverTitle,
            ENotificationType.UrgentBloodNeed => CollectionFacilityConstants.DefaultUrgentBloodNeedTitle,
            ENotificationType.DonationReminder => CollectionFacilityConstants.DefaultDonationReminderTitle,
            _ => throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null)
        };
        
        var message = notificationType switch
        {
            ENotificationType.Welcome => CollectionFacilityConstants.DefaultWelcomeMessage,
            ENotificationType.BloodNeed => CollectionFacilityConstants.DefaultBloodNeedMessage,
            ENotificationType.ThankYou => CollectionFacilityConstants.DefaultThankYouMessage,
            ENotificationType.RestingPeriodOver => CollectionFacilityConstants.DefaultRestingPeriodOverMessage,
            ENotificationType.UrgentBloodNeed => CollectionFacilityConstants.DefaultUrgentBloodNeedMessage,
            ENotificationType.DonationReminder => CollectionFacilityConstants.DefaultDonationReminderMessage,
            _ => throw new ArgumentOutOfRangeException(nameof(notificationType), notificationType, null)
        };
        
        var notificationContent = new NotificationContent
        {
            Title = title,
            Message = message,
            CollectionFacilityId = collectionFacilityId,
            NotificationType = notificationType,
        };
        _context.NotificationsContent.Add(notificationContent);
        _context.SaveChanges();
        return notificationContent;
    }
}