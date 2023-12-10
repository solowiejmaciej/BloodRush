using BloodRush.Contracts.Enums;
using BloodRush.DonationFacility.API.Dtos;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface INotificationsRepository
{
    Task<List<NotificationDto>> GetNotificationsAsync(Guid donorId);
    Task UpdateNotificationTemplateContent(ENotificationType requestNotificationType, int requestDonationFacilityId, string requestContent, string? requestTitle);
    Task<List<NotificationContentDto>> GetNotificationsContent(int requestDonationFacilityId);
}