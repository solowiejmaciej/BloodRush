using BloodRush.DonationFacility.API.Dtos;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface INotificationsRepository
{
    Task<List<NotificationDto>> GetNotificationsAsync(Guid donorId);
}