using BloodRush.DonationFacility.API.Entities;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface IJobManager
{
    void EnqueueProcessBloodNeedCreatedJob(BloodNeed bloodNeed);
}