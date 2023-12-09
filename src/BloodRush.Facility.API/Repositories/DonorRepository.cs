using System.Data;
using Azure.Storage.Blobs;
using BloodRush.DonationFacility.API.Entities;
using BloodRush.DonationFacility.API.Exceptions;
using BloodRush.DonationFacility.API.Interfaces;
using Dapper;



namespace BloodRush.DonationFacility.API.Repositories;

public class DonorRepository : IDonorRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly BlobContainerClient _blobContainerClient;

    public DonorRepository(IDbConnection dbConnection, IConfiguration configuration)
    {
        _dbConnection = dbConnection;
        string? connectionString = configuration.GetConnectionString("BlobStorage");
        string? containerName = configuration.GetValue<string>("Azure:BlobContainerName");

        var blobServiceClient = new BlobServiceClient(connectionString);
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
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
    public async Task<Stream?> GetDonorPictureByDonorIdAsync(Guid donorId)
    {
        var file = _blobContainerClient.GetBlobClient($"{donorId}-profile-picture");
        if (!await file.ExistsAsync()) return Stream.Null;

        var content = await file.DownloadContentAsync();
        return content.Value.Content.ToStream();
    }
}