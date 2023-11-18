namespace BloodRush.Notifier.Exceptions;

public class NotificationProfileNotFoundException : Exception
{
    public NotificationProfileNotFoundException() : base("Notification profile not found")
    {
    }
}