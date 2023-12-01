using System.Data;
using BloodRush.API.Dtos;

namespace BloodRush.API.Interfaces.Repositories;

public interface IDonationRepository
{ 
    Task<List<DonationDto>> GetDonationsByUserIdAsync(Guid donorId);
}