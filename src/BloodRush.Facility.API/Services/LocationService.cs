using BloodRush.DonationFacility.API.Interfaces;
using BloodRush.DonationFacility.API.Models;

namespace BloodRush.DonationFacility.API.Services;

public class LocationService : ILocationService
{
    public async Task<double> GetDistanceBetweenTwoAddresses(string address, string address2)
    {
        var addressOne = await GetLatitudeAndLongitudeFromAddress(address);
        var addressTwo = await GetLatitudeAndLongitudeFromAddress(address2);
        var distance = CalculateDistance(addressOne, addressTwo);
        return distance;
    }

    private async Task<Coordinates> GetLatitudeAndLongitudeFromAddress(string address)
    {
        throw new NotImplementedException();
    }
    
    private double CalculateDistance(Coordinates donorCoordinates, Coordinates donationFacilityCoordinates)
    {
        const double r = 6371; // Radius of the earth in km
        double dLat = DegreeToRadian(donationFacilityCoordinates.Latitude - donorCoordinates.Latitude);
        double dLon = DegreeToRadian(donationFacilityCoordinates.Longitude - donorCoordinates.Longitude);
        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                   Math.Cos(DegreeToRadian(donorCoordinates.Latitude)) * Math.Cos(DegreeToRadian(donationFacilityCoordinates.Latitude)) *
                   Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double distance = r * c; // Distance in km
        return distance;
    }

    private double DegreeToRadian(double degree)
    {
        return degree * (Math.PI / 180);
    }
}