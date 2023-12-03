#region

using BloodRush.Contracts.Events;
using BloodRush.Notifier.Entities;

#endregion

namespace BloodRush.Notifier.Interfaces;

public interface ISender
{
    Task SendAsync(Notification notification);
}