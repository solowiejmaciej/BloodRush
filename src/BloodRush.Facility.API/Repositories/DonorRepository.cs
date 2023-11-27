using System.Data;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using Dapper;



namespace BloodRush.DonationFacility.API.Repositories;

public class DonorRepository : IDonorRepository
{
    private readonly IDbConnection _dbConnection;

    public DonorRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task<List<Donor>> GetAllDonorsAsync()
    {
        _dbConnection.Open();
        var donors = await _dbConnection.QueryAsync<Donor>("SELECT * FROM Donors");
        _dbConnection.Close();
        return donors.ToList();
    }
    
    public async Task<Donor> GetDonorByIdAsync(Guid id)
    {
        _dbConnection.Open();
        var result = await _dbConnection.QueryAsync<Donor>("SELECT * FROM Donors WHERE Id = @Id", new {Id = id});
        _dbConnection.Close();
        var donor = result.FirstOrDefault();
        if (donor is null) throw new DonorNotFoundException();
        return donor;
    }
}