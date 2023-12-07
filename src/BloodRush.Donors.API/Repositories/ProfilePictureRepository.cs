using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BloodRush.API.Interfaces.Repositories;

namespace BloodRush.API.Repositories;

public class ProfilePictureRepository : IProfilePictureRepository
{
    private readonly BlobContainerClient _blobContainerClient;

    public ProfilePictureRepository(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("BlobStorage");
        string? containerName = configuration.GetValue<string>("Azure:BlobContainerName");

        var blobServiceClient = new BlobServiceClient(connectionString);
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        _blobContainerClient.CreateIfNotExists();
        
    }
    public async Task<Stream> GetProfilePictureByDonorIdAsync(Guid donorId)
    {
        var file = _blobContainerClient.GetBlobClient($"{donorId}-profile-picture");
        if (!await file.ExistsAsync()) return Stream.Null;

        var content = await file.DownloadContentAsync();
        return content.Value.Content.ToStream();
    }

    public async Task DeleteProfilePictureByDonorIdAsync(Guid donorId)
    {
        await _blobContainerClient.DeleteBlobIfExistsAsync($"{donorId}-profile-picture");
    }

    public async Task AddProfilePictureAsync(Guid donorId, IFormFile pictureFile)
    {
        var blobClient = _blobContainerClient.GetBlobClient($"{donorId}-profile-picture");
        using (Stream stream = pictureFile.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = pictureFile.ContentType });
        }
    }
}