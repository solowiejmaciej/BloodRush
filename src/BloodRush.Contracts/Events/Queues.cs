namespace BloodRush.Contracts.Events;

public static class RabbitQueues
{
    public const string NotificationsQueue = "notifications-events";
    public const string DonorCreated = "donor-created-events";
    public const string DonorDeleted = "donor-deleted-events";
}