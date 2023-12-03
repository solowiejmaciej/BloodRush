#region

using BloodRush.API.Entities.Enums;

#endregion

namespace BloodRush.API.Entities;

public class Donor
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
    public required string Password { get; set; }
    public required ESex Sex { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required EBloodType BloodType { get; set; }
    public required string PhoneNumber { get; set; }
    public bool IsPhoneNumberConfirmed { get; set; }
    public required string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public required string HomeAddress { get; set; }
    public required string Pesel { get; set; }
}