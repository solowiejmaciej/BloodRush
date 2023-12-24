#region

using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Hangfire.Jobs;
using Hangfire;

#endregion

namespace BloodRush.DonationFacility.API.Hangfire.Manager;

public interface IJobManager
{
    void EnqueueProcessBloodNeedCreatedJob(BloodNeedCreatedEvent notification);
    
}

public class JobManager : IJobManager
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public JobManager(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    public void EnqueueProcessBloodNeedCreatedJob(BloodNeedCreatedEvent notification)
    {
        _backgroundJobClient.Enqueue<ProcessBloodNeedCreatedJob>(x =>
            x.Execute(default, default, notification));
    }
}