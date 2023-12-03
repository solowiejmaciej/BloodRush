using BloodRush.DonationFacility.API.Models.Notifications;

namespace BloodRush.DonationFacility.API.Models.Requests;

public class AddNotificationRequest
{
    public required NotificationContent Content { get; set; }
}