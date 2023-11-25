#region

using BloodRush.API.Entities;
using BloodRush.API.Entities.Enums;
using BloodRush.Notifier.Constants;
using BloodRush.Notifier.Entities;
using BloodRush.Notifier.Entities.DbContext;
using BloodRush.Notifier.Exceptions;
using BloodRush.Notifier.Interfaces;
using Microsoft.Data.SqlClient;
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

    public async Task UpdatePushNotificationTokenAsync(Guid id, string token)
    {
        var notificationInfo = await GetNotificationInfoByDonorIdAsync(id);
        notificationInfo.PushNotificationToken = token;
        await _context.SaveChangesAsync();
    }

    public async Task ChangeDonorNotificationChanel(Guid id, ENotificationChannel channel)
    {
        var notificationInfo = await GetNotificationInfoByDonorIdAsync(id);

        notificationInfo.NotificationChannel = channel;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteNotificationInfoAsync(Guid donorId)
    {
        var notificationInfo = await GetNotificationInfoByDonorIdAsync(donorId);
        _context.Remove(notificationInfo);
        await _context.SaveChangesAsync();
    }
}