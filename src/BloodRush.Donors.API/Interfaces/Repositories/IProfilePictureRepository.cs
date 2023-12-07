namespace BloodRush.API.Interfaces.Repositories;

public interface IProfilePictureRepository
{
    Task<Stream> GetProfilePictureByDonorIdAsync(Guid donorId);
    Task DeleteProfilePictureByDonorIdAsync(Guid donorId);
    Task AddProfilePictureAsync(Guid donorId, IFormFile pictureFile);
}