namespace BloodRush.Contracts.Events;

public static class RabbitQueues
{
    public const string NotificationsQueue = "send-notification-events";
    public const string DonorCreated = "donor-created-events";
    public const string DonorDeleted = "donor-deleted-events";
    public const string BloodNeedCreated = "blood-need-created-events";
    public const string BloodNeedCanceled = "blood-need-canceled-events";
    public const string ConfirmationCodesQueue = "send-confirmation-code-events";
}