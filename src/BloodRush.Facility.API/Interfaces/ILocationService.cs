using BloodRush.DonationFacility.API.Models;

namespace BloodRush.DonationFacility.API.Interfaces;

public interface ILocationService
{
    Task<double> GetDistanceBetweenTwoAddresses(string address, string address2);
}
