#region

using BloodRush.API.Entities.Enums;
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
        // This was made because of the I need to get phone number from Donors table
        // And still keep the one source of truth
        var query = $"SELECT PhoneNumber FROM [BloodRush].[dbo].[Donors] Where Id ='{id}'";
        var phoneNumber = await _context.Database.ExecuteSqlRawAsync(query, id);
        
        var notificationInfo = await _context.DonorsNotificationInfo.FirstOrDefaultAsync(d => d.DonorId == id);
        if (notificationInfo is null) throw new NotificationProfileNotFoundException();
        
        notificationInfo.PhoneNumber = phoneNumber.ToString();
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
}