namespace BloodRush.Notifier.Exceptions;

public class InvalidNotificationChannelException : Exception
{
    public InvalidNotificationChannelException() : base("Invalid push channel.")
    {
    }
}