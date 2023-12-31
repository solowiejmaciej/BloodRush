#region

using BloodRush.API.Entities.Enums;

#endregion

namespace BloodRush.API.Dtos;

public class DonorDto
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required ESex Sex { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required EBloodType BloodType { get; set; }
    public required int PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string HomeAddress { get; set; }
    public required string Pesel { get; set; }
    public required int MaxDonationRangeInKm { get; set; }
}