#region

using BloodRush.DonationFacility.API.DomainEvents;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Hangfire.Jobs;
using BloodRush.DonationFacility.API.Interfaces;
using Hangfire;

#endregion

namespace BloodRush.DonationFacility.API.Hangfire.Manager;

public class JobManager : IJobManager
{
    private readonly IBackgroundJobClient _backgroundJobClient;

    public JobManager(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    public void EnqueueProcessBloodNeedCreatedJob(BloodNeed bloodNeed)
    {
        _backgroundJobClient.Enqueue<ProcessBloodNeedCreatedJob>(x =>
            x.Execute(default, default, bloodNeed));
    }
}